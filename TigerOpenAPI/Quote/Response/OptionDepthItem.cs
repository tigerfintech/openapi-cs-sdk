using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionDepthItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public long Expiry { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty(PropertyName = "ask")]
    public List<OptionDepthOrderBook> Ask { get; set; }
    [JsonProperty(PropertyName = "bid")]
    public List<OptionDepthOrderBook> Bid { get; set; }

  }
}

