using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Single option symbol for analysis request
  /// </summary>
  public class OptionAnalysisSymbolModel
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "period")]
    public string Period { get; set; }

    [JsonProperty(PropertyName = "require_volatility_list")]
    public bool? RequireVolatilityList { get; set; }

    public OptionAnalysisSymbolModel()
    {
    }

    public OptionAnalysisSymbolModel(string symbol, string period) : this()
    {
      Symbol = symbol;
      Period = period;
    }

    public OptionAnalysisSymbolModel(string symbol, string period, bool requireVolatilityList) : this(symbol, period)
    {
      RequireVolatilityList = requireVolatilityList;
    }
  }
}
