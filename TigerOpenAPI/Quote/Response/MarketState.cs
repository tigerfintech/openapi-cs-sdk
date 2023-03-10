using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class MarketState
  {
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "marketStatus")]
    public string MarketStatus { get; set; }
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }
    [JsonProperty(PropertyName = "openTime")]
    public string OpenTime { get; set; }

    public MarketState()
    {
    }
  }
}

