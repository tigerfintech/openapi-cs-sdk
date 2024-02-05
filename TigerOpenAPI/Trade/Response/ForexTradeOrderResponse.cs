using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class ForexTradeOrderResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public TradeOrder Data { get; set; }
  }
}

