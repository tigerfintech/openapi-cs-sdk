using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteOvernightItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "latestPrice")]
    public double LatestPrice { get; set; }

    [JsonProperty(PropertyName = "askPrice")]
    public double AskPrice { get; set; }

    [JsonProperty(PropertyName = "askSize")]
    public long AskSize { get; set; }

    [JsonProperty(PropertyName = "bidPrice")]
    public double BidPrice { get; set; }

    [JsonProperty(PropertyName = "bidSize")]
    public long BidSize { get; set; }

    [JsonProperty(PropertyName = "preClose")]
    public double PreClose { get; set; }

    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }

    [JsonProperty(PropertyName = "amount")]
    public double Amount { get; set; }

    [JsonProperty(PropertyName = "change")]
    public double Change { get; set; }

    [JsonProperty(PropertyName = "changeRate")]
    public double ChangeRate { get; set; }

    [JsonProperty(PropertyName = "amplitude")]
    public double Amplitude { get; set; }

    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Trading session status. The overnight session is 5; other values are
    /// 1 for pre-market, 2 for regular trading, and 3 for after-hours.
    /// </summary>
    [JsonProperty(PropertyName = "tradingStatus")]
    public int TradingStatus { get; set; }
  }
}
