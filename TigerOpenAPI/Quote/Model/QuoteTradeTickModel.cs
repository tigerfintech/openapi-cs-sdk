﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteTradeTickModel : QuoteSymbolModel
  {

    [JsonProperty(PropertyName = "begin_index")]
    public Int64 BeginIndex { get; set; }

    [JsonProperty(PropertyName = "end_index")]
    public Int64 EndIndex { get; set; }

    [JsonProperty(PropertyName = "limit")]
    public Int32 Limit { get; set; } = 200;

    public QuoteTradeTickModel() : base()
    {
    }
  }
}
