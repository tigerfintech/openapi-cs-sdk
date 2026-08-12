using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

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

