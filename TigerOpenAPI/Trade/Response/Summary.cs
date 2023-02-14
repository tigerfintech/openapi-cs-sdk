using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class Summary
  {
    [JsonProperty(PropertyName = "pnl")]
    public Double Pnl { get; set; }
    [JsonProperty(PropertyName = "pnlPercentage")]
    public Double PnlPercentage { get; set; }
    [JsonProperty(PropertyName = "annualizedReturn")]
    public Double AnnualizedReturn { get; set; }
    [JsonProperty(PropertyName = "overUserPercentage")]
    public Double OverUserPercentage { get; set; }

    public Summary()
    {
    }
  }
}

