using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class BrokerHoldPageItem
  {
    [JsonProperty(PropertyName = "page")]
    public int Page { get; set; }

    [JsonProperty(PropertyName = "totalPage")]
    public int TotalPage { get; set; }

    [JsonProperty(PropertyName = "totalCount")]
    public int TotalCount { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<BrokerHoldItem> Items { get; set; }
  }
}
