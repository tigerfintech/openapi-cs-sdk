using System;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FutureHistoryMainContractModel : FutureContractCodesModel
  {
    [JsonProperty(PropertyName = "begin_time")]
    public Int64 BeginTime { get; set; }

    [JsonProperty(PropertyName = "end_time")]
    public Int64 EndTime { get; set; }

    public FutureHistoryMainContractModel() : base()
    {
    }
  }
}

