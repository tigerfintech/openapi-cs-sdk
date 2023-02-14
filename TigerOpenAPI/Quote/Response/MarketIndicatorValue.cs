using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class MarketIndicatorValue
  {
    /** 指标索引 **/
    [JsonProperty(PropertyName = "index")]
    public int Index { get; set; }
    /** 指标名称 **/
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    /** 指标值 **/
    [JsonProperty(PropertyName = "value")]
    public Object Value { get; set; }

    public MarketIndicatorValue()
    {
    }
  }
}

