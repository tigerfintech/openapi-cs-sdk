using System;
using Newtonsoft.Json;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>退市事件</summary>
  public class CorporateDelistingItem : CorporateActionItem
  {
    [JsonProperty(PropertyName = "announcedDate")]
    public DateTime AnnouncedDate { get; set; }

    [JsonProperty(PropertyName = "reason")]
    public string Reason { get; set; }
  }
}
