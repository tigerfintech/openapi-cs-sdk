using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// Response for the industry_list API. Data is a flat list of
  /// <see cref="IndustryItem"/> entries.
  /// </summary>
  public class IndustryListResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<IndustryItem> Data { get; set; }
  }
}
