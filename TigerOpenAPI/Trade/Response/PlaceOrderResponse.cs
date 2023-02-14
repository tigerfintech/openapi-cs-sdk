using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class PlaceOrderResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public PlaceOrderItem Data { get; set; }

    public PlaceOrderResponse()
    {
    }
  }
}

