using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.News.Response
{
  /// <summary>
  /// 个股新闻列表响应（method=news_stock_list），
  /// data 分页信封结构见 NewsPageItem。
  /// </summary>
  public class NewsStockListResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public NewsPageItem Data { get; set; }

    public NewsStockListResponse() : base()
    {
    }
  }
}
