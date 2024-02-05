using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FundQuoteHistoryModel : FundSymbolModel
  {

    [JsonProperty(PropertyName = "begin_time")]
    public Int64 BeginTime { get; set; }

    [JsonProperty(PropertyName = "end_time")]
    public Int64 EndTime { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public Int32 Limit { get; set; }

    public FundQuoteHistoryModel() : base()
    {
    }
  }
}

