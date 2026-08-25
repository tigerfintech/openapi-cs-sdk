using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TigerOpenAPI.Trade;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for TradeApiService constant values, the AllTradeApiSet registry,
  /// and IsTradeApi() routing predicate. Pure static data — zero network.
  /// </summary>
  [TestFixture]
  public class TradeApiServiceTest
  {
    private static string[] GetConstantValues()
    {
      return typeof(TradeApiService)
          .GetFields(BindingFlags.Public | BindingFlags.Static)
          .Where(f => f.IsLiteral && f.FieldType == typeof(string))
          .Select(f => (string)f.GetRawConstantValue()!)
          .ToArray();
    }

    [Test]
    public void AllConstants_AreNonEmptyStrings()
    {
      var values = GetConstantValues();
      Assert.That(values, Is.Not.Empty, "TradeApiService should expose constants");
      foreach (var value in values)
      {
        Assert.That(value, Is.Not.Null.And.Not.Empty,
            "TradeApiService constant must be a non-empty string");
      }
    }

    [Test]
    public void AllConstants_AreSnakeCase_NoUppercase()
    {
      var values = GetConstantValues();
      foreach (var value in values)
      {
        Assert.That(value, Does.Not.Contain(" "),
            "constant must not contain spaces: " + value);
        Assert.That(value, Is.EqualTo(value.ToLowerInvariant()),
            "constant must be lowercase snake_case: " + value);
      }
    }

    [Test]
    public void AllTradeApiSet_ContainsExpectedTradeMethods()
    {
      string[] expected =
      {
        TradeApiService.PLACE_ORDER,
        TradeApiService.PREVIEW_ORDER,
        TradeApiService.CANCEL_ORDER,
        TradeApiService.MODIFY_ORDER,
        TradeApiService.ORDER_NO,
        TradeApiService.PLACE_FOREX_ORDER,
        TradeApiService.TRANSFER_SEGMENT_FUND,
        TradeApiService.CANCEL_SEGMENT_FUND,
      };
      foreach (var name in expected)
      {
        Assert.That(TradeApiService.AllTradeApiSet, Does.Contain(name),
            "AllTradeApiSet should contain trade method: " + name);
      }
    }

    [Test]
    public void AllTradeApiSet_ContainsExpectedAssetMethods()
    {
      string[] expected =
      {
        TradeApiService.ACCOUNTS,
        TradeApiService.ASSETS,
        TradeApiService.PRIME_ASSETS,
        TradeApiService.ANALYTICS_ASSET,
        TradeApiService.POSITIONS,
        TradeApiService.ORDERS,
        TradeApiService.ACTIVE_ORDERS,
        TradeApiService.INACTIVE_ORDERS,
        TradeApiService.FILLED_ORDERS,
        TradeApiService.ORDER_TRANSACTIONS,
        TradeApiService.SEGMENT_FUND_HISTORY,
        TradeApiService.SEGMENT_FUND_AVAILABLE,
        TradeApiService.ESTIMATE_TRADABLE_QUANTITY,
      };
      foreach (var name in expected)
      {
        Assert.That(TradeApiService.AllTradeApiSet, Does.Contain(name),
            "AllTradeApiSet should contain asset method: " + name);
      }
    }

    [Test]
    public void AllTradeApiSet_ContainsExpectedOptionExerciseMethods()
    {
      string[] expected =
      {
        TradeApiService.OPTION_EXERCISE_SUBMIT,
        TradeApiService.OPTION_EXERCISE_CHECK,
        TradeApiService.OPTION_EXERCISE_RECORD,
        TradeApiService.OPTION_EXERCISE_POSITION,
        TradeApiService.OPTION_EXERCISE_CANCEL,
      };
      foreach (var name in expected)
      {
        Assert.That(TradeApiService.AllTradeApiSet, Does.Contain(name),
            "AllTradeApiSet should contain option exercise method: " + name);
      }
    }

    [Test]
    public void AllTradeApiSet_ContainsExpectedPositionTransferMethods()
    {
      string[] expected =
      {
        TradeApiService.POSITION_TRANSFER,
        TradeApiService.POSITION_TRANSFER_RECORDS,
        TradeApiService.POSITION_TRANSFER_DETAIL,
        TradeApiService.POSITION_TRANSFER_EXTERNAL_RECORDS,
        TradeApiService.AGGREGATE_ASSETS,
        TradeApiService.TRANSFER_FUND,
        TradeApiService.FUND_DETAILS,
        TradeApiService.CONTRACT,
        TradeApiService.CONTRACTS,
      };
      foreach (var name in expected)
      {
        Assert.That(TradeApiService.AllTradeApiSet, Does.Contain(name),
            "AllTradeApiSet should contain method: " + name);
      }
    }

    [Test]
    public void AllTradeApiSet_HasExpectedCount()
    {
      // 8 trade + 13 account/asset + 5 option exercise + 2 contract
      // + 1 transfer_fund + 1 fund_details + 1 aggregate_assets
      // + 4 position transfer = 35
      Assert.That(TradeApiService.AllTradeApiSet.Count, Is.EqualTo(35),
          "AllTradeApiSet should hold exactly 35 trade API method names");
    }

    [Test]
    public void IsTradeApi_ReturnsTrue_ForKnownTradeMethods()
    {
      Assert.That(TradeApiService.IsTradeApi(TradeApiService.PLACE_ORDER), Is.True);
      Assert.That(TradeApiService.IsTradeApi(TradeApiService.ACCOUNTS), Is.True);
      Assert.That(TradeApiService.IsTradeApi(TradeApiService.POSITIONS), Is.True);
      Assert.That(TradeApiService.IsTradeApi(TradeApiService.OPTION_EXERCISE_SUBMIT), Is.True);
      Assert.That(TradeApiService.IsTradeApi(TradeApiService.POSITION_TRANSFER), Is.True);
    }

    [Test]
    public void IsTradeApi_ReturnsFalse_ForUnknownMethods()
    {
      Assert.That(TradeApiService.IsTradeApi("quote_real_time"), Is.False,
          "quote api method must not be a trade api");
      Assert.That(TradeApiService.IsTradeApi("not_a_real_api"), Is.False);
      Assert.That(TradeApiService.IsTradeApi("PLACE_ORDER"), Is.False,
          "case-sensitive: uppercase must not match");
    }

    [Test]
    public void IsTradeApi_ReturnsFalse_ForEmptyOrNull()
    {
      Assert.That(TradeApiService.IsTradeApi(""), Is.False);
      Assert.That(TradeApiService.IsTradeApi(null), Is.False);
      Assert.That(TradeApiService.IsTradeApi("   "), Is.False);
    }

    [Test]
    public void SpecificConstantValues_MatchExpectedWireNames()
    {
      Assert.That(TradeApiService.PLACE_ORDER, Is.EqualTo("place_order"));
      Assert.That(TradeApiService.PREVIEW_ORDER, Is.EqualTo("preview_order"));
      Assert.That(TradeApiService.CANCEL_ORDER, Is.EqualTo("cancel_order"));
      Assert.That(TradeApiService.MODIFY_ORDER, Is.EqualTo("modify_order"));
      Assert.That(TradeApiService.ACCOUNTS, Is.EqualTo("accounts"));
      Assert.That(TradeApiService.ASSETS, Is.EqualTo("assets"));
      Assert.That(TradeApiService.POSITIONS, Is.EqualTo("positions"));
      Assert.That(TradeApiService.ORDERS, Is.EqualTo("orders"));
      Assert.That(TradeApiService.OPTION_EXERCISE_SUBMIT, Is.EqualTo("option_exercise_submit"));
      Assert.That(TradeApiService.OPTION_EXERCISE_CANCEL, Is.EqualTo("option_exercise_cancel"));
      Assert.That(TradeApiService.POSITION_TRANSFER, Is.EqualTo("position_transfer"));
      Assert.That(TradeApiService.TRANSFER_FUND, Is.EqualTo("transfer_fund"));
      Assert.That(TradeApiService.FUND_DETAILS, Is.EqualTo("fund_details"));
      Assert.That(TradeApiService.AGGREGATE_ASSETS, Is.EqualTo("aggregate_assets"));
      Assert.That(TradeApiService.CONTRACT, Is.EqualTo("contract"));
      Assert.That(TradeApiService.CONTRACTS, Is.EqualTo("contracts"));
      Assert.That(TradeApiService.PLACE_FOREX_ORDER, Is.EqualTo("place_forex_order"));
      Assert.That(TradeApiService.TRANSFER_SEGMENT_FUND, Is.EqualTo("transfer_segment_fund"));
      Assert.That(TradeApiService.CANCEL_SEGMENT_FUND, Is.EqualTo("cancel_segment_fund"));
    }
  }
}
