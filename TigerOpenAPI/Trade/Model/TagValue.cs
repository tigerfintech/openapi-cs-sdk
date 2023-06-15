using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Model
{
  public class TagValue
  {

    [JsonProperty(PropertyName = "tag")]
    public string Tag { get; set; }
    [JsonProperty(PropertyName = "value")]
    public string? Value { get; set; }

    public TagValue()
    {
    }

    public static TagValue? BuildTagValue(string tag, Object? value)
    {
      if (string.IsNullOrEmpty(tag) || value is null)
      {
        return null;
      }
      return new TagValue()
      {
        Tag = tag,
        Value = value.ToString()
      };
    }
  }
}

