using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class CorporateActionItem
  {
    [JsonProperty(PropertyName = "id")]
    public Int64 Id { get; set; }
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "market")]
    public string Market { get; set; }
    [JsonProperty(PropertyName = "exchange")]
    public string Exchange { get; set; }
    [JsonProperty(PropertyName = "executeDate")]
    public DateTime ExecuteDate { get; set; }
    [JsonProperty(PropertyName = "actionType"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public CorporateActionType ActionType { get; set; }
  }
}

