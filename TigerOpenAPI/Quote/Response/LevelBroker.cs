using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class LevelBroker
  {
    [JsonProperty(PropertyName = "level")]
    public int Level { get; set; }
    [JsonProperty(PropertyName = "price")]
    public Double Price { get; set; }
    [JsonProperty(PropertyName = "brokerCount")]
    public int BrokerCount { get; set; }
    [JsonProperty(PropertyName = "broker")]
    public List<Broker> Broker { get; set; }
  }
}

