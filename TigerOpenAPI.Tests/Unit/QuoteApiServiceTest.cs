using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TigerOpenAPI.Quote;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for QuoteApiService constant values, the AllQuoteApiSet registry,
  /// and IsQuoteApi() routing predicate. Pure static data — zero network.
  /// </summary>
  [TestFixture]
  public class QuoteApiServiceTest
  {
    private static string[] GetConstantValues()
    {
      return typeof(QuoteApiService)
          .GetFields(BindingFlags.Public | BindingFlags.Static)
          .Where(f => f.IsLiteral && f.FieldType == typeof(string))
          .Select(f => (string)f.GetRawConstantValue()!)
          .ToArray();
    }

    [Test]
    public void AllConstants_AreNonEmptyStrings()
    {
      var values = GetConstantValues();
      Assert.That(values, Is.Not.Empty, "QuoteApiService should expose constants");
      foreach (var value in values)
      {
        Assert.That(value, Is.Not.Null.And.Not.Empty,
            "QuoteApiService constant must be a non-empty string");
      }
    }

    [Test]
    public void AllConstants_AreLowerSnakeCase_NoSpaces()
    {
      var values = GetConstantValues();
      foreach (var value in values)
      {
        Assert.That(value, Does.Match("^[a-z][a-z0-9]*(_[a-z0-9]+)*$"),
            "constant must be snake_case (lowercase letters/digits, underscore-delimited): " + value);
      }
    }

    [Test]
    public void AllQuoteApiSet_ContainsExpectedQuoteMethods()
    {
      string[] expected =
      {
        QuoteApiService.MARKET_STATE,
        QuoteApiService.ALL_SYMBOLS,
        QuoteApiService.ALL_SYMBOL_NAMES,
        QuoteApiService.BRIEF,
        QuoteApiService.STOCK_DETAIL,
        QuoteApiService.HOUR_TRADING_TIMELINE,
        QuoteApiService.TIMELINE,
        QuoteApiService.HISTORY_TIMELINE,
        QuoteApiService.KLINE,
        QuoteApiService.TRADE_TICK,
        QuoteApiService.QUOTE_CONTRACT,
        QuoteApiService.QUOTE_REAL_TIME,
        QuoteApiService.QUOTE_SHORTABLE_STOCKS,
        QuoteApiService.QUOTE_STOCK_TRADE,
        QuoteApiService.QUOTE_DEPTH,
        QuoteApiService.QUOTE_DELAY,
        QuoteApiService.QUOTE_OVERNIGHT,
        QuoteApiService.TRADING_CALENDAR,
        QuoteApiService.STOCK_BROKER,
        QuoteApiService.CAPITAL_DISTRIBUTION,
        QuoteApiService.CAPITAL_FLOW,
        QuoteApiService.MARKET_SCANNER,
        QuoteApiService.MARKET_SCANNER_TAGS,
      };
      foreach (var name in expected)
      {
        Assert.That(QuoteApiService.AllQuoteApiSet, Does.Contain(name),
            "AllQuoteApiSet should contain quote method: " + name);
      }
    }

    [Test]
    public void AllQuoteApiSet_ContainsExpectedOptionAndWarrantMethods()
    {
      string[] expected =
      {
        QuoteApiService.OPTION_EXPIRATION,
        QuoteApiService.OPTION_CHAIN,
        QuoteApiService.OPTION_BRIEF,
        QuoteApiService.OPTION_KLINE,
        QuoteApiService.OPTION_TRADE_TICK,
        QuoteApiService.OPTION_DEPTH,
        QuoteApiService.ALL_HK_OPTION_SYMBOLS,
        QuoteApiService.WARRANT_FILTER,
        QuoteApiService.WARRANT_REAL_TIME_QUOTE,
        QuoteApiService.OPTION_ANALYSIS,
        QuoteApiService.OPTION_TIMELINE,
      };
      foreach (var name in expected)
      {
        Assert.That(QuoteApiService.AllQuoteApiSet, Does.Contain(name),
            "AllQuoteApiSet should contain option/warrant method: " + name);
      }
    }

    [Test]
    public void AllQuoteApiSet_ContainsExpectedFutureAndFundMethods()
    {
      string[] expected =
      {
        QuoteApiService.FUTURE_EXCHANGE,
        QuoteApiService.FUTURE_CONTRACT_BY_CONTRACT_CODE,
        QuoteApiService.FUTURE_CONTRACT_BY_EXCHANGE_CODE,
        QuoteApiService.FUTURE_CONTINUOUS_CONTRACTS,
        QuoteApiService.FUTURE_CURRENT_CONTRACT,
        QuoteApiService.FUTURE_CONTRACTS,
        QuoteApiService.FUTURE_KLINE,
        QuoteApiService.FUTURE_REAL_TIME_QUOTE,
        QuoteApiService.FUTURE_TICK,
        QuoteApiService.FUTURE_TRADING_DATE,
        QuoteApiService.FUTURE_HISTORY_MAIN_CONTRACT,
        QuoteApiService.FUTURE_DEPTH,
        QuoteApiService.FUND_ALL_SYMBOLS,
        QuoteApiService.FUND_CONTRACTS,
        QuoteApiService.FUND_QUOTE,
        QuoteApiService.FUND_HISTORY_QUOTE,
      };
      foreach (var name in expected)
      {
        Assert.That(QuoteApiService.AllQuoteApiSet, Does.Contain(name),
            "AllQuoteApiSet should contain future/fund method: " + name);
      }
    }

    [Test]
    public void AllQuoteApiSet_ContainsExpectedFundamentalAndPermissionMethods()
    {
      string[] expected =
      {
        QuoteApiService.FINANCIAL_DAILY,
        QuoteApiService.FINANCIAL_REPORT,
        QuoteApiService.CORPORATE_ACTION,
        QuoteApiService.INDUSTRY_LIST,
        QuoteApiService.INDUSTRY_STOCKS,
        QuoteApiService.STOCK_INDUSTRY,
        QuoteApiService.FINANCIAL_CURRENCY,
        QuoteApiService.FINANCIAL_EXCHANGE_RATE,
        QuoteApiService.STOCK_FUNDAMENTAL,
        QuoteApiService.BROKER_HOLD,
        QuoteApiService.GRAB_QUOTE_PERMISSION,
        QuoteApiService.GET_QUOTE_PERMISSION,
        QuoteApiService.KLINE_QUOTA,
        QuoteApiService.USER_LICENSE,
        QuoteApiService.USER_TOKEN_REFRESH,
      };
      foreach (var name in expected)
      {
        Assert.That(QuoteApiService.AllQuoteApiSet, Does.Contain(name),
            "AllQuoteApiSet should contain method: " + name);
      }
    }

    [Test]
    public void AllQuoteApiSet_HasExpectedCount()
    {
      // count all public const string members and ensure the set mirrors them exactly
      var constCount = GetConstantValues().Length;
      Assert.That(QuoteApiService.AllQuoteApiSet.Count, Is.EqualTo(constCount),
          "AllQuoteApiSet should contain every declared constant");
    }

    [Test]
    public void IsQuoteApi_ReturnsTrue_ForKnownQuoteMethods()
    {
      Assert.That(QuoteApiService.IsQuoteApi(QuoteApiService.KLINE), Is.True);
      Assert.That(QuoteApiService.IsQuoteApi(QuoteApiService.TIMELINE), Is.True);
      Assert.That(QuoteApiService.IsQuoteApi(QuoteApiService.OPTION_CHAIN), Is.True);
      Assert.That(QuoteApiService.IsQuoteApi(QuoteApiService.USER_TOKEN_REFRESH), Is.True);
      Assert.That(QuoteApiService.IsQuoteApi(QuoteApiService.GRAB_QUOTE_PERMISSION), Is.True);
    }

    [Test]
    public void IsQuoteApi_ReturnsFalse_ForUnknownMethods()
    {
      Assert.That(QuoteApiService.IsQuoteApi("place_order"), Is.False,
          "trade api method must not be a quote api");
      Assert.That(QuoteApiService.IsQuoteApi("not_a_real_api"), Is.False);
      Assert.That(QuoteApiService.IsQuoteApi("KLINE"), Is.False,
          "case-sensitive: uppercase must not match");
    }

    [Test]
    public void IsQuoteApi_ReturnsFalse_ForEmptyOrNull()
    {
      Assert.That(QuoteApiService.IsQuoteApi(""), Is.False);
      Assert.That(QuoteApiService.IsQuoteApi(null), Is.False);
      Assert.That(QuoteApiService.IsQuoteApi("   "), Is.False);
    }

    [Test]
    public void SpecificConstantValues_MatchExpectedWireNames()
    {
      Assert.That(QuoteApiService.MARKET_STATE, Is.EqualTo("market_state"));
      Assert.That(QuoteApiService.ALL_SYMBOLS, Is.EqualTo("all_symbols"));
      Assert.That(QuoteApiService.BRIEF, Is.EqualTo("brief"));
      Assert.That(QuoteApiService.STOCK_DETAIL, Is.EqualTo("stock_detail"));
      Assert.That(QuoteApiService.KLINE, Is.EqualTo("kline"));
      Assert.That(QuoteApiService.TIMELINE, Is.EqualTo("timeline"));
      Assert.That(QuoteApiService.HISTORY_TIMELINE, Is.EqualTo("history_timeline"));
      Assert.That(QuoteApiService.TRADE_TICK, Is.EqualTo("trade_tick"));
      Assert.That(QuoteApiService.QUOTE_REAL_TIME, Is.EqualTo("quote_real_time"));
      Assert.That(QuoteApiService.QUOTE_DEPTH, Is.EqualTo("quote_depth"));
      Assert.That(QuoteApiService.TRADING_CALENDAR, Is.EqualTo("trading_calendar"));
      Assert.That(QuoteApiService.OPTION_CHAIN, Is.EqualTo("option_chain"));
      Assert.That(QuoteApiService.OPTION_BRIEF, Is.EqualTo("option_brief"));
      Assert.That(QuoteApiService.OPTION_KLINE, Is.EqualTo("option_kline"));
      Assert.That(QuoteApiService.OPTION_ANALYSIS, Is.EqualTo("option_analysis"));
      Assert.That(QuoteApiService.OPTION_TIMELINE, Is.EqualTo("option_timeline"));
      Assert.That(QuoteApiService.WARRANT_FILTER, Is.EqualTo("warrant_filter"));
      Assert.That(QuoteApiService.FUTURE_KLINE, Is.EqualTo("future_kline"));
      Assert.That(QuoteApiService.FUND_QUOTE, Is.EqualTo("fund_quote"));
      Assert.That(QuoteApiService.FINANCIAL_DAILY, Is.EqualTo("financial_daily"));
      Assert.That(QuoteApiService.FINANCIAL_REPORT, Is.EqualTo("financial_report"));
      Assert.That(QuoteApiService.CORPORATE_ACTION, Is.EqualTo("corporate_action"));
      Assert.That(QuoteApiService.BROKER_HOLD, Is.EqualTo("broker_hold"));
      Assert.That(QuoteApiService.GRAB_QUOTE_PERMISSION, Is.EqualTo("grab_quote_permission"));
      Assert.That(QuoteApiService.GET_QUOTE_PERMISSION, Is.EqualTo("get_quote_permission"));
      Assert.That(QuoteApiService.KLINE_QUOTA, Is.EqualTo("kline_quota"));
      Assert.That(QuoteApiService.USER_LICENSE, Is.EqualTo("user_license"));
      Assert.That(QuoteApiService.USER_TOKEN_REFRESH, Is.EqualTo("user_token_refresh"));
    }
  }
}
