using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// Response for the <c>all_symbols</c> and <c>fund_all_symbols</c> APIs.
  ///
  /// Wire shape (matches Java <c>QuoteSymbolResponse</c> and
  /// <c>FundSymbolResponse</c>):
  /// <code>
  /// { "code": 0, "message": "success", "data": ["AAPL", ".DJI", "IE00B11XZ988.USD", ...] }
  /// </code>
  ///
  /// <c>data</c> is a plain JSON array of symbol strings. Index symbols
  /// (leading <c>.</c>) and fund symbols (containing <c>.</c> plus currency
  /// suffix) are legitimate values — the previous approach reused
  /// <c>FundContractsResponse</c>, whose element type was a JSON object,
  /// so any array-of-strings payload failed with "cannot deserialize
  /// String into FundContractItem".
  /// </summary>
  public class SymbolsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<string> Data { get; set; }
  }
}
