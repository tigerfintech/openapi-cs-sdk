using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class TickPoint
  {
    [JsonProperty(PropertyName = "price")]
    public Double Price { get; set; }
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

  }
}

