using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class KlinePoint
  {
    [JsonProperty(PropertyName = "open")]
    public Double Open { get; set; }
    [JsonProperty(PropertyName = "high")]
    public Double High { get; set; }
    [JsonProperty(PropertyName = "low")]
    public Double Low { get; set; }
    [JsonProperty(PropertyName = "close")]
    public Double Close { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "amount")]
    public Double Amount { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }

  }
}

