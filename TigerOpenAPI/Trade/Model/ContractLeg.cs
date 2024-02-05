using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Model
{
  public class ContractLeg
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "sec_type")]
    public string SecType { get; set; }
    [JsonProperty(PropertyName = "expiry")]
    public string Expiry { get; set; }
    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }
    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }
    [JsonProperty(PropertyName = "action")]
    public string Action { get; set; }
    /** must be greater than 0 */
    [JsonProperty(PropertyName = "ratio")]
    public int Ratio { get; set; } = 1;

  }
}

