using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TigerOpenAPI.Trade.Model
{
  public class ModifyOrderModel : TradeModel
  {
    [JsonProperty(PropertyName = "id")]
    public long Id { get; set; }

    [JsonProperty(PropertyName = "total_quantity")]
    public Int64? TotalQuantity { get; set; }
    [JsonProperty(PropertyName = "total_quantity_scale")]
    public Int32? TotalQuantityScale { get; set; }

    [JsonProperty(PropertyName = "limit_price")]
    public Double? LimitPrice { get; set; }

    /**
     * stop loss price. This parameter is required when order_type is STP and STP_LMT,
     * when order_type is TRAIL, aux_price is the stop loss trailing amount
     */
    [JsonProperty(PropertyName = "aux_price")]
    public Double? AuxPrice { get; set; }

    /**
     * Trailing Stop Order - trailing percentage. When order_type is TRAIL,
     * trailing_percent is preferred when both aux_price and trailing_percent have values
     */
    [JsonProperty(PropertyName = "trailing_percent")]
    public Double? TrailingPercent { get; set; }

    [JsonProperty(PropertyName = "cash_amount")]
    public Double? CashAmount { get; set; }

    public ModifyOrderModel() : base()
    {
    }
  }
}

