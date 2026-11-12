using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.News.Response
{
  /// <summary>
  /// 资讯条目，news_stock_list 与 news_highlight_list 共用。
  /// 服务端响应为 camelCase（决策 #13），字段名与 JSON key 相同。
  /// contract: stock-openapi docs/news_api_design.md §3.3
  /// </summary>
  public class NewsItem
  {
    /// <summary>文章 ID（同时是 highlight 游标翻页的 last_id 取值来源）。</summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>标题。</summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    /// <summary>摘要。</summary>
    [JsonProperty(PropertyName = "summary")]
    public string Summary { get; set; }

    /// <summary>发布时间戳（秒级）。</summary>
    [JsonProperty(PropertyName = "pubTimestamp")]
    public long PubTimestamp { get; set; }

    /// <summary>详情页 URL。</summary>
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    public NewsItem()
    {
    }
  }
}
