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

