using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class IndustryStocksModel : ApiModel
  {
    [JsonProperty(PropertyName = "industry_id")]
    public string IndustryId { get; set; }

    /// <summary>
    /// 市场,如 US / HK / CN / SG(必填,服务端拒绝空值:
    /// "biz param error(field 'market' cannot be empty)")
    /// </summary>
    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; } = Market.US;
  }
}
