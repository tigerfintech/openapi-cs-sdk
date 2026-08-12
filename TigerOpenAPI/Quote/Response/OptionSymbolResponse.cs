using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionSymbolResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<OptionSymbolItem> Data { get; set; }
  }
}

