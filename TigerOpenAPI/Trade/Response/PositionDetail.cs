using System;
using Newtonsoft.Json;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionDetail
  {
    [JsonProperty(PropertyName = "account")]
    public string Account { get; set; }
    [JsonProperty(PropertyName = "position")]
    public long Position { get; set; }

    [JsonProperty(PropertyName = "averageCost")]
    public double AverageCost { get; set; }
    [JsonProperty(PropertyName = "marketValue")]
    public double MarketValue { get; set; }
    [JsonProperty(PropertyName = "latestPrice")]
    public double LatestPrice { get; set; }
    [JsonProperty(PropertyName = "realizedPnl")]
    public double RealizedPnl { get; set; }
    [JsonProperty(PropertyName = "unrealizedPnl")]
    public double UnrealizedPnl { get; set; }

    [JsonProperty(PropertyName = "salable")]
    public Int32 Salable { get; set; }
    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "identifier")]
    public string Identifier { get; set; }
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    // yyyy-MM-dd
    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public Double Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "multiplier")]
    public Double Multiplier { get; set; }
    [JsonProperty(PropertyName = "updateTimestamp")]
    public long UpdateTimestamp { get; set; }

    public PositionDetail()
    {
    }
  }
}

