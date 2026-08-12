using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteTradeRankResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<TradeRankItem> Data { get; set; }

  }
}

