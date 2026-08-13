using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class StockFundamentalData
  {
    [JsonProperty(PropertyName = "items")]
    public List<StockFundamentalItem> Items { get; set; }
  }
}
