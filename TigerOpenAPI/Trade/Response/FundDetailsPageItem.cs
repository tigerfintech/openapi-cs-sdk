using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class FundDetailsPageItem
  {
    [JsonProperty(PropertyName = "page")]
    public int Page { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public int Limit { get; set; }

    [JsonProperty(PropertyName = "itemCount")]
    public int ItemCount { get; set; }

    [JsonProperty(PropertyName = "pageCount")]
    public int PageCount { get; set; }

    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<FundDetailsItem> Items { get; set; }
  }
}
