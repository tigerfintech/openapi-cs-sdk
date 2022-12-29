using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FutureContractCodesModel : ApiModel
  {
    [JsonProperty(PropertyName = "contract_codes")]
    public List<string> ContractCodes { get; set; }

    public FutureContractCodesModel() : base()
    {
    }
  }
}

