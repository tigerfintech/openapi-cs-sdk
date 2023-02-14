using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public PositionsItem Data { get; set; }

    public PositionsResponse()
    {
    }
  }
}

