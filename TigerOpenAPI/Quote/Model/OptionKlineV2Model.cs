using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class OptionKlineV2Model : OptionModel
  {

    [JsonProperty(PropertyName = "option_query")]
    public List<OptionKlineModel> OptionQuery { get; set; }

    public OptionKlineV2Model() : base()
    {
    }
  }
}

