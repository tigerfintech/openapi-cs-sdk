using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Tests.TestSupport;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for Quote Model classes — serialization wire names, defaults, and constructor overloads.
  /// Zero network — pure model construction and JSON assertions.
  /// </summary>
  [TestFixture]
  public class QuoteModelTest
  {
    // --- QuoteSymbolModel / subclasses ---

    [Test]
    public void QuoteKlineModel_Defaults_PeriodDay_Limit300_RightBr()
    {
      var model = new QuoteKlineModel();
      Assert.That(model.Period, Is.EqualTo("day"));
      Assert.That(model.Limit, Is.EqualTo(300));
      Assert.That(model.Right, Is.EqualTo(RightOption.br));
    }

    [Test]
    public void QuoteKlineModel_Serialization_WireNames()
    {
      var model = new QuoteKlineModel
      {
        Symbols = new List<string> { "BTC.USD" },
        BeginTime = 1000,
        EndTime = 2000,
        PageToken = "next-page",
        SecType = SecType.CC
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbols\""));
      Assert.That(json, Does.Contain("\"period\":\"day\""));
      Assert.That(json, Does.Contain("\"begin_time\":1000"));
      Assert.That(json, Does.Contain("\"end_time\":2000"));
      Assert.That(json, Does.Contain("\"limit\":300"));
      Assert.That(json, Does.Contain("\"right\":\"br\""));
      Assert.That(json, Does.Contain("\"page_token\":\"next-page\""));
      Assert.That(json, Does.Contain("\"sec_type\":\"CC\""));
    }

    [Test]
    public void QuoteKlineModel_Serialization_OmitsNullSecType()
    {
      string json = JsonConvert.SerializeObject(new QuoteKlineModel(), TigerClient.JsonSet);
      Assert.That(json, Does.Not.Contain("\"sec_type\""));
    }

    [Test]
    public void QuoteKlineRequest_UsesApiVersion2()
    {
      var client = new QuoteClient(TestClientFactory.CreateOfflineConfig());
      var request = new TigerRequest<QuoteKlineResponse>
      {
        ApiMethodName = QuoteApiService.KLINE,
        ModelValue = new QuoteKlineModel { SecType = SecType.CC }
      };

      Assert.That(client.Validate(request, out _), Is.True);
      Assert.That(request.ApiVersion, Is.EqualTo(TigerApiConstants.API_VERSION_2));
    }

    [Test]
    public void QuoteTimelineModel_Serialization()
    {
      var model = new QuoteTimelineModel
      {
        Symbols = new List<string> { "BTC.USD" },
        BeginTime = 1000,
        SecType = SecType.CC
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"begin_time\":1000"));
      Assert.That(json, Does.Contain("\"sec_type\":\"CC\""));
    }

    [Test]
    public void QuoteTimelineRequest_UsesApiVersion3()
    {
      var client = new QuoteClient(TestClientFactory.CreateOfflineConfig());
      var request = new TigerRequest<QuoteTimelineResponse>
      {
        ApiMethodName = QuoteApiService.TIMELINE,
        ApiVersion = TigerApiConstants.API_VERSION_2,
        ModelValue = new QuoteTimelineModel { SecType = SecType.CC }
      };

      Assert.That(client.Validate(request, out _), Is.True);
      Assert.That(request.ApiVersion, Is.EqualTo(TigerApiConstants.API_VERSION_3));
    }

    [Test]
    public void QuoteTimelineRequest_WithoutCc_PreservesApiVersion()
    {
      var client = new QuoteClient(TestClientFactory.CreateOfflineConfig());
      var request = new TigerRequest<QuoteTimelineResponse>
      {
        ApiMethodName = QuoteApiService.TIMELINE,
        ApiVersion = TigerApiConstants.API_VERSION_2,
        ModelValue = new QuoteTimelineModel()
      };

      Assert.That(client.Validate(request, out _), Is.True);
      Assert.That(request.ApiVersion, Is.EqualTo(TigerApiConstants.API_VERSION_2));
    }

    [Test]
    public void QuoteStockTradeModel_Serialization()
    {
      var model = new QuoteStockTradeModel { Symbols = new List<string> { "AAPL", "GOOG" } };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("AAPL"));
      Assert.That(json, Does.Contain("GOOG"));
    }

    [Test]
    public void QuoteDepthModel_Serialization()
    {
      var model = new QuoteDepthModel { Symbols = new List<string> { "AAPL" }, Market = Market.US };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
    }

    [Test]
    public void QuoteMarketModel_Serialization()
    {
      var model = new QuoteMarketModel { Market = Market.US, IncludeOTC = true };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"include_otc\":true"));
    }

    [Test]
    public void QuoteTradeTickModel_DefaultLimit_Is200()
    {
      var model = new QuoteTradeTickModel();
      Assert.That(model.Limit, Is.EqualTo(200));
    }

    [Test]
    public void QuoteSymbolModel_Serialization_IncludesHourTrading()
    {
      var model = new QuoteSymbolModel { Symbols = new List<string> { "AAPL" }, IncludeHourTrading = true };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"include_hour_trading\":true"));
    }

    // --- Option models ---

    [Test]
    public void OptionChainModel_Serialization()
    {
      var model = new OptionChainModel { Symbol = "AAPL", Expiry = 20240119 };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"expiry\":20240119"));
    }

    [Test]
    public void OptionAnalysisSymbolModel_Constructors()
    {
      var m1 = new OptionAnalysisSymbolModel("AAPL", "day");
      Assert.That(m1.Symbol, Is.EqualTo("AAPL"));
      Assert.That(m1.Period, Is.EqualTo("day"));
      Assert.That(m1.RequireVolatilityList, Is.Null);

      var m2 = new OptionAnalysisSymbolModel("AAPL", "day", true);
      Assert.That(m2.RequireVolatilityList, Is.True);
    }

    [Test]
    public void OptionAnalysisModel_Constructors()
    {
      var symbols = new List<OptionAnalysisSymbolModel>
      {
        new OptionAnalysisSymbolModel("AAPL", "day")
      };
      var m1 = new OptionAnalysisModel(symbols);
      Assert.That(m1.Symbols.Count, Is.EqualTo(1));
      Assert.That(m1.Market, Is.EqualTo(Market.NONE));

      var m2 = new OptionAnalysisModel(symbols, Market.US);
      Assert.That(m2.Market, Is.EqualTo(Market.US));
    }

    [Test]
    public void OptionBasicModel_Serialization()
    {
      var model = new OptionBasicModel { Market = Market.US };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
    }

    [Test]
    public void OptionCommonModel_Serialization()
    {
      var model = new OptionCommonModel { Symbol = "AAPL", Right = "CALL", Strike = "180", Expiry = 20240119 };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"right\":\"CALL\""));
      Assert.That(json, Does.Contain("\"strike\":\"180\""));
      Assert.That(json, Does.Contain("\"expiry\":20240119"));
    }

    [Test]
    public void OptionKlineModel_Defaults_PeriodDay_Limit300()
    {
      var model = new OptionKlineModel();
      Assert.That(model.Period, Is.EqualTo("day"));
      Assert.That(model.Limit, Is.EqualTo(300));
    }

    [Test]
    public void OptionKlineV2Model_Serialization()
    {
      var model = new OptionKlineV2Model { Market = Market.US };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
    }

    [Test]
    public void OptionExpirationModel_Serialization()
    {
      var model = new OptionExpirationModel { Symbols = new List<string> { "AAPL" }, Market = Market.US };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
    }

    // --- QuoteBrokerHoldModel constructors ---

    [Test]
    public void QuoteBrokerHoldModel_MarketLimitConstructor()
    {
      var model = new QuoteBrokerHoldModel(Market.US, 50);
      Assert.That(model.Market, Is.EqualTo(Market.US));
      Assert.That(model.Limit, Is.EqualTo(50));
      Assert.That(model.Page, Is.EqualTo(0));
    }

    [Test]
    public void QuoteBrokerHoldModel_MarketLimitPageConstructor()
    {
      var model = new QuoteBrokerHoldModel(Market.HK, 20, 3);
      Assert.That(model.Market, Is.EqualTo(Market.HK));
      Assert.That(model.Limit, Is.EqualTo(20));
      Assert.That(model.Page, Is.EqualTo(3));
    }

    [Test]
    public void QuoteBrokerHoldModel_FullConstructor()
    {
      var model = new QuoteBrokerHoldModel(Market.US, 10, 1, "volume", "desc");
      Assert.That(model.OrderBy, Is.EqualTo("volume"));
      Assert.That(model.Direction, Is.EqualTo("desc"));
    }

    // --- Capital flow models ---

    [Test]
    public void QuoteCapitalFlowModel_Defaults_PeriodDay_Limit200()
    {
      var model = new QuoteCapitalFlowModel();
      Assert.That(model.Period, Is.EqualTo("day"));
      Assert.That(model.Limit, Is.EqualTo(200));
    }

    [Test]
    public void QuoteCapitalModel_Serialization()
    {
      var model = new QuoteCapitalModel { Symbol = "AAPL", Market = Market.US };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"market\":\"US\""));
    }

    // --- Other models ---

    [Test]
    public void QuoteContractsModel_Serialization()
    {
      var model = new QuoteContractsModel { Symbol = "AAPL", SecType = SecType.STK };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"sec_type\":\"STK\""));
    }

    [Test]
    public void QuoteHistoryTimelineModel_Serialization()
    {
      var model = new QuoteHistoryTimelineModel { Date = "2024-01-19", Right = RightOption.br };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"date\":\"2024-01-19\""));
      Assert.That(json, Does.Contain("\"right\":\"br\""));
      Assert.That(json, Does.Not.Contain("\"sec_type\""));
    }

    [Test]
    public void QuoteStockFundamentalModel_Serialization()
    {
      var model = new QuoteStockFundamentalModel { Symbols = new List<string> { "AAPL" }, Market = Market.US };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
    }

    [Test]
    public void QuoteStockBrokerModel_Serialization()
    {
      var model = new QuoteStockBrokerModel { Symbol = "AAPL", Limit = 10 };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"limit\":10"));
    }

    [Test]
    public void QuoteTradeRankModel_Serialization()
    {
      var model = new QuoteTradeRankModel { Market = Market.US };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
    }

    // --- Range and Greeks ---

    [Test]
    public void Range_Constructor_SetsMinMax()
    {
      var range = new Range<double>(0.1, 0.5);
      Assert.That(range.Min, Is.EqualTo(0.1));
      Assert.That(range.Max, Is.EqualTo(0.5));
    }

    [Test]
    public void OptionChainFilterModel_Serialization()
    {
      var model = new OptionChainFilterModel
      {
        InTheMoney = true,
        ImpliedVolatility = new Range<double>(0.1, 0.5),
        OpenInterest = new Range<int>(10, 1000)
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"in_the_money\":true"));
      Assert.That(json, Does.Contain("\"implied_volatility\""));
      Assert.That(json, Does.Contain("\"min\":0.1"));
      Assert.That(json, Does.Contain("\"max\":0.5"));
    }
  }
}
