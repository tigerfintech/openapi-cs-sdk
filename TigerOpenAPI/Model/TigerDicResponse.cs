using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Model
{
  public class TigerDicResponse : TigerResponse
  {

    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, Object> Data { get; set; }

    public TigerDicResponse()
    {
    }
  }
}

