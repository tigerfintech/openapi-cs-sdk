using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// Response for the <c>financial_report</c> API. <c>data</c> is a JSON
  /// array of one-metric-per-filing rows keyed by <c>field</c>. Matches
  /// Java <c>FinancialReportResponse</c>.
  /// </summary>
  public class FinancialReportResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FinancialReportItem> Data { get; set; }
  }
}
