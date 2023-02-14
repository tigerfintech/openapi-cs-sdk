using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class GlobalAssetsModel : TradeModel
  {
    [JsonProperty(PropertyName = "segment")]
    public Boolean Segment { get; set; }

    [JsonProperty(PropertyName = "market_value")]
    public Boolean MarketValue { get; set; }

    public GlobalAssetsModel() : base()
    {
    }
  }
}

