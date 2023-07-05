using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class ForexTradeOrderModel : TradeModel
  {

    [JsonProperty(PropertyName = "source_currency"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Currency SourceCurrency { get; set; }

    [JsonProperty(PropertyName = "source_amount")]
    public Double SourceAmount { get; set; }

    [JsonProperty(PropertyName = "target_currency"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Currency TargetCurrency { get; set; }

    [JsonProperty(PropertyName = "seg_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SegmentType SegType { get; set; }

    [JsonProperty(PropertyName = "external_id")]
    public string externalId;

    /**
     * order validity time range, forex order only support 'DAY' and 'GTC', default is 'DAY'
     * DAY：valid for the day
     * GTC：valid until cancelled
     */
    [JsonProperty(PropertyName = "time_in_force"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public TimeInForce TimeInForce { get; set; } = TimeInForce.DAY;

    public ForexTradeOrderModel() : base()
    {
    }
  }
}

