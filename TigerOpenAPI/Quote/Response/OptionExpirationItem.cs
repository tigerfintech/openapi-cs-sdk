using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Quote.Response
{
  public class OptionExpirationItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
    [JsonProperty(PropertyName = "count")]
    public int Count { get; set; }

    [JsonProperty(PropertyName = "dates")]
    public List<string> Dates { get; set; }
    [JsonProperty(PropertyName = "timestamps")]
    public List<long> Timestamps { get; set; }
    [JsonProperty(PropertyName = "periodTags")]
    public List<string> PeriodTags { get; set; }
    [JsonProperty(PropertyName = "optionSymbols")]
    public List<string> OptionSymbols { get; set; }

  }
}

