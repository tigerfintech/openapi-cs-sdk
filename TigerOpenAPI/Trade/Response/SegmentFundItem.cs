using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Trade.Response
{
  public class SegmentFundItem
  {
    [JsonProperty(PropertyName = "id")]
    public Int64 Id { get; set; }
    [JsonProperty(PropertyName = "fromSegment")]
    public string FromSegment { get; set; }
    [JsonProperty(PropertyName = "toSegment")]
    public string ToSegment { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "amount")]
    public Double Amount { get; set; } = Double.NaN;

    [JsonProperty(PropertyName = "status"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SegmentFundStatus Status { get; set; }
    [JsonProperty(PropertyName = "statusDesc")]
    public string StatusDesc { get; set; }
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }

    [JsonProperty(PropertyName = "settledAt")]
    public Int64 SettledTime { get; set; }
    [JsonProperty(PropertyName = "updatedAt")]
    public Int64 UpdatedTime { get; set; }
    [JsonProperty(PropertyName = "createdAt")]
    public Int64 CreatedTime { get; set; }

  }
}

