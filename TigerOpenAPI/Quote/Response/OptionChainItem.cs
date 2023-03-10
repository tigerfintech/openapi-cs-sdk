using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionChainItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public long Expiry { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<OptionRealTimeQuoteGroup> Items { get; set; }

  }
}

