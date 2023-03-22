using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class SegFundsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<SegFundItem> Data { get; set; }
  }
}

