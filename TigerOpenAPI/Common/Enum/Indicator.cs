using System;
using System.Collections.Generic;

namespace TigerOpenAPI.Common.Enum
{
  public interface Indicator
  {
    string GetValue();

    public static ISet<string> GetValues(ISet<Indicator>? indicators)
    {
      ISet<string> values = new HashSet<string>();
      if (indicators is not null)
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

