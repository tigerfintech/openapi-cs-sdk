using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class OptionCommonModel : ApiModel
  {

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "right")]
    public string Right { get; set; }

    [JsonProperty(PropertyName = "strike")]
    public string Strike { get; set; }

    [JsonProperty(PropertyName = "expiry")]
    public long Expiry { get; set; }

    public OptionCommonModel() : base()
    {
    }
  }
}

