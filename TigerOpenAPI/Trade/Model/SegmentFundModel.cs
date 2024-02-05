using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class SegmentFundModel : TradeModel
  {
    [JsonProperty(PropertyName = "from_segment"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SegmentType FromSegment { get; set; }

    [JsonProperty(PropertyName = "to_segment"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SegmentType ToSegment { get; set; }

    [JsonProperty(PropertyName = "currency"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Currency Currency { get; set; }

    [JsonProperty(PropertyName = "amount")]
    public Double Amount { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public Int32 Limit;

    [JsonProperty(PropertyName = "id")]
    public Int64 Id;

    public SegmentFundModel() : base()
    {
    }
  }
}

