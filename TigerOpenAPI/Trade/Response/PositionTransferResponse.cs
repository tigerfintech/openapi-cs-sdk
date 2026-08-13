using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionTransferResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public PositionTransferItem Data { get; set; }
  }
}
