using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteStockFundamentalResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public StockFundamental Data { get; set; }

    public List<StockFundamentalItem> GetStockFundamentalItems() {
      return Data.Items;
    }
  }
}

