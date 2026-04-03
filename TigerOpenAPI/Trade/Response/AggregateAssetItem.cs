using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class AggregateAssetItem
  {
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "cashBalance")]
    public double CashBalance { get; set; }

    [JsonProperty(PropertyName = "cashBalanceWithInTransit")]
    public double CashBalanceWithInTransit { get; set; }

    [JsonProperty(PropertyName = "equityWithLoan")]
    public double EquityWithLoan { get; set; }

    [JsonProperty(PropertyName = "netLiquidation")]
    public double NetLiquidation { get; set; }

    [JsonProperty(PropertyName = "initMargin")]
    public double InitMargin { get; set; }

    [JsonProperty(PropertyName = "maintainMargin")]
    public double MaintainMargin { get; set; }

    [JsonProperty(PropertyName = "tradeCurrencyMargin")]
    public double TradeCurrencyMargin { get; set; }

    [JsonProperty(PropertyName = "intradayRiskRatio")]
    public double IntradayRiskRatio { get; set; }

    [JsonProperty(PropertyName = "grossPositionValue")]
    public double GrossPositionValue { get; set; }

    [JsonProperty(PropertyName = "optionMarketValue")]
    public double OptionMarketValue { get; set; }

    [JsonProperty(PropertyName = "stockMarketValue")]
    public double StockMarketValue { get; set; }

    [JsonProperty(PropertyName = "cashAvailableForTrade")]
    public double CashAvailableForTrade { get; set; }

    [JsonProperty(PropertyName = "availableCash")]
    public double AvailableCash { get; set; }

    [JsonProperty(PropertyName = "lockedFunds")]
    public double LockedFunds { get; set; }

    [JsonProperty(PropertyName = "lockedCash")]
    public double LockedCash { get; set; }

    [JsonProperty(PropertyName = "creditLimit")]
    public double CreditLimit { get; set; }

    [JsonProperty(PropertyName = "excessEquity")]
    public double ExcessEquity { get; set; }

    [JsonProperty(PropertyName = "excessLiquidity")]
    public double ExcessLiquidity { get; set; }
  }
}
