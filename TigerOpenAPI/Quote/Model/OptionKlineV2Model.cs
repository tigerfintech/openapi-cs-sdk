using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  /// <summary>
  /// A single option kline query item for option_kline v2.
  /// Extends OptionQueryItem with period, begin_time, end_time, limit.
  /// Wire format element: { symbol, expiry, strike, right, period, begin_time, end_time, limit }
  /// </summary>
  public class OptionKlineQueryItem : OptionQueryItem
  {
    [JsonProperty(PropertyName = "period")]
    public string Period { get; set; } = "day";

    [JsonProperty(PropertyName = "begin_time", NullValueHandling = NullValueHandling.Ignore)]
    public long? BeginTime { get; set; }

    [JsonProperty(PropertyName = "end_time", NullValueHandling = NullValueHandling.Ignore)]
    public long? EndTime { get; set; }

    [JsonProperty(PropertyName = "limit", NullValueHandling = NullValueHandling.Ignore)]
    public int? Limit { get; set; }
  }

  /// <summary>
  /// Request model for option_kline API v2.
  /// Wire format: { "option_query": [{ symbol, expiry, strike, right, period, begin_time, end_time, limit }] }
  /// </summary>
  public class OptionKlineV2Model : OptionModel
  {
    [JsonProperty(PropertyName = "option_query")]
    public List<OptionKlineQueryItem> OptionQuery { get; set; }

    public OptionKlineV2Model() : base()
    {
    }
  }
}
