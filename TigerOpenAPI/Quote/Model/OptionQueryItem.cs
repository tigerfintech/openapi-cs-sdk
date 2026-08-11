using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// A single option contract specification used in v2-style list requests
  /// (option_kline v2, option_trade_tick, option_depth, option_timeline).
  /// </summary>
  public class OptionQueryItem
  {
    [JsonProperty(PropertyName = "symbol", NullValueHandling = NullValueHandling.Ignore)]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "expiry")]
    public long Expiry { get; set; }

    [JsonProperty(PropertyName = "strike", NullValueHandling = NullValueHandling.Ignore)]
    public string Strike { get; set; }

    [JsonProperty(PropertyName = "right", NullValueHandling = NullValueHandling.Ignore)]
    public string Right { get; set; }
  }
}
