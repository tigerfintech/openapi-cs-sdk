using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class TradeCalendar
  {
    /**
     * trading day date，yyyy-MM-dd
     */
    [JsonProperty(PropertyName = "date")]
    public string Date { get; set; }
    /**
     * trading day type:NORMAL/EARLY_CLOSE
     */
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    public TradeCalendar()
    {
    }
  }
}

