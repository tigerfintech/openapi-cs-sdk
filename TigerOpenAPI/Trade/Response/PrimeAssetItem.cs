using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class PrimeAssetItem
  {
    [JsonProperty(PropertyName = "accountId")]
    public string Account { get; set; }
    [JsonProperty(PropertyName = "updateTimestamp")]
    public long UpdateTimestamp { get; set; }
    [JsonProperty(PropertyName = "segments")]
    public List<Segment> Segments { get; set; }

    public PrimeAssetItem()
    {
    }
  }
}

