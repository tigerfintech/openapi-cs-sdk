using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class CorporateDividendResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, List<CorporateDividendItem>> Data { get; set; }

    public CorporateDividendResponse()
    {
    }
  }
}

