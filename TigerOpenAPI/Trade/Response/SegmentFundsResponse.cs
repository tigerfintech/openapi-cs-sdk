using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class SegmentFundsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<SegmentFundItem> Data { get; set; }
  }
}

