using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// Request model for option_timeline API.
  /// Wire format: { "option_query": [{ symbol, expiry, strike, right }], "market": "US" }
  /// </summary>
  public class OptionTimelineV2Model : ApiModel
  {
    [JsonProperty(PropertyName = "option_query")]
    public List<OptionQueryItem> OptionQuery { get; set; }

    [JsonProperty(PropertyName = "market", NullValueHandling = NullValueHandling.Ignore)]
    public string Market { get; set; }
  }
}
