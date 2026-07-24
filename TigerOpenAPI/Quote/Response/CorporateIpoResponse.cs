using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>新股上市事件响应</summary>
  public class CorporateIpoResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, List<CorporateIpoItem>> Data { get; set; }

    public CorporateIpoResponse()
    {
    }
  }
}
