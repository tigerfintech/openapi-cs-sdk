using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.News.Model
{
  /// <summary>
  /// news_stock_list 请求参数（wire key 为 snake_case，对齐服务端入参约定）。
  /// contract: stock-openapi docs/news_api_design.md §3.1
  /// </summary>
  /// <remarks>
  /// <para>symbol 必填、单标的——下游资讯服务实际不支持批量查询（传多个仅单个标的生效，决策 #5），
  /// 空值由服务端校验报参数错误；</para>
  /// <para>page_count / page_size 可选，null（缺省）由服务端归一化为 1 / 20（决策 #6）；
  /// 显式传值须满足 page_count >= 1、1 &lt;= page_size &lt;= 50（闭区间，服务端校验）；</para>
  /// <para>lang 为两接口共用可选参数（见 ApiModel.Lang，zh_CN/zh_TW/en_US）。</para>
  /// </remarks>
  public class NewsStockListModel : ApiModel
  {
    /// <summary>必填。单标的：下游资讯接口实际不支持批量查询。</summary>
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    /// <summary>可选。页码，缺省（null）由服务端归一化为 1；显式传值须 >= 1（服务端校验）。</summary>
    [JsonProperty(PropertyName = "page_count")]
    public int? PageCount { get; set; }

    /// <summary>可选。每页条数，缺省（null）由服务端归一化为 20；显式传值须 1-50（闭区间，服务端校验）。</summary>
    [JsonProperty(PropertyName = "page_size")]
    public int? PageSize { get; set; }

    public NewsStockListModel() : base()
    {
    }

    public NewsStockListModel(string symbol, int? pageCount = null, int? pageSize = null) : base()
    {
      Symbol = symbol;
      PageCount = pageCount;
      PageSize = pageSize;
    }
  }
}
