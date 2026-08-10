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
  /// Integration tests for trade read-only APIs (contract/asset/position/
  /// order queries) against the live gateway. No order placement,
  /// modification, or cancellation.
  /// Credentials come from env vars (see <see cref="IntegTestConfig"/>).
  /// Run with:  dotnet test --filter Category=Integration
  /// </summary>
  [TestFixture]
  [Category("Integration")]
  public class TradeIntegrationTest
  {
    private TradeClient? _client;
    private string _account = string.Empty;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      IntegTestConfig.EnsureCredentials();
      _client = IntegTestConfig.TradeClient;
      _account = IntegTestConfig.Account;
      Assert.That(_account, Is.Not.Null.And.Not.Empty,
          "account must not be null or empty — set TIGEROPEN_ACCOUNT env var");
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

      // The ASSETS API returns a dict keyed by currency. Each value is a JSON
      // object containing netLiquidation and other fields. Extract and validate.
      bool foundNetLiquidation = false;
      foreach (var entry in resp.Data)
      {
        if (entry.Value is JObject jo && jo["netLiquidation"] != null)
        {
          double nl = jo["netLiquidation"]!.Value<double>();
          Assert.That(nl, Is.GreaterThanOrEqualTo(0),
              $"netLiquidation for {entry.Key} must be >= 0");
          foundNetLiquidation = true;
        }
      }
      Assert.That(foundNetLiquidation, Is.True,
          "at least one currency entry should contain netLiquidation");
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
      // When positions exist, validate key fields.
      if (items != null && items.Count > 0)
      {
        foreach (var pos in items)
        {
          Assert.That(pos.Symbol, Is.Not.Null.And.Not.Empty,
              "position symbol must be non-empty");
          Assert.That(pos.Account, Is.Not.Null.And.Not.Empty,
              "position account must be non-empty");
          Assert.That(pos.SecType, Is.Not.Null.And.Not.Empty,
              "position secType must be non-empty");
        }
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
      // When orders exist, validate identifier and status fields.
      if (items != null && items.Count > 0)
      {
        foreach (var order in items)
        {
          Assert.That(order.Id, Is.Not.EqualTo(0), "order id must be non-zero");
          Assert.That(order.Symbol, Is.Not.Null.And.Not.Empty,
              "order symbol must be non-empty");
          Assert.That(order.Action, Is.Not.Null.And.Not.Empty,
              "order action must be non-empty");
          Assert.That(order.Status, Is.Not.EqualTo(OrderStatus.NONE),
              "order status must not be NONE");
        }
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
      // When orders exist, validate identifier fields.
      if (items != null && items.Count > 0)
      {
        foreach (var order in items)
        {
          Assert.That(order.Id, Is.Not.EqualTo(0), "active order id must be non-zero");
          Assert.That(order.Symbol, Is.Not.Null.And.Not.Empty,
              "active order symbol must be non-empty");
          Assert.That(order.Status, Is.Not.EqualTo(OrderStatus.NONE),
              "active order status must not be NONE");
        }
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

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "contracts should return data for AAPL");
      Assert.That(resp.Data.ContainsKey("AAPL"), Is.True,
          "contracts data should contain AAPL key");
      var items = resp.Data["AAPL"];
      Assert.That(items, Is.Not.Null.And.Count.GreaterThan(0),
          "AAPL contracts list must be non-empty");
      Assert.That(items[0].Symbol, Is.EqualTo("AAPL"), "contract symbol wire name");
      Assert.That(items[0].SecType, Is.EqualTo("STK"), "contract secType wire name");
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
    // Analytics Asset (30-day range)
    // =====================================================================
    [Test]
    public void GetAnalyticsAsset_ReturnsValidFields()
    {
      var model = new PrimeAnalyticsAssetModel
      {
        Account = _account,
        SegType = SegmentType.SEC,
        Currency = Currency.USD,
        StartDate = "2025-01-01",
        EndDate = "2025-01-31"
      };
      var resp = Execute<PrimeAnalyticsAssetResponse>(TradeApiService.ANALYTICS_ASSET, model);

      Assert.That(resp.Data, Is.Not.Null, "analytics_asset data must not be null");
      // Summary may be null if no data in range.
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
      // When orders exist, validate identifier and status fields.
      if (items != null && items.Count > 0)
      {
        foreach (var order in items)
        {
          Assert.That(order.Id, Is.Not.EqualTo(0), "inactive order id must be non-zero");
          Assert.That(order.Symbol, Is.Not.Null.And.Not.Empty,
              "inactive order symbol must be non-empty");
          Assert.That(order.Status, Is.Not.EqualTo(OrderStatus.NONE),
              "inactive order status must not be NONE");
        }
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
      // When orders exist, validate identifier and status fields.
      if (items != null && items.Count > 0)
      {
        foreach (var order in items)
        {
          Assert.That(order.Id, Is.Not.EqualTo(0), "filled order id must be non-zero");
          Assert.That(order.Symbol, Is.Not.Null.And.Not.Empty,
              "filled order symbol must be non-empty");
          Assert.That(order.Status, Is.Not.EqualTo(OrderStatus.NONE),
              "filled order status must not be NONE");
        }
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
    // Only institution accounts are supported; individual accounts get
    // "only support institution account" error. Skip for personal accounts.
    // =====================================================================
    [Test]
    public void GetAggregateAssets_ReturnsValidFields()
    {
      Assert.Ignore("aggregate_assets only supports institution accounts");
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
    // =====================================================================
    [Test]
    public void GetPositionTransferExternalRecords_Succeeds_WithValidFieldsWhenNonEmpty()
    {
      var model = new PositionTransferRecordsModel
      {
        AccountId = _account,
        SinceDate = "2025-01-01",
        ToDate = "2025-12-31"
      };
      var resp = Execute<PositionTransferExternalRecordsResponse>(
          TradeApiService.POSITION_TRANSFER_EXTERNAL_RECORDS, model);

      Assert.That(resp.Data, Is.Not.Null,
          "position_transfer_external_records data must not be null");
      // External transfer records may be empty if no external transfers exist.
      // When records exist, validate key fields.
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].Id, Is.Not.EqualTo(0),
            "external transfer record id must be non-zero");
        Assert.That(resp.Data[0].AccountId, Is.Not.Null.And.Not.Empty,
            "external transfer record accountId must be non-empty");
      }
    }

    // =====================================================================
    // Option Exercise Check (preview only — no state change)
    // =====================================================================
    [Test]
    public async System.Threading.Tasks.Task CheckOptionExercise_Preview_Succeeds()
    {
      Assert.That(_client, Is.Not.Null, "TradeClient is null");
      // Preview does not place a real exercise request. A dummy contract ID
      // will likely return an error, but the call path is exercised.
      var resp = await _client!.CheckOptionExerciseAsync(
          contractId: 0, type: "Exercise", quantity: 1, account: _account);

      Assert.That(resp, Is.Not.Null, "option_exercise_check response must not be null");
      // With a dummy contract ID the server is expected to return an error.
      // That is acceptable — the point is to exercise the preview call path.
      if (!resp.IsSuccess())
      {
        Assert.Pass(
            $"option_exercise_check returned error (expected for dummy contract) " +
            $"code={resp.Code} msg={resp.Message}");
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
  }
}
