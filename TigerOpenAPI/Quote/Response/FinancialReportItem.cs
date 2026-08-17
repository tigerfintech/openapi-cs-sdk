using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// A single row in the <c>financial_report</c> response (one field per
  /// filing). Matches Java <c>FinancialReportItem</c>. Values are returned
  /// as strings — some fields carry non-numeric markers (e.g. currency
  /// codes) so the wire type is <c>string</c> rather than a boxed number.
  /// </summary>
  public class FinancialReportItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "field")]
    public string Field { get; set; }

    [JsonProperty(PropertyName = "value")]
    public string Value { get; set; }

    [JsonProperty(PropertyName = "filingDate")]
    public string FilingDate { get; set; }

    [JsonProperty(PropertyName = "periodEndDate")]
    public string PeriodEndDate { get; set; }
  }
}
