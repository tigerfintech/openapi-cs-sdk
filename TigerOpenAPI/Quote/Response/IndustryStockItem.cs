using Newtonsoft.Json;

namespace TigerOpenAPI.Quote.Response
{
  /// <summary>
  /// A stock entry inside an industry, returned by industry_stocks.
  /// </summary>
  public class IndustryStockItem
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "industryId")]
    public string IndustryId { get; set; }

    [JsonProperty(PropertyName = "change")]
    public double Change { get; set; }

    [JsonProperty(PropertyName = "changeRate")]
    public double ChangeRate { get; set; }
  }
}
