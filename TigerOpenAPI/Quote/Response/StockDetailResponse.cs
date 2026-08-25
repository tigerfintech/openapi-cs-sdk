using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// Wrapper for the <c>stock_detail</c> data object. The wire shape is
  /// <c>{"data":{"items":[...]}}</c> — an object, not a top-level array
  /// like <c>quote_real_time</c>. Attempting to reuse
  /// <c>QuoteRealTimeQuoteResponse</c> failed with "cannot deserialize
  /// JSON object into List&lt;RealTimeQuoteItem&gt;".
  /// </summary>
  public class StockDetailData
  {
    [JsonProperty(PropertyName = "items")]
    public List<StockDetailItem> Items { get; set; }
  }

  public class StockDetailResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public StockDetailData Data { get; set; }
  }
}
