using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TigerOpenAPI.Model
{
  /// <summary>
  /// Model whose <c>Items</c> list is serialized as the top-level JSON array
  /// (see the special-case branch in <c>TigerClient.BuildParams</c>).
  ///
  /// Any reference type works as the item type; the constraint is
  /// intentionally loose so that leaf types with camelCase wire names
  /// (e.g. <c>OptionQueryItem</c>) can be used without having to inherit
  /// from <see cref="ApiModel"/> and pick up its <c>lang</c> / <c>account</c>
  /// fields as siblings inside each array element.
  /// </summary>
  public class BatchApiModel<T> : ApiModel where T : class
  {

    [JsonProperty(PropertyName = "items")]
    public List<T> Items { get; set; }

    public BatchApiModel() : base()
    {
    }
  }
}
