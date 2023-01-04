using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Trade.Model
{
  public class ContractsModel : BaseContractModel
  {
    [JsonProperty(PropertyName = "symbols")]
    public List<string> Symbols { get; set; }

    public ContractsModel() : base()
    {
    }
  }
}
