using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class RealTimeQuoteItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "open")]
    public Double Open { get; set; }
    [JsonProperty(PropertyName = "high")]
    public Double High { get; set; }
    [JsonProperty(PropertyName = "low")]
    public Double Low { get; set; }
    [JsonProperty(PropertyName = "close")]
    public Double Close { get; set; }
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
    [JsonProperty(PropertyName = "latestTime")]
    public long LatestTime { get; set; }

    [JsonProperty(PropertyName = "status"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public StockStatus Status { get; set; }

    [JsonProperty(PropertyName = "hourTrading")]
    public HourTrading HourTrading { get; set; }

  }
}

