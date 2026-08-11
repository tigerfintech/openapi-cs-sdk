using System.Collections.Generic;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class ImpliedVolMetricItem
  {
    [JsonProperty(PropertyName = "period")]
    public string Period { get; set; }
    [JsonProperty(PropertyName = "percentile")]
    public double Percentile { get; set; }
    [JsonProperty(PropertyName = "rank")]
    public double Rank { get; set; }
  }

  public class OptionVolatilityPoint
  {
    [JsonProperty(PropertyName = "impliedVol")]
    public double ImpliedVol { get; set; }
    [JsonProperty(PropertyName = "percentile")]
    public double Percentile { get; set; }
    [JsonProperty(PropertyName = "rank")]
    public double Rank { get; set; }
    [JsonProperty(PropertyName = "hisVolatility")]
    public double HisVolatility { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }
  }

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

    /// <summary>Implied volatility metrics (JSON object, not a string)</summary>
    [JsonProperty(PropertyName = "impliedVolMetric")]
    public ImpliedVolMetricItem ImpliedVolMetric { get; set; }

    /// <summary>Historical volatility list</summary>
    [JsonProperty(PropertyName = "volatilityList")]
    public List<OptionVolatilityPoint> VolatilityList { get; set; }
  }
}
