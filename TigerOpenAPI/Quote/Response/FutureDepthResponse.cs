using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureDepthResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FutureDepthItem> Data { get; set; }
  }
}
