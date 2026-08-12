using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteDepthResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<QuoteDepthItem> Data { get; set; }

  }
}

