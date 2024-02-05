using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class CapitalFlowItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "period")]
    public string Period { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<CapitalFlowPoint> Items { get; set; }

  }
}

