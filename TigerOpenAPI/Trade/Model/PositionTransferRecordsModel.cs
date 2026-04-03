using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  /// <summary>
  /// Request model for querying position transfer records (internal transfers)
  /// </summary>
  public class PositionTransferRecordsModel : TradeModel
  {
    [JsonProperty(PropertyName = "account_id")]
    public string AccountId { get; set; }

    [JsonProperty(PropertyName = "since_date")]
    public string SinceDate { get; set; }

    [JsonProperty(PropertyName = "to_date")]
    public string ToDate { get; set; }

    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    public PositionTransferRecordsModel() : base()
    {
    }

    public PositionTransferRecordsModel(string accountId, string sinceDate, string toDate) : base()
    {
      AccountId = accountId;
      SinceDate = sinceDate;
      ToDate = toDate;
    }

    public PositionTransferRecordsModel(string accountId, string sinceDate, string toDate, string status, string market, string symbol)
      : this(accountId, sinceDate, toDate)
    {
      Status = status;
      Market = market;
      Symbol = symbol;
    }
  }
}
