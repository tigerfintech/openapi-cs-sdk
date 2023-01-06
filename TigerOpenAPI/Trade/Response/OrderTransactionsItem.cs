using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class OrderTransactionsItem
  {
    [JsonProperty(PropertyName = "items")]
    public List<OrderTransactions> Items { get; set; }

    public OrderTransactionsItem()
    {
    }
  }
}

