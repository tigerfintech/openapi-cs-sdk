using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class TradeCalendarModel : ApiModel
  {

    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    [JsonProperty(PropertyName = "begin_date")]
    public string BeginDate { get; set; }

    [JsonProperty(PropertyName = "end_date")]
    public string EndDate { get; set; }

    public TradeCalendarModel() : base()
    {
    }
  }
}

