using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Trade.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class MarketScannerTagItem
  {

    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "multiTagField")]
    public string MultiTagField { get; set; }

    [JsonProperty(PropertyName = "tagList")]
    public List<TagValue> TagList { get; set; }

    public MarketScannerTagItem()
    {
    }
  }
}

