using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class QuotePermissionResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<QuotePermission> Data { get; set; }

    public QuotePermissionResponse()
    {
    }
  }
}

