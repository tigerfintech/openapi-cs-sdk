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

    /// <summary>Query start date. Leave unset (null) to skip this bound.</summary>
    [JsonProperty(PropertyName = "begin_date", NullValueHandling = NullValueHandling.Ignore)]
    public long? BeginDate { get; set; }

    /// <summary>Query end date. Leave unset (null) to skip this bound.</summary>
    [JsonProperty(PropertyName = "end_date", NullValueHandling = NullValueHandling.Ignore)]
    public long? EndDate { get; set; }

    public CorporateActionModel() : base()
    {
    }
  }
}

