using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class IndustryStocksModel : ApiModel
  {
    [JsonProperty(PropertyName = "industry_id")]
    public string IndustryId { get; set; }
  }
}
