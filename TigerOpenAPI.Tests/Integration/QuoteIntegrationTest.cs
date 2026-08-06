using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Tests.Integration
{
  /// <summary>
  /// Integration tests for quote read-only APIs against the live gateway.
  /// Credentials come from env vars (see <see cref="IntegTestConfig"/>).
  /// Run with:  dotnet test --filter Category=Integration
  ///
  /// Test philosophy: every assertion covers a concrete field from the real
  /// response. Non-null checks alone are insufficient — wire-name bugs
  /// produce null silently.
  /// </summary>
  [TestFixture]
  [Category("Integration")]
  public class QuoteIntegrationTest
  {
    private QuoteClient? _client;

    private static readonly Regex TimePattern = new Regex(@"^\d{2}:\d{2}");

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      IntegTestConfig.EnsureCredentials();
      _client = IntegTestConfig.QuoteClient;
    }

    // ---- helper ----
    private T Execute<T>(string method, ApiModel? model = null) where T : TigerResponse
    {
      Assert.That(_client, Is.Not.Null,
          "QuoteClient is null — credentials should have been checked in OneTimeSetUp");
      var req = new TigerRequest<T>
      {
        ApiMethodName = method,
        ModelValue = model ?? new ApiModel()
      };
      var resp = _client!.Execute(req);
      Assert.That(resp, Is.Not.Null, $"{method} response must not be null");
      Assert.That(resp!.IsSuccess(), Is.True,
          $"{method} returned error code={resp.Code} msg={resp.Message}");
      return resp;
    }

    // =====================================================================
    // Market State
    // =====================================================================
    [Test]
    public void GetMarketState_US_ReturnsValidFields()
    {
      var model = new QuoteMarketModel { Market = Market.US };
      var resp = Execute<MarketStateResponse>(QuoteApiService.MARKET_STATE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "MarketState data must be non-empty");
      var state = resp.Data[0];

      Assert.That(state.Market, Is.Not.Null.And.Not.Empty, "market wire name");
      Assert.That(state.MarketStatus, Is.Not.Null.And.Not.Empty, "marketStatus wire name");
      Assert.That(state.Status, Is.Not.Null.And.Not.Empty, "status wire name");
      Assert.That(state.OpenTime, Does.Match(TimePattern),
          "openTime must match HH:mm pattern");
    }

    // =====================================================================
    // Brief (real-time quote for AAPL)
    // =====================================================================
    [Test]
    public void GetBrief_AAPL_ReturnsPriceFields()
    {
      var model = new QuoteSymbolModel
      {
        Symbols = new List<string> { "AAPL" }
      };
      var resp = Execute<QuoteRealTimeQuoteResponse>(QuoteApiService.BRIEF, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.EqualTo(1),
          "Brief should return exactly 1 item for AAPL");
      var q = resp.Data[0];

      Assert.That(q.Symbol, Is.EqualTo("AAPL"), "symbol wire name");
      Assert.That(q.LatestPrice, Is.GreaterThan(0), "latestPrice wire name");
      Assert.That(q.LatestTime, Is.GreaterThan(1577836800000L),
          "latestTime must be a valid epoch millis (after 2020-01-01)");
      Assert.That(q.High, Is.GreaterThanOrEqualTo(q.Low),
          "high must be >= low");
    }

    // =====================================================================
    // Kline (daily candles for AAPL)
    // =====================================================================
    [Test]
    public void GetKline_AAPL_Daily_ReturnsValidCandles()
    {
      var model = new QuoteKlineModel
      {
        Symbols = new List<string> { "AAPL" },
        Period = KLineType.day.Value,
        Limit = 5
      };
      var resp = Execute<QuoteKlineResponse>(QuoteApiService.KLINE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.EqualTo(1),
          "Kline should return 1 symbol entry");
      var kline = resp.Data[0];

      Assert.That(kline.Symbol, Is.EqualTo("AAPL"), "kline symbol wire name");
      Assert.That(kline.Items, Is.Not.Null.And.Count.GreaterThan(0),
          "kline items must be non-empty");

      var candle = kline.Items[0];
      Assert.That(candle.Time, Is.GreaterThan(1577836800000L),
          "kline item time must be non-zero epoch millis");
      Assert.That(candle.Close, Is.GreaterThan(0), "kline close must be > 0");
      Assert.That(candle.High, Is.GreaterThanOrEqualTo(candle.Low),
          "kline high must be >= low");
    }

    // =====================================================================
    // Option Expiration (AAPL)
    // =====================================================================
    [Test]
    public void GetOptionExpiration_AAPL_ReturnsDatesAndTimestamps()
    {
      var model = new OptionExpirationModel
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US
      };
      var resp = Execute<OptionExpirationResponse>(QuoteApiService.OPTION_EXPIRATION, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "option expiration should return data for AAPL");
      var exp = resp.Data[0];

      Assert.That(exp.Symbol, Is.EqualTo("AAPL"), "expiration symbol wire name");
      Assert.That(exp.Dates, Is.Not.Null.And.Count.GreaterThan(0),
          "expiry dates must be non-empty");
      Assert.That(exp.Timestamps, Is.Not.Null.And.Count.GreaterThan(0),
          "expiry timestamps must be non-empty");
      Assert.That(exp.Dates.Count, Is.EqualTo(exp.Timestamps.Count),
          "dates and timestamps count must match");
    }

    // =====================================================================
    // Option Chain (AAPL, nearest expiry)
    // =====================================================================
    [Test]
    public void GetOptionChain_AAPL_ReturnsCallPutLegs()
    {
      // 1. Get expiry timestamps first
      var expModel = new OptionExpirationModel
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US
      };
      var expResp = Execute<OptionExpirationResponse>(QuoteApiService.OPTION_EXPIRATION, expModel);

      Assert.That(expResp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "option expiration should return data for AAPL");
      Assert.That(expResp.Data[0].Timestamps, Is.Not.Null.And.Count.GreaterThan(0),
          "need at least 1 expiry timestamp for chain query");
      long expiry = expResp.Data[0].Timestamps[0];

      // 2. Query option chain for that expiry
      var chainModel = new OptionChainV3Model
      {
        Market = Market.US,
        OptionBasic = new List<OptionChainModel>
        {
          new OptionChainModel { Symbol = "AAPL", Expiry = expiry }
        }
      };
      var resp = Execute<OptionChainResponse>(QuoteApiService.OPTION_CHAIN, chainModel);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "option chain should return data");
      var chain = resp.Data[0];

      Assert.That(chain.Symbol, Is.EqualTo("AAPL"), "chain symbol wire name");
      Assert.That(chain.Expiry, Is.GreaterThan(0), "chain expiry must be non-zero");
      Assert.That(chain.Items, Is.Not.Null.And.Count.GreaterThan(0),
          "chain items must be non-empty");

      // At least one row should have a call or put leg
      var row = chain.Items[0];
      Assert.That(row.Call != null || row.Put != null,
          "chain row must have at least one leg (call or put)");

      if (row.Call != null)
      {
        Assert.That(row.Call.Identifier, Is.Not.Null.And.Not.Empty,
            "call leg identifier must be non-empty");
        Assert.That(row.Call.Strike, Is.Not.Null.And.Not.Empty,
            "call leg strike must be non-empty");
      }
      if (row.Put != null)
      {
        Assert.That(row.Put.Identifier, Is.Not.Null.And.Not.Empty,
            "put leg identifier must be non-empty");
        Assert.That(row.Put.Strike, Is.Not.Null.And.Not.Empty,
            "put leg strike must be non-empty");
      }
    }

    // =====================================================================
    // Corporate Action — Dividend (AAPL, 2-year range)
    // =====================================================================
    [Test]
    public void GetCorporateAction_AAPL_Dividend_ReturnsValidFields()
    {
      long begin = DateUtil.ConvertTimestamp("2024-01-01", CustomTimeZone.NY_ZONE);
      long end = DateUtil.ConvertTimestamp("2025-12-31", CustomTimeZone.NY_ZONE);

      var model = new CorporateActionModel
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US,
        ActionType = CorporateActionType.DIVIDEND,
        BeginDate = begin,
        EndDate = end
      };
      var resp = Execute<CorporateDividendResponse>(QuoteApiService.CORPORATE_ACTION, model);

      Assert.That(resp.Data, Is.Not.Null, "corporate action data dict must not be null");
      Assert.That(resp.Data.ContainsKey("AAPL"), Is.True,
          "corporate action data should contain AAPL key");

      var dividends = resp.Data["AAPL"];
      Assert.That(dividends, Is.Not.Null.And.Count.GreaterThan(0),
          "AAPL should have at least 1 dividend record in a 2-year range");

      var div = dividends[0];
      Assert.That(div.Symbol, Is.EqualTo("AAPL"), "dividend symbol wire name");
      Assert.That(div.ActionType, Is.EqualTo(CorporateActionType.DIVIDEND),
          "actionType wire name");
      Assert.That(div.ExecuteDate, Is.GreaterThan(DateTime.MinValue),
          "executeDate must be populated");
    }

    // =====================================================================
    // Capital Flow (AAPL, daily)
    // =====================================================================
    [Test]
    public void GetCapitalFlow_AAPL_Daily_ReturnsValidPoints()
    {
      var model = new QuoteCapitalFlowModel
      {
        Symbol = "AAPL",
        Market = Market.US,
        Period = CapitalPeriod.day.Value
      };
      var resp = Execute<QuoteCapitalFlowResponse>(QuoteApiService.CAPITAL_FLOW, model);

      Assert.That(resp.Data, Is.Not.Null, "capital flow data must not be null");
      var flow = resp.Data;

      Assert.That(flow.Symbol, Is.EqualTo("AAPL"), "capital flow symbol wire name");
      Assert.That(flow.Period, Is.Not.Null.And.Not.Empty,
          "capital flow period must be non-empty");

      // Capital flow items may be empty outside trading hours; when present,
      // validate the timestamp field.
      if (flow.Items != null && flow.Items.Count > 0)
      {
        var point = flow.Items[0];
        Assert.That(point.Timestamp, Is.Not.EqualTo(0),
            "capital flow point timestamp must be non-zero");
      }
    }
  }
}
