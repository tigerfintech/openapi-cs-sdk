using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TigerOpenAPI.News.Response
{
  /// <summary>
  /// 资讯列表分页信封，news_stock_list 与 news_highlight_list 共用。
  /// 服务端响应为 camelCase（决策 #13）；news_highlight_list 的 PageCount 恒为 1
  /// （该接口游标翻页，见 news_api_design.md 决策 #14）。
  /// contract: stock-openapi docs/news_api_design.md §3.3
  /// </summary>
  public class NewsPageItem
  {
    /// <summary>当前页资讯列表。</summary>
    [JsonProperty(PropertyName = "items")]
    public List<NewsItem> Items { get; set; }

    /// <summary>当前页码（highlight 接口恒为 1）。</summary>
    [JsonProperty(PropertyName = "pageCount")]
    public int PageCount { get; set; }

    /// <summary>每页条数。</summary>
    [JsonProperty(PropertyName = "pageSize")]
    public int PageSize { get; set; }

    /// <summary>总页数。</summary>
    [JsonProperty(PropertyName = "totalPage")]
    public int TotalPage { get; set; }

    /// <summary>总条数。</summary>
    [JsonProperty(PropertyName = "totalSize")]
    public int TotalSize { get; set; }

    public NewsPageItem()
    {
    }
  }
}
