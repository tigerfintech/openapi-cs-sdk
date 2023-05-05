using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class MarketScannerTagsModel : ApiModel
  {
    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    /** MultiTagField list */
    [JsonProperty(PropertyName = "multi_tag_field_list")]
    public List<string> MultiTagFieldList { get; set; }

  }
}
