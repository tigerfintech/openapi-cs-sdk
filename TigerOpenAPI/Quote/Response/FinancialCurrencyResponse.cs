using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FinancialCurrencyResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FinancialCurrencyItem> Data { get; set; }

  }
}

