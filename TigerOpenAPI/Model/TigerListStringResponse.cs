using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Model
{
  public class TigerListStringResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<string> Data { get; set; }

    public TigerListStringResponse()
    {
    }
  }
}

