using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class KlineQuotaResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<QuotaItem> Data { get; set; }

  }
}

