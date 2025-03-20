using System;
using Newtonsoft.Json;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionDetail
  {
    [JsonProperty(PropertyName = "account")]
    public string Account { get; set; }
    [JsonProperty(PropertyName = "positionQty")]
    public Double PositionQty { get; set; }
    [JsonProperty(PropertyName = "salableQty")]
    public Double SalableQty { get; set; }
    [JsonProperty(PropertyName = "position")]
    public long Position { get; set; }
    [JsonProperty(PropertyName = "positionScale")]
    public int positionScale { get; set; }

    [JsonProperty(PropertyName = "averageCost")]
    public Double AverageCost { get; set; }
    [JsonProperty(PropertyName = "averageCostByAverage")]
    public Double AverageCostByAverage { get; set; }
    [JsonProperty(PropertyName = "averageCostOfCarry")]
    public Double AverageCostOfCarry { get; set; }
    [JsonProperty(PropertyName = "marketValue")]
    public Double MarketValue { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "latestPrice")]
    public Double LatestPrice { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "isLevel0Price")]
    public Boolean IsLevel0Price { get; set; }
    [JsonProperty(PropertyName = "realizedPnl")]
    public Double RealizedPnl { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "realizedPnlByAverage")]
    public Double RealizedPnlByAverage { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "unrealizedPnl")]
    public Double UnrealizedPnl { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "unrealizedPnlByAverage")]
    public Double UnrealizedPnlByAverage { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "unrealizedPnlPercent")]
    public Double UnrealizedPnlPercent { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "unrealizedPnlPercentByAverage")]
    public Double UnrealizedPnlPercentByAverage { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "unrealizedPnlByCostOfCarry")]
    public Double UnrealizedPnlByCostOfCarry { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "unrealizedPnlPercentByCostOfCarry")]
    public Double UnrealizedPnlPercentByCostOfCarry { get; set; } = Double.NaN;

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
    public Double Strike { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "multiplier")]
    public Double Multiplier { get; set; } = Double.NaN;
    [JsonProperty(PropertyName = "updateTimestamp")]
    public long UpdateTimestamp { get; set; }

    // maintenance margin percent
    [JsonProperty(PropertyName = "mmPercent")]
    public Double MmPercent { get; set; } = Double.NaN;
    // maintenance margin value
    [JsonProperty(PropertyName = "mmValue")]
    public Double MmValue { get; set; } = Double.NaN;
    // today's profit and loss
    [JsonProperty(PropertyName = "todayPnl")]
    public Double TodayPnl { get; set; } = Double.NaN;
    // today's profit and loss percent
    [JsonProperty(PropertyName = "todayPnlPercent")]
    public Double TodayPnlPercent { get; set; } = Double.NaN;
    // Fund only, yesterday's profit and loss
    [JsonProperty(PropertyName = "yesterdayPnl")]
    public Double YesterdayPnl { get; set; } = Double.NaN;
    // The closing price on the last trading day (pre-adjusted for stock rights)
    [JsonProperty(PropertyName = "lastClosePrice")]
    public Double LastClosePrice { get; set; }
    // contract categories
    [JsonProperty(PropertyName = "categories")]
    public List<string> Categories { get; set; }

    public PositionDetail()
    {
    }
  }
}

