using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class HistoryTimelineItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<TimelinePoint> Items { get; set; }

  }
}

