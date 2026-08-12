using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Trade.Response
{
  public class PositionTransferExternalRecordsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<PositionTransferExternalRecordItem> Data { get; set; }
  }
}
