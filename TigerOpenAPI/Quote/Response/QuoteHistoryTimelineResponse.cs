using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteHistoryTimelineResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<HistoryTimelineItem> Data { get; set; }

  }
}

