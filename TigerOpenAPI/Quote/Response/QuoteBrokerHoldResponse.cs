using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuoteBrokerHoldResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public BrokerHoldPageItem Data { get; set; }
  }
}
