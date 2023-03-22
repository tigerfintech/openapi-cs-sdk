using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class SegFundResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public SegFundItem Data { get; set; }
  }
}

