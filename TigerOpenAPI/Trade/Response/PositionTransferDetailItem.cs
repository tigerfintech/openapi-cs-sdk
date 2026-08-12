using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionTransferDetailItem
  {
    [JsonProperty(PropertyName = "id")]
    public long Id { get; set; }

    [JsonProperty(PropertyName = "accountId")]
    public string AccountId { get; set; }

    [JsonProperty(PropertyName = "counterpartyAccountId")]
    public string CounterpartyAccountId { get; set; }

    [JsonProperty(PropertyName = "method")]
    public string Method { get; set; }

    [JsonProperty(PropertyName = "direction")]
    public string Direction { get; set; }

    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    [JsonProperty(PropertyName = "memo")]
    public string Memo { get; set; }

    [JsonProperty(PropertyName = "userId")]
    public long UserId { get; set; }

    [JsonProperty(PropertyName = "userName")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "finishedAt")]
    public long FinishedAt { get; set; }

    [JsonProperty(PropertyName = "updatedAt")]
    public long UpdatedAt { get; set; }

    [JsonProperty(PropertyName = "createdAt")]
    public long CreatedAt { get; set; }

    [JsonProperty(PropertyName = "detail")]
    public List<PositionTransferStockDetail> Detail { get; set; }
  }

  public class PositionTransferStockDetail
  {
    [JsonProperty(PropertyName = "transferId")]
    public long TransferId { get; set; }

    [JsonProperty(PropertyName = "direction")]
    public string Direction { get; set; }

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "formattedSymbol")]
    public string FormattedSymbol { get; set; }

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }

    [JsonProperty(PropertyName = "quantity")]
    public long Quantity { get; set; }

    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }

    [JsonProperty(PropertyName = "updatedAt")]
    public long UpdatedAt { get; set; }

    [JsonProperty(PropertyName = "createdAt")]
    public long CreatedAt { get; set; }
  }
}
