using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionSymbolItem
  {

    /** symbol */
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    /** name */
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
        /** underlying symbol */
    [JsonProperty(PropertyName = "underlyingSymbol")]
    public string UnderlyingSymbol { get; set; }

    public OptionSymbolItem()
    {
    }
  }
}

