using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FutureExchangeModel : ApiModel
  {
    [JsonProperty(PropertyName = "sec_type")]
    public string SecType { get; set; }

    public FutureExchangeModel() : base()
    {
    }
  }
}

