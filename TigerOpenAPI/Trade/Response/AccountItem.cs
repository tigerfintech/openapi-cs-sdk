using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Response
{
  public class AccountItem
  {
    [JsonProperty(PropertyName = "account")]
    public string Account { get; set; }
    [JsonProperty(PropertyName = "capability")]
    public string Capability { get; set; }
    [JsonProperty(PropertyName = "accountType")]
    public string AccountType { get; set; }
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    public AccountItem()
    {
    }
  }
}

