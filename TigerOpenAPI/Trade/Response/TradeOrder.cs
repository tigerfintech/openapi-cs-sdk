using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Trade.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class TradeOrder
  {
    /**
     * contrat info
     */
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "multiplier")]
    public double Multiplier { get; set; }
    [JsonProperty(PropertyName = "identifier")]
    public string Identifier { get; set; }

    /**
     * order base info
     */
    [JsonProperty(PropertyName = "id")]
    public Int64 Id { get; set; }
    [JsonProperty(PropertyName = "orderId")]
    public int OrderId { get; set; }
    [JsonProperty(PropertyName = "externalId")]
    public string ExternalId { get; set; }
    [JsonProperty(PropertyName = "parentId")]
    public Int64 ParentId { get; set; }
    [JsonProperty(PropertyName = "account")]
    public string Account { get; set; }
    [JsonProperty(PropertyName = "action")]
    public string Action { get; set; }
    [JsonProperty(PropertyName = "orderType")]
    public string OrderType { get; set; }
    [JsonProperty(PropertyName = "limitPrice")]
    public Double LimitPrice { get; set; }
    [JsonProperty(PropertyName = "auxPrice")]
    public Double AuxPrice { get; set; }
    [JsonProperty(PropertyName = "trailingPercent")]
    public Double TrailingPercent { get; set; }

    //[JsonProperty(PropertyName = "trailStopPrice")]
    //public Double TrailStopPrice { get; set; }
    [JsonProperty(PropertyName = "totalQuantity")]
    public Int64 TotalQuantity { get; set; }
    [JsonProperty(PropertyName = "totalQuantityScale")]
    public Int32 TotalQuantityScale { get; set; }
    [JsonProperty(PropertyName = "filledQuantity")]
    public Int64 FilledQuantity { get; set; }
    [JsonProperty(PropertyName = "filledQuantityScale")]
    public Int32 FilledQuantityScale { get; set; }
    [JsonProperty(PropertyName = "totalCashAmount")]
    public Double TotalCashAmount { get; set; }
    [JsonProperty(PropertyName = "filledCashAmount")]
    public Double FilledCashAmount { get; set; }
    [JsonProperty(PropertyName = "refundCashAmount")]
    public Double RefundCashAmount { get; set; }
    [JsonProperty(PropertyName = "lastFillPrice")]
    public Double LastFillPrice { get; set; }
    [JsonProperty(PropertyName = "avgFillPrice")]
    public Double AvgFillPrice { get; set; }
    [JsonProperty(PropertyName = "timeInForce")]
    public string TimeInForce { get; set; }
    [JsonProperty(PropertyName = "expireTime")]
    public Int64 ExpireTime { get; set; }
    [JsonProperty(PropertyName = "goodTillDate")]
    public string GoodTillDate { get; set; }
    [JsonProperty(PropertyName = "outsideRth")]
    public Boolean OutsideRth { get; set; }
    [JsonProperty(PropertyName = "tradingSessionType")]
    public string TradingSessionType { get; set; }

    [JsonProperty(PropertyName = "commission")]
    public Double Commission { get; set; }
    /** Goods and Services Tax (TBSG only) */
    [JsonProperty(PropertyName = "gst")]
    public Double Gst { get; set; }
    [JsonProperty(PropertyName = "realizedPnl")]
    public Double RealizedPnl { get; set; }
    [JsonProperty(PropertyName = "remark")]
    public string Remark { get; set; }
    // Is it a forced liquidation order
    [JsonProperty(PropertyName = "liquidation")]
    public Boolean Liquidation { get; set; }
    [JsonProperty(PropertyName = "triggerStatus")]
    public string TriggerStatus { get; set; }

    [JsonProperty(PropertyName = "openTime")]
    public Int64 OpenTime { get; set; }
    [JsonProperty(PropertyName = "updateTime")]
    public Int64 UpdateTime { get; set; }
    [JsonProperty(PropertyName = "latestTime")]
    public Int64 LatestTime { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "latestPrice")]
    public Double LatestPrice { get; set; }

    [JsonProperty(PropertyName = "attrDesc")]
    public string AttrDesc { get; set; }
    [JsonProperty(PropertyName = "userMark")]
    public string UserMark { get; set; }
    [JsonProperty(PropertyName = "attrList")]
    public List<string> AttrList { get; set; }

    /** charge details. Only valid when querying order by ID and 'IsShowCharges' = true */
    [JsonProperty(PropertyName = "charges")]
    public List<Charge> Charges;
    /** commission discount amount. Only valid when querying order by ID */
    [JsonProperty(PropertyName = "commissionDiscountAmount")]
    public Double CommissionDiscountAmount;
    /** order discount status. 1：unfinished；2：finished；0：default */
    [JsonProperty(PropertyName = "orderDiscount")]
    public Int32 OrderDiscount;
    /** order discount amount. Only valid when querying order by ID */
    [JsonProperty(PropertyName = "orderDiscountAmount")]
    public Double OrderDiscountAmount;

    /**
     * OCA order
     */
    [JsonProperty(PropertyName = "ocaGroupId")]
    public int OcaGroupId { get; set; }
    /**
     * Combination order
     */
    [JsonProperty(PropertyName = "comboLegs")]
    public string ComboLegs { get; set; }

    /**
     * FA order
     */
    [JsonProperty(PropertyName = "allocAccounts")]
    public List<string> AllocAccounts { get; set; }
    [JsonProperty(PropertyName = "allocShares")]
    public List<Double> AllocShares { get; set; }

    /**
     * algorithmic order
     */
    [JsonProperty(PropertyName = "algoStrategy")]
    public string AlgoStrategy { get; set; }
    [JsonProperty(PropertyName = "algoParameters")]
    public List<TagValue> AlgoParameters { get; set; }

    [JsonProperty(PropertyName = "status"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public OrderStatus Status { get; set; }

    /** order replace status(NONE, RECEIVED, REPLACED, FAILED) */
    public string ReplaceStatus { get; set; }
    /** order cancel status(NONE, RECEIVED, FAILED) */
    public string CancelStatus { get; set; }

    [JsonProperty(PropertyName = "source")]
    public string Source { get; set; }
    [JsonProperty(PropertyName = "discount")]
    public Int32 Discount { get; set; }

    [JsonProperty(PropertyName = "canModify")]
    public Boolean CanModify { get; set; }
    [JsonProperty(PropertyName = "canCancel")]
    public Boolean CanCancel { get; set; }
    [JsonProperty(PropertyName = "isOpen")]
    public Boolean IsOpen { get; set; }

    [JsonProperty(PropertyName = "comboType")]
    public string ComboType { get; set; }
    [JsonProperty(PropertyName = "comboTypeDesc")]
    public string ComboTypeDesc { get; set; }
    /** order's multi leg info */
    [JsonProperty(PropertyName = "legs")]
    public List<OrderLeg> Legs { get; set; }

    public TradeOrder()
    {
    }
  }
}

