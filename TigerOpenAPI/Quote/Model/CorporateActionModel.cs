using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class CorporateActionModel : ApiModel
  {
    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    [JsonProperty(PropertyName = "symbols")]
    public List<string> Symbols { get; set; }

    [JsonProperty(PropertyName = "action_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public CorporateActionType ActionType { get; set; }

    [JsonProperty(PropertyName = "begin_date")]
    public Int64 BeginDate { get; set; }

    [JsonProperty(PropertyName = "end_date")]
    public Int64 EndDate { get; set; }

    public CorporateActionModel() : base()
    {
    }
  }
}

