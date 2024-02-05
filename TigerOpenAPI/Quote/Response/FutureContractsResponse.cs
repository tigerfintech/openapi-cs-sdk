﻿using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureContractsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<FutureContractItem> Data { get; set; }
  }
}

