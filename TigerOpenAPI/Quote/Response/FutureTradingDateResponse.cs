using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureTradingDateResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public FutureTradingDateItem Data { get; set; }

    public FutureTradingDateResponse()
    {
    }
  }
}

