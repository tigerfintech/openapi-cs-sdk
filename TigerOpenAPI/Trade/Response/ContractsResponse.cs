using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class ContractsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, List<ContractItem>> Data { get; set; }

    public ContractsResponse()
    {
    }
  }
}

