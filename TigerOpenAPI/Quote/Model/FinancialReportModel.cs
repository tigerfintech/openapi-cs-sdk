using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for the <c>financial_report</c> API.
  ///
  /// Wire shape (matches Java <c>FinancialReportModel</c>):
  /// <code>
  /// {
  ///   "symbols": ["AAPL"],
  ///   "market": "US",
  ///   "fields": ["revenues","net_income"],
  ///   "period_type": "Quarterly",
  ///   "begin_date": 1704067200000,
  ///   "end_date": 1735689599000
  /// }
  /// </code>
  ///
  /// <c>begin_date</c>/<c>end_date</c> are optional (epoch millis) — omit
  /// via <c>0</c> to skip. <c>period_type</c> is required and takes the
  /// values from <see cref="FinancialPeriodType"/>.
  /// </summary>
  public class FinancialReportModel : ApiModel
  {
    [JsonProperty(PropertyName = "symbols")]
    public List<string> Symbols { get; set; }

    [JsonProperty(PropertyName = "market"),
     Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    [JsonProperty(PropertyName = "fields")]
    public List<string> Fields { get; set; }

    [JsonProperty(PropertyName = "period_type"),
     Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public FinancialPeriodType PeriodType { get; set; }

    [JsonProperty(PropertyName = "begin_date", NullValueHandling = NullValueHandling.Ignore)]
    public long? BeginDate { get; set; }

    [JsonProperty(PropertyName = "end_date", NullValueHandling = NullValueHandling.Ignore)]
    public long? EndDate { get; set; }

    public FinancialReportModel() : base()
    {
    }
  }
}
