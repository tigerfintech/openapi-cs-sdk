using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class OrderTransactionsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public OrderTransactionsItem Data { get; set; }

    public OrderTransactionsResponse()
    {
    }
  }
}

