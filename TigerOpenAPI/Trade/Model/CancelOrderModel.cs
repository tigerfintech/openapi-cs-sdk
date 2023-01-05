using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TigerOpenAPI.Trade.Model
{
  public class CancelOrderModel : TradeModel
  {
    [JsonProperty(PropertyName = "id")]
    public long Id { get; set; }

    public CancelOrderModel() : base()
    {
    }
  }
}

