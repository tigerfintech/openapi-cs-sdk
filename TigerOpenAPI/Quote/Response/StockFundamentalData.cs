using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class StockFundamentalData
  {
    [JsonProperty(PropertyName = "items")]
    public List<StockFundamentalItem> Items { get; set; }
  }
}
