using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Trade.Model
{
  public class OrderTransactionsModel : TradeModel
  {
    // ’order_id‘ is the 'id' returned after placing an order
    [JsonProperty(PropertyName = "order_id")]
    public Int64 OrderId { get; set; }

    [JsonProperty(PropertyName = "sec_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SecType SecType { get; set; }

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "right")]
    public string? Right { get; set; }

    [JsonProperty(PropertyName = "strike")]
    public string? Strike { get; set; }

    [JsonProperty(PropertyName = "expiry")]
    public string? Expiry { get; set; }

    [JsonProperty(PropertyName = "start_date")]
    public Int64 StartDate { get; set; }

    [JsonProperty(PropertyName = "end_date")]
    public Int64 EndDate { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public Int32 Limit { get; set; } = 20;

    public OrderTransactionsModel() : base()
    {
    }
  }
}

