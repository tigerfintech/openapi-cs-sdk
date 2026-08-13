using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class TradeRankHourTradingItem
  {
    // 1 -> PreMarket, 2 -> Regular, 3 -> AfterHours
    [JsonProperty(PropertyName = "tradingStatus")]
    public Int32 TradingStatus { get; set; }
    [JsonProperty(PropertyName = "tradeSession"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public TradeSession TradeSession { get; set; }
    [JsonProperty(PropertyName = "changeRate")]
    public Double ChangeRate { get; set; }

  }
}

