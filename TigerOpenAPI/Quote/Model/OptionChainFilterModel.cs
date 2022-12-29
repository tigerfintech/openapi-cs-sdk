using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TigerOpenAPI.Quote.Model
{
  public class OptionChainFilterModel
  {

    [JsonProperty(PropertyName = "in_the_money")]
    public Boolean InTheMoney { get; set; }

    [JsonProperty(PropertyName = "implied_volatility")]
    public Range<Double> ImpliedVolatility { get; set; }

    [JsonProperty(PropertyName = "open_interest")]
    public Range<Int32> OpenInterest { get; set; }

    [JsonProperty(PropertyName = "greeks")]
    public Greeks Greeks { get; set; }

  }

  public class Greeks
  {
    [JsonProperty(PropertyName = "delta")]
    public Range<Double> Delta { get; set; }
    [JsonProperty(PropertyName = "gamma")]
    public Range<Double> Gamma { get; set; }
    [JsonProperty(PropertyName = "vega")]
    public Range<Double> Vega { get; set; }
    [JsonProperty(PropertyName = "theta")]
    public Range<Double> Theta { get; set; }
    [JsonProperty(PropertyName = "rho")]
    public Range<Double> Rho { get; set; }
  }

  public class Range<T>
  {

    [JsonProperty(PropertyName = "min")]
    public T? Min { get; set; }
    [JsonProperty(PropertyName = "max")]
    public T? Max { get; set; }

    public Range() { }

    public Range(T min, T max)
    {
      Min = min;
      Max = max;
    }
  }
}

