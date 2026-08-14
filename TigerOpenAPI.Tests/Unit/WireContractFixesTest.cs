using System.Collections.Generic;
using Newtonsoft.Json.Linq;
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
  /// Regression tests for four wire-contract bugs surfaced by the Rust SDK
  /// parity audit:
  ///
  ///  1. OptionTradeTickV2Model must serialize as a top-level JSON array.
  ///  3. IndustryListModel.IndustryLevel defaults to "GGROUP".
  ///  4. financial_report / trading_calendar / financial_daily pin API v2.
  ///  5. IndustryItem exposes nameCN / nameEN / industryLevel wire fields.
  /// </summary>
  [TestFixture]
  public class WireContractFixesTest
  {
    private QuoteClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      _client = new QuoteClient(TestClientFactory.CreateOfflineConfig());
    }

    // -----------------------------------------------------------------
    // Bug 1: option_trade_tick top-level array wire shape.
    // -----------------------------------------------------------------
    [Test]
    public void OptionTradeTick_BizContent_IsTopLevelArray()
    {
      var model = new OptionTradeTickV2Model
      {
        Items = new List<OptionQueryItem>
        {
          new OptionQueryItem
          {
            Symbol = "AAPL",
            Right = "CALL",
            Strike = "200",
            Expiry = 1234567890000L
          }
        }
      };
      var req = new TigerRequest<OptionTradeTickResponse>
      {
        ApiMethodName = QuoteApiService.OPTION_TRADE_TICK,
        ModelValue = model
      };
      _client.BuildParams(req);

      Assert.That(req.BizContent, Is.Not.Null.And.Not.Empty,
          "BizContent should be populated");
      var token = JToken.Parse(req.BizContent);
      Assert.That(token.Type, Is.EqualTo(JTokenType.Array),
          "option_trade_tick biz_content must be a top-level JSON array, " +
          "not an object wrapper such as {\"contracts\": [...]}. " +
          $"Got: {req.BizContent}");
      var arr = (JArray)token;
      Assert.That(arr.Count, Is.EqualTo(1));
      Assert.That((string?)arr[0]["symbol"], Is.EqualTo("AAPL"));
      Assert.That((string?)arr[0]["right"], Is.EqualTo("CALL"));
      Assert.That((string?)arr[0]["strike"], Is.EqualTo("200"));
      Assert.That((long?)arr[0]["expiry"], Is.EqualTo(1234567890000L));
    }

    // -----------------------------------------------------------------
    // Bug 3: IndustryListModel default industry_level = GGROUP.
    // -----------------------------------------------------------------
    [Test]
    public void IndustryListModel_DefaultIndustryLevel_IsGGROUP()
    {
      var model = new IndustryListModel();
      Assert.That(model.IndustryLevel, Is.EqualTo("GGROUP"),
          "IndustryListModel default must be GGROUP for parity with " +
          "Python (IndustryLevel.GGROUP) and Rust SDKs. Server rejects " +
          "when the field is absent.");
    }

    // -----------------------------------------------------------------
    // Bug 4: financial_report / trading_calendar / financial_daily V2.
    // -----------------------------------------------------------------
    [Test]
    public void Validate_FinancialReport_PinsApiVersion2()
    {
      var req = new TigerRequest<TigerResponse>
      {
        ApiMethodName = QuoteApiService.FINANCIAL_REPORT
      };
      Assert.That(req.ApiVersion, Is.EqualTo(TigerApiConstants.DEFAULT_VERSION),
          "sanity: default version before Validate should be DEFAULT_VERSION");

      var ok = _client.Validate(req, out _);
      Assert.That(ok, Is.True);
      Assert.That(req.ApiVersion, Is.EqualTo(TigerApiConstants.API_VERSION_2),
          "financial_report must be pinned to V2 (Java pins V2, server " +
          "rejects V3 with 'failed to parse parameters in biz_content')");
    }

    [Test]
    public void Validate_TradingCalendar_PinsApiVersion2()
    {
      var req = new TigerRequest<TigerResponse>
      {
        ApiMethodName = QuoteApiService.TRADING_CALENDAR
      };
      var ok = _client.Validate(req, out _);
      Assert.That(ok, Is.True);
      Assert.That(req.ApiVersion, Is.EqualTo(TigerApiConstants.API_VERSION_2),
          "trading_calendar must be pinned to V2 (Java pins V2, server " +
          "returns empty market field on V3)");
    }

    [Test]
    public void Validate_FinancialDaily_PinsApiVersion2()
    {
      var req = new TigerRequest<TigerResponse>
      {
        ApiMethodName = QuoteApiService.FINANCIAL_DAILY
      };
      var ok = _client.Validate(req, out _);
      Assert.That(ok, Is.True);
      Assert.That(req.ApiVersion, Is.EqualTo(TigerApiConstants.API_VERSION_2),
          "financial_daily must be pinned to V2 (Java FinancialDailyRequest " +
          "sets V2_0)");
    }

    // -----------------------------------------------------------------
    // Bug 5: IndustryItem deserializes nameCN / nameEN / industryLevel.
    // -----------------------------------------------------------------
    [Test]
    public void IndustryItem_DeserializesCamelCaseWireFields()
    {
      const string json = @"{
        ""id"": ""10"",
        ""nameCN"": ""能源"",
        ""nameEN"": ""Energy"",
        ""industryLevel"": ""GSECTOR""
      }";
      var item = Newtonsoft.Json.JsonConvert.DeserializeObject<IndustryItem>(json);
      Assert.That(item, Is.Not.Null);
      Assert.That(item!.Id, Is.EqualTo("10"));
      Assert.That(item.NameCN, Is.EqualTo("能源"),
          "IndustryItem.NameCN must map to wire field 'nameCN'");
      Assert.That(item.NameEN, Is.EqualTo("Energy"),
          "IndustryItem.NameEN must map to wire field 'nameEN'");
      Assert.That(item.IndustryLevel, Is.EqualTo("GSECTOR"),
          "IndustryItem.IndustryLevel must map to wire field 'industryLevel'");
    }

    [Test]
    public void IndustryListResponse_DeserializesDataArray()
    {
      const string json = @"{
        ""code"": 0,
        ""message"": ""success"",
        ""data"": [
          { ""id"": ""10"", ""nameCN"": ""能源"", ""nameEN"": ""Energy"", ""industryLevel"": ""GSECTOR"" },
          { ""id"": ""15"", ""nameCN"": ""材料"", ""nameEN"": ""Materials"", ""industryLevel"": ""GSECTOR"" }
        ]
      }";
      var resp = Newtonsoft.Json.JsonConvert.DeserializeObject<IndustryListResponse>(json);
      Assert.That(resp, Is.Not.Null);
      Assert.That(resp!.Data, Is.Not.Null.And.Count.EqualTo(2));
      Assert.That(resp.Data[0].NameEN, Is.EqualTo("Energy"));
      Assert.That(resp.Data[1].NameEN, Is.EqualTo("Materials"));
    }
  }
}
