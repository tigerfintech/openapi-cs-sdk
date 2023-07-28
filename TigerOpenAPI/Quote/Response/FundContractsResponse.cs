using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FundContractsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FundContractItem> Data { get; set; }
  }
}

