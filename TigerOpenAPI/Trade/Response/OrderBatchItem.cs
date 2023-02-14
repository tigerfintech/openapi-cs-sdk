using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class OrderBatchItem
  {
    [JsonProperty(PropertyName = "nextPageToken")]
    public string NextPageToken;
    [JsonProperty(PropertyName = "items")]
    public List<TradeOrder> Items { get; set; }

    public OrderBatchItem()
    {
    }
  }
}

