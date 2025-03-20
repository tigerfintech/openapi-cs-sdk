using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Trade.Response
{
  public class DepositWithdrawItem
  {
    [JsonProperty(PropertyName = "id")]
    public Int64 Id { get; set; }
    [JsonProperty(PropertyName = "refId")]
    public string RefId { get; set; }
    [JsonProperty(PropertyName = "type")]
    public Int32 Type { get; set; }
    [JsonProperty(PropertyName = "typeDesc")]
    public string TypeDesc { get; set; }

    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "amount")]
    public Double Amount { get; set; } = Double.NaN;

    [JsonProperty(PropertyName = "businessDate")]
    public string BusinessDate { get; set; }
    [JsonProperty(PropertyName = "completedStatus")]
    public Boolean CompletedStatus { get; set; }
    [JsonProperty(PropertyName = "updatedAt")]
    public Int64 UpdatedTime { get; set; }
    [JsonProperty(PropertyName = "createdAt")]
    public Int64 CreatedTime { get; set; }

  }
}

