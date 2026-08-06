using System;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteKlineModel : QuoteSymbolModel
  {
    [JsonProperty(PropertyName = "period")]
    public string Period { get; set; } = KLineType.day.Value;

    [JsonProperty(PropertyName = "right"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public RightOption Right { get; set; } = RightOption.br;

    /// <summary>
    /// Deprecated: use <see cref="Right"/> instead. This was a historical typo.
    /// </summary>
    [JsonIgnore, Obsolete("Use Right instead")]
    public RightOption Rigth { get => Right; set => Right = value; }

    [JsonProperty(PropertyName = "begin_time")]
    public Int64 BeginTime { get; set; }

    [JsonProperty(PropertyName = "end_time")]
    public Int64 EndTime { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public Int32 Limit { get; set; } = 300;

    [JsonProperty(PropertyName = "page_token")]
    public string PageToken { get; set; }

    public QuoteKlineModel() : base()
    {
    }
  }
}

