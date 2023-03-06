using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteStockTradeResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<QuoteStockTradeItem> Data { get; set; }
  }
}

