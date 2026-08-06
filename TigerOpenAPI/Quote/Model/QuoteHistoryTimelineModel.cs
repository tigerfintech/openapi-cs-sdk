using System;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteHistoryTimelineModel : QuoteSymbolModel
  {
    // yyyy-MM-dd
    [JsonProperty(PropertyName = "date")]
    public string Date { get; set; }

    [JsonProperty(PropertyName = "right"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public RightOption Right { get; set; }

    /// <summary>
    /// Deprecated: use <see cref="Right"/> instead. This was a historical typo.
    /// </summary>
    [JsonIgnore, Obsolete("Use Right instead")]
    public RightOption Rigth { get => Right; set => Right = value; }

    public QuoteHistoryTimelineModel() : base()
    {
    }
  }
}

