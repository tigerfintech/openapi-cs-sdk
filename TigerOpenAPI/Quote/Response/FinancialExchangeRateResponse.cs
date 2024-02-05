using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FinancialExchangeRateResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FinancialExchangeRateItem> Data { get; set; }

  }
}

