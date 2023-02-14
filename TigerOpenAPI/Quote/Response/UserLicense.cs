using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  public class UserLicense
  {
    [JsonProperty(PropertyName = "license")]
    public string License { get; set; }

    public UserLicense()
    {
    }
  }
}

