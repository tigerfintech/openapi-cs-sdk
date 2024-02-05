using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class QuoteMarketModel : ApiModel
  {

    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    // 'package_name' and 'include_otc' only used by QuoteApiService.ALL_SYMBOLS
    [JsonProperty(PropertyName = "package_name"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public PackageName PackageName { get; set; }
    [JsonProperty(PropertyName = "include_otc")]
    public Boolean IncludeOTC { get; set; }

    public QuoteMarketModel() : base()
    {
    }
  }
}

