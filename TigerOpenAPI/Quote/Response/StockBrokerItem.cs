using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class StockBrokerItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "bidBroker")]
    public List<LevelBroker> BidBroker { get; set; }
    [JsonProperty(PropertyName = "askBroker")]
    public List<LevelBroker> AskBroker { get; set; }

  }
}

