using System;
using Newtonsoft.Json;

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

