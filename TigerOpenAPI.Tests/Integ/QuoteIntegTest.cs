using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Tests.Integ
{
  /// <summary>
  /// Integration tests against the live API. Requires real credentials via env vars:
  ///   TIGER_ID        — your tiger developer ID
  ///   TIGER_PRIVATE_KEY — PKCS8 RSA private key (base64)
  ///   TIGER_LICENSE   — e.g. TBNZ / TBSG / TBHK / TBAU
  ///
  /// Run with:  dotnet test --filter Category=Integration
  /// CI:        see .gitlab-ci.yml `integ` stage (allow_failure: true)
  ///
  /// Test philosophy: every assertion covers a concrete field from the real response.
  /// Non-null checks alone are insufficient — wire-name bugs produce null silently.
  /// </summary>
  [TestFixture]
  [Category("Integration")]
  public class QuoteIntegTest
  {
    private TigerConfig _config = null!;
    private QuoteClient _client = null!;

    private static readonly Regex DatePattern = new Regex(@"^\d{4}-\d{2}-\d{2}$");
    private static readonly Regex TimePattern = new Regex(@"^\d{1,2}:\d{2}");

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      string tigerId    = Environment.GetEnvironmentVariable("TIGER_ID")         ?? string.Empty;
      string privateKey = Environment.GetEnvironmentVariable("TIGER_PRIVATE_KEY") ?? string.Empty;
      string licenseStr = Environment.GetEnvironmentVariable("TIGER_LICENSE")    ?? "TBNZ";

      if (string.IsNullOrWhiteSpace(tigerId) || string.IsNullOrWhiteSpace(privateKey))
        Assert.Ignore("Integration credentials not set — skipping (set TIGER_ID + TIGER_PRIVATE_KEY)");

      if (!Enum.TryParse<License>(licenseStr, true, out var license))
        Assert.Ignore($"TIGER_LICENSE='{licenseStr}' is not a valid License value");

      _config = new TigerConfig
      {
        TigerId = tigerId, License = license, PrivateKey = privateKey,
        AutoGrabPermission = false, AutoRefreshToken = false,
      };
      _client = new QuoteClient(_config);
    }

    // ---- helpers ----
    private T Execute<T>(string method, ApiModel? model = null) where T : TigerResponse
    {
      var req = new TigerRequest<T> { ApiMethodName = method, ModelValue = model ?? new ApiModel() };
      var resp = _client.Execute(req);
      Assert.That(resp, Is.Not.Null, $"{method} response must not be null");
      Assert.That(resp!.IsSuccess(), Is.True,
          $"{method} returned error code={resp.Code} msg={resp.Message}");
      return resp;
    }

    // =====================================================================
    // Trading Calendar
    // =====================================================================
    [Test]
    public void GetTradeCalendar_US_ReturnsNonEmptyList_WithValidFields()
    {
      var model = new QuoteMarketModel { Market = Market.US };
      var resp = Execute<TradeCalendarResponse>(QuoteApiService.TRADING_CALENDAR, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0),
          "US trade calendar should have >0 entries");

      foreach (var entry in resp.Data)
      {
        Assert.That(entry.Date, Does.Match(DatePattern),
            $"Each calendar entry.Date must match yyyy-MM-dd, got: '{entry.Date}'");
        Assert.That(new[] { "NORMAL", "EARLY_CLOSE", "TRADING", "HOLIDAY", "HALF_DAY" }, Does.Contain(entry.Type),
            $"entry.Type must be a known calendar type, got: '{entry.Type}'");
      }
    }

    [Test]
    public void GetTradeCalendar_HK_ReturnsValidEntries()
    {
      var model = new QuoteMarketModel { Market = Market.HK };
      var resp = Execute<TradeCalendarResponse>(QuoteApiService.TRADING_CALENDAR, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0));
      // Spot-check first entry has all required fields populated
      var first = resp.Data[0];
      Assert.That(first.Date, Does.Match(DatePattern), "HK calendar first.Date must match yyyy-MM-dd");
      Assert.That(first.Type, Is.Not.Null.And.Not.Empty, "HK calendar first.Type must be non-empty");
    }

    // =====================================================================
    // Market State
    // =====================================================================
    [Test]
    public void GetMarketState_US_ReturnsValidFields()
    {
      var model = new QuoteMarketModel { Market = Market.US };
      var resp = Execute<MarketStateResponse>(QuoteApiService.MARKET_STATE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0));
      var state = resp.Data[0];

      // All four [JsonProperty]-annotated fields must be populated
      Assert.That(state.Market,       Is.Not.Null.And.Not.Empty, "market wire name");
      Assert.That(state.MarketStatus, Is.Not.Null.And.Not.Empty, "marketStatus wire name");
      Assert.That(state.Status,       Is.Not.Null.And.Not.Empty, "status wire name");
      // openTime may be null outside trading hours
      if (state.OpenTime != null)
        Assert.That(state.OpenTime, Does.Match(TimePattern), "openTime must match HH:mm pattern when present");
    }

    // =====================================================================
    // Real-Time Quote (AAPL)
    // =====================================================================
    [Test]
    public void GetRealTimeQuote_AAPL_ReturnsAllPriceFields()
    {
      var model = new QuoteSymbolModel
      {
        Symbols = new System.Collections.Generic.List<string> { "AAPL" }
      };
      var resp = Execute<QuoteRealTimeQuoteResponse>(QuoteApiService.QUOTE_REAL_TIME, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.EqualTo(1));
      var q = resp.Data[0];

      // Symbol identity
      Assert.That(q.Symbol, Is.EqualTo("AAPL"), "symbol wire name");

      // Price fields — all must be positive for a liquid stock
      Assert.That(q.Open,        Is.GreaterThan(0), "open");
      Assert.That(q.High,        Is.GreaterThan(0), "high");
      Assert.That(q.Low,         Is.GreaterThan(0), "low");
      Assert.That(q.Close,       Is.GreaterThan(0).Or.EqualTo(0), "close (may be 0 before market close)");
      Assert.That(q.PreClose,    Is.GreaterThan(0), "preClose wire name");
      Assert.That(q.LatestPrice, Is.GreaterThan(0), "latestPrice wire name");

      // Bid/ask sanity
      Assert.That(q.BidPrice, Is.GreaterThanOrEqualTo(0), "bidPrice wire name");
      Assert.That(q.AskPrice, Is.GreaterThanOrEqualTo(0), "askPrice wire name");

      // Volume should be positive on a trading day (or 0 on weekend/holiday)
      Assert.That(q.Volume, Is.GreaterThanOrEqualTo(0), "volume wire name");

      // latestTime must be a valid epoch millis (after 2020-01-01)
      Assert.That(q.LatestTime, Is.GreaterThan(1577836800000L), "latestTime wire name");
    }

    [Test]
    public void GetRealTimeQuote_MultipleSymbols_EachHasSymbolPopulated()
    {
      var symbols = new System.Collections.Generic.List<string> { "AAPL", "MSFT", "TSLA" };
      var model = new QuoteSymbolModel { Symbols = symbols };
      var resp = Execute<QuoteRealTimeQuoteResponse>(QuoteApiService.QUOTE_REAL_TIME, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.EqualTo(3));
      foreach (var q in resp.Data)
      {
        Assert.That(q.Symbol, Is.Not.Null.And.Not.Empty,
            "Each real-time quote must have Symbol populated");
        Assert.That(symbols, Does.Contain(q.Symbol),
            $"Symbol '{q.Symbol}' was not in request");
        Assert.That(q.LatestPrice, Is.GreaterThan(0),
            $"LatestPrice for {q.Symbol} must be > 0");
      }
    }

    // =====================================================================
    // Market State — multiple markets
    // =====================================================================
    [Test]
    public void GetMarketState_HK_ReturnsValidFields()
    {
      var model = new QuoteMarketModel { Market = Market.HK };
      var resp = Execute<MarketStateResponse>(QuoteApiService.MARKET_STATE, model);

      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0));
      var state = resp.Data[0];
      Assert.That(state.Market,       Is.Not.Null.And.Not.Empty);
      Assert.That(state.MarketStatus, Is.Not.Null.And.Not.Empty);
      Assert.That(state.Status,       Is.Not.Null.And.Not.Empty);
      // openTime may be empty for HK if market info differs — allow empty
      Assert.That(state.OpenTime, Is.Not.Null);
    }
  }
}
