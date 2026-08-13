using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class Segment
  {
    [JsonProperty(PropertyName = "capability")]
    public string Capability { get; set; }
    [JsonProperty(PropertyName = "category")]
    public string Category { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "cashBalance")]
    public Double CashBalance { get; set; }
    [JsonProperty(PropertyName = "cashAvailableForTrade")]
    public Double CashAvailableForTrade { get; set; }
    [JsonProperty(PropertyName = "cashAvailableForWithdrawal")]
    public Double CashAvailableForWithdrawal { get; set; }
    [JsonProperty(PropertyName = "grossPositionValue")]
    public Double GrossPositionValue { get; set; }
    [JsonProperty(PropertyName = "equityWithLoan")]
    public Double EquityWithLoan { get; set; }

    [JsonProperty(PropertyName = "netLiquidation")]
    public Double NetLiquidation { get; set; }
    [JsonProperty(PropertyName = "initMargin")]
    public Double InitMargin { get; set; }
    [JsonProperty(PropertyName = "maintainMargin")]
    public Double MaintainMargin { get; set; }
    [JsonProperty(PropertyName = "overnightMargin")]
    public Double OvernightMargin { get; set; }
    [JsonProperty(PropertyName = "unrealizedPL")]
    public Double UnrealizedPL { get; set; }

    // today's profit and loss(only Futures)
    [JsonProperty(PropertyName = "realizedPL")]
    public Double RealizedPL { get; set; }
    [JsonProperty(PropertyName = "excessLiquidation")]
    public Double ExcessLiquidation { get; set; }
    [JsonProperty(PropertyName = "overnightLiquidation")]
    public Double OvernightLiquidation { get; set; }
    [JsonProperty(PropertyName = "buyingPower")]
    public Double BuyingPower { get; set; }
    [JsonProperty(PropertyName = "leverage")]
    public Double Leverage { get; set; }

    // Unrealized profit and loss, calculated according to the A-share model
    [JsonProperty(PropertyName = "unrealizedPLByCostOfCarry")]
    public Double UnrealizedPLByCostOfCarry { get; set; }
    // today's total profit and loss
    [JsonProperty(PropertyName = "totalTodayPL")]
    public Double TotalTodayPL { get; set; }
    // locked excess equity
    [JsonProperty(PropertyName = "lockedFunds")]
    public Double LockedFunds { get; set; }
    // assets in-transit
    [JsonProperty(PropertyName = "uncollected")]
    public Double Uncollected { get; set; }

    [JsonProperty(PropertyName = "currencyAssets")]
    public List<CurrencyAssets> CurrencyAssets { get; set; }

    [JsonProperty(PropertyName = "consolidatedSegTypes")]
    public List<string> ConsolidatedSegTypes { get; set; }

    public Segment()
    {
    }
  }
}

