using Newtonsoft.Json;

namespace TigerOpenAPI.Trade.Model
{
  public class OptionExerciseCancelModel : TradeModel
  {
    /// <summary>Exercise record ID from GetOptionExerciseRecords.</summary>
    [JsonProperty(PropertyName = "id")]
    public long Id { get; set; }

    public OptionExerciseCancelModel() : base() { }
  }
}
