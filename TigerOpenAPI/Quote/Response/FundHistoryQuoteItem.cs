using System;
using Newtonsoft.Json;
using System.Collections.Generic;

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

