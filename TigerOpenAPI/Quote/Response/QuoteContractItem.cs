using System;
using Newtonsoft.Json;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteContractItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<QuoteContract> Items { get; set; }

    public QuoteContractItem()
    {
    }
  }
}

