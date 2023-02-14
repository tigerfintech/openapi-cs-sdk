using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class WarrantFilterResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public WarrantFilterItem Data { get; set; }

    public WarrantFilterResponse()
    {
    }
  }
}

