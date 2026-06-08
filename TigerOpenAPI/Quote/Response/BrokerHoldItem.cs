using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class BrokerHoldItem
  {
    [JsonProperty(PropertyName = "orgId")]
    public string OrgId { get; set; }

    [JsonProperty(PropertyName = "orgName")]
    public string OrgName { get; set; }

    [JsonProperty(PropertyName = "date")]
    public string Date { get; set; }

    [JsonProperty(PropertyName = "sharesHold")]
    public long SharesHold { get; set; }

    [JsonProperty(PropertyName = "marketValue")]
    public double MarketValue { get; set; }

    [JsonProperty(PropertyName = "buyAmount")]
    public double BuyAmount { get; set; }

    [JsonProperty(PropertyName = "buyAmount5")]
    public double BuyAmount5 { get; set; }

    [JsonProperty(PropertyName = "buyAmount20")]
    public double BuyAmount20 { get; set; }

    [JsonProperty(PropertyName = "buyAmount60")]
    public double BuyAmount60 { get; set; }

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
  }
}
