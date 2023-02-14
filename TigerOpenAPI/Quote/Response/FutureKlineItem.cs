using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureKlineItem
  {
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }
    [JsonProperty(PropertyName = "lastTime")]
    public long LastTime { get; set; }
    [JsonProperty(PropertyName = "open")]
    public decimal Open { get; set; }
    [JsonProperty(PropertyName = "close")]
    public decimal Close { get; set; }
    [JsonProperty(PropertyName = "high")]
    public decimal High { get; set; }
    [JsonProperty(PropertyName = "low")]
    public decimal Low { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "openInterest")]
    public long OpenInterest { get; set; }
    [JsonProperty(PropertyName = "settlement")]
    public decimal Settlement { get; set; }

    public FutureKlineItem()
    {
    }
  }
}

