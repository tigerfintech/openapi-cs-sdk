using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FutureContractByConCodeModel : ApiModel
  {
    [JsonProperty(PropertyName = "contract_code")]
    public string ContractCode { get; set; }

    public FutureContractByConCodeModel() : base()
    {
    }
  }
}

