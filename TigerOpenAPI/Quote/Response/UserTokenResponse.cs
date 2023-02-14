using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class UserTokenResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public UserToken Data { get; set; }

    public UserTokenResponse()
    {
    }
  }
}

