using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class SymbolNameItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    public SymbolNameItem()
    {
    }
  }
}

