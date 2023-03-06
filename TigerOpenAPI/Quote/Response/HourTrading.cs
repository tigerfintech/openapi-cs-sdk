using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class HourTrading
  {
    [JsonProperty(PropertyName = "tag")]
    public string Tag { get; set; }
    [JsonProperty(PropertyName = "latestPrice")]
    public Double LatestPrice { get; set; }
    [JsonProperty(PropertyName = "preClose")]
    public Double PreClose { get; set; }
    [JsonProperty(PropertyName = "latestTime")]
    public string LatestTime { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

  }
}

