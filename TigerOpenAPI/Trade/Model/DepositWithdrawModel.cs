using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class DepositWithdrawModel : TradeModel
  {
    [JsonProperty(PropertyName = "seg_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SegmentType SegType { get; set; }

    public DepositWithdrawModel() : base()
    {
    }
  }
}

