using System;
using System.Threading.Tasks;
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
  /// </summary>
  [TestFixture]
  [Category("Integration")]
  public class QuoteIntegTest
  {
    private TigerConfig _config = null!;
    private QuoteClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      string tigerId     = Environment.GetEnvironmentVariable("TIGER_ID")     ?? string.Empty;
      string privateKey  = Environment.GetEnvironmentVariable("TIGER_PRIVATE_KEY") ?? string.Empty;
      string licenseStr  = Environment.GetEnvironmentVariable("TIGER_LICENSE") ?? "TBNZ";

      if (string.IsNullOrWhiteSpace(tigerId) || string.IsNullOrWhiteSpace(privateKey))
        Assert.Ignore("Integration credentials not set — skipping (set TIGER_ID + TIGER_PRIVATE_KEY)");

      if (!Enum.TryParse<License>(licenseStr, true, out var license))
        Assert.Ignore($"TIGER_LICENSE='{licenseStr}' is not a valid License enum value");

      _config = new TigerConfig
      {
        TigerId = tigerId,
        License = license,
        PrivateKey = privateKey,
        AutoGrabPermission = false,
        AutoRefreshToken = false,
      };
      _client = new QuoteClient(_config);
    }

    [Test]
    public async Task GetTradeCalendar_ReturnsNonEmptyList()
    {
      var request = new TigerRequest<TradeCalendarResponse>()
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new TradeCalendarModel()
        {
          Market = Market.US,
          BeginDate = "2025-01-01",
          EndDate = "2025-01-31"
        }
      };
      var resp = await _client.ExecuteAsync(request);
      Assert.That(resp, Is.Not.Null);
      Assert.That(resp!.Data, Is.Not.Null.And.Not.Empty,
          "Expected at least one trade calendar entry for US market");
    }

    [Test]
    public async Task GetAllSymbols_ReturnsNonEmptyList()
    {
      var request = new TigerRequest<SymbolNameResponse>()
      {
        ApiMethodName = QuoteApiService.ALL_SYMBOLS,
        ModelValue = new QuoteMarketModel() { Market = Market.US }
      };
      var resp = await _client.ExecuteAsync(request);
      Assert.That(resp, Is.Not.Null);
      Assert.That(resp!.Data, Is.Not.Null.And.Not.Empty,
          "Expected at least one symbol for US market");
    }
  }
}
