using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>Current GTH/CURB option quote; absent during regular trading.</summary>
  public class OptionHourTrading
  {
    [JsonProperty("tag")] public string? Tag { get; set; }
    [JsonProperty("session")] public string? Session { get; set; }
    [JsonProperty("latestPrice")] public double? LatestPrice { get; set; }
    [JsonProperty("latestTimestamp")] public long? LatestTimestamp { get; set; }
    [JsonProperty("volume")] public long? Volume { get; set; }
    [JsonProperty("amount")] public double? Amount { get; set; }
    [JsonProperty("preClose")] public double? PreClose { get; set; }
  }
}
