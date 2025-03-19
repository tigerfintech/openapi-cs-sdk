using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class Charge
  {
    [JsonProperty(PropertyName = "category")]
    public string Category { get; set; }
    [JsonProperty(PropertyName = "categoryDesc")]
    public string CategoryDesc { get; set; }
    [JsonProperty(PropertyName = "total")]
    public Double Total { get; set; }
    [JsonProperty(PropertyName = "details")]
    public List<ChargeDetails> Details { get; set; }

    public Charge()
    {
    }
  }
}

