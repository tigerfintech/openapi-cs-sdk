using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteTradeTickResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<TradeTickItem> Data { get; set; }
  }
}

