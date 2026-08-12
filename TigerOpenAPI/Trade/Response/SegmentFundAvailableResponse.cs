using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Trade.Response
{
  public class SegmentFundAvailableResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<SegmentFundAvailableItem> Data { get; set; }
  }
}

