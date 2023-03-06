using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class TradeCalendarResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<TradeCalendar> Data { get; set; }

    public TradeCalendarResponse()
    {
    }
  }
}

