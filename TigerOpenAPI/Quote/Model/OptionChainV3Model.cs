using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class OptionChainV3Model : ApiModel
  {

    [JsonProperty(PropertyName = "option_basic")]
    public List<OptionChainModel> OptionBasic { get; set; }

    [JsonProperty(PropertyName = "option_filter")]
    public OptionChainFilterModel OptionFilter { get; set; }

    public OptionChainV3Model() : base()
    {
    }
  }
}

