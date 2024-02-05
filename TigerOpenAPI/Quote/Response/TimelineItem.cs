using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class TimelineItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "period")]
    public string Period { get; set; }
    [JsonProperty(PropertyName = "preClose")]
    public Double PreClose { get; set; }

    [JsonProperty(PropertyName = "intraday")]
    public TimelineRange Intraday { get; set; }
    [JsonProperty(PropertyName = "preMarket")]
    public TimelineRange PreMarket { get; set; }
    [JsonProperty(PropertyName = "afterHours")]
    public TimelineRange AfterHours { get; set; }

  }
}

