using Newtonsoft.Json;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Response
{
  public class OptionExerciseSubmitResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    public object Data { get; set; }

    public OptionExerciseSubmitResponse() { }
  }
}
