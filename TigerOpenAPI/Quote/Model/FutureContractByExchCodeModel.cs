using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class FutureContractByExchCodeModel : ApiModel
  {
    [JsonProperty(PropertyName = "exchange_code")]
    public string ExchangeCode { get; set; }

    public FutureContractByExchCodeModel() : base()
    {
    }
  }
}

