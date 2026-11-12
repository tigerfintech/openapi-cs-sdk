using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.News;
using TigerOpenAPI.News.Model;
using TigerOpenAPI.News.Response;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Tests.TestSupport;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// 资讯接口逻辑覆盖测试（news_stock_list / news_highlight_list），与行情逻辑测试相互独立。
  /// 契约见 stock-openapi docs/news_api_design.md §3.1-§3.3 与决策 #5/#6/#11/#13/#14。
  /// 零网络：TestableNewsClient 跳过域名花园发现，其余为纯序列化/反序列化断言。
  /// </summary>
  [TestFixture]
  public class NewsApiLogicTest
  {
    private TigerConfig _config = null!;
    private TestableNewsClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      _config = TestClientFactory.CreateOfflineConfig();
      // TestableNewsClient skips RefreshServerUri (the only network path in the ctor)
      _client = new TestableNewsClient(_config);
    }

    // ---------- 请求 wire 格式（snake_case，缺省 null 省略，决策 #13） ----------

    [Test]
    public void StockNewsRequest_WireFormat_SnakeCase_AllFields()
    {
      var request = NewsClient.NewStockNewsRequest("AAPL", Language.zh_CN, 2, 50);
      _client.BuildParams(request);
      Assert.That(request.ApiMethodName, Is.EqualTo(NewsApiService.NEWS_STOCK_LIST));
      Assert.That(request.BizContent, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(request.BizContent, Does.Contain("\"page_count\":2"));
      Assert.That(request.BizContent, Does.Contain("\"page_size\":50"));
      Assert.That(request.BizContent, Does.Contain("\"lang\":\"zh_CN\""));
    }

    [Test]
    public void StockNewsRequest_OmitsOptionalNulls()
    {
      // page_count / page_size 缺省（null）= 服务端默认 1 / 20（决策 #6）——wire 上不出现
      var request = NewsClient.NewStockNewsRequest("TSLA", null, null, null);
      _client.BuildParams(request);
      Assert.That(request.BizContent, Does.Contain("\"symbol\":\"TSLA\""));
      Assert.That(request.BizContent, Does.Not.Contain("page_count"),
          "absent page_count must be omitted (server default 1)");
      Assert.That(request.BizContent, Does.Not.Contain("page_size"),
          "absent page_size must be omitted (server default 20)");
    }

    [Test]
    public void HighlightRequest_WireFormat_LastIdSnakeCase()
    {
      var request = NewsClient.NewMarketNewsRequest(Language.zh_TW, "789");
      _client.BuildParams(request);
      Assert.That(request.ApiMethodName, Is.EqualTo(NewsApiService.NEWS_HIGHLIGHT_LIST));
      Assert.That(request.BizContent, Does.Contain("\"last_id\":\"789\""));
      Assert.That(request.BizContent, Does.Contain("\"lang\":\"zh_TW\""));
    }

    [Test]
    public void HighlightRequest_OmitsLastId_WhenNull()
    {
      var request = NewsClient.NewMarketNewsRequest(null, null);
      _client.BuildParams(request);
      Assert.That(request.BizContent, Does.Not.Contain("last_id"),
          "absent last_id must be omitted (start from newest)");
    }

    [Test]
    public void HighlightRequest_CursorOnly_NoPagingFields()
    {
      // 决策 #14：highlight 撤除 page_count，只保留 last_id 游标翻页——
      // 请求 wire 上永远不出现 page_count / page_size
      var request = NewsClient.NewMarketNewsRequest(Language.en_US, "123");
      _client.BuildParams(request);
      Assert.That(request.BizContent, Does.Not.Contain("page_count"),
          "highlight must never carry page_count (cursor-only pagination)");
      Assert.That(request.BizContent, Does.Not.Contain("page_size"),
          "highlight must never carry page_size (cursor-only pagination)");
    }

    [Test]
    public void StockNewsRequest_SingleSymbol_NoBatchShape()
    {
      // 决策 #5：单标的 string 字段，wire 上不得出现批量形态（symbols 数组）
      var request = NewsClient.NewStockNewsRequest("AAPL", null, null, null);
      _client.BuildParams(request);
      Assert.That(request.BizContent, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(request.BizContent, Does.Not.Contain("\"symbols\""));
    }

    // ---------- lang 处理（决策 #11） ----------

    [Test]
    public void Lang_Omitted_InheritsClientConfigLanguage()
    {
      // 不传 lang 时由 TigerClient.BuildParams 继承客户端语言配置（默认 en_US），
      // 与 Python SDK 的 NewsClient 行为一致
      var request = NewsClient.NewStockNewsRequest("AAPL", null, null, null);
      _client.BuildParams(request);
      Assert.That(request.BizContent, Does.Contain($"\"lang\":\"{_config.Language}\""));
    }

    [Test]
    public void Lang_NoneConfig_OmitsLang_MixedContent()
    {
      // TigerConfig.Language = Language.None 时 lang 缺省（= 服务端中英混合内容）
      var config = TestClientFactory.CreateOfflineConfig();
      config.Language = Language.None;
      var client = new TestableNewsClient(config);
      var request = NewsClient.NewMarketNewsRequest(null, null);
      client.BuildParams(request);
      Assert.That(request.BizContent, Does.Not.Contain("\"lang\""),
          "Language.None must serialize to an absent lang (mixed content)");
    }

    // ---------- 响应解析（camelCase 出参，决策 #13） ----------

    [Test]
    public void StockNewsResponse_Deserialize_FullEnvelope()
    {
      string json = "{\"code\":0,\"message\":\"success\",\"data\":{"
          + "\"items\":[{\"id\":\"123456\",\"title\":\"Apple Q2\",\"summary\":\"Earnings beat\","
          + "\"pubTimestamp\":1729413600,"
          + "\"url\":\"https://example.com/news/123456\"}],"
          + "\"pageCount\":1,\"pageSize\":20,\"totalPage\":10,\"totalSize\":200}}";
      var response = JsonConvert.DeserializeObject<NewsStockListResponse>(json, TigerClient.JsonSet);
      Assert.That(response!.IsSuccess(), Is.True);
      NewsPageItem page = response.Data!;
      Assert.That(page.PageCount, Is.EqualTo(1));
      Assert.That(page.PageSize, Is.EqualTo(20));
      Assert.That(page.TotalPage, Is.EqualTo(10));
      Assert.That(page.TotalSize, Is.EqualTo(200));
      Assert.That(page.Items, Is.Not.Null);
      Assert.That(page.Items.Count, Is.EqualTo(1));
      NewsItem item = page.Items[0];
      Assert.That(item.Id, Is.EqualTo("123456"));
      Assert.That(item.Title, Is.EqualTo("Apple Q2"));
      Assert.That(item.Summary, Is.EqualTo("Earnings beat"));
      Assert.That(item.PubTimestamp, Is.EqualTo(1729413600L));
      Assert.That(item.Url, Is.EqualTo("https://example.com/news/123456"));
    }

    [Test]
    public void HighlightNewsResponse_Deserialize_FullEnvelope()
    {
      string json = "{\"code\":0,\"message\":\"success\",\"data\":{"
          + "\"items\":[{\"id\":\"789\",\"title\":\"Fed cuts rates\",\"summary\":\"FOMC decision\","
          + "\"pubTimestamp\":1761429600,"
          + "\"url\":\"https://example.com/highlight/detail?id=789\"}],"
          + "\"pageCount\":2,\"pageSize\":20,\"totalPage\":5,\"totalSize\":100}}";
      var response = JsonConvert.DeserializeObject<NewsHighlightListResponse>(json, TigerClient.JsonSet);
      Assert.That(response!.IsSuccess(), Is.True);
      NewsPageItem page = response.Data!;
      Assert.That(page.PageCount, Is.EqualTo(2));
      Assert.That(page.PageSize, Is.EqualTo(20));
      Assert.That(page.TotalPage, Is.EqualTo(5));
      Assert.That(page.TotalSize, Is.EqualTo(100));
      Assert.That(page.Items, Is.Not.Null);
      Assert.That(page.Items.Count, Is.EqualTo(1));
      Assert.That(page.Items[0].Id, Is.EqualTo("789"));
      Assert.That(page.Items[0].Title, Is.EqualTo("Fed cuts rates"));
      Assert.That(page.Items[0].PubTimestamp, Is.EqualTo(1761429600L));
    }

    [Test]
    public void NewsResponse_Deserialize_EmptyItems_IsSuccess()
    {
      // 下游返回空 items 属正常成功（news_api_design.md §3.4），不报错
      string json = "{\"code\":0,\"message\":\"success\",\"data\":"
          + "{\"items\":[],\"pageCount\":1,\"pageSize\":20,\"totalPage\":0,\"totalSize\":0}}";
      var response = JsonConvert.DeserializeObject<NewsStockListResponse>(json, TigerClient.JsonSet);
      Assert.That(response!.IsSuccess(), Is.True);
      Assert.That(response.Data!.Items, Is.Not.Null);
      Assert.That(response.Data.Items.Count, Is.EqualTo(0));
    }

    [Test]
    public void NewsResponse_Deserialize_ErrorEnvelope()
    {
      string json = "{\"code\":1006,\"message\":\"biz param error\",\"data\":null}";
      var response = JsonConvert.DeserializeObject<NewsStockListResponse>(json, TigerClient.JsonSet);
      Assert.That(response!.IsSuccess(), Is.False);
      Assert.That(response.Message, Is.EqualTo("biz param error"));
      Assert.That(response.Data, Is.Null);
    }

    // ---------- 游标翻页语义（决策 #14） ----------

    [Test]
    public void HighlightCursor_TakesLastItemIdOfPreviousPage()
    {
      // 翻页语义：下一页 last_id = 上一页 items 最后一条的 id
      string firstPageJson = "{\"code\":0,\"data\":{\"items\":["
          + "{\"id\":\"1\",\"title\":\"first\"},{\"id\":\"2\",\"title\":\"second\"}],"
          + "\"pageCount\":1,\"pageSize\":2,\"totalPage\":2,\"totalSize\":4}}";
      var firstPage = JsonConvert.DeserializeObject<NewsHighlightListResponse>(firstPageJson, TigerClient.JsonSet);
      string lastId = firstPage!.Data!.Items[^1].Id;

      var request = NewsClient.NewMarketNewsRequest(null, lastId);
      _client.BuildParams(request);
      Assert.That(request.BizContent, Does.Contain("\"last_id\":\"2\""));
    }

    [Test]
    public void ConvenienceRequests_CarryMethodAndModel()
    {
      var stockRequest = NewsClient.NewStockNewsRequest("AAPL", Language.zh_CN, 1, 20);
      Assert.That(stockRequest.ApiMethodName, Is.EqualTo(NewsApiService.NEWS_STOCK_LIST));
      Assert.That(stockRequest.ModelValue, Is.InstanceOf<NewsStockListModel>());
      var stockModel = (NewsStockListModel)stockRequest.ModelValue!;
      Assert.That(stockModel.Symbol, Is.EqualTo("AAPL"));
      Assert.That(stockModel.PageCount, Is.EqualTo(1));
      Assert.That(stockModel.PageSize, Is.EqualTo(20));
      Assert.That(stockModel.Lang, Is.EqualTo(Language.zh_CN));

      var highlightRequest = NewsClient.NewMarketNewsRequest(Language.en_US, "789");
      Assert.That(highlightRequest.ApiMethodName, Is.EqualTo(NewsApiService.NEWS_HIGHLIGHT_LIST));
      Assert.That(highlightRequest.ModelValue, Is.InstanceOf<NewsHighlightListModel>());
      var highlightModel = (NewsHighlightListModel)highlightRequest.ModelValue!;
      Assert.That(highlightModel.LastId, Is.EqualTo("789"));
      Assert.That(highlightModel.Lang, Is.EqualTo(Language.en_US));
    }

    // ---------- 路由回归：news 必须走主网关，不得路由行情域名 ----------

    /// <summary>
    /// 等价于 Java SDK 的 testNewsRoutesToMainGatewayNotQuote：
    /// 域名花园对 TBNZ/TBSG 配置了 license-QUOTE 分流（/hkg-quote 路径），
    /// news 若被路由到行情集群属于错误分流。断言 news 客户端的主网关选择逻辑只认 COMMON，
    /// 并用行情客户端注册表做对照（news 方法不在 QuoteApiService 注册表中，
    /// QuoteClient.Validate 会拒绝 news 请求，news 流量不可能进入行情域名）。
    /// </summary>
    [Test]
    public void News_RoutesToMainGateway_NotQuoteServer()
    {
      var uriDict = new Dictionary<UriType, string>
      {
        { UriType.COMMON, "https://openapi.example.com/gateway" },
        { UriType.QUOTE, "https://hkg-quote.example.com/gateway" },
        { UriType.TRADE, "https://trade.example.com/gateway" }
      };
      Assert.That(NewsClient.SelectMainGatewayUrl(uriDict),
          Is.EqualTo("https://openapi.example.com/gateway"),
          "news must resolve to the main (COMMON) gateway, never the quote cluster");
    }

    [Test]
    public void News_MainGatewaySelection_NeverFallsBackToQuoteOrTrade()
    {
      // COMMON 缺失（理论场景，GetServerAddress 保证 COMMON 存在）时宁可失败，
      // 也不把资讯流量路由到行情/交易专用域名
      var quoteOnly = new Dictionary<UriType, string>
      {
        { UriType.QUOTE, "https://hkg-quote.example.com/gateway" },
        { UriType.TRADE, "https://trade.example.com/gateway" }
      };
      Assert.That(NewsClient.SelectMainGatewayUrl(quoteOnly), Is.EqualTo(string.Empty),
          "without a COMMON entry news must not silently fall back to the quote/trade cluster");
    }

    [Test]
    public void News_GetServerUri_AlwaysMainGateway_ForBothMethods()
    {
      _client.UseCustomServerUrl("https://main.example.com/gateway");
      var stockRequest = NewsClient.NewStockNewsRequest("AAPL", Language.zh_CN, 1, 20);
      var highlightRequest = NewsClient.NewMarketNewsRequest(Language.en_US, null);
      Assert.That(_client.GetServerUri(stockRequest), Is.EqualTo("https://main.example.com/gateway"));
      Assert.That(_client.GetServerUri(highlightRequest), Is.EqualTo("https://main.example.com/gateway"));
    }

    [Test]
    public void News_RegistrySeparation_QuoteClientRejectsNewsMethods()
    {
      // 对照组（等价 Java 的 QUOTE 类型仍走行情域名断言）：
      // news 方法不在行情注册表中，QuoteClient 会拒绝，防止 news 被误配到行情客户端
      Assert.That(QuoteApiService.IsQuoteApi(NewsApiService.NEWS_STOCK_LIST), Is.False);
      Assert.That(QuoteApiService.IsQuoteApi(NewsApiService.NEWS_HIGHLIGHT_LIST), Is.False);
      Assert.That(QuoteApiService.AllQuoteApiSet, Does.Not.Contain(NewsApiService.NEWS_STOCK_LIST));
      Assert.That(QuoteApiService.AllQuoteApiSet, Does.Not.Contain(NewsApiService.NEWS_HIGHLIGHT_LIST));
      // 反向：行情方法不在 news 注册表中
      Assert.That(NewsApiService.IsNewsApi(QuoteApiService.MARKET_STATE), Is.False);
    }

    [Test]
    public void News_Validate_RejectsForeignMethod_WithParamError()
    {
      var quoteRequest = new TigerRequest<MarketStateResponse>
      {
        ApiMethodName = QuoteApiService.MARKET_STATE
      };
      bool valid = _client.Validate(quoteRequest, out string errorMsg);
      Assert.That(valid, Is.False);
      Assert.That(errorMsg, Does.Contain("is not news api"));

      var newsRequest = NewsClient.NewStockNewsRequest("AAPL", null, null, null);
      Assert.That(_client.Validate(newsRequest, out string _), Is.True);
    }

    /// <summary>
    /// 跳过 RefreshServerUri 的域名花园发现（唯一的构造期网络路径），实现零网络单测。
    /// </summary>
    private class TestableNewsClient : NewsClient
    {
      public TestableNewsClient(TigerConfig config) : base(config) { }

      protected override void RefreshServerUri(TigerConfig config)
      {
        // no domain-garden fetch in unit tests
      }
    }
  }
}
