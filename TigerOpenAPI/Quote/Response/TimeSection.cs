using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class TimeSection
  {

    [JsonProperty(PropertyName = "start")]
    public long Start { get; set; }
    [JsonProperty(PropertyName = "end")]
    public long End { get; set; }

    public TimeSection()
    {
    }
  }
}

