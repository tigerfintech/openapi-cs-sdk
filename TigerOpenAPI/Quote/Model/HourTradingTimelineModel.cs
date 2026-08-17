using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for the <c>hour_trading_timeline</c> API.
  ///
  /// Wire shape (per <c>docs/扩展SDK/http/market-data-http/stocks-http.md</c>):
  ///   <c>{ "symbol": "AAPL", "begin_time": 0, "lang": "en_US" }</c>
  ///
  /// The server rejects the plural <c>symbols</c> field used by
  /// <see cref="QuoteSymbolModel"/> — this API accepts a single non-empty
  /// symbol string only. See Java <c>MethodName.HOUR_TRADING_TIMELINE</c>
  /// and the archived Python <c>QuoteHourTradingTimelineResponse</c> for the
  /// same shape.
  /// </summary>
  public class HourTradingTimelineModel : ApiModel
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "begin_time", NullValueHandling = NullValueHandling.Ignore)]
    public long? BeginTime { get; set; }

    public HourTradingTimelineModel() : base()
    {
    }
  }
}
