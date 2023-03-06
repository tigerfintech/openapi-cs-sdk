using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteCapitalFlowResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public CapitalFlowItem Data { get; set; }

  }
}

