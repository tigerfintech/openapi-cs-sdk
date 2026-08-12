using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteRealTimeQuoteResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<RealTimeQuoteItem> Data { get; set; }

  }
}

