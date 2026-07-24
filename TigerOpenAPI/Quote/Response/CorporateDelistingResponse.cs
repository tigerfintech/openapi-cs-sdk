using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>退市事件响应</summary>
  public class CorporateDelistingResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, List<CorporateDelistingItem>> Data { get; set; }

    public CorporateDelistingResponse()
    {
    }
  }
}
