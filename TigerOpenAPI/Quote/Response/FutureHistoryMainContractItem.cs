using System;
using Newtonsoft.Json;
using System.Collections.Generic;

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

