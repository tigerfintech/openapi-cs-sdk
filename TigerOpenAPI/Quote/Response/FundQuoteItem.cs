using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FundQuoteItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol;
    [JsonProperty(PropertyName = "close")]
    public float Close;
    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

  }
}

