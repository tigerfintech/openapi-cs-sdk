using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.News.Response
{
  /// <summary>
  /// 市场要闻列表响应（method=news_highlight_list），
  /// data 分页信封结构见 NewsPageItem（该接口 PageCount 恒为 1，游标翻页）。
  /// </summary>
  public class NewsHighlightListResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public NewsPageItem Data { get; set; }

    public NewsHighlightListResponse() : base()
    {
    }
  }
}
