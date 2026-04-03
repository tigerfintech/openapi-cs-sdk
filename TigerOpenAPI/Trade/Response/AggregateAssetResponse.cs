using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class AggregateAssetResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public AggregateAssetItem Data { get; set; }
  }
}
