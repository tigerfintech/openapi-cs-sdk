using System;
using Newtonsoft.Json;
using System.Collections.Generic;

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

