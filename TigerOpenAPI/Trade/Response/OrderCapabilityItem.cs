using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  /// <summary>A supported combination of order type, time in force, and trading session.</summary>
  public class OrderCapabilityItem
  {
    [JsonProperty(PropertyName = "orderType", NullValueHandling = NullValueHandling.Ignore)]
    public string? OrderType { get; set; }

    [JsonProperty(PropertyName = "timeInForce", NullValueHandling = NullValueHandling.Ignore)]
    public string? TimeInForce { get; set; }

    [JsonProperty(PropertyName = "tradingSessionType", NullValueHandling = NullValueHandling.Ignore)]
    public string? TradingSessionType { get; set; }
  }
}
