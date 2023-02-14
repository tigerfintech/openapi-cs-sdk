using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionTradeTickItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public long Expiry { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "items")]
    public List<OptionTradeTickPoint> Items { get; set; }

    public OptionTradeTickItem()
    {
    }
  }
}

