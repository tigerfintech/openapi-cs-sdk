// DictionaryExtensions.cs — polyfill for Dictionary.TryAdd (available in .NET Core 2.0+)
#if !NETCOREAPP2_0_OR_GREATER && !NET5_0_OR_GREATER
using System.Collections.Generic;

namespace TigerOpenAPI.Common.Util
{
  internal static class DictionaryExtensions
  {
    /// <summary>
    /// Attempts to add the specified key and value to the dictionary.
    /// Returns false if the key already exists.
    /// </summary>
    public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
    {
      if (dict.ContainsKey(key))
        return false;
      dict.Add(key, value);
      return true;
    }
  }
}
#endif
