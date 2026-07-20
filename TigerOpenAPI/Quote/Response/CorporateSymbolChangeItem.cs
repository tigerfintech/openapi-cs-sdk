using System;
using Newtonsoft.Json;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>股票代码变更事件</summary>
  public class CorporateSymbolChangeItem : CorporateActionItem
  {
    [JsonProperty(PropertyName = "oldSymbol")]
    public string OldSymbol { get; set; }

    /// <summary>newSymbol is the ticker after the rename; same as the inherited Symbol field.</summary>
    [JsonProperty(PropertyName = "newSymbol")]
    public string NewSymbol { get; set; }
  }
}
