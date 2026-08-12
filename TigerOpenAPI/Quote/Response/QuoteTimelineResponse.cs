using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteTimelineResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<TimelineItem> Data { get; set; }

  }
}

