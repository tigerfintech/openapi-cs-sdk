using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class SingleOrderResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public TradeOrder Data { get; set; }

    public SingleOrderResponse()
    {
    }
  }
}

