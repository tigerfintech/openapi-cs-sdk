using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FundSymbolModel : ApiModel
  {
    [JsonProperty(PropertyName = "symbols")]
    public List<string> Symbols { get; set; }

    public FundSymbolModel() : base()
    {
    }
  }
}

