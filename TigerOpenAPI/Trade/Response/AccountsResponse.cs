using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Trade.Response
{
  public class AccountsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, List<AccountItem>> Data { get; set; }

    public AccountsResponse()
    {
    }
  }
}

