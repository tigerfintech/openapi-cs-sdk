using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteStockBrokerResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public StockBrokerItem Data { get; set; }

  }
}

