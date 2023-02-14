using System;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteTimelineModel : QuoteSymbolModel
  {

    [JsonProperty(PropertyName = "begin_time")]
    public Int64 BeginTime { get; set; }

    [JsonProperty(PropertyName = "period"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public TimeLineType Period { get; set; }

    public QuoteTimelineModel() : base()
    {
    }
  }
}

