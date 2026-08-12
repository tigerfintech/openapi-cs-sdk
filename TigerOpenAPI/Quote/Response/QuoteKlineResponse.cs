using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteKlineResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<KlineItem> Data { get; set; }

  }
}

