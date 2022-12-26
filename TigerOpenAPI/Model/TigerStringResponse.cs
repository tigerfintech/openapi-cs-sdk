using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Model
{
  public class TigerStringResponse : TigerResponse
  {

    [JsonProperty(PropertyName = "data")]
    public string Data { get; set; }

    public TigerStringResponse()
    {
    }
  }
}

