using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class DepositWithdrawItem
  {
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "refId")]
    public string RefId { get; set; }

    /// <summary>Transaction type (deposit/withdraw)</summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [JsonProperty(PropertyName = "typeDesc")]
    public string TypeDesc { get; set; }

    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "amount")]
    public double Amount { get; set; }

    [JsonProperty(PropertyName = "businessDate")]
    public string BusinessDate { get; set; }

    [JsonProperty(PropertyName = "completedStatus")]
    public string CompletedStatus { get; set; }

    [JsonProperty(PropertyName = "updatedAt")]
    public long UpdatedAt { get; set; }

    [JsonProperty(PropertyName = "createdAt")]
    public long CreatedAt { get; set; }
  }
}
