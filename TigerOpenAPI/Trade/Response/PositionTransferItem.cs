using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionTransferItem
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
  }
}
