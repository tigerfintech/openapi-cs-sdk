using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class MarketScannerTagsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<MarketScannerTagItem> Data { get; set; }

    public MarketScannerTagsResponse()
    {
    }
  }
}

