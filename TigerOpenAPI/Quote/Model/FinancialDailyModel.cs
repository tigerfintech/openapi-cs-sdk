using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for the <c>financial_daily</c> API.
  ///
  /// Wire shape (matches Java <c>FinancialDailyModel</c>):
  /// <code>
  /// {
  ///   "symbols": ["AAPL","MSFT"],
  ///   "market": "US",
  ///   "fields": ["net_profit","open_price"],
  ///   "begin_date": 1704067200000,
  ///   "end_date": 1735689599000
  /// }
  /// </code>
  ///
  /// <c>begin_date</c>/<c>end_date</c> are epoch millis. The server rejects
  /// requests missing <c>market</c>, <c>fields</c>, or either date bound,
  /// which is why the pre-existing test skipped when only
  /// <see cref="QuoteSymbolModel"/> was available.
  /// </summary>
  public class FinancialDailyModel : ApiModel
  {
    [JsonProperty(PropertyName = "symbols")]
    public List<string> Symbols { get; set; }

    [JsonProperty(PropertyName = "market"),
     Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    [JsonProperty(PropertyName = "fields")]
    public List<string> Fields { get; set; }

    [JsonProperty(PropertyName = "begin_date")]
    public long BeginDate { get; set; }

    [JsonProperty(PropertyName = "end_date")]
    public long EndDate { get; set; }

    public FinancialDailyModel() : base()
    {
    }
  }
}
