using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class PlaceOrderItem
  {
    [JsonProperty(PropertyName = "id")]
    public long Id { get; set; }
    [JsonProperty(PropertyName = "subIds")]
    public List<Int64> SubIds { get; set; }
    [JsonProperty(PropertyName = "orders")]
    public List<TradeOrder> Orders { get; set; }

    public PlaceOrderItem()
    {
    }
  }
}

