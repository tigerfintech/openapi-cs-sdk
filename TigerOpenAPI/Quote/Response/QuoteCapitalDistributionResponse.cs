using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteCapitalDistributionResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public CapitalDistributionItem Data { get; set; }

  }
}

