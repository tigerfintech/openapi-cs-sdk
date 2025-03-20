using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteOvernight
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "preClose")]
    public Double PreClose { get; set; }
    [JsonProperty(PropertyName = "latestPrice")]
    public Double LatestPrice { get; set; }

    [JsonProperty(PropertyName = "askPrice")]
    public Double AskPrice { get; set; }
    [JsonProperty(PropertyName = "askSize")]
    public long AskSize { get; set; }
    [JsonProperty(PropertyName = "bidPrice")]
    public Double BidPrice { get; set; }
    [JsonProperty(PropertyName = "bidSize")]
    public long BidSize { get; set; }

    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "amount")]
    public Double Amount { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

  }
}

