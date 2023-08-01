using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FundQuotePoint
  {
    [JsonProperty(PropertyName = "nav")]
    public Double Nav { get; set; }
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }

  }
}

