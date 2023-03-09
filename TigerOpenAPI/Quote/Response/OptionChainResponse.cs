using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionChainResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<OptionChainItem> Data { get; set; }
  }
}

