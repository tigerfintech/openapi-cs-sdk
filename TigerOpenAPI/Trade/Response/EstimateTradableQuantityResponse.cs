using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class EstimateTradableQuantityResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public TradableQuantityItem Data { get; set; }
  }
}

