using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureRealTimeQuoteResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FutureRealTimeItem> Data { get; set; }

    public FutureRealTimeQuoteResponse()
    {
    }
  }
}

