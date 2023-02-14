using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class ContractModel : BaseContractModel
  {
    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    public ContractModel() : base()
    {
    }
  }
}
