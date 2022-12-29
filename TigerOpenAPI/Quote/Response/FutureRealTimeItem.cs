using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureRealTimeItem
  {
    [JsonProperty(PropertyName = "contractCode")]
    public string ContractCode { get; set; }
    [JsonProperty(PropertyName = "latestPrice")]
    public Decimal LatestPrice { get; set; }
    [JsonProperty(PropertyName = "latestSize")]
    public Int64 LatestSize { get; set; }
    [JsonProperty(PropertyName = "latestTime")]
    public Int64 LatestTime { get; set; }
    [JsonProperty(PropertyName = "bidPrice")]
    public Decimal BidPrice { get; set; }
    [JsonProperty(PropertyName = "bidSize")]
    public Int64 BidSize { get; set; }
    [JsonProperty(PropertyName = "askPrice")]
    public Decimal AskPrice { get; set; }
    [JsonProperty(PropertyName = "askSize")]
    public Int64 AskSize { get; set; }
    [JsonProperty(PropertyName = "openInterest")]
    public Int64 OpenInterest { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public Int64 Volume { get; set; }
    [JsonProperty(PropertyName = "open")]
    public Decimal Open { get; set; }
    [JsonProperty(PropertyName = "high")]
    public Decimal High { get; set; }
    [JsonProperty(PropertyName = "low")]
    public Decimal Low { get; set; }
    [JsonProperty(PropertyName = "settlement")]
    public Decimal Settlement { get; set; }
    [JsonProperty(PropertyName = "limitUp")]
    public Decimal LimitUp { get; set; }
    [JsonProperty(PropertyName = "limitDown")]
    public Decimal LimitDown { get; set; }

    public FutureRealTimeItem()
    {
    }
  }
}

