using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionTransferExternalRecordsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<PositionTransferExternalRecordItem> Data { get; set; }
  }
}
