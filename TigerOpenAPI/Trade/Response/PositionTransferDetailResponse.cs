using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionTransferDetailResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public PositionTransferDetailItem Data { get; set; }
  }
}
