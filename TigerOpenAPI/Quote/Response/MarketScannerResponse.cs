using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class MarketScannerResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public MarketScannerPageItem Data { get; set; }

    public MarketScannerResponse()
    {
    }
  }
}

