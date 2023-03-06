using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class SymbolNameResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<SymbolNameItem> Data { get; set; }

    public SymbolNameResponse()
    {
    }
  }
}

