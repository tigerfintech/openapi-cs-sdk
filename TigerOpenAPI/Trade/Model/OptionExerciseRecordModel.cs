using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Model
{
  public class OptionExerciseRecordModel : TradeModel
  {
    [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
    public string Type { get; set; }

    [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
    public string Status { get; set; }

    [JsonProperty(PropertyName = "symbol", NullValueHandling = NullValueHandling.Ignore)]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "order_by", NullValueHandling = NullValueHandling.Ignore)]
    public string OrderBy { get; set; }

    [JsonProperty(PropertyName = "page")]
    public int Page { get; set; } = 1;

    [JsonProperty(PropertyName = "size")]
    public int Size { get; set; } = 20;

    public OptionExerciseRecordModel() : base() { }
  }
}
