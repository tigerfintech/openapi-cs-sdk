using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class ChargeDetails
  {
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }
    [JsonProperty(PropertyName = "typeDesc")]
    public string TypeDesc { get; set; }
    [JsonProperty(PropertyName = "originalAmount")]
    public Double OriginalAmount { get; set; }
    [JsonProperty(PropertyName = "afterDiscountAmount")]
    public Double AfterDiscountAmount { get; set; }

    public ChargeDetails()
    {
    }
  }
}

