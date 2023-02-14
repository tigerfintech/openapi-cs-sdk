using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Model
{
  public class TigerDictResponse : TigerResponse
  {

    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, object> Data { get; set; }

    public TigerDictResponse()
    {
    }
  }
}

