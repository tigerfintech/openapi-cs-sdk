using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Tests.TestSupport;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for TigerClient.BuildParams() — zero network.
  /// Verifies RSA signing, timestamp, tigerId, sign field population.
  /// </summary>
  [TestFixture]
  public class BuildParamsTest
  {
    private QuoteClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      _client = new QuoteClient(TestClientFactory.CreateOfflineConfig());
    }

    [Test]
    public void BuildParams_PopulatesTimestamp()
    {
      var req = new TigerRequest<TradeCalendarResponse>
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new QuoteMarketModel { Market = Common.Enum.Market.US }
      };
      _client.BuildParams(req);
      Assert.That(req.Timestamp, Is.Not.Null.And.Not.Empty,
          "Timestamp should be set by BuildParams");
    }

    [Test]
    public void BuildParams_PopulatesTigerId()
    {
      var req = new TigerRequest<TradeCalendarResponse>
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new QuoteMarketModel { Market = Common.Enum.Market.US }
      };
      _client.BuildParams(req);
      Assert.That(req.TigerId, Is.EqualTo(TestClientFactory.CreateOfflineConfig().TigerId));
    }

    [Test]
    public void BuildParams_PopulatesSign_NonEmpty()
    {
      var req = new TigerRequest<TradeCalendarResponse>
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new QuoteMarketModel { Market = Common.Enum.Market.US }
      };
      _client.BuildParams(req);
      Assert.That(req.Sign, Is.Not.Null.And.Not.Empty,
          "RSA signature should be non-empty after BuildParams");
    }

    [Test]
    public void BuildParams_Sign_IsValidBase64()
    {
      var req = new TigerRequest<TradeCalendarResponse>
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new QuoteMarketModel { Market = Common.Enum.Market.US }
      };
      _client.BuildParams(req);
      Assert.DoesNotThrow(() => Convert.FromBase64String(req.Sign),
          "Sign should be valid Base64 (RSA-SHA1 output)");
    }

    [Test]
    public void BuildParams_BizContent_ContainsMarketField()
    {
      var req = new TigerRequest<TradeCalendarResponse>
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new QuoteMarketModel { Market = Common.Enum.Market.US }
      };
      _client.BuildParams(req);
      // BizContent is the JSON-serialized model value
      Assert.That(req.BizContent, Does.Contain("\"market\":\"US\""),
          "BizContent should contain the wire-name/value pair market:US");
    }

    [Test]
    public void BuildParams_DifferentApiMethods_ProduceDifferentSigns()
    {
      var req1 = new TigerRequest<TradeCalendarResponse> { ApiMethodName = QuoteApiService.TRADING_CALENDAR };
      var req2 = new TigerRequest<MarketStateResponse>  { ApiMethodName = QuoteApiService.MARKET_STATE };
      _client.BuildParams(req1);
      _client.BuildParams(req2);
      Assert.That(req1.Sign, Is.Not.EqualTo(req2.Sign),
          "Different API methods should produce different signatures");
    }

    [Test]
    public void BuildParams_NullValueHandling_OmitsNullFields()
    {
      // QuoteMarketModel with null TradeSession — should NOT appear in BizContent
      var req = new TigerRequest<TradeCalendarResponse>
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR,
        ModelValue = new QuoteMarketModel { Market = Common.Enum.Market.US }
      };
      _client.BuildParams(req);
      // TradeSession is null — NullValueHandling.Ignore means it must be absent
      Assert.That(req.BizContent, Does.Not.Contain("trade_session"),
          "Null properties must be omitted from serialized BizContent");
    }
  }
}
