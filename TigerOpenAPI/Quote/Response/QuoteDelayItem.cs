using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteDelayItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "open")]
    public Double Open { get; set; }
    [JsonProperty(PropertyName = "high")]
    public Double High { get; set; }
    [JsonProperty(PropertyName = "low")]
    public Double Low { get; set; }
    [JsonProperty(PropertyName = "close")]
    public Double Close { get; set; }
    [JsonProperty(PropertyName = "preClose")]
    public Double PreClose { get; set; }
    /**
     * symbol status (halted. 0: Normal 3: Suspension 4: Delisted 7: IPO 8: Changing)
     */
    [JsonProperty(PropertyName = "halted")]
    public Double Halted { get; set; } = Double.NaN;

    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }

  }
}

