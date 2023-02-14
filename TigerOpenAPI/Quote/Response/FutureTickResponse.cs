using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureTickResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public FutureTickBatchItem Data { get; set; }

    public FutureTickResponse()
    {
    }
  }
}

