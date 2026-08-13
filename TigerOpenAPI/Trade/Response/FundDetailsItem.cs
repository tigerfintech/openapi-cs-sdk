using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class FundDetailsItem
  {
    [JsonProperty(PropertyName = "id")]
    public long Id { get; set; }

    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    /// <summary>Fund flow type (e.g. DEPOSIT, WITHDRAW, TRADE)</summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [JsonProperty(PropertyName = "desc")]
    public string Desc { get; set; }

    [JsonProperty(PropertyName = "contractName")]
    public string ContractName { get; set; }

    [JsonProperty(PropertyName = "segType")]
    public string SegType { get; set; }

    [JsonProperty(PropertyName = "amount")]
    public double Amount { get; set; }

    [JsonProperty(PropertyName = "businessDate")]
    public string BusinessDate { get; set; }

    [JsonProperty(PropertyName = "updatedAt")]
    public long UpdatedAt { get; set; }
  }
}
