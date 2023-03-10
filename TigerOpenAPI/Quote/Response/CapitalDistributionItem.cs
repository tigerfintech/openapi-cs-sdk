using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class CapitalDistributionItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "netInflow")]
    public Double NetInflow { get; set; }

    [JsonProperty(PropertyName = "inAll")]
    public Double InAll { get; set; }
    [JsonProperty(PropertyName = "inBig")]
    public Double InBig { get; set; }
    [JsonProperty(PropertyName = "inMid")]
    public Double InMid { get; set; }
    [JsonProperty(PropertyName = "inSmall")]
    public Double InSmall { get; set; }

    [JsonProperty(PropertyName = "outAll")]
    public Double OutAll { get; set; }
    [JsonProperty(PropertyName = "outBig")]
    public Double OutBig { get; set; }
    [JsonProperty(PropertyName = "outMid")]
    public Double OutMid { get; set; }
    [JsonProperty(PropertyName = "outSmall")]
    public Double OutSmall { get; set; }
  }
}

