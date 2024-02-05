using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FundHistoryQuoteResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FundHistoryQuoteItem> Data { get; set; }
  }
}

