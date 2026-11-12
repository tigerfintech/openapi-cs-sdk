using System;
using System.Collections.Generic;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.News.Model;
using TigerOpenAPI.News.Response;

namespace TigerOpenAPI.News
{
  /// <summary>
  /// 资讯客户端（news_stock_list / news_highlight_list），
  /// contract: stock-openapi docs/news_api_design.md §3。
  /// </summary>
  /// <remarks>
  /// 路由：news 系列接口必须走主网关（UriType.COMMON，即交易域方法缺省使用的统一网关），
  /// 不走行情集群分流——对齐 Java SDK 的 MethodType.NEWS 语义（只有 QUOTE 类型方法路由行情域名，
  /// 其余一律主网关）。域名花园对 TBNZ/TBSG 等配置了 license-QUOTE 分流，若误用 QuoteClient
  /// 承载 news 请求会把资讯流量打进行情集群。本客户端结构上不存在行情 URL 分支，
  /// GetServerUri 对任意请求恒返回主网关地址。
  /// </remarks>
  public class NewsClient : TigerClient
  {
    private string ServerUrl { get; set; }

    public NewsClient(TigerConfig config) : base(config)
    {
      ApiLogger.Debug($"NewsClient env:{config.Environment}, license:{config.License}");
      RefreshServerUri(config);

      TokenManager.GetInstance().Init(config, this);
    }

    protected override void RefreshServerUri(TigerConfig config)
    {
      // get serverAddress by Env and license
      Dictionary<UriType, string> uriDict = NetworkUtil.GetServerAddress(Protocol.HTTP, config.License, config.Environment);
      ServerUrl = SelectMainGatewayUrl(uriDict);
    }

    /// <summary>
    /// news 请求恒走主网关（COMMON）：与 QuoteClient（优先 QUOTE 分流域名）相对，
    /// 本方法只认 COMMON，任何情况下都不会把资讯流量路由到行情域名。
    /// </summary>
    internal static string SelectMainGatewayUrl(Dictionary<UriType, string> uriDict)
    {
      if (uriDict != null
          && uriDict.TryGetValue(UriType.COMMON, out string commonUrl)
          && !string.IsNullOrWhiteSpace(commonUrl))
      {
        return commonUrl;
      }
      return string.Empty;
    }

    public override string GetServerUri<T>(TigerRequest<T> request)
    {
      return ServerUrl;
    }

    public override void UseCustomServerUrl(string customServerUrl)
    {
      this.ServerUrl = customServerUrl;
      this.IsCustomServerUrl = true;
    }

    public override bool Validate<T>(TigerRequest<T> request, out string errorMsg)
    {
      errorMsg = string.Empty;
      if (!NewsApiService.IsNewsApi(request.ApiMethodName))
      {
        errorMsg = string.Format(TigerApiCode.HTTP_COMMON_PARAM_ERROR.Message, $"'ApiMethodName'({request?.ApiMethodName}) is not news api");
        return false;
      }
      // symbol / paging range / lang validity are validated by the server
      // (BIZ_PARAM_ERROR / silent en_US fallback, see news_api_design.md §3.1)
      return true;
    }

    /// <summary>
    /// 查询单只股票的资讯列表（method=news_stock_list）。
    /// </summary>
    /// <param name="symbol">股票代码，单标的必填——下游资讯服务实际不支持批量查询（传多个仅单个标的生效），
    /// 空值由服务端校验报参数错误</param>
    /// <param name="lang">语言（zh_CN/zh_TW/en_US）</param>
    /// <param name="pageCount">页码，null = 服务端默认 1；显式传值须 >= 1（服务端校验）</param>
    /// <param name="pageSize">每页条数，null = 服务端默认 20；显式传值须 1-50（闭区间，服务端校验）</param>
    /// <returns>个股新闻列表分页信封，见 <see cref="NewsPageItem"/></returns>
    public Task<NewsStockListResponse> GetStockNewsAsync(string symbol, Language? lang = null,
        int? pageCount = null, int? pageSize = null)
    {
      TigerRequest<NewsStockListResponse> request =
          NewStockNewsRequest(symbol, lang, pageCount, pageSize);
      return ExecuteAsync(request);
    }

    /// <summary>
    /// 查询市场要闻列表（method=news_highlight_list）。
    /// 翻页只走 last_id 游标：取上一页 items 最后一条的 id 作为 last_id；null 从最新开始。
    /// </summary>
    /// <param name="lang">语言（zh_CN/zh_TW/en_US）</param>
    /// <param name="lastId">游标翻页参数，取上一页 items 最后一条的 id；null = 从最新开始</param>
    /// <returns>要闻列表分页信封，见 <see cref="NewsPageItem"/>（该接口 PageCount 恒为 1）</returns>
    public Task<NewsHighlightListResponse> GetMarketNewsAsync(Language? lang = null, string lastId = null)
    {
      TigerRequest<NewsHighlightListResponse> request = NewMarketNewsRequest(lang, lastId);
      return ExecuteAsync(request);
    }

    /// <summary>Builds the news_stock_list request; also the test seam for wire-format checks.</summary>
    internal static TigerRequest<NewsStockListResponse> NewStockNewsRequest(string symbol,
        Language? lang, int? pageCount, int? pageSize)
    {
      var model = new NewsStockListModel(symbol, pageCount, pageSize);
      if (lang.HasValue)
      {
        model.Lang = lang.Value;
      }
      return new TigerRequest<NewsStockListResponse>
      {
        ApiMethodName = NewsApiService.NEWS_STOCK_LIST,
        ModelValue = model
      };
    }

    /// <summary>Builds the news_highlight_list request; also the test seam for wire-format checks.</summary>
    internal static TigerRequest<NewsHighlightListResponse> NewMarketNewsRequest(
        Language? lang, string lastId)
    {
      var model = new NewsHighlightListModel(lastId);
      if (lang.HasValue)
      {
        model.Lang = lang.Value;
      }
      return new TigerRequest<NewsHighlightListResponse>
      {
        ApiMethodName = NewsApiService.NEWS_HIGHLIGHT_LIST,
        ModelValue = model
      };
    }
  }
}
