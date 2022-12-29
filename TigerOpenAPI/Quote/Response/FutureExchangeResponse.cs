using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureExchangeResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FutureExchangeItem> Data { get; set; }

    public FutureExchangeResponse()
    {
    }
  }
}

