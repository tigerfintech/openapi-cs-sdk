using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class PrimeAnalyticsAssetModel : TradeModel
  {
    [JsonProperty(PropertyName = "seg_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SegmentType SegType { get; set; }

    [JsonProperty(PropertyName = "currency"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Currency Currency { get; set; }

    [JsonProperty(PropertyName = "sub_account")]
    public string SubAccount { get; set; }

    // yyyy-MM-dd
    [JsonProperty(PropertyName = "start_date")]
    public string StartDate { get; set; }
    // yyyy-MM-dd
    [JsonProperty(PropertyName = "end_date")]
    public string EndDate { get; set; }

    public PrimeAnalyticsAssetModel() : base()
    {
    }
  }
}

