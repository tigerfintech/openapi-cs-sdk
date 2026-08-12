using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class StockFundamental
  {
    [JsonProperty(PropertyName = "items")]
    public List<StockFundamentalItem> Items { get; set; }

  }
}

