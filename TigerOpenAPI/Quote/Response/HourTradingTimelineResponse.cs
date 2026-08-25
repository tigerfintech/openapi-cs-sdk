using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// Wrapper for the <c>hour_trading_timeline</c> data object.
  ///
  /// Wire shape:
  /// <code>
  /// {
  ///   "data": {
  ///     "preClose": 224.05,
  ///     "detail": {
  ///       "tag": "盘前",
  ///       "open": 224.10, "high": 224.30, "low": 223.95,
  ///       "preClose": 224.05, "latestPrice": 224.20,
  ///       "volume": 12345, "timestamp": 1700000000000
  ///     },
  ///     "items": [ { "time": ..., "price": ..., "avgPrice": ..., "volume": ... } ]
  ///   }
  /// }
  /// </code>
  ///
  /// <c>detail</c> reuses <see cref="HourTrading"/> for the pre/after-hours
  /// snapshot; <c>items</c> is a list of intraday extended-session points.
  /// </summary>
  public class HourTradingTimelineData
  {
    [JsonProperty(PropertyName = "preClose")]
    public double PreClose { get; set; }

    [JsonProperty(PropertyName = "detail")]
    public HourTrading Detail { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<TimelinePoint> Items { get; set; }
  }

  public class HourTradingTimelineResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public HourTradingTimelineData Data { get; set; }

    public HourTradingTimelineResponse()
    {
    }
  }
}
