using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Model
{
  public class OptionExercisePositionModel : TradeModel
  {
    /// <summary>Exercise type: "Exercise" | "Expire"</summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    public OptionExercisePositionModel() : base() { }
  }
}
