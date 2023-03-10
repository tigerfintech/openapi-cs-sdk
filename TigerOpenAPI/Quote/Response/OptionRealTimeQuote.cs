using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionRealTimeQuote
  {
    [JsonProperty(PropertyName = "identifier")]
    public string Identifier { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }

    [JsonProperty(PropertyName = "askPrice")]
    public Double AskPrice { get; set; }
    [JsonProperty(PropertyName = "askSize")]
    public long AskSize { get; set; }
    [JsonProperty(PropertyName = "bidPrice")]
    public Double BidPrice { get; set; }
    [JsonProperty(PropertyName = "bidSize")]
    public long BidSize { get; set; }

    [JsonProperty(PropertyName = "latestPrice")]
    public Double LatestPrice { get; set; }
    [JsonProperty(PropertyName = "preClose")]
    public Double PreClose { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "openInterest")]
    public Int32 OpenInterest { get; set; }
    [JsonProperty(PropertyName = "multiplier")]
    public Int32 Multiplier { get; set; }

    [JsonProperty(PropertyName = "lastTimestamp")]
    public long LastTimestamp { get; set; }
  }
}

