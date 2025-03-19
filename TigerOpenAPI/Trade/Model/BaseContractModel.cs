using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class BaseContractModel : TradeModel
  {
    [JsonProperty(PropertyName = "sec_type")]
    public string SecType { get; set; } = TigerOpenAPI.Common.Enum.SecType.STK.ToString();

    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }

    [JsonProperty(PropertyName = "strike")]
    public Double Strike { get; set; }

    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }

    [JsonProperty(PropertyName = "exchange")]
    public string Exchange { get; set; }

    public BaseContractModel() : base()
    {
    }
  }
}
