using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TigerOpenAPI.Common
{
  /// <summary>
  /// Deserializes a JSON value that may be either a single object T or an array of T
  /// into a List&lt;T&gt;. Handles the inconsistent server response shape for some
  /// future APIs (e.g. future_continuous_contracts).
  /// </summary>
  public class ListOrObjectConverter<T> : JsonConverter<List<T>>
  {
    public override List<T>? ReadJson(JsonReader reader, Type objectType, List<T>? existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
      var token = JToken.Load(reader);
      if (token.Type == JTokenType.Array)
        return token.ToObject<List<T>>(serializer);
      if (token.Type == JTokenType.Object || token.Type == JTokenType.String)
        return new List<T> { token.ToObject<T>(serializer)! };
      if (token.Type == JTokenType.Null)
        return new List<T>();
      return new List<T>();
    }

    public override void WriteJson(JsonWriter writer, List<T>? value, JsonSerializer serializer)
      => serializer.Serialize(writer, value);
  }
}
