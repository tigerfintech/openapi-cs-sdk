using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureTickItem
  {
    [JsonProperty(PropertyName = "index")]
    public long Index { get; set; }
    [JsonProperty(PropertyName = "price")]
    public decimal Price { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }

    public FutureTickItem()
    {
    }
  }
}

