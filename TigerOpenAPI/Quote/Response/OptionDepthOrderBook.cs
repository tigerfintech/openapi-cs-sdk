using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionDepthOrderBook
  {
    [JsonProperty(PropertyName = "price")]
    public Double Price { get; set; }
    [JsonProperty(PropertyName = "code")]
    public string Code { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    public Int64 Timestamp { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public Int32 Volume { get; set; }
    [JsonProperty(PropertyName = "count")]
    public Int32 Count { get; set; }
  }
}

