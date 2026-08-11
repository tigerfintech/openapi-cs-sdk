using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class IndustryListModel : ApiModel
  {
    [JsonProperty(PropertyName = "market"), JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    /// <summary>Supported: GSECTOR, GGROUP, GIND, GSUBIND</summary>
    [JsonProperty(PropertyName = "industry_level")]
    public string IndustryLevel { get; set; } = "GSECTOR";
  }
}
