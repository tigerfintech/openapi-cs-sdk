using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class TradeTickItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "beginIndex")]
    public long BeginIndex { get; set; }
    [JsonProperty(PropertyName = "endIndex")]
    public long EndIndex { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<TickPoint> Items { get; set; }

  }
}

