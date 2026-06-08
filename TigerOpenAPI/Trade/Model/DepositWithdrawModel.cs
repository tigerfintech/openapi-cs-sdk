using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  /// <summary>
  /// Request model for querying deposit/withdraw records
  /// </summary>
  public class DepositWithdrawModel : TradeModel
  {
    [JsonProperty(PropertyName = "seg_type"), JsonConverter(typeof(StringEnumConverter))]
    public SegmentType? SegType { get; set; }

    [JsonProperty(PropertyName = "start_date")]
    public string StartDate { get; set; }

    [JsonProperty(PropertyName = "end_date")]
    public string EndDate { get; set; }

    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public int? Limit { get; set; }

    [JsonProperty(PropertyName = "page")]
    public int? Page { get; set; }

    public DepositWithdrawModel() : base()
    {
    }
  }
}
