using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// An industry entry from the industry_list API.
  ///
  /// Wire format uses camelCase field names:
  ///   { "id": "10", "nameCN": "能源", "nameEN": "Energy", "industryLevel": "GSECTOR" }
  ///
  /// Matches Python's IndustryListResponse which reads ind.get('nameCN'),
  /// ind.get('nameEN'), ind.get('industryLevel'). Rust SDK mirrors the
  /// same shape (name_cn / name_en / level).
  /// </summary>
  public class IndustryItem
  {
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>Chinese name. Wire: <c>nameCN</c>.</summary>
    [JsonProperty(PropertyName = "nameCN")]
    public string NameCN { get; set; }

    /// <summary>English name. Wire: <c>nameEN</c>.</summary>
    [JsonProperty(PropertyName = "nameEN")]
    public string NameEN { get; set; }

    /// <summary>Industry level: GSECTOR / GGROUP / GIND / GSUBIND. Wire: <c>industryLevel</c>.</summary>
    [JsonProperty(PropertyName = "industryLevel")]
    public string IndustryLevel { get; set; }
  }
}
