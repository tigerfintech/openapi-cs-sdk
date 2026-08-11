using System.Collections.Generic;
using Newtonsoft.Json;
using TigerOpenAPI.Common;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Response
{
  public class FutureContractsResponse : TigerResponse
  {
    [JsonProperty(PropertyName = "data")]
    [JsonConverter(typeof(ListOrObjectConverter<FutureContractItem>))]
    public List<FutureContractItem> Data { get; set; }
  }
}
