using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class TimelinePoint
  {
    [JsonProperty(PropertyName = "price")]
    public Double Price { get; set; }
    [JsonProperty(PropertyName = "avgPrice")]
    public Double AvgPrice { get; set; }

    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }

  }
}

