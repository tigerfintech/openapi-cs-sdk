using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>股票代码变更事件响应</summary>
  public class CorporateSymbolChangeResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, List<CorporateSymbolChangeItem>> Data { get; set; }

    public CorporateSymbolChangeResponse()
    {
    }
  }
}
