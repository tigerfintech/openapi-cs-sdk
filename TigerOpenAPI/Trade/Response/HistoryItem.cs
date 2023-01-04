using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class HistoryItem
  {
    [JsonProperty(PropertyName = "date")]
    public long Date { get; set; }
    [JsonProperty(PropertyName = "asset")]
    public Double Asset { get; set; }
    [JsonProperty(PropertyName = "pnl")]
    public Double Pnl { get; set; }
    [JsonProperty(PropertyName = "pnlPercentage")]
    public Double PnlPercentage { get; set; }
    [JsonProperty(PropertyName = "cashBalance")]
    public Double CashBalance { get; set; }
    [JsonProperty(PropertyName = "grossPositionValue")]
    public Double GrossPositionValue { get; set; }
    [JsonProperty(PropertyName = "deposit")]
    public Double Deposit { get; set; }
    [JsonProperty(PropertyName = "withdrawal")]
    public Double Withdrawal { get; set; }

    public HistoryItem()
    {
    }
  }
}

