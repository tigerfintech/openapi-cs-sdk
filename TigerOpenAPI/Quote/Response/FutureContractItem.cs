using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureContractItem
  {
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "ibCode")]
    public string IbCode { get; set; }
    [JsonProperty(PropertyName = "contractCode")]
    public string ContractCode { get; set; }
    [JsonProperty(PropertyName = "contractMonth")]
    public string ContractMonth { get; set; }
    [JsonProperty(PropertyName = "exchangeCode")]
    public string ExchangeCode { get; set; }
    [JsonProperty(PropertyName = "exchange")]
    public string Exchange { get; set; }
    [JsonProperty(PropertyName = "multiplier")]
    public decimal Multiplier { get; set; }
    [JsonProperty(PropertyName = "minTick")]
    public decimal MinTick { get; set; }
    [JsonProperty(PropertyName = "lastTradingDate")]
    public string LastTradingDate { get; set; }
    [JsonProperty(PropertyName = "firstNoticeDate")]
    public string FirstNoticeDate { get; set; }
    [JsonProperty(PropertyName = "lastBiddingCloseTime")]
    public long LastBiddingCloseTime { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "continuous")]
    public bool Continuous { get; set; }
    [JsonProperty(PropertyName = "trade")]
    public bool Trade { get; set; }

    public FutureContractItem()
    {
    }
  }
}

