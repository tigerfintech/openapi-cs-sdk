using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class OptionBasicModel : OptionModel
  {

    [JsonProperty(PropertyName = "option_basic")]
    public List<OptionCommonModel> OptionBasic { get; set; }

    public OptionBasicModel() : base()
    {
    }
  }
}

