using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class PrimeAssetResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public PrimeAssetItem Data { get; set; }

    public PrimeAssetResponse()
    {
    }
  }
}

