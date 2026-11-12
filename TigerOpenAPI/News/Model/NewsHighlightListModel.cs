using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.News.Model
{
  /// <summary>
  /// news_highlight_list 请求参数（wire key 为 snake_case，对齐服务端入参约定）。
  /// contract: stock-openapi docs/news_api_design.md §3.2
  /// </summary>
  /// <remarks>
  /// <para>翻页只走 last_id 游标（服务端决策 #14 撤除 page_count，调下游固定 pageCount=1，
  /// 响应信封的 pageCount 恒为 1）：</para>
  /// <para>last_id 取上一页 items 最后一条的 id 作为下一页游标；null（缺省）从最新开始；</para>
  /// <para>lang 为两接口共用可选参数（见 ApiModel.Lang，规则同 NewsStockListModel）。</para>
  /// </remarks>
  public class NewsHighlightListModel : ApiModel
  {
    /// <summary>可选游标——唯一翻页方式，取上一页 items 最后一条的 id；null = 从最新开始。</summary>
    [JsonProperty(PropertyName = "last_id")]
    public string LastId { get; set; }

    public NewsHighlightListModel() : base()
    {
    }

    public NewsHighlightListModel(string lastId = null) : base()
    {
      LastId = lastId;
    }
  }
}
