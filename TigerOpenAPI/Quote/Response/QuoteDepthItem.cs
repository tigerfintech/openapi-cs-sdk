using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteDepthItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "asks")]
    public List<DepthEntry> Asks { get; set; }
    [JsonProperty(PropertyName = "bids")]
    public List<DepthEntry> Bids { get; set; }

  }
}

