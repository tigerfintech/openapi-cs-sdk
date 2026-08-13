using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteOvernightResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<QuoteOvernightItem> Data { get; set; }
  }
}
