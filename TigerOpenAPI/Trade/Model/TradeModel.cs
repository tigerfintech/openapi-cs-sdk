using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class TradeModel : ApiModel
  {
    [JsonProperty(PropertyName = "secret_key")]
    public string SecretKey { get; set; }

    public TradeModel() : base()
    {
    }
  }
}

