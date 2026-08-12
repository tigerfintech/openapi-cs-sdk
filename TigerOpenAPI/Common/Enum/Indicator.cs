using System;
using System.Collections.Generic;

namespace TigerOpenAPI.Common.Enum
{
  public interface Indicator
  {
    string GetValue();
  }

  /// <summary>
  /// Helper class providing static methods for Indicator interface.
  /// On .NET 5+, also accessible via Indicator.GetValues() default interface method.
  /// </summary>
  public static class IndicatorHelper
  {
    public static ISet<string> GetValues(ISet<Indicator>? indicators)
    {
      ISet<string> values = new HashSet<string>();
      if (indicators != null)
      {
        foreach (Indicator indicator in indicators)
        {
          values.Add(indicator.GetValue());
        }
      }
      return values;
    }
  }
}
