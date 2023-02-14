using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class WarrantFilterItem
  {
    [JsonProperty(PropertyName = "page")]
    public Int32 Page { get; set; }
    [JsonProperty(PropertyName = "totalPage")]
    public Int32 TotalPage { get; set; }
    [JsonProperty(PropertyName = "totalCount")]
    public Int32 TotalCount { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<WarrantItem> Items { get; set; }
    [JsonProperty(PropertyName = "bounds")]
    public FilterBounds Bounds { get; set; }

    public WarrantFilterItem()
    {
    }
  }
}

