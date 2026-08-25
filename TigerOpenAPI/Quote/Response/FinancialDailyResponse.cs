using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// Response for the <c>financial_daily</c> API. <c>data</c> is a JSON
  /// array of one-metric-per-day rows keyed by <c>field</c>. Matches Java
  /// <c>FinancialDailyResponse</c>.
  /// </summary>
  public class FinancialDailyResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FinancialDailyItem> Data { get; set; }
  }
}
