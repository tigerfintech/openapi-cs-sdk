using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteContractResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public QuoteContractItem Data { get; set; }

    public QuoteContractResponse()
    {
    }
  }
}

