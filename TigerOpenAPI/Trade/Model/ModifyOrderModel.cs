using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Trade.Model
{
  public class ModifyOrderModel : TradeModel
  {
    [JsonProperty(PropertyName = "id")]
    public long Id { get; set; }

    [JsonProperty(PropertyName = "order_type", NullValueHandling = NullValueHandling.Ignore),
     Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public OrderType? OrderType { get; set; }

    [JsonProperty(PropertyName = "total_quantity")]
    public Int64? TotalQuantity { get; set; }
    [JsonProperty(PropertyName = "total_quantity_scale")]
    public Int32? TotalQuantityScale { get; set; }

    [JsonProperty(PropertyName = "limit_price")]
    public Double? LimitPrice { get; set; }

    [JsonProperty(PropertyName = "aux_price")]
    public Double? AuxPrice { get; set; }

    [JsonProperty(PropertyName = "trailing_percent")]
    public Double? TrailingPercent { get; set; }

    [JsonProperty(PropertyName = "cash_amount")]
    public Double? CashAmount { get; set; }

    /** 冰山单：展示数量 */
    [JsonProperty(PropertyName = "display_size", NullValueHandling = NullValueHandling.Ignore)]
    public Int64? DisplaySize { get; set; }

    /** 冰山单：最小展示数量 */
    [JsonProperty(PropertyName = "min_display_size", NullValueHandling = NullValueHandling.Ignore)]
    public Int64? MinDisplaySize { get; set; }

    /** 冰山单：价格类型（LIMIT_PRICE / OPPONENT_PRICE） */
    [JsonProperty(PropertyName = "price_type", NullValueHandling = NullValueHandling.Ignore)]
    public string? PriceType { get; set; }

    public ModifyOrderModel() : base()
    {
    }
  }
}

