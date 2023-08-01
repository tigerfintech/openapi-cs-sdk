using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FundContractItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "companyName")]
    public string CompanyName { get; set; }
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "secType")]
    public string SecType { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "tradeable")]
    public bool Tradeable { get; set; }
    [JsonProperty(PropertyName = "subType")]
    public string SubType { get; set; }
    [JsonProperty(PropertyName = "dividendType")]
    public string DividendType { get; set; }
    [JsonProperty(PropertyName = "tigerVault")]
    public bool TigerVault { get; set; }
  }
}

