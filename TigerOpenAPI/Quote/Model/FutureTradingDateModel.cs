using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FutureTradingDateModel : ApiModel
  {
    [JsonProperty(PropertyName = "contract_code")]
    public string ContractCode { get; set; }

    [JsonProperty(PropertyName = "trading_date")]
    public long TradingDate { get; set; }

    public FutureTradingDateModel() : base()
    {
    }
  }
}

