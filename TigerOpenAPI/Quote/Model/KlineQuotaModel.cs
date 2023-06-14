using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Quote.Model
{
  public class KlineQuotaModel : ApiModel
  {

    [JsonProperty(PropertyName = "with_details")]
    public Boolean WithDetails { get; set; }

    public KlineQuotaModel() : base()
    {
    }
  }
}

