using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class KlineItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "period")]
    public string Period { get; set; }
    /**
     * Token that can be used to query the next page. Nullable
     */
    [JsonProperty(PropertyName = "nextPageToken")]
    public string NextPageToken { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<KlinePoint> Items { get; set; }

  }
}

