using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class StockFundamentalItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    // rate of return
    [JsonProperty(PropertyName = "roe")]
    public Double Roe { get; set; }
    // price-to-book ratio
    [JsonProperty(PropertyName = "pbRate")]
    public Double PbRate { get; set; }
    // divide rate
    [JsonProperty(PropertyName = "divideRate")]
    public Double DivideRate { get; set; }
  }
}

