using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Trade.Response
{
  public class TickSizeItem
  {
    [JsonProperty(PropertyName = "begin")]
    public string Begin { get; set; }
    [JsonProperty(PropertyName = "end")]
    public string End { get; set; }
    [JsonProperty(PropertyName = "type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public TickSizeType Type { get; set; }
    [JsonProperty(PropertyName = "tickSize")]
    public double TickSize { get; set; }

    public TickSizeItem()
    {
    }
  }
}

