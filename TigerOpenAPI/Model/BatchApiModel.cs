using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TigerOpenAPI.Model
{
  public class BatchApiModel<T> : ApiModel where T : ApiModel
  {

    [JsonProperty(PropertyName = "items")]
    public List<T> Items { get; set; }

    public BatchApiModel() : base()
    {
    }
  }
}
