using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureContractResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public FutureContractItem Data { get; set; }
  }
}

