using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureTickBatchItem
  {
    [JsonProperty(PropertyName = "contractCode")]
    public string ContractCode;
    [JsonProperty(PropertyName = "items")]
    public List<FutureTickItem> Items { get; set; }

    public FutureTickBatchItem()
    {
    }
  }
}

