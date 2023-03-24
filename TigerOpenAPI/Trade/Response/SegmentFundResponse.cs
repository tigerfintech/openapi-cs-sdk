using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class SegmentFundResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public SegmentFundItem Data { get; set; }
  }
}

