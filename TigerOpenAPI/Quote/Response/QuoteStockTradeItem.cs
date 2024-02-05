using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteStockTradeItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "lotSize")]
    public Int32 LotSize { get; set; }
    [JsonProperty(PropertyName = "spreadScale")]
    public Int32 SpreadScale { get; set; }
    [JsonProperty(PropertyName = "minTick")]
    public Double MinTick { get; set; }
  }
}

