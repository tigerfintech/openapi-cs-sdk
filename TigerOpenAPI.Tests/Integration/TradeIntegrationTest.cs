using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;
using TigerOpenAPI.Trade;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Integration
{
  /// <summary>
  /// Integration tests for trade APIs (contract/asset/position/order
  /// queries, plus order preview/placement/modification/cancellation)
  /// against the live gateway.
  /// Credentials come from env vars (see <see cref="IntegTestConfig"/>).
  /// Run with:  dotnet test --filter Category=Integration
  /// </summary>
  [TestFixture]
  [Category("Integration")]
  public class TradeIntegrationTest
  {
    // Generic business param-validation error code returned by the gateway
    // when a required field fails server-side validation (e.g. contractId=0,
    // order id=0). Shared across multiple test methods below — keep a single
    // definition so a future server-side code change only needs one edit.
    private const int CodeBizParamError = 1010;

    // Deliberately unfillable limit prices for BUY/SELL orders that must
    // survive PREVIEW/PLACE without ever actually executing — mirrors
    // Python's/Rust's SAFE_BUY_PRICE / SAFE_SELL_PRICE conventions.
    private const double SafeBuyPrice = 0.01;
    private const double SafeSellPrice = 999_999.0;

    // Substring markers (lowercased match) indicating the gateway rejected
    // an order for a permission/capability reason (no entitlement, market
    // closed, unsupported order type for this account/instrument, etc.)
    // rather than a real code defect. Cross-checked against Rust's
    // battle-tested PERMISSION_ERROR_MARKERS (openapi-rust-sdk/tests/integ_trade.rs).
    private static readonly string[] PermissionErrorMarkers =
    {
      "access forbidden", "forbidden", "no permission", "not supported",
      "license", "not open", "not enabled", "no token",
      "don't support trading", "don’t support trading",
      "unsupported instrument", "only limit orders are supported",
      "outside of regular trading hours", "market is closed",
      "only limit orders can be placed",
      "only limit, stop or stop-limit orders are allowed",
      "at non-trading hour", "orders cannot be placed at this moment",
      "auction order is not allowed at this moment",
      "does not support stock long", "does not support stock short",
      "only trade cash order by market order", "cash order by market order",
      "time range for the order",
      "opening or adding to positions is temporarily unavailable",
      // Rate limiting — transient, not a permission boundary, but the same
      // tolerate-and-skip treatment applies since retrying isn't this
      // suite's job. Matches Java/Rust's RATE_LIMIT_PATTERNS.
      "too_many_requests", "rate limit", "requestrateexceedlimit",
    };

    private static bool IsPermissionError(string? message)
    {
      if (string.IsNullOrEmpty(message)) return false;
      string lower = message.ToLowerInvariant();
      foreach (var marker in PermissionErrorMarkers)
      {
        if (lower.Contains(marker)) return true;
      }
      return false;
    }

    /// <summary>
    /// Previews an order (permission-tolerant skip on failure), then places
    /// it (permission-tolerant skip on failure). On success, asserts a
    /// non-negative order id. Returns <c>true</c> if the order was placed,
    /// <c>false</c> if the flow was skipped for a permission/capability
    /// reason. Any other failure fails the test outright.
    /// Mirrors Python's <c>_preview_and_place</c> / Rust's
    /// <c>preview_and_place</c>.
    /// </summary>
    private bool PreviewAndPlace(PlaceOrderModel order, string context)
    {
      var previewReq = new TigerRequest<PlaceOrderResponse>
      {
        ApiMethodName = TradeApiService.PREVIEW_ORDER,
        ModelValue = order
      };
      var previewResp = _client!.Execute(previewReq);
      if (previewResp == null)
      {
        Assert.Fail($"{context} — preview_order returned null response");
        return false;
      }
      if (!previewResp.IsSuccess())
      {
        if (IsPermissionError(previewResp.Message))
        {
          TestContext.Progress.WriteLine($"{context} — skipped at preview: {previewResp.Message}");
          return false;
        }
        Assert.Fail($"{context} — preview_order failed: {previewResp.Message}");
        return false;
      }

      var placeReq = new TigerRequest<PlaceOrderResponse>
      {
        ApiMethodName = TradeApiService.PLACE_ORDER,
        ModelValue = order
      };
      var placeResp = _client!.Execute(placeReq);
      if (placeResp == null)
      {
        Assert.Fail($"{context} — place_order returned null response");
        return false;
      }
      if (!placeResp.IsSuccess())
      {
        if (IsPermissionError(placeResp.Message))
        {
          TestContext.Progress.WriteLine($"{context} — skipped at place: {placeResp.Message}");
          return false;
        }
        Assert.Fail($"{context} — place_order failed: {placeResp.Message}");
        return false;
      }

      Assert.That(placeResp.Data, Is.Not.Null, $"{context} — place_order data must not be null");
      Assert.That(placeResp.Data!.Id, Is.GreaterThanOrEqualTo(0), $"{context} — place_order id must be non-negative");
      return true;
    }

    /// <summary>
    /// Fetches a stock contract. Returns <c>null</c> when the gateway
    /// rejects the symbol for a permission/capability reason (e.g. "we
    /// don't support trading of this stock now") — callers should
    /// <see cref="Assert.Ignore(string)"/> in that case. Any other failure
    /// fails the test outright.
    /// </summary>
    private ContractItem? FetchStockContract(string symbol, string secType = "STK")
    {
      var contractReq = new TigerRequest<ContractResponse>
      {
        ApiMethodName = TradeApiService.CONTRACT,
        ModelValue = new ContractModel { Symbol = symbol, SecType = secType }
      };
      var contractResp = _client!.Execute(contractReq);
      Assert.That(contractResp, Is.Not.Null, $"contract fetch for {symbol} must not be null");
      if (!contractResp!.IsSuccess())
      {
        if (IsPermissionError(contractResp.Message)) return null;
        Assert.Fail($"contract fetch for {symbol} failed: {contractResp.Message}");
      }
      Assert.That(contractResp.Data, Is.Not.Null, $"contract fetch for {symbol} data must not be null");
      return contractResp.Data!;
    }

    private TradeClient? _client;
    private string _account = string.Empty;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      IntegTestConfig.EnsureTradeCredentials();
      _client = IntegTestConfig.TradeClient;
      _account = IntegTestConfig.Account;
    }

    // ---- helper ----
    private T Execute<T>(string method, TradeModel? model = null) where T : TigerResponse
    {
      Assert.That(_client, Is.Not.Null,
          "TradeClient is null — credentials should have been checked in OneTimeSetUp");
      var req = new TigerRequest<T>
      {
        ApiMethodName = method,
        ModelValue = model ?? new TradeModel()
      };
      // Ensure account is set on every trade model
      if (req.ModelValue != null && string.IsNullOrWhiteSpace(req.ModelValue.Account))
        req.ModelValue.Account = _account;
      var resp = _client!.Execute(req);
      Assert.That(resp, Is.Not.Null, $"{method} response must not be null");
      Assert.That(resp!.IsSuccess(), Is.True,
          $"{method} returned error code={resp.Code} msg={resp.Message}");
      return resp;
    }

    // =====================================================================
    // Contract (single symbol)
    // =====================================================================
    [Test]
    public void GetContract_AAPL_ReturnsSecTypeAndCurrency()
    {
      var model = new ContractModel
      {
        Symbol = "AAPL",
        SecType = SecType.STK.ToString()
      };
      var resp = Execute<ContractResponse>(TradeApiService.CONTRACT, model);

      Assert.That(resp.Data, Is.Not.Null, "contract data must not be null");
      var contract = resp.Data!;

      Assert.That(contract.Symbol, Is.EqualTo("AAPL"), "contract symbol wire name");
      Assert.That(contract.SecType, Is.EqualTo("STK"), "contract secType wire name");
      Assert.That(contract.Currency, Is.Not.Null.And.Not.Empty,
          "contract currency must be non-empty (e.g. USD)");
      Assert.That(contract.ContractId, Is.GreaterThan(0),
          "contractId must be a positive integer");
      Assert.That(contract.Identifier, Is.Not.Null.And.Not.Empty,
          "contract identifier must be non-empty");
    }

    // =====================================================================
    // Assets (account-level asset summary)
    // Response shape (per Python + Java + Go SDKs):
    //   { code, data: { items: [ { account, netLiquidation, segments: {...} } ] } }
    // TigerDictResponse deserializes `data` as Dictionary<string, object>; the
    // "items" entry is a JArray. This test walks both the top-level entries
    // and the items array so it works across account variants.
    // =====================================================================
    [Test]
    public void GetAssets_ReturnsNonEmptyData_WithValidNetLiquidation()
    {
      var model = new GlobalAssetsModel
      {
        Account = _account,
        Segment = true,
        MarketValue = true
      };
      var resp = Execute<TigerDictResponse>(TradeApiService.ASSETS, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "assets data dict must be non-empty");

      // Collect every JSON node that carries a netLiquidation field, whether
      // it lives at the top level (currency-keyed) or under items[*].segments.
      var candidates = new List<JObject>();
      foreach (var entry in resp.Data)
      {
        switch (entry.Value)
        {
          case JObject jo:
            candidates.Add(jo);
            break;
          case JArray ja:
            foreach (var it in ja)
              if (it is JObject j) candidates.Add(j);
            break;
        }
      }

      double? netLiq = null;
      foreach (var jo in candidates)
      {
        if (jo["netLiquidation"] != null)
        {
          netLiq = jo["netLiquidation"]!.Value<double>();
          break;
        }
        // Some responses nest netLiquidation under segments.
        if (jo["segments"] is JObject segs)
        {
          foreach (var seg in segs.Properties())
          {
            if (seg.Value is JObject sv && sv["netLiquidation"] != null)
            {
              netLiq = sv["netLiquidation"]!.Value<double>();
              break;
            }
          }
        }
        if (netLiq.HasValue) break;
      }

      Assert.That(netLiq.HasValue, Is.True,
          "assets response must expose a netLiquidation field somewhere in " +
          "the payload (top-level, items[*], or items[*].segments.*). " +
          $"Received keys: [{string.Join(",", resp.Data.Keys)}]");
      // netLiquidation can be negative in margin-deficit / over-leveraged accounts,
      // so only assert that the field deserializes to a finite number.
      Assert.That(double.IsNaN(netLiq!.Value), Is.False,
          "netLiquidation must be a finite number, not NaN");
    }

    // =====================================================================
    // Positions (may be empty for fresh accounts)
    // =====================================================================
    [Test]
    public void GetPositions_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      var model = new PositionsModel
      {
        Account = _account
      };
      var resp = Execute<PositionsResponse>(TradeApiService.POSITIONS, model);

      Assert.That(resp.Data, Is.Not.Null, "positions data wrapper must not be null");
      var items = resp.Data!.Items;

      // Positions may legitimately be empty for a fresh paper account.
      // Assume.That skips (not passes) when there are no positions to validate.
      Assume.That(items, Is.Not.Null.And.Count.GreaterThan(0),
          "no positions to validate — skipping field checks (empty account)");
      foreach (var pos in items!)
      {
        Assert.That(pos.Symbol, Is.Not.Null.And.Not.Empty,
            "position symbol must be non-empty");
        Assert.That(pos.Account, Is.Not.Null.And.Not.Empty,
            "position account must be non-empty");
        Assert.That(pos.SecType, Is.Not.Null.And.Not.Empty,
            "position secType must be non-empty");
        Assert.That(pos.AverageCost, Is.GreaterThan(0),
            "position averageCost must be > 0");
        Assert.That(pos.MarketValue, Is.GreaterThanOrEqualTo(0),
            "position marketValue must be populated");
      }
    }

    // =====================================================================
    // Orders (all orders in last 30 days; may be empty)
    // =====================================================================
    [Test]
    public void GetOrders_Last30Days_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      long now = DateUtil.CurrentTimeMillis();
      var model = new QueryOrderModel
      {
        Account = _account,
        StartDate = now - 30L * 24 * 3600 * 1000,
        EndDate = now,
        Limit = 10
      };
      var resp = Execute<OrderBatchResponse>(TradeApiService.ORDERS, model);

      Assert.That(resp.Data, Is.Not.Null, "orders data wrapper must not be null");
      var items = resp.Data!.Items;

      // Orders may be empty if no trades in the last 30 days.
      // Assume.That skips (not passes) when there are no orders to validate.
      Assume.That(items, Is.Not.Null.And.Count.GreaterThan(0),
          "no orders to validate — skipping field checks (no trades in last 30 days)");
      foreach (var order in items!)
      {
        Assert.That(order.Id, Is.Not.EqualTo(0), "order id must be non-zero");
        Assert.That(order.Symbol, Is.Not.Null.And.Not.Empty,
            "order symbol must be non-empty");
        Assert.That(order.Action, Is.Not.Null.And.Not.Empty,
            "order action must be non-empty");
        Assert.That(order.Status, Is.Not.EqualTo(OrderStatus.NONE),
            "order status must not be NONE");
        Assert.That(order.OrderType, Is.Not.Null.And.Not.Empty,
            "order orderType must be non-empty");
      }
    }

    // =====================================================================
    // Filled Orders (last 30 days; may be empty — uses FILLED_ORDERS endpoint)
    // =====================================================================
    [Test]
    public void GetOrders_Last30Days_FilledStatus_WithValidFieldsWhenNonEmpty()
    {
      long now = DateUtil.CurrentTimeMillis();
      var model = new QueryOrderModel
      {
        Account = _account,
        StartDate = now - 30L * 24 * 3600 * 1000,
        EndDate = now,
        Limit = 10,
      };
      var resp = Execute<OrderBatchResponse>(TradeApiService.FILLED_ORDERS, model);

      Assert.That(resp.Data, Is.Not.Null, "filled orders data wrapper must not be null");
      var items = resp.Data!.Items;

      Assume.That(items, Is.Not.Null.And.Count.GreaterThan(0),
          "no filled orders to validate — skipping field checks (no fills in last 30 days)");
      foreach (var order in items!)
      {
        Assert.That(order.Id, Is.Not.EqualTo(0), "filled order id must be non-zero");
        Assert.That(order.Symbol, Is.Not.Null.And.Not.Empty,
            "filled order symbol must be non-empty");
        Assert.That(order.OrderType, Is.Not.Null.And.Not.Empty,
            "filled order orderType must be non-empty");
      }
    }

    // =====================================================================
    // Active Orders (open orders; may be empty)
    // =====================================================================
    [Test]
    public void GetActiveOrders_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      var model = new QueryOrderModel
      {
        Account = _account,
        Limit = 10
      };
      var resp = Execute<OrderBatchResponse>(TradeApiService.ACTIVE_ORDERS, model);

      Assert.That(resp.Data, Is.Not.Null, "active orders data wrapper must not be null");
      var items = resp.Data!.Items;

      // Active orders may be empty if no open orders exist.
      // Assume.That skips (not passes) when there are no active orders to validate.
      Assume.That(items, Is.Not.Null.And.Count.GreaterThan(0),
          "no active orders to validate — skipping field checks (no open orders)");
      foreach (var order in items!)
      {
        Assert.That(order.Id, Is.Not.EqualTo(0), "active order id must be non-zero");
        Assert.That(order.Symbol, Is.Not.Null.And.Not.Empty,
            "active order symbol must be non-empty");
        Assert.That(order.Status, Is.Not.EqualTo(OrderStatus.NONE),
            "active order status must not be NONE");
      }
    }

    // =====================================================================
    // Accounts (list all linked accounts)
    // =====================================================================
    [Test]
    public void GetAccounts_ReturnsValidFields()
    {
      // ACCOUNTS does not require an account parameter
      var req = new TigerRequest<AccountsResponse>
      {
        ApiMethodName = TradeApiService.ACCOUNTS,
        ModelValue = new TradeModel()
      };
      var resp = _client!.Execute(req);
      Assert.That(resp, Is.Not.Null, "accounts response must not be null");
      Assert.That(resp!.IsSuccess(), Is.True,
          $"accounts returned error code={resp.Code} msg={resp.Message}");
      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "accounts should return at least 1 account");
      foreach (var kv in resp.Data)
      {
        foreach (var acct in kv.Value)
        {
          Assert.That(acct.Account, Is.Not.Null.And.Not.Empty,
              "account id must be non-empty");
          Assert.That(acct.AccountType, Is.Not.Null.And.Not.Empty,
              "accountType must be non-empty");
        }
      }
    }

    // =====================================================================
    // Contracts (batch symbol lookup)
    // =====================================================================
    [Test]
    public void GetContracts_AAPL_ReturnsValidFields()
    {
      var model = new ContractsModel
      {
        Symbols = new List<string> { "AAPL" },
        SecType = SecType.STK.ToString()
      };
      var resp = Execute<ContractsResponse>(TradeApiService.CONTRACTS, model);

      Assert.That(resp.Data, Is.Not.Null, "contracts data must not be null");
      // Server may return empty map if no contracts are available for this account.
      Assume.That(resp.Data.Count, Is.GreaterThan(0),
          "contracts returned empty map — skipping field checks (no account state)");
      // Key may be symbol alone or a composite identifier (e.g. "AAPL,STK,USD,SMART").
      var aaplKey = resp.Data.Keys.FirstOrDefault(k =>
          k.StartsWith("AAPL", StringComparison.OrdinalIgnoreCase));
      Assume.That(aaplKey, Is.Not.Null,
          "contracts response has no AAPL entry — skipping field checks");
      var items = resp.Data[aaplKey!];
      Assert.That(items, Is.Not.Null.And.Count.GreaterThan(0),
          "contracts list must be non-empty");
      Assert.That(items[0].Currency, Is.Not.Null.And.Not.Empty,
          "contract currency must be non-empty");
    }

    // =====================================================================
    // Prime Assets
    // =====================================================================
    [Test]
    public void GetPrimeAssets_ReturnsValidFields()
    {
      var model = new PrimeAssetsModel
      {
        Account = _account,
        BaseCurrency = "USD",
        Consolidated = true
      };
      var resp = Execute<PrimeAssetResponse>(TradeApiService.PRIME_ASSETS, model);

      Assert.That(resp.Data, Is.Not.Null, "prime_assets data must not be null");
      Assert.That(resp.Data.Account, Is.Not.Null.And.Not.Empty,
          "prime asset account wire name");
    }

    // =====================================================================
    // Analytics Asset (rolling 30-day window)
    // =====================================================================
    [Test]
    public void GetAnalyticsAsset_ReturnsValidFields()
    {
      long nowMs   = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
      long startMs = nowMs - 30L * 24 * 3600 * 1000;
      string startDate = DateTimeOffset.FromUnixTimeMilliseconds(startMs).ToString("yyyy-MM-dd");
      string endDate   = DateTimeOffset.FromUnixTimeMilliseconds(nowMs).ToString("yyyy-MM-dd");

      var model = new PrimeAnalyticsAssetModel
      {
        Account = _account,
        SegType = SegmentType.SEC,
        Currency = Currency.USD,
        StartDate = startDate,
        EndDate = endDate
      };
      var resp = Execute<PrimeAnalyticsAssetResponse>(TradeApiService.ANALYTICS_ASSET, model);

      Assert.That(resp.Data, Is.Not.Null, "analytics_asset data must not be null");
      if (resp.Data.Summary != null)
      {
        // Verify key summary wire-names deserialize correctly.
        Assert.That(resp.Data.Summary.PnlPercentage, Is.Not.Null,
            "summary.pnlPercentage wire name");
      }
    }

    // =====================================================================
    // Inactive Orders (may be empty)
    // =====================================================================
    [Test]
    public void GetInactiveOrders_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      var model = new QueryOrderModel
      {
        Account = _account,
        Limit = 10
      };
      var resp = Execute<OrderBatchResponse>(TradeApiService.INACTIVE_ORDERS, model);

      Assert.That(resp.Data, Is.Not.Null, "inactive orders data wrapper must not be null");
      var items = resp.Data!.Items;
      // Inactive orders may be empty if no cancelled/rejected orders exist.
      // Assume.That skips (not passes) when there are no inactive orders to validate.
      Assume.That(items, Is.Not.Null.And.Count.GreaterThan(0),
          "no inactive orders to validate — skipping field checks (no cancelled/rejected orders)");
      foreach (var order in items!)
      {
        Assert.That(order.Id, Is.Not.EqualTo(0), "inactive order id must be non-zero");
        Assert.That(order.Symbol, Is.Not.Null.And.Not.Empty,
            "inactive order symbol must be non-empty");
        Assert.That(order.Status, Is.Not.EqualTo(OrderStatus.NONE),
            "inactive order status must not be NONE");
      }
    }

    // =====================================================================
    // Filled Orders (last 30 days; may be empty)
    // =====================================================================
    [Test]
    public void GetFilledOrders_Last30Days_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      long now = DateUtil.CurrentTimeMillis();
      var model = new QueryOrderModel
      {
        Account = _account,
        StartDate = now - 30L * 24 * 3600 * 1000,
        EndDate = now,
        Limit = 10
      };
      var resp = Execute<OrderBatchResponse>(TradeApiService.FILLED_ORDERS, model);

      Assert.That(resp.Data, Is.Not.Null, "filled orders data wrapper must not be null");
      var items = resp.Data!.Items;
      // Filled orders may be empty if no fills in the last 30 days.
      // Assume.That skips (not passes) when there are no filled orders to validate.
      Assume.That(items, Is.Not.Null.And.Count.GreaterThan(0),
          "no filled orders to validate — skipping field checks (no fills in last 30 days)");
      foreach (var order in items!)
      {
        Assert.That(order.Id, Is.Not.EqualTo(0), "filled order id must be non-zero");
        Assert.That(order.Symbol, Is.Not.Null.And.Not.Empty,
            "filled order symbol must be non-empty");
        Assert.That(order.Status, Is.Not.EqualTo(OrderStatus.NONE),
            "filled order status must not be NONE");
      }
    }

    // =====================================================================
    // Order Transactions (last 30 days; may be empty)
    // =====================================================================
    [Test]
    public void GetOrderTransactions_Last30Days_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      long now = DateUtil.CurrentTimeMillis();
      var model = new OrderTransactionsModel
      {
        Account = _account,
        Symbol = "AAPL",
        StartDate = now - 30L * 24 * 3600 * 1000,
        EndDate = now,
        Limit = 10
      };
      var resp = Execute<OrderTransactionsResponse>(TradeApiService.ORDER_TRANSACTIONS, model);

      Assert.That(resp.Data, Is.Not.Null, "order_transactions data must not be null");
      // Order transactions may be empty if no fills in the last 30 days.
      // When transactions exist, validate key fields.
      if (resp.Data!.Items != null && resp.Data.Items.Count > 0)
      {
        var txn = resp.Data.Items[0];
        Assert.That(txn.Id, Is.Not.EqualTo(0), "transaction id must be non-zero");
        Assert.That(txn.Symbol, Is.Not.Null.And.Not.Empty,
            "transaction symbol must be non-empty");
        Assert.That(txn.Action, Is.Not.Null.And.Not.Empty,
            "transaction action must be non-empty");
      }
    }

    // =====================================================================
    // Segment Fund History (may be empty)
    // =====================================================================
    [Test]
    public void GetSegmentFundHistory_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      var model = new SegmentFundModel
      {
        Account = _account,
        FromSegment = SegmentType.SEC,
        ToSegment = SegmentType.FUT,
        Currency = Currency.USD
      };
      var resp = Execute<SegmentFundsResponse>(TradeApiService.SEGMENT_FUND_HISTORY, model);

      Assert.That(resp.Data, Is.Not.Null, "segment_fund_history data must not be null");
      // Segment fund history may be empty if no fund transfers in the query range.
      // When records exist, validate key fields.
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].Id, Is.Not.EqualTo(0),
            "segment fund id must be non-zero");
        Assert.That(resp.Data[0].Currency, Is.Not.Null.And.Not.Empty,
            "segment fund currency must be non-empty");
      }
    }

    // =====================================================================
    // Segment Fund Available (may be empty)
    // =====================================================================
    [Test]
    public void GetSegmentFundAvailable_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      var model = new SegmentFundModel
      {
        Account = _account,
        FromSegment = SegmentType.SEC,
        Currency = Currency.USD
      };
      var resp = Execute<SegmentFundAvailableResponse>(TradeApiService.SEGMENT_FUND_AVAILABLE, model);

      Assert.That(resp.Data, Is.Not.Null, "segment_fund_available data must not be null");
      // Available segment funds may be empty if no transferable balance exists.
      // When records exist, validate key fields.
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].FromSegment, Is.Not.Null.And.Not.Empty,
            "available fund fromSegment wire name");
        Assert.That(resp.Data[0].Currency, Is.Not.Null.And.Not.Empty,
            "available fund currency must be non-empty");
      }
    }

    // =====================================================================
    // Estimate Tradable Quantity (AAPL buy)
    // =====================================================================
    [Test]
    public void GetEstimateTradableQuantity_AAPL_ReturnsValidFields()
    {
      var model = new EstimateTradableQuantityModel
      {
        Account = _account,
        Symbol = "AAPL",
        SecType = SecType.STK,
        SegType = SegmentType.SEC,
        Action = ActionType.BUY,
        OrderType = OrderType.MKT
      };
      var resp = Execute<EstimateTradableQuantityResponse>(TradeApiService.ESTIMATE_TRADABLE_QUANTITY, model);

      Assert.That(resp.Data, Is.Not.Null, "estimate_tradable_quantity data must not be null");
      Assert.That(resp.Data.TradableQuantity, Is.GreaterThanOrEqualTo(0),
          "tradableQuantity must be >= 0");
    }

    // =====================================================================
    // Fund Details (cash flow records; may be empty)
    // =====================================================================
    [Test]
    public void GetFundDetails_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      var model = new FundDetailsModel(_account)
      {
        SegTypes = new List<string> { SegmentType.SEC.ToString() },
        Currency = Currency.USD.ToString(),
        Start = 0,
        Limit = 5
      };
      var resp = Execute<FundDetailsResponse>(TradeApiService.FUND_DETAILS, model);

      Assert.That(resp.Data, Is.Not.Null, "fund_details data must not be null");
      // Items may be empty if no cash flow in range.
      if (resp.Data.Items != null && resp.Data.Items.Count > 0)
      {
        Assert.That(resp.Data.Items[0].Id, Is.Not.EqualTo(0),
            "fund detail id must be non-zero");
        Assert.That(resp.Data.Items[0].Currency, Is.Not.Null.And.Not.Empty,
            "fund detail currency must be non-empty");
      }
    }

    // =====================================================================
    // Aggregate Assets
    // Institution-only endpoint: individual / retail / paper accounts get
    // "only support institution account" back from the server. Kept as a
    // skip until CI credentials point at an institutional account.
    // =====================================================================
    [Test]
    public void GetAggregateAssets_ReturnsValidFields()
    {
      Assert.Ignore(
          "aggregate_assets is only available to institutional accounts; " +
          "server responds \"only support institution account\" for standard " +
          "and paper accounts. Re-enable when CI credentials use an " +
          "institutional account.");
    }

    // =====================================================================
    // Position Transfer Records (may be empty)
    // =====================================================================
    [Test]
    public void GetPositionTransferRecords_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      var model = new PositionTransferRecordsModel
      {
        AccountId = _account,
        SinceDate = "2025-01-01",
        ToDate = "2025-01-31"
      };
      var resp = Execute<PositionTransferRecordsResponse>(TradeApiService.POSITION_TRANSFER_RECORDS, model);

      Assert.That(resp.Data, Is.Not.Null, "position_transfer_records data must not be null");
      // Transfer records may be empty if no transfers in the query range.
      // When records exist, validate key fields.
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].Id, Is.Not.EqualTo(0),
            "transfer record id must be non-zero");
        Assert.That(resp.Data[0].AccountId, Is.Not.Null.And.Not.Empty,
            "transfer record accountId must be non-empty");
      }
    }

    // =====================================================================
    // Position Transfer External Records (may be empty)
    // API requires account_id (not account), since_date, and to_date.
    // Standard / paper accounts get code=1200 bad_request; only specific
    // brokerage account types support this endpoint. Kept as a skip until
    // CI credentials point at an eligible account.
    // =====================================================================
    [Test]
    public void GetPositionTransferExternalRecords_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      Assert.Ignore(
          "position_transfer_external_records returns code=1200 bad_request for " +
          "standard / paper accounts; endpoint is limited to specific brokerage " +
          "account types (external-transfer-enabled). Re-enable when CI " +
          "credentials point at an eligible account.");
    }

    // =====================================================================
    // Option Exercise Check (preview only — no state change)
    // =====================================================================
    [Test]
    public async System.Threading.Tasks.Task CheckOptionExercise_Preview_Succeeds()
    {
      Assert.That(_client, Is.Not.Null, "TradeClient is null");
      // Preview does not place a real exercise request. A dummy contract ID
      // will likely return a business error, but the call path is exercised.
      var resp = await _client!.CheckOptionExerciseAsync(
          contractId: 0, type: "Exercise", quantity: 1, account: _account);

      Assert.That(resp, Is.Not.Null, "option_exercise_check response must not be null");
      if (!resp.IsSuccess())
      {
        // Only pass on expected business errors (contract not found / no position /
        // invalid param). CodeBizParamError is returned when contractId=0 is
        // rejected by server-side validation ("contractId can not be null").
        // Any other error code (auth failure, 5xx, etc.) is a real failure.
        const int codeContractNotFound = 70011;
        const int codeNoPosition       = 70012;
        if (resp.Code == codeContractNotFound || resp.Code == codeNoPosition
            || resp.Code == CodeBizParamError)
          Assert.Pass($"expected error for dummy contract, code={resp.Code} msg={resp.Message}");
        else
          Assert.Fail($"unexpected error from option_exercise_check, code={resp.Code} msg={resp.Message}");
      }
      // If the call succeeded, validate the data fields.
      Assert.That(resp.Data, Is.Not.Null,
          "option_exercise_check data must not be null on success");
    }

    // =====================================================================
    // Option Exercise Records (read-only query; may be empty)
    // =====================================================================
    [Test]
    public async System.Threading.Tasks.Task GetOptionExerciseRecords_Succeeds()
    {
      Assert.That(_client, Is.Not.Null, "TradeClient is null");
      var resp = await _client!.GetOptionExerciseRecordsAsync(account: _account);

      Assert.That(resp, Is.Not.Null, "option_exercise_record response must not be null");
      Assert.That(resp.IsSuccess(), Is.True,
          $"option_exercise_record returned error code={resp.Code} msg={resp.Message}");
      // Records may be empty if no exercise history.
    }

    // =====================================================================
    // Option Exercise Positions (read-only query; may be empty)
    // =====================================================================
    [Test]
    public async System.Threading.Tasks.Task GetOptionExercisePositions_Succeeds()
    {
      Assert.That(_client, Is.Not.Null, "TradeClient is null");
      var resp = await _client!.GetOptionExercisePositionsAsync("Exercise", account: _account);

      Assert.That(resp, Is.Not.Null, "option_exercise_position response must not be null");
      Assert.That(resp.IsSuccess(), Is.True,
          $"option_exercise_position returned error code={resp.Code} msg={resp.Message}");
      // Positions may be empty if no exercisable options.
    }

    // =====================================================================
    // Preview Order (AAPL MKT BUY 1 — no actual order placed)
    // preview_order sends the order to the gateway for fee/margin estimation
    // only; no real order is created. The response reuses PlaceOrderResponse
    // (same wire shape). isPass may be false when buying power is insufficient
    // on a paper account, but the call itself must succeed.
    // =====================================================================
    [Test]
    public void PreviewOrder_AAPL_MktBuy_Succeeds()
    {
      // Fetch the AAPL contract so we can build a well-formed PlaceOrderModel.
      var contractReq = new TigerRequest<ContractResponse>
      {
        ApiMethodName = TradeApiService.CONTRACT,
        ModelValue = new ContractModel { Symbol = "AAPL", SecType = SecType.STK.ToString() }
      };
      var contractResp = _client!.Execute(contractReq);
      Assert.That(contractResp, Is.Not.Null, "contract prerequisite must not be null");
      Assert.That(contractResp!.IsSuccess(), Is.True,
          $"contract prerequisite failed: code={contractResp.Code} msg={contractResp.Message}");
      Assert.That(contractResp.Data, Is.Not.Null, "contract data must not be null");

      var model = PlaceOrderModel.BuildMarketOrder(
          _account, contractResp.Data!, ActionType.BUY, quantity: 1);
      var req = new TigerRequest<PlaceOrderResponse>
      {
        ApiMethodName = TradeApiService.PREVIEW_ORDER,
        ModelValue = model
      };
      var resp = _client!.Execute(req);
      Assert.That(resp, Is.Not.Null, "preview_order response must not be null");
      Assert.That(resp!.IsSuccess(), Is.True,
          $"preview_order returned error code={resp.Code} msg={resp.Message}");
      Assert.That(resp.Data, Is.Not.Null, "preview_order data must not be null");
      // Id == 0 is expected for preview (no order stored); just verify it
      // deserialises cleanly with a non-negative value.
      Assert.That(resp.Data.Id, Is.GreaterThanOrEqualTo(0),
          "preview_order id must be >= 0");
    }

    // =====================================================================
    // Place Order (AAPL MKT BUY 1) — Mode A
    // Attempts to place a real order. Outside US trading hours the gateway
    // returns a "not in trading hours" error (codes 70009 / 70010 / 1000 or
    // a message containing "trading hours" / "market closed"). In that case
    // the test passes because the wire path was validated. During US TRADING
    // hours the order must succeed and return a positive order id.
    // =====================================================================
    [Test]
    public void PlaceOrder_AAPL_MktBuy_ModeA_TradingHoursOrExpectedError()
    {
      var contractReq = new TigerRequest<ContractResponse>
      {
        ApiMethodName = TradeApiService.CONTRACT,
        ModelValue = new ContractModel { Symbol = "AAPL", SecType = SecType.STK.ToString() }
      };
      var contractResp = _client!.Execute(contractReq);
      Assert.That(contractResp, Is.Not.Null, "contract prerequisite must not be null");
      Assert.That(contractResp!.IsSuccess(), Is.True,
          $"contract prerequisite failed: code={contractResp.Code} msg={contractResp.Message}");
      Assert.That(contractResp.Data, Is.Not.Null, "contract data must not be null");

      var model = PlaceOrderModel.BuildMarketOrder(
          _account, contractResp.Data!, ActionType.BUY, quantity: 1);
      var req = new TigerRequest<PlaceOrderResponse>
      {
        ApiMethodName = TradeApiService.PLACE_ORDER,
        ModelValue = model
      };
      var resp = _client!.Execute(req);
      Assert.That(resp, Is.Not.Null, "place_order response must not be null");

      if (!resp!.IsSuccess())
      {
        // Outside trading hours the server returns a "not in trading hours"
        // or "market closed" business error — accepted as a PASS because
        // the full wire path was exercised. Any other error code is a real
        // failure and must be surfaced.
        var msg = resp.Message ?? string.Empty;
        bool isExpectedOutOfHoursError =
            msg.IndexOf("trading hours", StringComparison.OrdinalIgnoreCase) >= 0 ||
            msg.IndexOf("market closed", StringComparison.OrdinalIgnoreCase) >= 0 ||
            msg.IndexOf("not trading", StringComparison.OrdinalIgnoreCase) >= 0;
        if (isExpectedOutOfHoursError || IsPermissionError(msg))
        {
          TestContext.Progress.WriteLine(
              $"place_order declined outside trading hours or permission-restricted (expected): code={resp.Code} msg={msg}");
          return; // out-of-hours or permission-restricted: wire path validated by request completing
        }
        Assert.Fail($"place_order failed with unexpected error: code={resp.Code} msg={msg}");
      }

      // Inside trading hours the order must be accepted.
      Assert.That(resp.Data, Is.Not.Null, "place_order data must not be null on success");
      Assert.That(resp.Data!.Id, Is.GreaterThan(0),
          "place_order id must be > 0 on success");
    }

    // =====================================================================
    // US STK order-type sweep — deliberately unfillable prices (SafeBuyPrice/
    // SafeSellPrice) so PREVIEW+PLACE round-trip the wire path without ever
    // executing. Market order-type already covered above — not duplicated.
    // =====================================================================
    [Test]
    public void PlaceOrder_AAPL_StopSell_AcceptsOrSkips()
    {
      var contract = FetchStockContract("AAPL");
      var model = PlaceOrderModel.BuildStopOrder(
          _account, contract, ActionType.SELL, quantity: 1, auxPrice: SafeSellPrice);
      PreviewAndPlace(model, "StopSell AAPL");
    }

    [Test]
    public void PlaceOrder_AAPL_StopLimitSell_AcceptsOrSkips()
    {
      var contract = FetchStockContract("AAPL");
      var model = PlaceOrderModel.BuildStopLimitOrder(
          _account, contract, ActionType.SELL, quantity: 1,
          limitPrice: SafeSellPrice, auxPrice: SafeSellPrice);
      PreviewAndPlace(model, "StopLimitSell AAPL");
    }

    [Test]
    public void PlaceOrder_AAPL_TrailSell_AcceptsOrSkips()
    {
      var contract = FetchStockContract("AAPL");
      var model = PlaceOrderModel.BuildTrailOrder(
          _account, contract, ActionType.SELL, quantity: 1,
          trailingPercent: 10.0, auxPrice: SafeSellPrice);
      PreviewAndPlace(model, "TrailSell AAPL");
    }

    [Test]
    public void PlaceOrder_AAPL_TwapBuy_AcceptsOrSkips()
    {
      long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
      long start = now + 60_000;
      long end = now + 3_600_000;
      var model = PlaceOrderModel.BuildTWAPOrder(
          _account, "AAPL", ActionType.BUY, quantity: 1,
          startTime: start, endTime: end, limitPrice: SafeBuyPrice);
      PreviewAndPlace(model, "TwapBuy AAPL");
    }

    [Test]
    public void PlaceOrder_AAPL_VwapBuy_AcceptsOrSkips()
    {
      long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
      long start = now + 60_000;
      long end = now + 3_600_000;
      var model = PlaceOrderModel.BuildVWAPOrder(
          _account, "AAPL", ActionType.BUY, quantity: 1,
          startTime: start, endTime: end, participationRate: 0.1, limitPrice: SafeBuyPrice);
      PreviewAndPlace(model, "VwapBuy AAPL");
    }

    [Test]
    public void PlaceOrder_AAPL_IcebergBuy_AcceptsOrSkips()
    {
      var contract = FetchStockContract("AAPL");
      var model = PlaceOrderModel.BuildIcebergOrder(
          _account, contract, ActionType.BUY, quantity: 10,
          limitPrice: SafeBuyPrice, displaySize: 1);
      PreviewAndPlace(model, "IcebergBuy AAPL");
    }

    [Test]
    public void PlaceOrder_AAPL_OcaBrackets_AcceptsOrSkips()
    {
      var contract = FetchStockContract("AAPL");
      var model = PlaceOrderModel.BuildOCABracketsOrder(
          _account, contract, ActionType.BUY, quantity: 1,
          profitTakerPrice: SafeSellPrice, profitTakerTif: TimeInForce.DAY, profitTakerRth: true,
          stopLossPrice: SafeBuyPrice, stopLossTif: TimeInForce.DAY, stopLossRth: true);
      PreviewAndPlace(model, "OcaBrackets AAPL");
    }

    [Test]
    public void PlaceOrder_AAPL_WithBrackets_AcceptsOrSkips()
    {
      var contract = FetchStockContract("AAPL");
      var model = PlaceOrderModel.BuildLimitOrder(
          _account, contract, ActionType.BUY, quantity: 1, limitPrice: SafeBuyPrice);
      model.AddBracketsOrder(
          profitTakerPrice: SafeSellPrice, profitTakerTif: TimeInForce.DAY, profitTakerRth: true,
          stopLossPrice: SafeBuyPrice / 2, stopLossTif: TimeInForce.DAY);
      PreviewAndPlace(model, "WithBrackets AAPL");
    }

    // =====================================================================
    // US OPT limit — resolved via MarketHelpers.ResolveUsOptionIdentifier
    // =====================================================================
    [Test]
    public void PlaceOrder_UsOptLimit_AcceptsOrSkips()
    {
      string? identifier = MarketHelpers.ResolveUsOptionIdentifier(IntegTestConfig.QuoteClient);
      if (string.IsNullOrEmpty(identifier))
      {
        Assert.Ignore("could not resolve a live US option identifier");
        return;
      }
      var contract = ContractItem.BuildOptionContract(identifier!);
      var model = PlaceOrderModel.BuildLimitOrder(
          _account, contract, ActionType.BUY, quantity: 1, limitPrice: SafeBuyPrice);
      PreviewAndPlace(model, $"UsOptLimit {identifier}");
    }

    // =====================================================================
    // US FUT limit — resolved via MarketHelpers.ResolveUsFuturesContract
    // (server-resolved front-month contract, no client-side date math)
    // =====================================================================
    [Test]
    public void PlaceOrder_UsFutLimit_AcceptsOrSkips()
    {
      var contract = MarketHelpers.ResolveUsFuturesContract(IntegTestConfig.QuoteClient);
      if (contract == null)
      {
        Assert.Ignore("could not resolve a live US futures contract");
        return;
      }
      var model = PlaceOrderModel.BuildLimitOrder(
          _account, contract, ActionType.BUY, quantity: 1, limitPrice: SafeBuyPrice);
      PreviewAndPlace(model, $"UsFutLimit {contract.Symbol}");
    }

    // =====================================================================
    // HK/CN/SG STK limit — Python's exact symbol conventions.
    // =====================================================================
    [Test]
    public void PlaceOrder_HkStkLimit_AcceptsOrSkips()
    {
      var contract = FetchStockContract("00700");
      if (contract == null) { Assert.Ignore("00700 contract not tradeable for this account"); return; }
      contract.Currency = Currency.HKD.ToString();
      var model = PlaceOrderModel.BuildLimitOrder(
          _account, contract, ActionType.BUY, quantity: 100, limitPrice: SafeBuyPrice);
      PreviewAndPlace(model, "HkStkLimit 00700");
    }

    [Test]
    public void PlaceOrder_CnStkLimit_AcceptsOrSkips()
    {
      var contractReq = new TigerRequest<ContractResponse>
      {
        ApiMethodName = TradeApiService.CONTRACT,
        ModelValue = new ContractModel
        {
          Symbol = "000001",
          SecType = SecType.STK.ToString(),
          Currency = "CNH",
          Exchange = "SEHKSZSE"
        }
      };
      var contractResp = _client!.Execute(contractReq);
      Assert.That(contractResp, Is.Not.Null, "contract fetch for 000001 must not be null");
      if (!contractResp!.IsSuccess())
      {
        if (IsPermissionError(contractResp.Message)) { Assert.Ignore("000001 contract not tradeable for this account"); return; }
        Assert.Fail($"contract fetch for 000001 failed: {contractResp.Message}");
      }
      Assert.That(contractResp.Data, Is.Not.Null, "contract fetch for 000001 data must not be null");

      var model = PlaceOrderModel.BuildLimitOrder(
          _account, contractResp.Data!, ActionType.BUY, quantity: 100, limitPrice: SafeBuyPrice);
      PreviewAndPlace(model, "CnStkLimit 000001");
    }

    [Test]
    public void PlaceOrder_SgStkLimit_AcceptsOrSkips()
    {
      var contract = FetchStockContract("D05");
      if (contract == null) { Assert.Ignore("D05 contract not tradeable for this account"); return; }
      contract.Currency = Currency.SGD.ToString();
      var model = PlaceOrderModel.BuildLimitOrder(
          _account, contract, ActionType.BUY, quantity: 100, limitPrice: SafeBuyPrice);
      PreviewAndPlace(model, "SgStkLimit D05");
    }

    // =====================================================================
    // HK auction — limit gated on extended hours; market preview-only since
    // fill price at auction is unpredictable.
    // =====================================================================
    [Test]
    public void PlaceOrder_HkAuctionLimit_AcceptsOrSkips()
    {
      if (!MarketHelpers.IsMarketOpenExtended(IntegTestConfig.QuoteClient, Market.HK))
      {
        Assert.Ignore("HK market not in an open/extended session — auction order not applicable");
        return;
      }
      var contract = FetchStockContract("00700");
      if (contract == null) { Assert.Ignore("00700 contract not tradeable for this account"); return; }
      contract.Currency = Currency.HKD.ToString();
      var model = PlaceOrderModel.BuildAuctionOrder(
          _account, contract, ActionType.BUY, quantity: 100, limitPrice: SafeBuyPrice);
      PreviewAndPlace(model, "HkAuctionLimit 00700");
    }

    [Test]
    public void PlaceOrder_HkAuctionMarket_PreviewOnly()
    {
      var contract = FetchStockContract("00700");
      if (contract == null) { Assert.Ignore("00700 contract not tradeable for this account"); return; }
      contract.Currency = Currency.HKD.ToString();
      var model = PlaceOrderModel.BuildAuctionOrder(
          _account, contract, ActionType.BUY, quantity: 100, limitPrice: SafeBuyPrice,
          orderType: OrderType.AM);
      var previewReq = new TigerRequest<PlaceOrderResponse>
      {
        ApiMethodName = TradeApiService.PREVIEW_ORDER,
        ModelValue = model
      };
      var previewResp = _client!.Execute(previewReq);
      Assert.That(previewResp, Is.Not.Null, "HkAuctionMarket preview response must not be null");
      if (!previewResp!.IsSuccess() && !IsPermissionError(previewResp.Message))
      {
        Assert.Fail($"HkAuctionMarket preview failed: {previewResp.Message}");
      }
    }

    [Test]
    public void PlaceOrder_HkBracket_AcceptsOrSkips()
    {
      var contract = FetchStockContract("00700");
      if (contract == null) { Assert.Ignore("00700 contract not tradeable for this account"); return; }
      contract.Currency = Currency.HKD.ToString();
      var model = PlaceOrderModel.BuildLimitOrder(
          _account, contract, ActionType.BUY, quantity: 100, limitPrice: SafeBuyPrice);
      model.AddBracketsOrder(
          profitTakerPrice: SafeSellPrice, profitTakerTif: TimeInForce.DAY, profitTakerRth: true,
          stopLossPrice: SafeBuyPrice / 2, stopLossTif: TimeInForce.DAY);
      PreviewAndPlace(model, "HkBracket 00700");
    }

    // =====================================================================
    // US MLEG vertical spread — resolved via
    // MarketHelpers.ResolveUsVerticalSpreadLegs. Limit price is deliberately
    // deep-negative (-100.0): a credit-spread combo at that price cannot
    // execute — the safety mechanism, analogous to SafeBuyPrice/SafeSellPrice
    // for single-leg orders.
    // =====================================================================
    [Test]
    public void PlaceOrder_UsMlegVerticalSpread_AcceptsOrSkips()
    {
      var legs = MarketHelpers.ResolveUsVerticalSpreadLegs(IntegTestConfig.QuoteClient);
      if (legs == null)
      {
        Assert.Ignore("could not resolve live AAPL PUT strikes for a vertical spread");
        return;
      }
      var (lower, upper) = legs.Value;
      lower.Action = ActionType.BUY.ToString();
      lower.Ratio = 1;
      upper.Action = ActionType.SELL.ToString();
      upper.Ratio = 1;

      var model = PlaceOrderModel.BuildMultiLegOrder(
          _account, new List<ContractLeg> { lower, upper }, ComboType.VERTICAL,
          ActionType.BUY, quantity: 1, orderType: OrderType.LMT,
          limitPrice: -100.0, auxPrice: null, trailingPercent: null);
      PreviewAndPlace(model, $"UsMlegVerticalSpread {lower.Strike}/{upper.Strike}");
    }

    // =====================================================================
    // Cancel Order — Mode A
    // Attempts to cancel a non-existent order (id=0). The gateway returns a
    // known business error (order not found / invalid id). This validates the
    // full cancel_order wire path without requiring a live open order.
    // If an open order happens to exist, prefer that id for a cleaner test.
    // =====================================================================
    [Test]
    public void CancelOrder_NonExistentId_ReturnsExpectedBusinessError()
    {
      // Try to find a real active order id first; fall back to 0.
      long orderId = 0;
      var activeReq = new TigerRequest<OrderBatchResponse>
      {
        ApiMethodName = TradeApiService.ACTIVE_ORDERS,
        ModelValue = new QueryOrderModel { Account = _account, Limit = 1 }
      };
      var activeResp = _client!.Execute(activeReq);
      if (activeResp != null && activeResp.IsSuccess()
          && activeResp.Data?.Items != null && activeResp.Data.Items.Count > 0)
      {
        orderId = activeResp.Data.Items[0].Id;
        TestContext.Progress.WriteLine(
            $"cancel_order: using real active order id={orderId}");
      }

      var model = new CancelOrderModel
      {
        Account = _account,
        Id = orderId
      };
      var req = new TigerRequest<PlaceOrderResponse>
      {
        ApiMethodName = TradeApiService.CANCEL_ORDER,
        ModelValue = model
      };
      var resp = _client!.Execute(req);
      Assert.That(resp, Is.Not.Null, "cancel_order response must not be null");

      if (orderId > 0 && resp!.IsSuccess())
      {
        // A real active order was cancelled successfully.
        TestContext.Progress.WriteLine(
            $"cancel_order succeeded for active order id={orderId}");
        return;
      }

      // For id=0 (or an order that is no longer cancellable) the gateway
      // returns a business error. Verify it is a recognised order-related
      // error code rather than an auth or 5xx failure.
      // CodeBizParamError is a generic biz param error returned when id=0 is
      // rejected by server-side validation (e.g. "field 'id' cannot be empty").
      var msg = resp!.Message ?? string.Empty;
      bool isExpectedOrderError =
          resp.Code == CodeBizParamError ||
          msg.IndexOf("order", StringComparison.OrdinalIgnoreCase) >= 0 ||
          msg.IndexOf("not found", StringComparison.OrdinalIgnoreCase) >= 0 ||
          msg.IndexOf("invalid", StringComparison.OrdinalIgnoreCase) >= 0 ||
          msg.IndexOf("cancel", StringComparison.OrdinalIgnoreCase) >= 0;
      Assert.That(isExpectedOrderError, Is.True,
          $"cancel_order returned an unrecognised error: code={resp.Code} msg={msg}. " +
          "Expected an order-related business error for a non-existent / non-cancellable id.");
    }

    // =====================================================================
    // Modify Order — Mode A
    // Attempts to modify a non-existent order (id=0). The gateway returns a
    // known business error (order not found / invalid). If an active order
    // exists the test uses that id instead and verifies the call succeeds.
    // =====================================================================
    [Test]
    public void ModifyOrder_NonExistentId_ReturnsExpectedBusinessError()
    {
      // Try to find a real active order id first; fall back to 0.
      long orderId = 0;
      var activeReq = new TigerRequest<OrderBatchResponse>
      {
        ApiMethodName = TradeApiService.ACTIVE_ORDERS,
        ModelValue = new QueryOrderModel { Account = _account, Limit = 1 }
      };
      var activeResp = _client!.Execute(activeReq);
      if (activeResp != null && activeResp.IsSuccess()
          && activeResp.Data?.Items != null && activeResp.Data.Items.Count > 0)
      {
        orderId = activeResp.Data.Items[0].Id;
        TestContext.Progress.WriteLine(
            $"modify_order: using real active order id={orderId}");
      }

      var model = new ModifyOrderModel
      {
        Account = _account,
        Id = orderId,
        // Bump quantity by 1 as a minimal modification; gateway validates
        // before accepting so an invalid id will fail before reaching fill.
        TotalQuantity = 2
      };
      var req = new TigerRequest<PlaceOrderResponse>
      {
        ApiMethodName = TradeApiService.MODIFY_ORDER,
        ModelValue = model
      };
      var resp = _client!.Execute(req);
      Assert.That(resp, Is.Not.Null, "modify_order response must not be null");

      if (orderId > 0 && resp!.IsSuccess())
      {
        // A real active order was modified successfully.
        Assert.That(resp.Data, Is.Not.Null, "modify_order data must not be null on success");
        TestContext.Progress.WriteLine(
            $"modify_order succeeded for active order id={orderId}");
        return;
      }

      // For id=0 (or an order that is no longer modifiable) the gateway
      // returns a business error. Verify it is a recognised order-related
      // error code rather than an auth or 5xx failure.
      var msg = resp!.Message ?? string.Empty;
      bool isExpectedOrderError =
          resp.Code == CodeBizParamError ||
          msg.IndexOf("order", StringComparison.OrdinalIgnoreCase) >= 0 ||
          msg.IndexOf("not found", StringComparison.OrdinalIgnoreCase) >= 0 ||
          msg.IndexOf("invalid", StringComparison.OrdinalIgnoreCase) >= 0 ||
          msg.IndexOf("modify", StringComparison.OrdinalIgnoreCase) >= 0 ||
          msg.IndexOf("cannot be a negative", StringComparison.OrdinalIgnoreCase) >= 0;
      Assert.That(isExpectedOrderError, Is.True,
          $"modify_order returned an unrecognised error: code={resp.Code} msg={msg}. " +
          "Expected an order-related business error for a non-existent / non-modifiable id.");
    }
  }
}
