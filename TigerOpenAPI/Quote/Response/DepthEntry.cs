using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class DepthEntry
  {
    [JsonProperty(PropertyName = "price")]
    public Double Price { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "count")]
    public Int32 Count { get; set; }

  }
}

