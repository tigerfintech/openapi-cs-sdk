using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// Response for the brief/quote_real_time API.
  /// Wire shape: { "data": { "items": [...] } }
  /// </summary>
  public class BriefDataWrapper
  {
    [JsonProperty(PropertyName = "items")]
    public List<RealTimeQuoteItem> Items { get; set; }
  }

  public class BriefResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public BriefDataWrapper Data { get; set; }
  }
}
