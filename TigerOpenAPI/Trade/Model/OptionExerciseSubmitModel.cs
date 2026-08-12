using Newtonsoft.Json;
using System;

namespace TigerOpenAPI.Trade.Model
{
  public class OptionExerciseSubmitModel : TradeModel
  {
    [JsonProperty(PropertyName = "contract_id")]
    public long ContractId { get; set; }

    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    [JsonProperty(PropertyName = "quantity")]
    public double Quantity { get; set; }

    /// <summary>Execution date yyyy-MM-dd. Required when Type=Exercise.</summary>
    [JsonProperty(PropertyName = "executing_date", NullValueHandling = NullValueHandling.Ignore)]
    public string ExecutingDate { get; set; }

    /// <summary>Whether to force exercise. Required when Type=Exercise.</summary>
    [JsonProperty(PropertyName = "is_force", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsForce { get; set; }

    /// <summary>In-the-money rate 0-10. Optional, only for Expire type.</summary>
    [JsonProperty(PropertyName = "itm_rate", NullValueHandling = NullValueHandling.Ignore)]
    public int? ItmRate { get; set; }

    public OptionExerciseSubmitModel() : base() { }
  }
}
