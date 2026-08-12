using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureDepthItem
  {
    [JsonProperty(PropertyName = "contractId")]
    public string ContractId { get; set; }

    [JsonProperty(PropertyName = "contractCode")]
    public string ContractCode { get; set; }

    [JsonProperty(PropertyName = "ask")]
    public List<FutureDepthAskBidItem> Ask { get; set; }

    [JsonProperty(PropertyName = "bid")]
    public List<FutureDepthAskBidItem> Bid { get; set; }
  }
}
