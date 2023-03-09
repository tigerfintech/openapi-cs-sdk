using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionRealTimeQuoteGroup
  {
    [JsonProperty(PropertyName = "put")]
    public OptionRealTimeQuote Put { get; set; }
    [JsonProperty(PropertyName = "call")]
    public OptionRealTimeQuote Call { get; set; }

  }
}

