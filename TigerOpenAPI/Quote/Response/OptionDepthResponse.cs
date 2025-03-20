﻿using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionDepthResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<OptionDepthItem> Data { get; set; }
  }
}

