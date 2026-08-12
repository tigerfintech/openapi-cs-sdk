using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TigerOpenAPI.Trade.Response
{
  public class PrimeAnalyticsAssetItem
  {
    [JsonProperty(PropertyName = "summary")]
    public Summary Summary { get; set; }
    [JsonProperty(PropertyName = "history")]
    public List<HistoryItem> History { get; set; }

    public PrimeAnalyticsAssetItem()
    {
    }
  }
}

