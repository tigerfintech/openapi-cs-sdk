using System;
using Newtonsoft.Json;
using TigerOpenAPI.Quote.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FilterBounds
  {

    [JsonProperty(PropertyName = "issuerName")]
    public List<string> IssuerName { get; set; }
    [JsonProperty(PropertyName = "expireDate")]
    public List<string> ExpireDate { get; set; }
    [JsonProperty(PropertyName = "lotSize")]
    public List<Int32> LotSize { get; set; }
    [JsonProperty(PropertyName = "entitlementRatio")]
    public List<Double> EntitlementRatio { get; set; }

    [JsonProperty(PropertyName = "leverageRatio")]
    public Range<Double> LeverageRatio { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public Range<Double> Strike { get; set; }
    [JsonProperty(PropertyName = "premium")]
    public Range<Double> Premium { get; set; }
    [JsonProperty(PropertyName = "outstandingRatio")]
    public Range<Double> OutstandingRatio { get; set; }
    [JsonProperty(PropertyName = "impliedVolatility")]
    public Range<Double> ImpliedVolatility { get; set; }
    [JsonProperty(PropertyName = "effectiveLeverage")]
    public Range<Double> EffectiveLeverage { get; set; }
    [JsonProperty(PropertyName = "callPrice")]
    public Range<Double> CallPrice { get; set; }

    public FilterBounds()
    {
    }
  }
}

