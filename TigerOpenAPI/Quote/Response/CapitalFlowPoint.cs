using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class CapitalFlowPoint
  {
    [JsonProperty(PropertyName = "time")]
    public string Time { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }
    [JsonProperty(PropertyName = "netInflow")]
    public Double NetInflow { get; set; }
  }
}

