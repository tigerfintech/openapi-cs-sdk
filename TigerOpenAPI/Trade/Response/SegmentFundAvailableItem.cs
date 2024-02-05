using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Trade.Response
{
  public class SegmentFundAvailableItem
  {
    [JsonProperty(PropertyName = "fromSegment")]
    public string FromSegment { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "amount")]
    public Double Amount { get; set; }
  }
}

