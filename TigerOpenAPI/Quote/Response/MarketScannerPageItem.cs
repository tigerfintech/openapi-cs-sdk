using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class MarketScannerPageItem
  {

    [JsonProperty(PropertyName = "page")]
    public Int32 Page { get; set; }
    [JsonProperty(PropertyName = "pageSize")]
    public Int32 PageSize { get; set; }

    [JsonProperty(PropertyName = "totalPage")]
    public Int32 TotalPage { get; set; }
    [JsonProperty(PropertyName = "totalCount")]
    public Int32 TotalCount { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<MarketScannerItem> Items { get; set; }

    public MarketScannerPageItem()
    {
    }
  }
}

