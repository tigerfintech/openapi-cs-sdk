using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class UserLicenseResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public UserLicense Data { get; set; }

    public UserLicenseResponse()
    {
    }
  }
}

