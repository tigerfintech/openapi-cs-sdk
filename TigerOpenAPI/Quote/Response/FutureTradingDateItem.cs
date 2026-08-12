using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureTradingDateItem
  {
    [JsonProperty(PropertyName = "biddingTimes")]
    List<TimeSection> BiddingTimes { get; set; }
    [JsonProperty(PropertyName = "tradingTimes")]
    List<TimeSection> TradingTimes { get; set; }
    [JsonProperty(PropertyName = "timeSection")]
    public string TimeSection { get; set; }

    public FutureTradingDateItem()
    {
    }
  }
}

