using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class PrimeAssetsModel : TradeModel
  {
    [JsonProperty(PropertyName = "base_currency")]
    public string BaseCurrency { get; set; }

    [JsonProperty(PropertyName = "consolidated")]
    public Boolean Consolidated { get; set; }
  }
}

