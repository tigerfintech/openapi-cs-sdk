using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class DepositWithdrawResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<DepositWithdrawItem> Data { get; set; }
  }
}

