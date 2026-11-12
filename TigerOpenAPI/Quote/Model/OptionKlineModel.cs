using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class OptionKlineModel : OptionCommonModel
  {
    /// <summary>Include GTH/CURB minute bars; null keeps the server default.</summary>
    [JsonProperty("with_hour_trading", NullValueHandling = NullValueHandling.Ignore)]
    public bool? WithHourTrading { get; set; }

    [JsonProperty(PropertyName = "period")]
    public string Period { get; set; } = OptionKType.day.Value;

    [JsonProperty(PropertyName = "begin_time")]
    public Int64 BeginTime { get; set; }

    [JsonProperty(PropertyName = "end_time")]
    public Int64 EndTime { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public Int32 Limit { get; set; } = 300;

    /** Sort Direction */
    [JsonProperty(PropertyName = "sort_dir"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SortDir SortDir { get; set; }

    public OptionKlineModel() : base()
    {
    }
  }
}

