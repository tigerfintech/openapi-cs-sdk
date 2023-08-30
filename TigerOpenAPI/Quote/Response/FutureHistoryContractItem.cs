using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureHistoryContractItem
  {
    [JsonProperty(PropertyName = "time")]
    public long Time { get; set; }
    [JsonProperty(PropertyName = "referContractCode")]
    public string ReferContractCode { get; set; }

  }
}

