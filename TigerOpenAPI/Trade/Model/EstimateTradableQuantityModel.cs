using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class EstimateTradableQuantityModel : TradeModel
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "right")]
    public string? Right { get; set; }

    [JsonProperty(PropertyName = "strike")]
    public string? Strike { get; set; }

    [JsonProperty(PropertyName = "expiry")]
    public string? Expiry { get; set; }

    [JsonProperty(PropertyName = "seg_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SegmentType SegType { get; set; }

    [JsonProperty(PropertyName = "sec_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SecType SecType { get; set; }

    [JsonProperty(PropertyName = "action"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public ActionType Action { get; set; }

    [JsonProperty(PropertyName = "order_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public OrderType OrderType { get; set; }

    [JsonProperty(PropertyName = "limit_price")]
    public Double LimitPrice { get; set; }

    [JsonProperty(PropertyName = "stop_price")]
    public Double StopPrice { get; set; }

    public EstimateTradableQuantityModel() : base()
    {
    }
  }
}

