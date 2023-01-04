using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class ContractResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public ContractItem Data { get; set; }

    public ContractResponse()
    {
    }
  }
}

