using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class WarrantQuoteResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public WarrantQuoteItem Data { get; set; }
  }
}

