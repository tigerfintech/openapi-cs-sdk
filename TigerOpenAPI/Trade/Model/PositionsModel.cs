using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class PositionsModel : TradeModel
  {
    [JsonProperty(PropertyName = "sec_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SecType SecType { get; set; }

    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    [JsonProperty(PropertyName = "currency"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Currency Currency { get; set; }

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }

    [JsonProperty(PropertyName = "strike")]
    public Double Strike { get; set; }

    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }

    public PositionsModel() : base()
    {
    }
  }
}

