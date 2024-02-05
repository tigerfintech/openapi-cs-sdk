using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class TimelineRange
  {
    [JsonProperty(PropertyName = "items")]
    public List<TimelinePoint> Items { get; set; }

    [JsonProperty(PropertyName = "beginTime")]
    public long BeginTime { get; set; }
    [JsonProperty(PropertyName = "endTime")]
    public long EndTime { get; set; }

  }
}

