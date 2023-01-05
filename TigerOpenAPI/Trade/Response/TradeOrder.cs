﻿using System;
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
    [JsonProperty(PropertyName = "filledQuantity")]
    public Int64 FilledQuantity { get; set; }
    [JsonProperty(PropertyName = "cashQuantity")]
    public Double CashQuantity { get; set; }
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

    [JsonProperty(PropertyName = "commission")]
    public Double Commission { get; set; }
    [JsonProperty(PropertyName = "realizedPnl")]
    public Double RealizedPnl { get; set; }
    [JsonProperty(PropertyName = "remark")]
    public string Remark { get; set; }
    // Is it a closing order
    [JsonProperty(PropertyName = "liquidation")]
    public Boolean Liquidation { get; set; }

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

    [JsonProperty(PropertyName = "source")]
    public string Source { get; set; }
    [JsonProperty(PropertyName = "discount")]
    public Double Discount { get; set; }

    [JsonProperty(PropertyName = "canModify")]
    public Boolean CanModify { get; set; }
    [JsonProperty(PropertyName = "canCancel")]
    public Boolean CanCancel { get; set; }

    public TradeOrder()
    {
    }
  }
}

