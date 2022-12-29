using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class QuotePermission
  {

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "expireAt")]
    public long ExpireAt { get; set; }

    public QuotePermission()
    {
    }
  }
}

