using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class TradeRankItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }
    [JsonProperty(PropertyName = "changeRate")]
    public Double ChangeRate { get; set; }
    [JsonProperty(PropertyName = "sellOrderRate")]
    public Double SellOrderRate { get; set; }
    [JsonProperty(PropertyName = "buyOrderRate")]
    public Double BuyOrderRate { get; set; }

    [JsonProperty(PropertyName = "hourTrading")]
    public TradeRankHourTradingItem HourTrading { get; set; }

  }
}

