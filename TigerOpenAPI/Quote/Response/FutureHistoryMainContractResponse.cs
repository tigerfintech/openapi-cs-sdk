using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureHistoryMainContractResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FutureHistoryMainContractItem> Data { get; set; }
  }
}

