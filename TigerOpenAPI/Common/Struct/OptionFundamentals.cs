using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Common.Struct
{
  public class OptionFundamentals
  {
    [JsonProperty(PropertyName = "delta")]
    public double Delta { get; set; }

    [JsonProperty(PropertyName = "gamma")]
    public double Gamma { get; set; }

    [JsonProperty(PropertyName = "theta")]
    public double Theta { get; set; }

    [JsonProperty(PropertyName = "vega")]
    public double Vega { get; set; }

    [JsonProperty(PropertyName = "rho")]
    public double Rho { get; set; }

    [JsonProperty(PropertyName = "predictedValue")]
    public double PredictedValue { get; set; }

    [JsonProperty(PropertyName = "timeValue")]
    public double TimeValue { get; set; }

    [JsonProperty(PropertyName = "premiumRate")]
    public double PremiumRate { get; set; }

    [JsonProperty(PropertyName = "profitRate")]
    public double ProfitRate { get; set; }

    [JsonProperty(PropertyName = "volatility")]
    public double Volatility { get; set; }

    [JsonProperty(PropertyName = "leverage")]
    public double Leverage { get; set; }

    [JsonProperty(PropertyName = "insideValue")]
    public double InsideValue { get; set; }

    [JsonProperty(PropertyName = "historyVolatility")]
    public double HistoryVolatility { get; set; }

    [JsonProperty(PropertyName = "openInterest")]
    public double OpenInterest { get; set; }

    public OptionFundamentals()
    {
    }
  }
}

