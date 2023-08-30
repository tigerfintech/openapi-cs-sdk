using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TigerOpenAPI.Quote.Response
{
  public class FinancialExchangeRateItem
  {
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "dailyValueList")]
    public List<DailyValue> DailyValueList { get; set; }
  }

  public class DailyValue
  {
    [JsonProperty(PropertyName = "date")]
    public long Date { get; set; }
    [JsonProperty(PropertyName = "value")]
    public decimal Value { get; set; }
  }
}

