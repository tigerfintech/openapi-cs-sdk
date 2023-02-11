using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class WarrantQuote
  {

    /** symbol */
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    /** name */
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    /** exchange */
    [JsonProperty(PropertyName = "exchange")]
    public string Exchange { get; set; }
    /** security type */
    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }
    /** market */
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    /** currency */
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    /** expiry date，formate：yyyy-MM-dd */
    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }
    /** strike price */
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    /** PUT/CALL */
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }

    /** multiplier */
    [JsonProperty(PropertyName = "multiplier")]
    public Double Multiplier { get; set; }
    /** last trading date */
    [JsonProperty(PropertyName = "lastTradingDate")]
    public long LastTradingDate { get; set; }

    /** entitlement ratio */
    [JsonProperty(PropertyName = "entitlementRatio")]
    public Double EntitlementRatio { get; set; }
    /** entitlement price */
    [JsonProperty(PropertyName = "entitlementPrice")]
    public Double EntitlementPrice { get; set; }
    /** min tick */
    [JsonProperty(PropertyName = "minTick")]
    public Double MinTick { get; set; }
    /** listing date */
    [JsonProperty(PropertyName = "listingDate")]
    public long ListingDate { get; set; }
    /** call price（only IOPT） */
    [JsonProperty(PropertyName = "callPrice")]
    public Double CallPrice { get; set; }
    /** halted。0: Normal 3: Suspension 4: Delisted */
    [JsonProperty(PropertyName = "halted"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public HaltedStatus Halted { get; set; }
    /** underlying symbol */
    [JsonProperty(PropertyName = "underlyingSymbol")]
    public string UnderlyingSymbol { get; set; }
    /** timestamp */
    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

    /** latest price */
    [JsonProperty(PropertyName = "latestPrice")]
    public Double LatestPrice { get; set; }
    /** previous close price */
    [JsonProperty(PropertyName = "preClose")]
    public Double PreClose { get; set; }
    /** open price */
    [JsonProperty(PropertyName = "open")]
    public Double Open { get; set; }
    /** high */
    [JsonProperty(PropertyName = "high")]
    public Double High { get; set; }
    /** low */
    [JsonProperty(PropertyName = "low")]
    public Double Low { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "amount")]
    public Double Amount { get; set; }

    /** premium (2.141% -> 0.02141) */
    [JsonProperty(PropertyName = "premium")]
    public Double Premium { get; set; }
    /** outstanding ratio (0.19% -> 0.0019) */
    [JsonProperty(PropertyName = "outstandingRatio")]
    public Double OutstandingRatio { get; set; }
    /**
     * implied volatility  (Bull/Bear not support)
     */
    [JsonProperty(PropertyName = "impliedVolatility")]
    public Double ImpliedVolatility { get; set; } = Double.NaN;
    /** in/out price (in the money, 20.744% -> 0.20744) */
    [JsonProperty(PropertyName = "inOutPrice")]
    public Double InOutPrice { get; set; }

    /** delta (Bull/Bear not support) */
    [JsonProperty(PropertyName = "delta")]
    public Double Delta { get; set; } = Double.NaN;
    /** leverage ratio */
    [JsonProperty(PropertyName = "leverageRatio")]
    public Double LeverageRatio { get; set; }
    /** breakeven point */
    [JsonProperty(PropertyName = "breakevenPoint")]
    public Double BreakevenPoint { get; set; }

    public WarrantQuote()
    {
    }
  }
}

