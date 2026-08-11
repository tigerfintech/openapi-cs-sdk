using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for option_depth API.
  /// Wire format: { "option_basic": [{ symbol, expiry, strike, right }], "market": "US" }
  /// </summary>
  public class OptionDepthV2Model : ApiModel
  {
    [JsonProperty(PropertyName = "option_basic")]
    public List<OptionQueryItem> OptionBasic { get; set; }

    [JsonProperty(PropertyName = "market", NullValueHandling = NullValueHandling.Ignore)]
    public string Market { get; set; }
  }
}
