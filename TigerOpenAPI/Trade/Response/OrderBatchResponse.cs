using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class OrderBatchResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public OrderBatchItem Data { get; set; }

    public OrderBatchResponse()
    {
    }
  }
}

