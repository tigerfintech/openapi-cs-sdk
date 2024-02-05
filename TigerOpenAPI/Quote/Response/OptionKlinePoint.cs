using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionKlinePoint : KlinePoint
  {
    [JsonProperty(PropertyName = "openInterest")]
    public Int32 OpenInterest { get; set; }

  }
}

