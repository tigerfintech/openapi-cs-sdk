using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class WarrantQuoteItem
  {
    [JsonProperty(PropertyName = "items")]
    public List<WarrantQuote> Items { get; set; }

    public WarrantQuoteItem()
    {
    }
  }
}

