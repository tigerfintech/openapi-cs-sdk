using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class MarketScannerModel : ApiModel
  {
    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    [JsonProperty(PropertyName = "base_filter_list")]
    public List<BaseFilter> BaseFilterList { get; set; }

    [JsonProperty(PropertyName = "accumulate_filter_list")]
    public List<AccumulateFilter> AccumulateFilterList { get; set; }

    [JsonProperty(PropertyName = "financial_filter_list")]
    public List<FinancialFilter> FinancialFilterList { get; set; }

    [JsonProperty(PropertyName = "multi_tags_filter_list")]
    public List<MultiTagsRelationFilter> MultiTagsFilterList { get; set; }

    [JsonProperty(PropertyName = "sort_field_data")]
    public SortFieldData SortFieldData { get; set; }

    [JsonProperty(PropertyName = "page")]
    public int Page { get; set; }

    [JsonProperty(PropertyName = "page_size")]
    public int PageSize { get; set; }

    public MarketScannerModel() : base()
    {
    }
  }

  public class BaseFilter
  {
    /** StockField 简单属性 */
    [JsonProperty(PropertyName = "field_name"), Newtonsoft.Json.JsonConverter(typeof(EnumNameConverter))]
    public StockField FieldName { get; set; }
    /** 区间下限（闭区间），不传代表下限为 -∞ */
    [JsonProperty(PropertyName = "filter_min")]
    public Double FilterMin { get; set; }
    /** 区间上限（闭区间），不传代表上限为 +∞ */
    [JsonProperty(PropertyName = "filter_max")]
    public Double FilterMax { get; set; }
    /** 该字段是否不需要筛选，True：不筛选，False：筛选。不传默认筛选 */
    [JsonProperty(PropertyName = "is_no_filter")]
    public bool IsNoFilter { get; set; } = false;
  }

  public class AccumulateFilter
  {
    /** AccumulateField 累积属性 */
    [JsonProperty(PropertyName = "field_name"), Newtonsoft.Json.JsonConverter(typeof(EnumNameConverter))]
    public AccumulateField FieldName { get; set; }
    /** 区间下限（闭区间），不传代表下限为 -∞ 如果为百分位数，不需要加%，例如10%，数值为10即可 */
    [JsonProperty(PropertyName = "filter_min")]
    public Double FilterMin { get; set; }
    /** 区间上限（闭区间），不传代表上限为 +∞ */
    [JsonProperty(PropertyName = "filter_max")]
    public Double FilterMax { get; set; }
    /** 该字段是否不需要筛选，True：不筛选，False：筛选。不传默认筛选 */
    [JsonProperty(PropertyName = "is_no_filter")]
    public bool IsNoFilter { get; set; } = false;
    /** 时间周期 AccumulatePeriod 非必传项 */
    [JsonProperty(PropertyName = "period"), Newtonsoft.Json.JsonConverter(typeof(EnumNameConverter))]
    public AccumulatePeriod Period { get; set; }
  }

  public class FinancialFilter
  {
    /** FinancialField 财务属性 */
    [JsonProperty(PropertyName = "field_name"), Newtonsoft.Json.JsonConverter(typeof(EnumNameConverter))]
    public FinancialField FieldName { get; set; }
    /** 区间下限（闭区间），不传代表下限为 -∞ 如果为百分位数，不需要加%，例如10%，数值为10即可 */
    [JsonProperty(PropertyName = "filter_min")]
    public Double FilterMin { get; set; }
    /** 区间上限（闭区间），不传代表上限为 +∞ */
    [JsonProperty(PropertyName = "filter_max")]
    public Double FilterMax { get; set; }
    /** 该字段是否不需要筛选，True：不筛选，False：筛选。不传默认筛选 */
    [JsonProperty(PropertyName = "is_no_filter")]
    public bool IsNoFilter { get; set; } = false;
    /** FinancialQuarter 财报累积时间 */
    [JsonProperty(PropertyName = "quarter"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public FinancialPeriod Quarter { get; set; }
  }

  public class MultiTagsRelationFilter
  {

    /** MultiTagField 获取标签枚举值 */
    [JsonProperty(PropertyName = "field_name"), Newtonsoft.Json.JsonConverter(typeof(EnumNameConverter))]
    public MultiTagField FieldName { get; set; }
    /** 多个tag列表 */
    [JsonProperty(PropertyName = "tag_list")]
    public List<string> TagList { get; set; }
    /** 该字段是否不需要筛选，True：不筛选，False：筛选。不传默认筛选 */
    [JsonProperty(PropertyName = "is_no_filter")]
    public bool IsNoFilter { get; set; } = false;
  }

  public class SortFieldData
  {
    /** 排序属性 */
    [JsonProperty(PropertyName = "field_name")]
    public Int32 FieldName { get; set; }
    /** */
    [JsonProperty(PropertyName = "period")]
    private Int32 Period { get; set; }
    /** 排序属性所属类别 参考 FieldBelongType */
    [JsonProperty(PropertyName = "field_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public FieldBelongType FieldType { get; set; }
    /** SortDir 排序方向，默认不排序。 */
    [JsonProperty(PropertyName = "sort_dir"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SortDir SortDir { get; set; }
  }
}

