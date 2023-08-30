using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureHistoryMainContractItem
  {
    [JsonProperty(PropertyName = "contractCode")]
    public string ContractCode;
    [JsonProperty(PropertyName = "mainReferItems")]
    public List<FutureHistoryContractItem> Items { get; set; }

  }
}

