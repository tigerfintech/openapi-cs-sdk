using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class OptionExerciseCheckItem
  {
    [JsonProperty(PropertyName = "availableQuantity")]
    public double? AvailableQuantity { get; set; }

    [JsonProperty(PropertyName = "position")]
    public double? Position { get; set; }

    [JsonProperty(PropertyName = "stkPosition")]
    public double? StkPosition { get; set; }

    [JsonProperty(PropertyName = "stkPositionChange")]
    public double? StkPositionChange { get; set; }

    [JsonProperty(PropertyName = "stkPositionBefore")]
    public double? StkPositionBefore { get; set; }

    [JsonProperty(PropertyName = "stkPositionAfter")]
    public double? StkPositionAfter { get; set; }

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }
  }

  public class OptionExerciseCheckResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public OptionExerciseCheckItem Data { get; set; }

    public OptionExerciseCheckResponse() { }
  }
}
