using System;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Trade;

namespace TigerOpenAPI.Tests.Integration
{
  /// <summary>
  /// Shared configuration for integration tests. Reads credentials from
  /// environment variables:
  ///   <list type="bullet">
  ///     <item>TIGEROPEN_TIGER_ID      (fallback: TIGER_ID)</item>
  ///     <item>TIGEROPEN_PRIVATE_KEY   (fallback: TIGER_PRIVATE_KEY)</item>
  ///     <item>TIGEROPEN_ACCOUNT       (fallback: TIGER_ACCOUNT)</item>
  ///     <item>TIGEROPEN_LICENSE       (fallback: TIGER_LICENSE, default: TBNZ)</item>
  ///   </list>
  /// When credentials are missing, <see cref="EnsureCredentials"/> ignores
  /// the fixture so CI without secrets still passes.
  /// </summary>
  public static class IntegTestConfig
  {
    private static readonly Lazy<QuoteClient?> _quoteClient =
        new Lazy<QuoteClient?>(CreateQuoteClient);
    private static readonly Lazy<TradeClient?> _tradeClient =
        new Lazy<TradeClient?>(CreateTradeClient);

    /// <summary>The account ID for trade API calls.</summary>
    public static string Account =>
        Env("TIGEROPEN_ACCOUNT") ?? Env("TIGER_ACCOUNT") ?? string.Empty;

    /// <summary>Shared QuoteClient; null if credentials are missing.</summary>
    public static QuoteClient? QuoteClient => _quoteClient.Value;

    /// <summary>Shared TradeClient; null if credentials are missing.</summary>
    public static TradeClient? TradeClient => _tradeClient.Value;

    /// <summary>
    /// Call from <c>[OneTimeSetUp]</c> for quote-only fixtures.
    /// Ignores the fixture when tiger_id / private_key are absent.
    /// Does <em>not</em> require TIGEROPEN_ACCOUNT — quote APIs work without it.
    /// </summary>
    public static void EnsureCredentials()
    {
      string tigerId = Env("TIGEROPEN_TIGER_ID") ?? Env("TIGER_ID");
      string privateKey = Env("TIGEROPEN_PRIVATE_KEY") ?? Env("TIGER_PRIVATE_KEY");

      if (string.IsNullOrWhiteSpace(tigerId) || string.IsNullOrWhiteSpace(privateKey))
      {
        Assert.Ignore(
            "Integration credentials not set — skipping. " +
            "Set TIGEROPEN_TIGER_ID + TIGEROPEN_PRIVATE_KEY " +
            "(+ TIGEROPEN_ACCOUNT for trade tests).");
      }
    }

    /// <summary>
    /// Call from <c>[OneTimeSetUp]</c> for trade fixtures.
    /// Ignores the fixture when any of tiger_id / private_key / account are absent.
    /// </summary>
    public static void EnsureTradeCredentials()
    {
      EnsureCredentials();   // check tiger_id + private_key first

      string account = Env("TIGEROPEN_ACCOUNT") ?? Env("TIGER_ACCOUNT");
      if (string.IsNullOrWhiteSpace(account))
      {
        Assert.Ignore(
            "TIGEROPEN_ACCOUNT is not set — skipping trade tests. " +
            "Set TIGEROPEN_ACCOUNT (or TIGER_ACCOUNT) to run integration tests.");
      }
    }

    private static TigerConfig BuildConfig()
    {
      string tigerId = Env("TIGEROPEN_TIGER_ID") ?? Env("TIGER_ID") ?? string.Empty;
      string privateKey = Env("TIGEROPEN_PRIVATE_KEY") ?? Env("TIGER_PRIVATE_KEY") ?? string.Empty;
      string account = Env("TIGEROPEN_ACCOUNT") ?? Env("TIGER_ACCOUNT") ?? string.Empty;
      string licenseStr = Env("TIGEROPEN_LICENSE") ?? Env("TIGER_LICENSE") ?? "TBNZ";

      if (!Enum.TryParse<License>(licenseStr, true, out var license))
        license = License.TBNZ;

      return new TigerConfig
      {
        TigerId = tigerId,
        PrivateKey = privateKey,
        DefaultAccount = account,
        License = license,
        AutoGrabPermission = false,
        AutoRefreshToken = false,
      };
    }

    private static QuoteClient? CreateQuoteClient()
    {
      string tigerId = Env("TIGEROPEN_TIGER_ID") ?? Env("TIGER_ID");
      string privateKey = Env("TIGEROPEN_PRIVATE_KEY") ?? Env("TIGER_PRIVATE_KEY");
      if (string.IsNullOrWhiteSpace(tigerId) || string.IsNullOrWhiteSpace(privateKey))
        return null;
      return new QuoteClient(BuildConfig());
    }

    private static TradeClient? CreateTradeClient()
    {
      string tigerId = Env("TIGEROPEN_TIGER_ID") ?? Env("TIGER_ID");
      string privateKey = Env("TIGEROPEN_PRIVATE_KEY") ?? Env("TIGER_PRIVATE_KEY");
      if (string.IsNullOrWhiteSpace(tigerId) || string.IsNullOrWhiteSpace(privateKey))
        return null;
      return new TradeClient(BuildConfig());
    }

    private static string? Env(string key) =>
        Environment.GetEnvironmentVariable(key) is string v && !string.IsNullOrWhiteSpace(v) ? v : null;
  }
}
