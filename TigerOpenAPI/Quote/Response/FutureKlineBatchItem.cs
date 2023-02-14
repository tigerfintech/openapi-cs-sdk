using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureKlineBatchItem
  {
    [JsonProperty(PropertyName = "contractCode")]
    public string ContractCode;
    [JsonProperty(PropertyName = "nextPageToken")]
    public string NextPageToken;
    [JsonProperty(PropertyName = "items")]
    public List<FutureKlineItem> Items { get; set; }

    public FutureKlineBatchItem()
    {
    }
  }
}

