using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Trade.Response
{
  public class TradableQuantityItem
  {
    /** tradable quantity for cash */
    [JsonProperty(PropertyName = "tradableQuantity")]
    public Double TradableQuantity { get; set; }
    /** tradable quantity for margin */
    [JsonProperty(PropertyName = "financingQuantity")]
    public Double FinancingQuantity { get; set; } = double.NaN;
    /** position quantity */
    [JsonProperty(PropertyName = "positionQuantity")]
    public Double PositionQuantity { get; set; }
    /** tradable quantity in the position */
    [JsonProperty(PropertyName = "tradablePositionQuantity")]
    public Double TradablePositionQuantity { get; set; }

  }
}

