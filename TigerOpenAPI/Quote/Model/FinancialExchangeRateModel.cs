using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FinancialExchangeRateModel : ApiModel
  {
    [JsonProperty(PropertyName = "currency_list")]
    public List<string> CurrencyList { get; set; }

    [JsonProperty(PropertyName = "begin_date")]
    public Int64 BeginDate { get; set; }

    [JsonProperty(PropertyName = "end_date")]
    public Int64 EndDate { get; set; }

    public FinancialExchangeRateModel() : base()
    {
    }
  }
}

