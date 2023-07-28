using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FundHistoryQuoteItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol;
    [JsonProperty(PropertyName = "items")]
    public List<FundQuotePoint> Items { get; set; }

  }
}

