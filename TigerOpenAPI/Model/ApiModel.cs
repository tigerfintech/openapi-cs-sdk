using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;

namespace TigerOpenAPI.Model
{
  public class ApiModel
  {
    [JsonProperty(PropertyName = "lang"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Language Lang { get; set; }

    [JsonProperty(PropertyName = "account")]
    public string? Account { get; set; }

    public ApiModel()
    {
    }
  }
}
