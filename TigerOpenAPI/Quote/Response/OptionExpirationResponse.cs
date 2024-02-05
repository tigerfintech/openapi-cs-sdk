using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionExpirationResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<OptionExpirationItem> Data { get; set; }

  }
}

