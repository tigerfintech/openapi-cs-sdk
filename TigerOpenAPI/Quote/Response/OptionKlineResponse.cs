using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionKlineResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<OptionKlineItem> Data { get; set; }
  }
}

