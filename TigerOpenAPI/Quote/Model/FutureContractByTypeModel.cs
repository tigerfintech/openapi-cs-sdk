using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FutureContractByTypeModel : ApiModel
  {
    [JsonProperty(PropertyName = "type")]
    public string FutureType { get; set; }

    public FutureContractByTypeModel() : base()
    {
    }
  }
}

