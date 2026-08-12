using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class FundQuoteResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FundQuoteItem> Data { get; set; }
  }
}

