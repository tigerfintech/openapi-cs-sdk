using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class WarrantItem
  {

    /** symbol */
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    /** name */
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    /** warrant type 1：Call，2：Put，3：Bull，4：Bear */
    [JsonProperty(PropertyName = "type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public WarrantType Type { get; set; }
    /** security type */
    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }
    /** market */
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }

    /** entitlement ratio */
    [JsonProperty(PropertyName = "entitlementRatio")]
    public Double EntitlementRatio { get; set; }
    /** entitlement price */
    [JsonProperty(PropertyName = "entitlementPrice")]
    public Double EntitlementPrice { get; set; }
    /** premium */
    [JsonProperty(PropertyName = "premium")]
    public Double Premium { get; set; }
    /** breakeven point */
    [JsonProperty(PropertyName = "breakevenPoint")]
    public Double BreakevenPoint { get; set; }
    /** call price */
    [JsonProperty(PropertyName = "callPrice")]
    public Double CallPrice { get; set; }
    /**
     * before call level(%)
     */
    [JsonProperty(PropertyName = "beforeCallLevel")]
    public Double BeforeCallLevel { get; set; }
    /** expiry date，formate：yyyy-MM-dd */
    [JsonProperty(PropertyName = "expireDate")]
    public string ExpireDate { get; set; }
    /** last trading date, formate: yyyy-MM-dd */
    [JsonProperty(PropertyName = "lastTradingDate")]
    public string LastTradingDate { get; set; }
    /** state */
    [JsonProperty(PropertyName = "state"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public WarrantState State { get; set; }

    /**
     * change rate
     */
    [JsonProperty(PropertyName = "changeRate")]
    public Double changeRate { get; set; }
    /**
     * change amount
     */
    [JsonProperty(PropertyName = "change")]
    public Double Change { get; set; }
    [JsonProperty(PropertyName = "latestPrice")]
    public Double LatestPrice { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "amount")]
    public Double Amount { get; set; }
    /** outstanding ratio */
    [JsonProperty(PropertyName = "outstandingRatio")]
    public Double OutstandingRatio { get; set; }
    /** lot size */
    [JsonProperty(PropertyName = "lotSize")]
    public int LotSize { get; set; }
    /** strike price */
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    /** in/out */
    [JsonProperty(PropertyName = "inOutPrice")]
    public Double InOutPrice { get; set; }
    /** delta */
    [JsonProperty(PropertyName = "delta")]
    public Double Delta { get; set; }
    /** leverage ratio */
    [JsonProperty(PropertyName = "leverageRatio")]
    public Double LeverageRatio { get; set; }
    /**
     * effective leverage
     */
    [JsonProperty(PropertyName = "effectiveLeverage")]
    public Double EffectiveLeverage { get; set; }
    /**
     * implied volatility
     */
    [JsonProperty(PropertyName = "impliedVolatility")]
    public Double ImpliedVolatility { get; set; }

    public WarrantItem()
    {
    }
  }
}

