using System;

namespace TigerOpenAPI.Common.Util
{
  public class AccountUtil
  {
    private AccountUtil() { }

    private const int PAPER_ACCOUNT_LEN = 17;

    public static bool IsOmnibusAccount(string? account) => string.IsNullOrWhiteSpace(account) ?
      false : (account.Length < PAPER_ACCOUNT_LEN && account.All(char.IsDigit));

    public static bool IsVirtualAccount(string? account) => string.IsNullOrWhiteSpace(account) ?
      false : (account.Length == PAPER_ACCOUNT_LEN && account.All(char.IsDigit));

    public static bool IsGlobalAccount(string? account) => string.IsNullOrWhiteSpace(account) ?
      false : (account.StartsWith("U") || account.StartsWith("DU") || account.StartsWith("F") || account.StartsWith("DF"));
  }
}

