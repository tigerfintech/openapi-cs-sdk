using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureExchangeItem
  {
    [JsonProperty(PropertyName = "code")]
    public string Code { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "zoneId")]
    public string ZoneId { get; set; }

    public FutureExchangeItem()
    {
    }
  }
}

