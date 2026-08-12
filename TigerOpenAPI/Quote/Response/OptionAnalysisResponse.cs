using System;
using Newtonsoft.Json;
using TigerOpenAPI.Model;
using System.Collections.Generic;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionAnalysisResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public List<OptionAnalysisItem> Data { get; set; }
  }
}
