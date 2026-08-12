using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for option analysis (IV, HV, call/put ratio, etc.)
  /// </summary>
  public class OptionAnalysisModel : OptionModel
  {
    [JsonProperty(PropertyName = "symbols")]
    public List<OptionAnalysisSymbolModel> Symbols { get; set; }

    public OptionAnalysisModel() : base()
    {
    }

    public OptionAnalysisModel(List<OptionAnalysisSymbolModel> symbols) : base()
    {
      Symbols = symbols;
    }

    public OptionAnalysisModel(List<OptionAnalysisSymbolModel> symbols, Market market) : base()
    {
      Symbols = symbols;
      Market = market;
    }
  }
}
