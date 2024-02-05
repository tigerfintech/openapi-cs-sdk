using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionKlineItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public long Expiry { get; set; }
    [JsonProperty(PropertyName = "period")]
    public string Period { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<OptionKlinePoint> Items { get; set; }

  }
}

