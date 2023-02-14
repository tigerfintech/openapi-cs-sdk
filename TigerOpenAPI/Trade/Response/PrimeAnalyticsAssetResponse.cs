using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class PrimeAnalyticsAssetResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public PrimeAnalyticsAssetItem Data { get; set; }

    public PrimeAnalyticsAssetResponse()
    {
    }
  }
}

