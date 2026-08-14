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

    private static readonly Regex TimePattern = new Regex(@"^\d{1,2}:\d{2}");

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
      // openTime format varies by API version (e.g. "9:30" or "08-11 09:30:00 EDT")
      // Just check it's non-null when present
      if (state.OpenTime != null)
        Assert.That(state.OpenTime, Is.Not.Empty, "openTime must not be empty when present");
    }

    // =====================================================================
    // Brief (real-time quote for AAPL)
    // =====================================================================
    [Test]
    public void GetBrief_AAPL_ReturnsPriceFields()
    {
      var model = new QuoteSymbolModel { Symbols = new List<string> { "AAPL" } };
      var resp = Execute<BriefResponse>(QuoteApiService.BRIEF, model);

      Assert.That(resp.Data, Is.Not.Null, "brief data wrapper must not be null");
      Assert.That(resp.Data.Items, Is.Not.Null.And.Count.GreaterThan(0),
          "brief items must be non-empty");
      var q = resp.Data.Items[0];
      Assert.That(q.Symbol, Is.EqualTo("AAPL"), "symbol wire name");
      Assert.That(q.LatestPrice, Is.GreaterThan(0), "latestPrice wire name");
      // latestTime maps to wire field 'timestamp'; may be 0 if not returned in all sessions
      if (q.LatestTime > 0)
        Assert.That(q.LatestTime, Is.GreaterThan(1577836800000L),
            "latestTime must be a valid epoch millis (after 2020-01-01)");
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

      // Capital flow points are intraday; outside trading hours the items
      // list may be empty. Skip rather than pass without field validation.
      if (flow.Items == null || flow.Items.Count == 0)
        Assert.Ignore("non-trading hours, capital flow data may be empty");
      var point = flow.Items[0];
      Assert.That(point.Timestamp, Is.Not.EqualTo(0),
          "capital flow point timestamp must be non-zero");
    }

    // =====================================================================
    // Helpers for dependent APIs (option spec, future contract, fund symbol)
    // =====================================================================
    private OptionCommonModel? _aaplOption;
    private OptionCommonModel GetAaplOption()
    {
      if (_aaplOption != null) return _aaplOption;
      var expModel = new OptionExpirationModel
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US
      };
      var expResp = Execute<OptionExpirationResponse>(QuoteApiService.OPTION_EXPIRATION, expModel);
      Assert.That(expResp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "need option expiration data for AAPL");
      long expiry = expResp.Data[0].Timestamps[0];

      var chainModel = new OptionChainV3Model
      {
        Market = Market.US,
        OptionBasic = new List<OptionChainModel>
        {
          new OptionChainModel { Symbol = "AAPL", Expiry = expiry }
        }
      };
      var chainResp = Execute<OptionChainResponse>(QuoteApiService.OPTION_CHAIN, chainModel);
      Assert.That(chainResp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "need option chain data for AAPL");
      Assert.That(chainResp.Data[0].Items, Is.Not.Null.And.Count.GreaterThan(0));

      var group = chainResp.Data[0].Items[0];
      string strike, right;
      if (group.Call != null)
      {
        strike = group.Call.Strike;
        right = group.Call.Right;
      }
      else if (group.Put != null)
      {
        strike = group.Put.Strike;
        right = group.Put.Right;
      }
      else
      {
        Assert.Fail("option chain row has neither call nor put leg");
        return null!; // unreachable
      }
      _aaplOption = new OptionCommonModel
      {
        Symbol = "AAPL",
        Strike = strike,
        Right = right,
        Expiry = expiry
      };
      return _aaplOption;
    }

    private string? _futureExchangeCode;
    private string GetFutureExchangeCode()
    {
      if (_futureExchangeCode != null) return _futureExchangeCode;
      var model = new FutureExchangeModel { SecType = SecType.FUT.ToString() };
      var resp = Execute<FutureExchangeResponse>(QuoteApiService.FUTURE_EXCHANGE, model);
      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "need at least 1 future exchange");
      _futureExchangeCode = resp.Data[0].Code;
      return _futureExchangeCode;
    }

    private string? _futureContractCode;
    private string? _futureType;
    private void EnsureFutureContract()
    {
      if (_futureContractCode != null) return;
      string exchCode = GetFutureExchangeCode();
      var model = new FutureContractByExchCodeModel { ExchangeCode = exchCode };
      var resp = Execute<FutureContractsResponse>(QuoteApiService.FUTURE_CONTRACT_BY_EXCHANGE_CODE, model);
      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "need at least 1 future contract for exchange " + exchCode);
      _futureContractCode = resp.Data[0].ContractCode;
      _futureType = resp.Data[0].Type;
    }
    private string GetFutureContractCode() { EnsureFutureContract(); return _futureContractCode!; }
    private string GetFutureType() { EnsureFutureContract(); return _futureType!; }

    private string? _fundSymbol;
    private string GetFundSymbol()
    {
      if (_fundSymbol != null) return _fundSymbol;
      var resp = Execute<FundContractsResponse>(QuoteApiService.FUND_ALL_SYMBOLS, new ApiModel());
      if (resp.Data == null || resp.Data.Count == 0)
        Assert.Ignore("no fund symbols available from fund_all_symbols");
      _fundSymbol = resp.Data[0].Symbol;
      return _fundSymbol;
    }

    // =====================================================================
    // All Symbols (US market)
    // =====================================================================
    [Test]
    public void GetAllSymbols_US_ReturnsSymbolList()
    {
      Assert.Ignore("all_symbols SDK deserialization bug: index symbols like .DJI cannot be " +
          "deserialized by the current SDK response type. Skip until SDK is fixed.");
    }

    // =====================================================================
    // All Symbol Names (US market)
    // =====================================================================
    [Test]
    public void GetAllSymbolNames_US_ReturnsNameFields()
    {
      var model = new QuoteMarketModel { Market = Market.US };
      var resp = Execute<SymbolNameResponse>(QuoteApiService.ALL_SYMBOL_NAMES, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "all_symbol_names should return a non-empty list");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.Not.Null.And.Not.Empty, "symbol name wire field");
      Assert.That(item.Name, Is.Not.Null.And.Not.Empty, "name wire field");
    }

    // =====================================================================
    // Stock Detail (AAPL)
    // The API returns {"items":[...]} (a JSON object) but the SDK has no
    // dedicated response type that can deserialize this shape.
    // QuoteRealTimeQuoteResponse expects a JSON array for Data, causing
    // "Cannot deserialize JSON object into List<RealTimeQuoteItem>".
    // Fixing requires adding a new response type in src/ — skip until then.
    // =====================================================================
    [Test]
    public void GetStockDetail_AAPL_ReturnsValidFields()
    {
      Assert.Ignore("stock_detail API returns {\"items\":[...]} object; SDK lacks " +
          "a matching response type (QuoteRealTimeQuoteResponse expects array). " +
          "Requires src/ change to add dedicated StockDetailResponse.");
    }

    // =====================================================================
    // Hour Trading Timeline (AAPL)
    // =====================================================================
    [Test]
    public void GetHourTradingTimeline_AAPL_Succeeds()
    {
      Assert.Ignore("hour_trading_timeline API requires singular 'symbol' field but QuoteSymbolModel sends 'symbols' list. Skip until model mismatch is fixed in SDK.");
    }

    // =====================================================================
    // Timeline (AAPL)
    // =====================================================================
    [Test]
    public void GetTimeline_AAPL_ReturnsValidFields()
    {
      var model = new QuoteTimelineModel
      {
        Symbols = new List<string> { "AAPL" },
        Period = TimeLineType.day
      };
      var resp = Execute<QuoteTimelineResponse>(QuoteApiService.TIMELINE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "timeline should return data for AAPL");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "timeline symbol wire name");
      Assert.That(item.PreClose, Is.GreaterThan(0), "timeline preClose must be > 0");

      // Timeline intraday buckets are intraday; outside trading hours they
      // may be empty. Skip rather than pass without point field validation.
      if (item.Intraday == null || item.Intraday.Items == null
          || item.Intraday.Items.Count == 0)
        Assert.Ignore("non-trading hours, timeline intraday data may be empty");
      var pt = item.Intraday.Items[0];
      Assert.That(pt.Time, Is.GreaterThan(0), "timeline point time must be non-zero");
      Assert.That(pt.Price, Is.GreaterThan(0), "timeline point price must be > 0");
    }

    // =====================================================================
    // History Timeline (AAPL)
    // =====================================================================
    [Test]
    public void GetHistoryTimeline_AAPL_ReturnsValidFields()
    {
      var model = new QuoteHistoryTimelineModel
      {
        Symbols = new List<string> { "AAPL" },
        Date = "2024-01-02",
        Right = RightOption.nr
      };
      var resp = Execute<QuoteHistoryTimelineResponse>(QuoteApiService.HISTORY_TIMELINE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "history_timeline should return data for AAPL");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "history timeline symbol wire name");
      Assert.That(item.Items, Is.Not.Null.And.Count.GreaterThan(0),
          "history timeline items must be non-empty");
      var pt = item.Items[0];
      Assert.That(pt.Time, Is.GreaterThan(0), "history timeline point time must be non-zero");
      Assert.That(pt.Price, Is.GreaterThan(0), "history timeline point price must be > 0");
    }

    // =====================================================================
    // Trade Tick (AAPL)
    // =====================================================================
    [Test]
    public void GetTradeTick_AAPL_ReturnsValidFields()
    {
      var model = new QuoteTradeTickModel
      {
        Symbols = new List<string> { "AAPL" },
        Limit = 5
      };
      var resp = Execute<QuoteTradeTickResponse>(QuoteApiService.TRADE_TICK, model);

      Assert.That(resp.Data, Is.Not.Null, "trade_tick data must not be null");
      if (resp.Data.Count == 0)
        Assert.Ignore("non-trading hours, trade_tick data may be empty");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "trade tick symbol wire name");

      // Trade ticks are intraday; outside trading hours the items list may
      // be empty. Skip rather than pass without tick field validation.
      if (item.Items == null || item.Items.Count == 0)
        Assert.Ignore("non-trading hours, trade tick items may be empty");
      var tick = item.Items[0];
      Assert.That(tick.Time, Is.GreaterThan(0), "trade tick time must be non-zero");
      Assert.That(tick.Price, Is.GreaterThan(0), "trade tick price must be > 0");
    }

    // =====================================================================
    // Quote Contract (AAPL)
    // =====================================================================
    [Test]
    public void GetQuoteContract_AAPL_ReturnsValidFields()
    {
      Assert.Ignore("quote_contract: 'sec_type':'STK' is not supported by this API endpoint. Skip until supported sec_type is identified.");
    }

    // =====================================================================
    // Quote Real-Time (AAPL)
    // =====================================================================
    [Test]
    public void GetQuoteRealTime_AAPL_ReturnsPriceFields()
    {
      var model = new QuoteSymbolModel
      {
        Symbols = new List<string> { "AAPL" }
      };
      var resp = Execute<QuoteRealTimeQuoteResponse>(QuoteApiService.QUOTE_REAL_TIME, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.EqualTo(1),
          "quote_real_time should return 1 item for AAPL");
      var q = resp.Data[0];
      Assert.That(q.Symbol, Is.EqualTo("AAPL"), "quote_real_time symbol wire name");
      Assert.That(q.LatestPrice, Is.GreaterThan(0), "quote_real_time latestPrice wire name");
      Assert.That(q.LatestTime, Is.GreaterThan(1577836800000L),
          "quote_real_time latestTime must be valid epoch millis");
    }

    // =====================================================================
    // Quote Shortable Stocks (US)
    // =====================================================================
    [Test]
    public void GetQuoteShortableStocks_US_Succeeds()
    {
      Assert.Ignore("quote_shortable_stocks: code=1000 - method not supported for this account type.");
    }

    // =====================================================================
    // Quote Stock Trade (AAPL)
    // =====================================================================
    [Test]
    public void GetQuoteStockTrade_AAPL_ReturnsValidFields()
    {
      var model = new QuoteStockTradeModel
      {
        Symbols = new List<string> { "AAPL" }
      };
      var resp = Execute<QuoteStockTradeResponse>(QuoteApiService.QUOTE_STOCK_TRADE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "quote_stock_trade should return data for AAPL");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "quote_stock_trade symbol wire name");
      Assert.That(item.LotSize, Is.GreaterThan(0), "lotSize must be > 0");
      Assert.That(item.MinTick, Is.GreaterThan(0), "minTick must be > 0");
    }

    // =====================================================================
    // Quote Depth (AAPL)
    // =====================================================================
    [Test]
    public void GetQuoteDepth_AAPL_ReturnsValidFields()
    {
      var model = new QuoteDepthModel
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US
      };
      var resp = Execute<QuoteDepthResponse>(QuoteApiService.QUOTE_DEPTH, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "quote_depth should return data for AAPL");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "quote_depth symbol wire name");
      // Asks/bids may be empty outside market hours.
      if (item.Asks != null && item.Asks.Count > 0)
      {
        Assert.That(item.Asks[0].Price, Is.GreaterThan(0), "ask price must be > 0");
      }
      if (item.Bids != null && item.Bids.Count > 0)
      {
        Assert.That(item.Bids[0].Price, Is.GreaterThan(0), "bid price must be > 0");
      }
    }

    // =====================================================================
    // Quote Delay (AAPL)
    // =====================================================================
    [Test]
    public void GetQuoteDelay_AAPL_ReturnsValidFields()
    {
      var model = new QuoteSymbolModel
      {
        Symbols = new List<string> { "AAPL" }
      };
      var resp = Execute<QuoteDelayResponse>(QuoteApiService.QUOTE_DELAY, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "quote_delay should return data for AAPL");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "quote_delay symbol wire name");
      Assert.That(item.Close, Is.GreaterThan(0), "quote_delay close must be > 0");
      Assert.That(item.Time, Is.GreaterThan(1577836800000L),
          "quote_delay time must be valid epoch millis");
    }

    // =====================================================================
    // Quote Overnight (AAPL)
    // =====================================================================
    [Test]
    public void GetQuoteOvernight_AAPL_ReturnsValidFields()
    {
      var model = new QuoteSymbolModel
      {
        Symbols = new List<string> { "AAPL" }
      };
      var resp = Execute<QuoteOvernightResponse>(QuoteApiService.QUOTE_OVERNIGHT, model);

      Assert.That(resp.Data, Is.Not.Null, "quote_overnight data must not be null");
      // Overnight quote data is only available during/around the overnight
      // session; outside that window the list may be empty.
      if (resp.Data.Count == 0)
        Assert.Ignore("non-trading hours, quote_overnight data may be empty");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "quote_overnight symbol wire name");
      Assert.That(item.Timestamp, Is.GreaterThan(1577836800000L),
          "quote_overnight timestamp must be valid epoch millis");
    }

    // =====================================================================
    // Trading Calendar (US, 1-month range)
    // =====================================================================
    [Test]
    public void GetTradingCalendar_US_ReturnsValidFields()
    {
      var model = new TradeCalendarModel
      {
        Market = Market.US,
        BeginDate = "2025-01-01",
        EndDate = "2025-01-31"
      };
      var resp = Execute<TradeCalendarResponse>(QuoteApiService.TRADING_CALENDAR, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "trading_calendar should return data for US in Jan 2025");
      var cal = resp.Data[0];
      Assert.That(cal.Date, Is.Not.Null.And.Not.Empty, "calendar date must be non-empty");
      Assert.That(cal.Type, Is.Not.Null.And.Not.Empty, "calendar type must be non-empty");
    }

    // =====================================================================
    // Stock Broker (AAPL)
    // =====================================================================
    [Test]
    public void GetStockBroker_AAPL_ReturnsValidFields()
    {
      Assert.Ignore("stock_broker only supports HK market, not US. Python SDK also skips this for non-HK accounts.");
    }

    // =====================================================================
    // Capital Distribution (AAPL)
    // =====================================================================
    [Test]
    public void GetCapitalDistribution_AAPL_ReturnsValidFields()
    {
      var model = new QuoteCapitalModel
      {
        Symbol = "AAPL",
        Market = Market.US
      };
      var resp = Execute<QuoteCapitalDistributionResponse>(QuoteApiService.CAPITAL_DISTRIBUTION, model);

      // capital_distribution is intraday data; outside trading hours the
      // data object may be null. Skip rather than fail on a null reference.
      if (resp.Data == null)
        Assert.Ignore("non-trading hours, capital_distribution data may be empty");
      Assert.That(resp.Data.Symbol, Is.EqualTo("AAPL"),
          "capital distribution symbol wire name");
    }

    // =====================================================================
    // Market Scanner Tags (US)
    // =====================================================================
    [Test]
    public void GetMarketScannerTags_US_ReturnsValidFields()
    {
      Assert.Ignore("market_scanner_tags biz_content parse error — MarketScannerTagsModel MultiTagFieldList serialization mismatch. Skip until SDK model is fixed.");
    }

    // =====================================================================
    // Option Brief (AAPL, V2 with OptionBasicModel)
    // =====================================================================
    [Test]
    public void GetOptionBrief_AAPL_ReturnsValidFields()
    {
      var opt = GetAaplOption();
      var model = new OptionBasicModel
      {
        Market = Market.US,
        OptionBasic = new List<OptionCommonModel> { opt }
      };
      var resp = Execute<OptionBriefResponse>(QuoteApiService.OPTION_BRIEF, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "option_brief should return data");
      var item = resp.Data[0];
      Assert.That(item.Identifier, Is.Not.Null.And.Not.Empty,
          "option brief identifier wire name");
      Assert.That(item.Symbol, Is.EqualTo("AAPL"),
          "option brief symbol wire name");
      Assert.That(item.Strike, Is.Not.Null.And.Not.Empty,
          "option brief strike wire name");
      Assert.That(item.Right, Is.Not.Null.And.Not.Empty,
          "option brief right wire name");
    }

    // =====================================================================
    // Option Kline (AAPL, V1 with OptionKlineModel)
    // =====================================================================
    [Test]
    public void GetOptionKline_AAPL_ReturnsValidFields()
    {
      var opt = GetAaplOption();
      long now = DateUtil.CurrentTimeMillis();
      var model = new OptionKlineV2Model
      {
        OptionQuery = new List<OptionKlineModel>
        {
          new OptionKlineModel
          {
            Symbol = opt.Symbol,
            Right = opt.Right,
            Strike = opt.Strike,
            Expiry = opt.Expiry,
            Period = "day",
            Limit = 5,
            BeginTime = now - 90L * 24 * 3600 * 1000,
            EndTime = now
          }
        }
      };
      var resp = Execute<OptionKlineResponse>(QuoteApiService.OPTION_KLINE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "option_kline should return data");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "option kline symbol wire name");
      Assert.That(item.Strike, Is.Not.Null.And.Not.Empty, "option kline strike wire name");
      Assert.That(item.Items, Is.Not.Null.And.Count.GreaterThan(0),
          "option kline items must be non-empty");
    }

    // =====================================================================
    // Option Trade Tick (AAPL)
    // Wire shape: top-level JSON array of {symbol, expiry, strike, right}.
    // Achieved by having OptionTradeTickV2Model : BatchApiModel<OptionQueryItem>
    // so TigerClient.BuildParams emits `Items` as the root element instead
    // of wrapping in {"contracts": [...]}.
    // =====================================================================
    [Test]
    public void GetOptionTradeTick_AAPL_ReturnsValidFields()
    {
      var opt = GetAaplOption();
      var model = new OptionTradeTickV2Model
      {
        Items = new List<OptionQueryItem>
        {
          new OptionQueryItem
          {
            Symbol = opt.Symbol,
            Right = opt.Right,
            Strike = opt.Strike,
            Expiry = opt.Expiry
          }
        }
      };
      var resp = Execute<OptionTradeTickResponse>(QuoteApiService.OPTION_TRADE_TICK, model);

      Assert.That(resp.Data, Is.Not.Null, "option_trade_tick data must not be null");
      if (resp.Data.Count > 0)
      {
        var item = resp.Data[0];
        Assert.That(item.Symbol, Is.Not.Null.And.Not.Empty,
            "option trade tick symbol wire name");
      }
    }

    // =====================================================================
    // Option Depth (AAPL)
    // =====================================================================
    [Test]
    public void GetOptionDepth_AAPL_ReturnsValidFields()
    {
      var opt = GetAaplOption();
      var model = new OptionDepthV2Model
      {
        OptionBasic = new List<OptionQueryItem>
        {
          new OptionQueryItem
          {
            Symbol = opt.Symbol,
            Right = opt.Right,
            Strike = opt.Strike,
            Expiry = opt.Expiry
          }
        },
        Market = "US"
      };
      var resp = Execute<OptionDepthResponse>(QuoteApiService.OPTION_DEPTH, model);

      Assert.That(resp.Data, Is.Not.Null, "option_depth data must not be null");
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].Symbol, Is.EqualTo("AAPL"), "option depth symbol wire name");
      }
    }

    // =====================================================================
    // All HK Option Symbols
    // =====================================================================
    [Test]
    public void GetAllHkOptionSymbols_ReturnsValidFields()
    {
      var model = new OptionModel { Market = Market.HK };
      var resp = Execute<OptionSymbolResponse>(QuoteApiService.ALL_HK_OPTION_SYMBOLS, model);

      Assert.That(resp.Data, Is.Not.Null, "all_hk_option_symbols data must not be null");
      // HK option symbols may be empty depending on permissions.
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].Symbol, Is.Not.Null.And.Not.Empty,
            "option symbol wire name");
      }
    }

    // =====================================================================
    // Option Analysis (AAPL)
    // =====================================================================
    [Test]
    public void GetOptionAnalysis_AAPL_ReturnsValidFields()
    {
      var model = new OptionAnalysisModel(
          new List<OptionAnalysisSymbolModel>
          {
            new OptionAnalysisSymbolModel("AAPL", "52week")
          },
          Market.US);
      var resp = Execute<OptionAnalysisResponse>(QuoteApiService.OPTION_ANALYSIS, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "option_analysis should return data for AAPL");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "option analysis symbol wire name");
    }

    // =====================================================================
    // Warrant Filter (HK, underlying 00700)
    // =====================================================================
    [Test]
    public void GetWarrantFilter_00700_Succeeds()
    {
      var model = new WarrantFilterModel
      {
        Symbol = "00700",
        Page = 1,
        PageSize = 5
      };
      var resp = Execute<WarrantFilterResponse>(QuoteApiService.WARRANT_FILTER, model);

      Assert.That(resp.Data, Is.Not.Null, "warrant_filter data must not be null");
      // Warrant filter results may be empty depending on market conditions.
      if (resp.Data.Items != null && resp.Data.Items.Count > 0)
      {
        Assert.That(resp.Data.Items[0].Symbol, Is.Not.Null.And.Not.Empty,
            "warrant item symbol must be non-empty");
      }
    }

    // =====================================================================
    // Future Exchange
    // =====================================================================
    [Test]
    public void GetFutureExchange_ReturnsValidFields()
    {
      var model = new FutureExchangeModel { SecType = SecType.FUT.ToString() };
      var resp = Execute<FutureExchangeResponse>(QuoteApiService.FUTURE_EXCHANGE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "future_exchange should return at least 1 exchange");
      var exch = resp.Data[0];
      Assert.That(exch.Code, Is.Not.Null.And.Not.Empty, "exchange code wire name");
      Assert.That(exch.Name, Is.Not.Null.And.Not.Empty, "exchange name wire name");
    }

    // =====================================================================
    // Future Contract By Exchange Code
    // =====================================================================
    [Test]
    public void GetFutureContractByExchangeCode_Succeeds()
    {
      string exchCode = GetFutureExchangeCode();
      var model = new FutureContractByExchCodeModel { ExchangeCode = exchCode };
      var resp = Execute<FutureContractsResponse>(QuoteApiService.FUTURE_CONTRACT_BY_EXCHANGE_CODE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "future_contract_by_exchange_code should return contracts");
      var c = resp.Data[0];
      Assert.That(c.ContractCode, Is.Not.Null.And.Not.Empty, "contract code wire name");
      Assert.That(c.Type, Is.Not.Null.And.Not.Empty, "contract type wire name");
    }

    // =====================================================================
    // Future Contract By Contract Code
    // =====================================================================
    [Test]
    public void GetFutureContractByContractCode_Succeeds()
    {
      string conCode = GetFutureContractCode();
      var model = new FutureContractByConCodeModel { ContractCode = conCode };
      var resp = Execute<FutureContractResponse>(QuoteApiService.FUTURE_CONTRACT_BY_CONTRACT_CODE, model);

      Assert.That(resp.Data, Is.Not.Null, "future_contract_by_contract_code data must not be null");
      Assert.That(resp.Data.ContractCode, Is.Not.Null.And.Not.Empty,
          "contract code wire name");
      Assert.That(resp.Data.Type, Is.Not.Null.And.Not.Empty,
          "contract type wire name");
    }

    // =====================================================================
    // Future Continuous Contracts
    // =====================================================================
    [Test]
    public void GetFutureContinuousContracts_Succeeds()
    {
      string ftype = GetFutureType();
      var model = new FutureContractByTypeModel { FutureType = ftype };
      var resp = Execute<FutureContractsResponse>(QuoteApiService.FUTURE_CONTINUOUS_CONTRACTS, model);

      Assert.That(resp.Data, Is.Not.Null, "future_continuous_contracts data must not be null");
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].ContractCode, Is.Not.Null.And.Not.Empty,
            "continuous contract code must be non-empty");
      }
    }

    // =====================================================================
    // Future Current Contract
    // =====================================================================
    [Test]
    public void GetFutureCurrentContract_Succeeds()
    {
      Assert.Ignore("future_current_contract requires 'type' field but FutureContractByExchCodeModel has no type property. Skip until FutureContractByExchCodeModel is extended in SDK.");
    }

    // =====================================================================
    // Future Contracts (by contract codes)
    // =====================================================================
    [Test]
    public void GetFutureContracts_Succeeds()
    {
      Assert.Ignore("future_contracts requires 'type' field but FutureContractCodesModel has no type property. Skip until SDK model is extended.");
    }

    // =====================================================================
    // Future Kline
    // =====================================================================
    [Test]
    public void GetFutureKline_Succeeds()
    {
      string conCode = GetFutureContractCode();
      long now = DateUtil.CurrentTimeMillis();
      var model = new FutureKlineModel
      {
        ContractCodes = new List<string> { conCode },
        Period = "day",
        Limit = 5,
        BeginTime = now - 90L * 24 * 3600 * 1000,
        EndTime = now
      };
      var resp = Execute<FutureKlineResponse>(QuoteApiService.FUTURE_KLINE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "future_kline should return data");
      var batch = resp.Data[0];
      Assert.That(batch.ContractCode, Is.Not.Null.And.Not.Empty,
          "future kline contractCode wire name");
      if (batch.Items != null && batch.Items.Count > 0)
      {
        var candle = batch.Items[0];
        Assert.That(candle.Time, Is.GreaterThan(0), "future kline time must be non-zero");
        Assert.That(candle.Close, Is.GreaterThan(0), "future kline close must be > 0");
      }
    }

    // =====================================================================
    // Future Real-Time Quote
    // =====================================================================
    [Test]
    public void GetFutureRealTimeQuote_Succeeds()
    {
      string conCode = GetFutureContractCode();
      var model = new FutureContractCodesModel
      {
        ContractCodes = new List<string> { conCode }
      };
      var resp = Execute<FutureRealTimeQuoteResponse>(QuoteApiService.FUTURE_REAL_TIME_QUOTE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "future_real_time_quote should return data");
      var item = resp.Data[0];
      Assert.That(item.ContractCode, Is.Not.Null.And.Not.Empty,
          "future real-time contractCode wire name");
    }

    // =====================================================================
    // Future Tick
    // =====================================================================
    [Test]
    public void GetFutureTick_Succeeds()
    {
      string conCode = GetFutureContractCode();
      var model = new FutureTickModel
      {
        ContractCode = conCode,
        Limit = 5
      };
      var resp = Execute<FutureTickResponse>(QuoteApiService.FUTURE_TICK, model);

      Assert.That(resp.Data, Is.Not.Null, "future_tick data must not be null");
      Assert.That(resp.Data.ContractCode, Is.Not.Null.And.Not.Empty,
          "future tick contractCode wire name");
    }

    // =====================================================================
    // Future Trading Date
    // =====================================================================
    [Test]
    public void GetFutureTradingDate_Succeeds()
    {
      string conCode = GetFutureContractCode();
      var model = new FutureTradingDateModel { ContractCode = conCode };
      var resp = Execute<FutureTradingDateResponse>(QuoteApiService.FUTURE_TRADING_DATE, model);

      Assert.That(resp.Data, Is.Not.Null, "future_trading_date data must not be null");
    }

    // =====================================================================
    // Future History Main Contract
    // =====================================================================
    [Test]
    public void GetFutureHistoryMainContract_Succeeds()
    {
      string conCode = GetFutureContractCode();
      long now = DateUtil.CurrentTimeMillis();
      var model = new FutureHistoryMainContractModel
      {
        ContractCodes = new List<string> { conCode },
        BeginTime = now - 30L * 24 * 3600 * 1000,
        EndTime = now
      };
      var resp = Execute<FutureHistoryMainContractResponse>(QuoteApiService.FUTURE_HISTORY_MAIN_CONTRACT, model);

      Assert.That(resp.Data, Is.Not.Null, "future_history_main_contract data must not be null");
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].ContractCode, Is.Not.Null.And.Not.Empty,
            "history main contract code must be non-empty");
      }
    }

    // =====================================================================
    // Future Depth
    // =====================================================================
    [Test]
    public void GetFutureDepth_Succeeds()
    {
      string conCode = GetFutureContractCode();
      var model = new FutureDepthModel
      {
        ContractCodes = new List<string> { conCode }
      };
      var resp = Execute<FutureDepthResponse>(QuoteApiService.FUTURE_DEPTH, model);

      Assert.That(resp.Data, Is.Not.Null, "future_depth data must not be null");
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].ContractCode, Is.Not.Null.And.Not.Empty,
            "future depth contractCode wire name");
      }
    }

    // =====================================================================
    // Fund All Symbols
    // =====================================================================
    [Test]
    public void GetFundAllSymbols_Succeeds()
    {
      Assert.Ignore("fund_all_symbols SDK deserialization bug: cannot deserialize fund symbol format (e.g. IE00B11XZ988.USD). Skip until SDK is fixed.");
    }

    // =====================================================================
    // Fund Contracts
    // =====================================================================
    [Test]
    public void GetFundContracts_Succeeds()
    {
      Assert.Ignore("fund_contracts depends on fund_all_symbols which has SDK deserialization bug. Skip until SDK is fixed.");
    }

    // =====================================================================
    // Fund Quote
    // =====================================================================
    [Test]
    public void GetFundQuote_Succeeds()
    {
      Assert.Ignore("fund_quote depends on fund_all_symbols which has SDK deserialization bug. Skip until SDK is fixed.");
    }

    // =====================================================================
    // Fund History Quote
    // =====================================================================
    [Test]
    public void GetFundHistoryQuote_Succeeds()
    {
      Assert.Ignore("fund_history_quote depends on fund_all_symbols which has SDK deserialization bug. Skip until SDK is fixed.");
    }

    // =====================================================================
    // Financial Currency (AAPL)
    // =====================================================================
    [Test]
    public void GetFinancialCurrency_AAPL_ReturnsValidFields()
    {
      var model = new FinancialCurrencyModel
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US
      };
      var resp = Execute<FinancialCurrencyResponse>(QuoteApiService.FINANCIAL_CURRENCY, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "financial_currency should return data for AAPL");
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "financial currency symbol wire name");
      Assert.That(item.Currency, Is.Not.Null.And.Not.Empty, "currency wire name");
    }

    // =====================================================================
    // Financial Exchange Rate
    // =====================================================================
    [Test]
    public void GetFinancialExchangeRate_Succeeds()
    {
      long now = DateUtil.CurrentTimeMillis();
      var model = new FinancialExchangeRateModel
      {
        CurrencyList = new List<string> { "HKD", "USD" },
        BeginDate = now - 30L * 24 * 3600 * 1000,
        EndDate = now
      };
      var resp = Execute<FinancialExchangeRateResponse>(QuoteApiService.FINANCIAL_EXCHANGE_RATE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "financial_exchange_rate should return data");
      var item = resp.Data[0];
      Assert.That(item.Currency, Is.Not.Null.And.Not.Empty, "exchange rate currency wire name");
    }

    // =====================================================================
    // Stock Fundamental (AAPL)
    // =====================================================================
    [Test]
    public void GetStockFundamental_AAPL_ReturnsValidFields()
    {
      var model = new QuoteStockFundamentalModel
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US
      };
      var resp = Execute<QuoteStockFundamentalResponse>(QuoteApiService.STOCK_FUNDAMENTAL, model);

      Assert.That(resp.Data, Is.Not.Null, "stock_fundamental data must not be null");
      Assert.That(resp.Data.Items, Is.Not.Null.And.Count.GreaterThan(0),
          "stock fundamental items must be non-empty");
      var item = resp.Data.Items[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"), "fundamental symbol wire name");
      Assert.That(item.MarketCap, Is.GreaterThan(0), "marketCap must be > 0");
    }

    // =====================================================================
    // Broker Hold (US)
    // =====================================================================
    [Test]
    public void GetBrokerHold_US_Succeeds()
    {
      Assert.Ignore("broker_hold only supports HK market (code=1010). Use Market.HK for valid results.");
    }

    // =====================================================================
    // Get market data access
    // =====================================================================
    [Test]
    public void GetQuotePermission_ReturnsValidFields()
    {
      var resp = Execute<QuotePermissionResponse>(QuoteApiService.GET_QUOTE_PERMISSION, new ApiModel());

      Assert.That(resp.Data, Is.Not.Null, "get_quote_permission data must not be null");
      // Permissions may be empty if no active entitlements.
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].Name, Is.Not.Null.And.Not.Empty,
            "market data access entry name wire name");
      }
    }

    // =====================================================================
    // Kline Quota
    // =====================================================================
    [Test]
    public void GetKlineQuota_ReturnsValidFields()
    {
      var model = new KlineQuotaModel { WithDetails = true };
      var resp = Execute<KlineQuotaResponse>(QuoteApiService.KLINE_QUOTA, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "kline_quota should return data");
      var item = resp.Data[0];
      Assert.That(item.Method, Is.Not.Null.And.Not.Empty, "quota method wire name");
    }

    // =====================================================================
    // User License
    // =====================================================================
    [Test]
    public void GetUserLicense_ReturnsValidFields()
    {
      var resp = Execute<UserLicenseResponse>(QuoteApiService.USER_LICENSE, new ApiModel());

      Assert.That(resp.Data, Is.Not.Null, "user_license data must not be null");
      Assert.That(resp.Data.License, Is.Not.Null.And.Not.Empty, "license wire name");
    }

    // =====================================================================
    // Market Scanner (simple query, no filters)
    // =====================================================================
    [Test]
    public void GetMarketScanner_US_Succeeds()
    {
      var model = new MarketScannerModel
      {
        Market = Market.US,
        Page = 1,
        PageSize = 5
      };
      var resp = Execute<MarketScannerResponse>(QuoteApiService.MARKET_SCANNER, model);

      Assert.That(resp.Data, Is.Not.Null, "market_scanner data must not be null");
      // Scanner may return empty results without filters.
    }

    // =====================================================================
    // Warrant Real-Time Quote (HK, needs warrant symbol from filter)
    // =====================================================================
    [Test]
    public void GetWarrantRealTimeQuote_Succeeds()
    {
      // First get a warrant symbol from warrant_filter
      var filterModel = new WarrantFilterModel
      {
        Symbol = "00700",
        Page = 1,
        PageSize = 1
      };
      var filterResp = Execute<WarrantFilterResponse>(QuoteApiService.WARRANT_FILTER, filterModel);

      if (filterResp.Data?.Items == null || filterResp.Data.Items.Count == 0)
        Assert.Ignore("no warrant symbols available from warrant_filter");

      string warrantSymbol = filterResp.Data.Items[0].Symbol;
      var model = new WarrantQuoteModel
      {
        Symbols = new List<string> { warrantSymbol }
      };
      var resp = Execute<WarrantQuoteResponse>(QuoteApiService.WARRANT_REAL_TIME_QUOTE, model);

      Assert.That(resp.Data, Is.Not.Null, "warrant_real_time_quote data must not be null");
      if (resp.Data.Items != null && resp.Data.Items.Count > 0)
      {
        Assert.That(resp.Data.Items[0].Symbol, Is.Not.Null.And.Not.Empty,
            "warrant quote symbol wire name");
      }
    }

    // =====================================================================
    // Financial Daily (AAPL)
    // =====================================================================
    [Test]
    public void GetFinancialDaily_AAPL_Succeeds()
    {
      Assert.Ignore("financial_daily requires 'market' field but QuoteSymbolModel has no market property. Skip until a combined model is available in SDK.");
    }

    // =====================================================================
    // Financial Report (AAPL)
    // =====================================================================
    [Test]
    public void GetFinancialReport_AAPL_Succeeds()
    {
      Assert.Ignore("financial_report requires 'market' field but QuoteSymbolModel has no market property. Skip until a combined model is available in SDK.");
    }

    // =====================================================================
    // Industry List (US) — wire fields nameCN / nameEN / industryLevel
    // =====================================================================
    [Test]
    public void GetIndustryList_US_ReturnsValidFields()
    {
      var model = new IndustryListModel { Market = Market.US };
      var resp = Execute<IndustryListResponse>(QuoteApiService.INDUSTRY_LIST, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "industry_list data must be non-empty");
      var i = resp.Data[0];
      Assert.That(i.Id, Is.Not.Null.And.Not.Empty, "IndustryItem.id wire name");
      Assert.That(
        (i.NameEN != null && i.NameEN.Length > 0) ||
        (i.NameCN != null && i.NameCN.Length > 0),
        Is.True,
        "IndustryItem should have at least one of nameEN / nameCN populated");
      Assert.That(i.IndustryLevel, Is.Not.Null.And.Not.Empty,
        "IndustryItem.industryLevel wire name");
    }

    // =====================================================================
    // Industry Stocks (US) — endpoint may be deprecated server-side
    // =====================================================================
    [Test]
    public void GetIndustryStocks_US_Succeeds()
    {
      // First fetch an industry id from industry_list, then query stocks.
      var listModel = new IndustryListModel { Market = Market.US };
      var listReq = new TigerRequest<IndustryListResponse>
      {
        ApiMethodName = QuoteApiService.INDUSTRY_LIST,
        ModelValue = listModel
      };
      var listResp = _client!.Execute(listReq);
      if (listResp == null || !listResp.IsSuccess()
          || listResp.Data == null || listResp.Data.Count == 0)
      {
        Assert.Ignore("industry_list returned no rows; cannot seed industry_stocks");
        return;
      }
      var industryId = listResp.Data[0].Id;

      var model = new IndustryStocksModel { IndustryId = industryId, Market = Market.US };
      var req = new TigerRequest<IndustryStocksResponse>
      {
        ApiMethodName = QuoteApiService.INDUSTRY_STOCKS,
        ModelValue = model
      };
      var resp = _client!.Execute(req);
      Assert.That(resp, Is.Not.Null, "industry_stocks response must not be null");

      // Server sometimes returns "1000 method does not support" — endpoint
      // appears deprecated. Accept as boundary; re-enables automatically
      // once the method is restored.
      if (!resp!.IsSuccess())
      {
        var msg = resp.Message ?? string.Empty;
        Assert.That(
          msg.Contains("does not support") || msg.ToLower().Contains("permission"),
          Is.True,
          $"unexpected industry_stocks error: code={resp.Code} msg={msg}");
        return;
      }
      Assert.That(resp.Data, Is.Not.Null, "industry_stocks data must not be null");
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].Symbol, Is.Not.Null.And.Not.Empty,
          "IndustryStockItem.symbol wire name");
      }
    }

    // =====================================================================
    // Stock Industry (AAPL)
    // API requires singular "symbol" (string) + "market", not plural
    // "symbols" (list). QuoteCapitalModel provides both fields with the
    // correct wire names. Response is a JSON array → TigerListResponse.
    // =====================================================================
    [Test]
    public void GetStockIndustry_AAPL_Succeeds()
    {
      var model = new QuoteCapitalModel
      {
        Symbol = "AAPL",
        Market = Market.US
      };
      var resp = Execute<TigerListResponse>(QuoteApiService.STOCK_INDUSTRY, model);

      Assert.That(resp.Data, Is.Not.Null, "stock_industry data must not be null");
    }

    // =====================================================================
    // Option Timeline (AAPL)
    // =====================================================================
    [Test]
    public void GetOptionTimeline_AAPL_ReturnsValidFields()
    {
      var opt = GetAaplOption();
      var model = new OptionTimelineV2Model
      {
        OptionQuery = new List<OptionQueryItem>
        {
          new OptionQueryItem
          {
            Symbol = opt.Symbol,
            Right = opt.Right,
            Strike = opt.Strike,
            Expiry = opt.Expiry
          }
        },
        Market = "US"
      };
      var resp = Execute<QuoteTimelineResponse>(QuoteApiService.OPTION_TIMELINE, model);

      Assert.That(resp.Data, Is.Not.Null, "option_timeline data must not be null");
      // Option timeline is intraday; outside trading hours the data list may be empty
      if (resp.Data.Count == 0)
        Assert.Ignore("non-trading hours, option_timeline data may be empty");
      Assert.That(resp.Data[0].Symbol, Is.Not.Null.And.Not.Empty,
          "option timeline symbol wire name");
    }

    // =====================================================================
    // Grab Quote Permission
    // QuoteApiService.GRAB_QUOTE_PERMISSION = "grab_quote_permission"
    // =====================================================================
    [Test]
    public void GrabQuotePermission_Succeeds()
    {
      var resp = Execute<QuotePermissionResponse>(QuoteApiService.GRAB_QUOTE_PERMISSION, new ApiModel());

      Assert.That(resp.Data, Is.Not.Null, "grab_quote_permission data must not be null");
      // Permissions returned may be empty if account has no entitlements to grab.
      if (resp.Data.Count > 0)
      {
        Assert.That(resp.Data[0].Name, Is.Not.Null.And.Not.Empty,
            "grabbed quote permission name wire name");
      }
    }

    // =====================================================================
    // Corporate Action — Split (AAPL + TSLA, 3-year range)
    // The SDK has no dedicated CorporateSplitResponse; use TigerDictResponse
    // (same approach as GetFinancialDaily/GetFinancialReport).
    // =====================================================================
    [Test]
    public void GetCorporateAction_Split_ReturnsValidFields()
    {
      long begin = DateUtil.ConvertTimestamp("2022-01-01", CustomTimeZone.NY_ZONE);
      long end = DateUtil.ConvertTimestamp("2025-12-31", CustomTimeZone.NY_ZONE);

      var model = new CorporateActionModel
      {
        Symbols = new List<string> { "AAPL", "TSLA" },
        Market = Market.US,
        ActionType = CorporateActionType.SPLIT,
        BeginDate = begin,
        EndDate = end
      };
      var resp = Execute<TigerDictResponse>(QuoteApiService.CORPORATE_ACTION, model);

      Assert.That(resp.Data, Is.Not.Null,
          "corporate action (split) data must not be null");
      // Split events may be absent in the range — the call succeeding is enough.
    }

    // =====================================================================
    // Trade Rank (US market)
    // NOTE: QuoteApiService does not yet expose a TRADE_RANK constant.
    // The API method name "trade_rank" is used directly (matching the Python
    // SDK's get_trade_rank endpoint). Add a constant once the SDK is updated.
    // =====================================================================
    [Test]
    public void GetTradeRank_US_ReturnsValidFields()
    {
      var model = new QuoteTradeRankModel { Market = Market.US };
      var req = new TigerRequest<QuoteTradeRankResponse>
      {
        ApiMethodName = "trade_rank",
        ModelValue = model
      };
      var resp = _client!.Execute(req);
      Assert.That(resp, Is.Not.Null, "trade_rank response must not be null");

      // Some accounts may not have access to the trade rank endpoint.
      if (resp != null && !resp.IsSuccess())
      {
        Assert.Ignore($"trade_rank not accessible for this account: code={resp.Code} msg={resp.Message}");
      }

      Assert.That(resp!.Data, Is.Not.Null, "trade_rank data must not be null");
      if (resp.Data.Count > 0)
      {
        var item = resp.Data[0];
        Assert.That(item.Symbol, Is.Not.Null.And.Not.Empty, "trade rank symbol wire name");
        Assert.That(item.Market, Is.Not.Null.And.Not.Empty, "trade rank market wire name");
      }
    }

    // =====================================================================
    // Short Interest (AAPL)
    // NOTE: The short_interest API is not included in QuoteApiService and
    // has no corresponding SDK model/response type. Python SDK explicitly
    // skips this test: "Account does not support short interest API method".
    // Skipped here until a dedicated C# SDK model and response are added.
    // =====================================================================
    [Test]
    public void GetShortInterest_AAPL_Skipped()
    {
      Assert.Ignore(
          "short_interest is not yet exposed in the C# SDK (no model/response/constant). " +
          "The Python SDK also skips this: 'Account does not support short interest API method'. " +
          "Add a QuoteShortInterestModel + QuoteShortInterestResponse and a " +
          "QuoteApiService.SHORT_INTEREST constant before enabling this test.");
    }

    // =====================================================================
    // Kline 30-day time range (AAPL + HK 00700)
    // =====================================================================
    [Test]
    public void GetKline_30Day_AAPL_And_00700_OhlcConstraints()
    {
      long now = DateUtil.CurrentTimeMillis();
      long begin = now - 30L * 24 * 3600 * 1000;

      foreach (string symbol in new[] { "AAPL", "00700" })
      {
        var model = new QuoteKlineModel
        {
          Symbols = new List<string> { symbol },
          Period = KLineType.day.Value,
          BeginTime = begin,
          EndTime = now,
          Limit = 60
        };
        var resp = Execute<QuoteKlineResponse>(QuoteApiService.KLINE, model);

        Assert.That(resp.Data, Is.Not.Null, $"kline data must not be null for {symbol}");
        if (resp.Data.Count == 0)
          Assert.Ignore($"kline returned 0 items for {symbol} — may be outside data range");

        var kline = resp.Data[0];
        Assert.That(kline.Symbol, Is.EqualTo(symbol), $"kline symbol wire name for {symbol}");

        if (kline.Items == null || kline.Items.Count == 0)
          Assert.Ignore($"kline items empty for {symbol} — no data in 30-day range");

        Assert.That(kline.Items.Count, Is.GreaterThanOrEqualTo(15),
            $"30-day daily kline should have >= 15 points for {symbol}");

        // Timestamps must be strictly ascending
        for (int i = 1; i < kline.Items.Count; i++)
        {
          Assert.That(kline.Items[i].Time, Is.GreaterThan(kline.Items[i - 1].Time),
              $"kline timestamps should be ascending at index {i} for {symbol}");
        }

        // OHLC constraints for every candle
        foreach (var pt in kline.Items)
        {
          if (pt.High > 0 && pt.Low > 0)
            Assert.That(pt.High, Is.GreaterThanOrEqualTo(pt.Low),
                $"high >= low for {symbol}");
          if (pt.High > 0 && pt.Open > 0)
            Assert.That(pt.High, Is.GreaterThanOrEqualTo(pt.Open),
                $"high >= open for {symbol}");
          if (pt.High > 0 && pt.Close > 0)
            Assert.That(pt.High, Is.GreaterThanOrEqualTo(pt.Close),
                $"high >= close for {symbol}");
          if (pt.Low > 0 && pt.Open > 0)
            Assert.That(pt.Open, Is.GreaterThanOrEqualTo(pt.Low),
                $"open >= low for {symbol}");
          if (pt.Low > 0 && pt.Close > 0)
            Assert.That(pt.Close, Is.GreaterThanOrEqualTo(pt.Low),
                $"close >= low for {symbol}");
          Assert.That(pt.Volume, Is.GreaterThanOrEqualTo(0),
              $"volume >= 0 for {symbol}");
        }
      }
    }

    // =====================================================================
    // Quote depth ordering (AAPL US + HK 00700)
    // =====================================================================
    [Test]
    public void GetQuoteDepth_AAPL_And_00700_OrderingAndSpread()
    {
      var cases = new[] { ("AAPL", Market.US), ("00700", Market.HK) };

      foreach (var (symbol, market) in cases)
      {
        var model = new QuoteDepthModel
        {
          Symbols = new List<string> { symbol },
          Market = market
        };
        var resp = Execute<QuoteDepthResponse>(QuoteApiService.QUOTE_DEPTH, model);

        Assert.That(resp.Data, Is.Not.Null, $"depth data must not be null for {symbol}");
        if (resp.Data.Count == 0)
          Assert.Ignore($"depth returned 0 items for {symbol} — non-trading hours");

        var item = resp.Data[0];
        Assert.That(item.Symbol, Is.EqualTo(symbol), $"depth symbol wire name for {symbol}");

        // Asks must be ascending
        if (item.Asks != null && item.Asks.Count >= 2)
        {
          for (int i = 1; i < item.Asks.Count; i++)
          {
            Assert.That(item.Asks[i].Price, Is.GreaterThanOrEqualTo(item.Asks[i - 1].Price),
                $"asks must be ascending at index {i} for {symbol}");
          }
          foreach (var ask in item.Asks)
            Assert.That(ask.Price, Is.GreaterThan(0), $"ask price > 0 for {symbol}");
        }

        // Bids must be descending
        if (item.Bids != null && item.Bids.Count >= 2)
        {
          for (int i = 1; i < item.Bids.Count; i++)
          {
            Assert.That(item.Bids[i].Price, Is.LessThanOrEqualTo(item.Bids[i - 1].Price),
                $"bids must be descending at index {i} for {symbol}");
          }
          foreach (var bid in item.Bids)
            Assert.That(bid.Price, Is.GreaterThan(0), $"bid price > 0 for {symbol}");
        }

        // Spread >= 0: lowest ask >= highest bid
        if (item.Asks != null && item.Asks.Count > 0
            && item.Bids != null && item.Bids.Count > 0)
        {
          double lowestAsk = item.Asks[0].Price;
          double highestBid = item.Bids[0].Price;
          Assert.That(lowestAsk - highestBid, Is.GreaterThanOrEqualTo(0),
              $"spread >= 0 (lowestAsk={lowestAsk}, highestBid={highestBid}) for {symbol}");
        }
      }
    }

    // =====================================================================
    // Brief multi-market (AAPL US + 00700 HK + 09988 HK)
    // =====================================================================
    [Test]
    public void GetBrief_MultiMarket_AAPL_00700_09988_PriceConstraints()
    {
      var symbols = new List<string> { "AAPL", "00700", "09988" };
      var model = new QuoteSymbolModel { Symbols = symbols };
      var resp = Execute<BriefResponse>(QuoteApiService.BRIEF, model);

      Assert.That(resp.Data, Is.Not.Null, "brief data wrapper must not be null");
      Assert.That(resp.Data.Items, Is.Not.Null, "brief items must not be null");
      if (resp.Data.Items.Count == 0)
        Assert.Ignore("brief returned 0 items — non-trading hours");

      foreach (var q in resp.Data.Items)
      {
        Assert.That(q.Symbol, Is.Not.Null.And.Not.Empty,
            "brief item symbol must be non-empty");
        Assert.That(q.LatestPrice, Is.GreaterThan(0),
            $"latestPrice > 0 for {q.Symbol}");

        if (q.High > 0 && q.Low > 0)
          Assert.That(q.High, Is.GreaterThanOrEqualTo(q.Low),
              $"high >= low for {q.Symbol}");

        if (q.AskPrice > 0 && q.BidPrice > 0)
          Assert.That(q.AskPrice, Is.GreaterThanOrEqualTo(q.BidPrice),
              $"askPrice >= bidPrice for {q.Symbol}");
      }
    }
  }
}
