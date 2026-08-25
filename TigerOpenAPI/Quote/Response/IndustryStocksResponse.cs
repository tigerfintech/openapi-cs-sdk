using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// Response for the industry_stocks API. Data is a flat list of
  /// <see cref="IndustryStockItem"/> entries.
  /// </summary>
  public class IndustryStocksResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<IndustryStockItem> Data { get; set; }
  }
}
