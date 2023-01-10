using System;
using System.Security.Principal;
using System.Text.RegularExpressions;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Common.Util
{
  public class SymbolUtil
  {
    private SymbolUtil() { }

    private static string CHAR_SYMBOL_PATTERN = "[A-Z]+(.[A-Z0-9]+)?";
    private static string FUTURE_SYMBOL_PATTERN = "^[0-9A-Z]+([0-9]{4}|main){1}$";

    public static OptionSymbol convertToOptionSymbol(string identifier)
    {
      if (string.IsNullOrWhiteSpace(identifier) || identifier.Length != 21)
      {
        throw new TigerApiException("option identifier format error");
      }

      string[] symbolKeys = identifier.Split(" +");
      if (symbolKeys != null && symbolKeys.Length != 1 && symbolKeys.Length != 2)
      {
        throw new TigerApiException("option identifier format error");
      }

      string expiryRightStrike = identifier.Substring(6);

      OptionSymbol optionSymbol = new OptionSymbol();
      optionSymbol.Symbol = identifier.Substring(0, 6).Trim();
      optionSymbol.Expiry = "20" + expiryRightStrike.Substring(0, 2) + "-"
          + expiryRightStrike.Substring(2, 2) + "-"
          + expiryRightStrike.Substring(4, 2);
      optionSymbol.Right = expiryRightStrike.Substring(6, 1).Equals("C") ? "CALL" : "PUT";
      optionSymbol.Strike =
          Int32.Parse(expiryRightStrike.Substring(7, 5)) + "." + expiryRightStrike.Substring(12);

      return optionSymbol;
    }

    public static bool isUsStockSymbol(String symbol)
    {
      if (string.IsNullOrWhiteSpace(symbol))
      {
        return false;
      }
      return Regex.IsMatch(symbol, CHAR_SYMBOL_PATTERN);
    }

    public static TimeZoneInfo getZoneIdBySymbol(string symbol, TimeZoneInfo defaultTimeZoneInfo)
    {
      if (string.IsNullOrWhiteSpace(symbol))
      {
        return defaultTimeZoneInfo;
      }
      return isUsStockSymbol(symbol) ? CustomTimeZone.NY_ZONE : CustomTimeZone.HK_ZONE;
    }

    public static bool isFutureSymbol(string symbol)
    {
      if (string.IsNullOrWhiteSpace(symbol)
          || symbol.Length <= 4 || symbol.Length >= 12
          || symbol.StartsWith("BK") || symbol.All(char.IsDigit))
      {
        return false;
      }
      return Regex.IsMatch(symbol, FUTURE_SYMBOL_PATTERN);
    }
  }
}

