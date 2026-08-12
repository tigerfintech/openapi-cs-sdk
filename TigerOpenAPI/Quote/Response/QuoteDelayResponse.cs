using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteDelayResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<QuoteDelayItem> Data { get; set; }

    public QuoteDelayResponse()
    {
    }
  }
}

