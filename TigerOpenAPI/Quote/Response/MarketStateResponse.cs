using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class MarketStateResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<MarketState> Data { get; set; }

    public MarketStateResponse()
    {
    }
  }
}

