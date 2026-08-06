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
  }
}
