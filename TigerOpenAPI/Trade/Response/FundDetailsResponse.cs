using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class FundDetailsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public FundDetailsPageItem Data { get; set; }
  }
}
