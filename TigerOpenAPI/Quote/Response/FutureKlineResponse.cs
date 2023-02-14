using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureKlineResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FutureKlineBatchItem> Data { get; set; }

    public FutureKlineResponse()
    {
    }
  }
}

