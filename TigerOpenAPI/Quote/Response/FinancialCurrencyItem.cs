using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TigerOpenAPI.Quote.Response
{
  public class FinancialCurrencyItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "companyCurrency")]
    public string CompanyCurrency { get; set; }
  }
}

