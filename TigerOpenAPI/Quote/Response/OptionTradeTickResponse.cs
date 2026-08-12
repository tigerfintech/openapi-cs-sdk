using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionTradeTickResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<OptionTradeTickItem> Data { get; set; }
  }
}

