using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionTransferRecordsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<PositionTransferRecordItem> Data { get; set; }
  }
}
