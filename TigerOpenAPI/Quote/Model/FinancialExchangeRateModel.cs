using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FinancialExchangeRateModel : ApiModel
  {
    [JsonProperty(PropertyName = "currency_list")]
    public List<string> CurrencyList { get; set; }

    /// <summary>Query start date. Leave unset (null) to skip this bound.</summary>
    [JsonProperty(PropertyName = "begin_date", NullValueHandling = NullValueHandling.Ignore)]
    public long? BeginDate { get; set; }

    /// <summary>Query end date. Leave unset (null) to skip this bound.</summary>
    [JsonProperty(PropertyName = "end_date", NullValueHandling = NullValueHandling.Ignore)]
    public long? EndDate { get; set; }

    public FinancialExchangeRateModel() : base()
    {
    }
  }
}

