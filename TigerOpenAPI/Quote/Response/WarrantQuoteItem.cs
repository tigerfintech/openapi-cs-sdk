using System;
using Newtonsoft.Json;

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

