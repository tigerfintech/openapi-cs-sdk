using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class CurrencyAssets
  {
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "cashBalance")]
    public Double CashBalance { get; set; }
    [JsonProperty(PropertyName = "cashAvailableForTrade")]
    public Double CashAvailableForTrade { get; set; }
    [JsonProperty(PropertyName = "grossPositionValue")]
    public Double GrossPositionValue { get; set; }
    [JsonProperty(PropertyName = "stockMarketValue")]
    public Double StockMarketValue { get; set; }
    [JsonProperty(PropertyName = "optionMarketValue")]
    public Double OptionMarketValue { get; set; }

    [JsonProperty(PropertyName = "futuresMarketValue")]
    public Double FuturesMarketValue { get; set; }
    [JsonProperty(PropertyName = "unrealizedPL")]
    public Double UnrealizedPL { get; set; }
    [JsonProperty(PropertyName = "realizedPL")]
    public Double RealizedPL { get; set; }

    public CurrencyAssets()
    {
    }
  }
}

