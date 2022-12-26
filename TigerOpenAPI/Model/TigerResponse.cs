using System;
using Newtonsoft.Json;
using TigerOpenAPI.Common;

namespace TigerOpenAPI.Model
{
  public class TigerResponse
  {
    [JsonProperty(PropertyName = "code")]
    public int Code { get; set; }
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    public long Timestamp { get; set; }
    [JsonProperty(PropertyName = "sign")]
    public string Sign { get; set; }

    public TigerResponse()
    {
    }

    public bool IsSuccess()
    {
      return Code == TigerApiCode.SUCCESS.Code;
    }
  }
}
