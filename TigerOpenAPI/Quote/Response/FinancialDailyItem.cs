using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// A single row in the <c>financial_daily</c> response (one field / one
  /// date). Matches Java <c>FinancialDailyItem</c>. <c>Date</c> is an
  /// epoch-millis long as returned by the wire.
  /// </summary>
  public class FinancialDailyItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "date")]
    public long Date { get; set; }

    [JsonProperty(PropertyName = "field")]
    public string Field { get; set; }

    [JsonProperty(PropertyName = "value")]
    public double Value { get; set; }
  }
}
