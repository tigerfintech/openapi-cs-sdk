using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionAnalysisItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    /// <summary>30-day implied volatility</summary>
    [JsonProperty(PropertyName = "impliedVol30Days")]
    public double ImpliedVol30Days { get; set; }

    /// <summary>Historical volatility</summary>
    [JsonProperty(PropertyName = "hisVolatility")]
    public double HisVolatility { get; set; }

    /// <summary>IV/HV ratio</summary>
    [JsonProperty(PropertyName = "ivHisVRatio")]
    public double IvHisVRatio { get; set; }

    /// <summary>Call/Put ratio</summary>
    [JsonProperty(PropertyName = "callPutRatio")]
    public double CallPutRatio { get; set; }

    /// <summary>Implied volatility metrics</summary>
    [JsonProperty(PropertyName = "impliedVolMetric")]
    public string ImpliedVolMetric { get; set; }

    /// <summary>Historical volatility list (optional)</summary>
    [JsonProperty(PropertyName = "volatilityList")]
    public List<object> VolatilityList { get; set; }
  }
}
