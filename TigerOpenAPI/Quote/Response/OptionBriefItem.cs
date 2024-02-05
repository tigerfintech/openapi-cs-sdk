using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionBriefItem
  {
    [JsonProperty(PropertyName = "identifier")]
    public string Identifier { get; set; }
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public long Expiry { get; set; }

    [JsonProperty(PropertyName = "askPrice")]
    public Double AskPrice { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "askSize")]
    public long AskSize { get; set; }
    [JsonProperty(PropertyName = "bidPrice")]
    public Double BidPrice { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "bidSize")]
    public long BidSize { get; set; }

    [JsonProperty(PropertyName = "latestPrice")]
    public Double LatestPrice { get; set; }
    [JsonProperty(PropertyName = "preClose")]
    public Double PreClose { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "high")]
    public Double High { get; set; }
    [JsonProperty(PropertyName = "low")]
    public Double Low { get; set; }
    [JsonProperty(PropertyName = "open")]
    public Double Open { get; set; }

    [JsonProperty(PropertyName = "openInterest")]
    public Int32 OpenInterest { get; set; }
    [JsonProperty(PropertyName = "change")]
    public Double Change { get; set; }
    [JsonProperty(PropertyName = "multiplier")]
    public Int32 Multiplier { get; set; }
    [JsonProperty(PropertyName = "volatility")]
    public string Volatility { get; set; }
    [JsonProperty(PropertyName = "ratesBonds")]
    public Double RatesBonds { get; set; }

    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }
  }
}

