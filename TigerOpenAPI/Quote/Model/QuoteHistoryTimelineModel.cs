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
    public RightOption Rigth { get; set; }

    public QuoteHistoryTimelineModel() : base()
    {
    }
  }
}

