using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class CorporateDividendItem : CorporateActionItem
  {
    [JsonProperty(PropertyName = "recordDate")]
    public DateTime RecordDate { get; set; }
    [JsonProperty(PropertyName = "announcedDate")]
    public DateTime AnnouncedDate { get; set; }
    [JsonProperty(PropertyName = "payDate")]
    public DateTime PayDate { get; set; }
    [JsonProperty(PropertyName = "amount")]
    public Double Amount { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }
  }
}

