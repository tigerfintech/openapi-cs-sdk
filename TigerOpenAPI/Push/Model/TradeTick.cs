using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Push.Model
{
  public class TradeTick
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "secType"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SecType SecType { get; set; }

    [JsonProperty(PropertyName = "quoteLevel")]
    public string QuoteLevel { get; set; }

    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty(PropertyName = "ticks")]
    public List<Tick> Ticks { get; set; }

    public TradeTick() : base()
    {
    }
  }

  public class Tick
  {
    [JsonProperty(PropertyName = "sn")]
    public long Sn { get; set; }
    [JsonProperty(PropertyName = "volume")]
    public long Volume { get; set; }
    [JsonProperty(PropertyName = "tickType")]
    public string TickType { get; set; }
    [JsonProperty(PropertyName = "price")]
    public double Price { get; set; }
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }
    [JsonProperty(PropertyName = "cond")]
    public string Cond { get; set; }
    [JsonProperty(PropertyName = "partCode")]
    public string PartCode { get; set; }
    [JsonProperty(PropertyName = "partName")]
    public string PartName { get; set; }
  }
}

