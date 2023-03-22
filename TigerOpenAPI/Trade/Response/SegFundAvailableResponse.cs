using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class SegFundAvailableResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<SegFundAvailableItem> Data { get; set; }
  }
}

