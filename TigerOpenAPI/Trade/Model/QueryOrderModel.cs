using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Trade.Model
{
  public class QueryOrderModel : TradeModel
  {
    [JsonProperty(PropertyName = "id")]
    public Int64 Id { get; set; }

    // Only valid for order query by id
    [JsonProperty(PropertyName = "show_charges")]
    public Boolean IsShowCharges { get; set; }

    [JsonProperty(PropertyName = "seg_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SegmentType SegType { get; set; }

    [JsonProperty(PropertyName = "sec_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SecType SecType { get; set; }

    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "right")]
    public string? Right { get; set; }

    [JsonProperty(PropertyName = "strike")]
    public string? Strike { get; set; }

    [JsonProperty(PropertyName = "expiry")]
    public string? Expiry { get; set; }

    [JsonProperty(PropertyName = "start_date")]
    public Int64 StartDate { get; set; }

    [JsonProperty(PropertyName = "end_date")]
    public Int64 EndDate { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public Int32 Limit { get; set; } = 100;

    [JsonProperty(PropertyName = "sort_by"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public OrderSortBy SortBy { get; set; }

    [JsonProperty(PropertyName = "page_token")]
    public string PageToken { get; set; }

    public QueryOrderModel() : base()
    {
    }
  }
}

