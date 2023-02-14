using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class UserToken
  {
    [JsonProperty(PropertyName = "tigerId")]
    public string TigerId { get; set; }
    [JsonProperty(PropertyName = "license")]
    public string License { get; set; }
    [JsonProperty(PropertyName = "token")]
    public string Token { get; set; }
    [JsonProperty(PropertyName = "createTime")]
    public long CreateTime { get; set; }
    [JsonProperty(PropertyName = "expriedTime")]
    public long ExpriedTime { get; set; }

    public UserToken()
    {
    }
  }
}

