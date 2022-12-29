using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionTradeTickPoint
  {

    [JsonProperty(PropertyName = "price")]
    public double Price { get; set; }
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }

    public OptionTradeTickPoint()
    {
    }
  }
}

