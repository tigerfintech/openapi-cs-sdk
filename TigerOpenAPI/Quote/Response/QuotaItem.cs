using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class QuotaItem
  {
    [JsonProperty(PropertyName = "remain")]
    public int Remain { get; set; }
    [JsonProperty(PropertyName = "used")]
    public int Used { get; set; }
    [JsonProperty(PropertyName = "method")]
    public string Method { get; set; }
    [JsonProperty(PropertyName = "details")]
    public List<string> Details { get; set; }

  }
}

