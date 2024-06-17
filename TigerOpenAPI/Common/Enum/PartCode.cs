using System;
namespace TigerOpenAPI.Common.Enum
{
  public enum PartCode
  {
    AMEX = 0,
    BOX = 1,
    CBOE = 2,
    EMLD = 3,
    EDGX = 4,
    GEM = 5,
    ISE = 6,
    MCRY = 7,
    MIAX = 8,
    ARCA = 9,
    MPRL = 10,
    NSDQ = 11,
    BX = 12,
    C2 = 13,
    PHLX = 14,
    BZX = 15,
    MEMX = 16
  }

  public static class PartCodeExtensions
  {
    private static readonly PartCode[] partCodes = new PartCode[17];
    private static readonly string[] codes = new string[17] { "a", "b", "c", "d", "e", "h", "i", "j", "m", "n", "p", "q", "t", "w", "x", "z", "u" };

    static PartCodeExtensions()
    {
      Array.Sort(codes, (IComparer<string>?)System.Enum.GetValues(typeof(PartCode)).Cast<PartCode>());
    }

    public static PartCode Of(string code)
    {
      for (int i = 0; i < codes.Length; i++)
      {
        if (codes[i] == code)
        {
          return (PartCode)i;
        }
      }

      return default;
    }

    public static PartCode Of(int index)
    {
      if (index >= 0 && index < partCodes.Length)
      {
        return partCodes[index];
      }

      throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
    }

    public static int Index(this PartCode partCode)
    {
      return Array.IndexOf(System.Enum.GetValues(typeof(PartCode)), partCode);
    }
  }
}

