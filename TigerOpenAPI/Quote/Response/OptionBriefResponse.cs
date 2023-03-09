using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionBriefResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<OptionBriefItem> Data { get; set; }
  }
}

