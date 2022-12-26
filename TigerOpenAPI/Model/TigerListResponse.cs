using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class TigerListResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<Dictionary<string, Object>> Data { get; set; }

    public TigerListResponse()
    {
    }
  }
}

