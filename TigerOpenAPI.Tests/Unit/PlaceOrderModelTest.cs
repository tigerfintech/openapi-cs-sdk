using System;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies PlaceOrderModel serialization: correct wire names, no leakage of null/default fields.
  /// </summary>
  [TestFixture]
  public class PlaceOrderModelTest
  {
    private static readonly JsonSerializerSettings Settings = TigerClient.JsonSet;

    private ContractItem _contract = null!;

    [SetUp]
    public void SetUp()
    {
      _contract = ContractItem.BuildStockContract("AAPL", Currency.USD.ToString());
    }

    [Test]
    public void IcebergOrder_SerializesDisplaySize_WithCorrectWireName()
    {
      var o = PlaceOrderModel.BuildIcebergOrder("ACC1", _contract, ActionType.BUY, 100, 150.0, 10);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"display_size\":"),
          "displaySize must use wire name 'display_size'");
      Assert.That(json, Does.Contain("\"order_type\":"),
          "orderType must use wire name 'order_type'");
      Assert.That(json, Does.Contain("ICEBERG"),
          "order_type value must be ICEBERG");
    }

    [Test]
    public void IcebergOrder_SimpleOverload_MinDisplaySizeDefaultsToDisplaySize()
    {
      // Gateway requires min_display_size; the simple overload defaults it to
      // displaySize so callers who don't care about it still get a valid order.
      var o = PlaceOrderModel.BuildIcebergOrder("ACC1", _contract, ActionType.BUY, 100, 150.0, 10);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"min_display_size\":10"),
          "min_display_size must default to displaySize on the simple overload");
    }

    [Test]
    public void IcebergOrder_NullOptional_NotPresentInJson()
    {
      // Full overload with explicit nulls — CheckIntervals/PriceType/StartTime/EndTime
      // stay optional and must be omitted (NullValueHandling.Ignore).
      var o = PlaceOrderModel.BuildIcebergOrder(
          "ACC1", _contract, ActionType.BUY, 100, 150.0,
          10, null, null, null, null, null);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Not.Contain("min_display_size"),
          "min_display_size (null) must not appear in serialized JSON");
      Assert.That(json, Does.Not.Contain("check_intervals"),
          "check_intervals (null) must not appear in serialized JSON");
    }

    [Test]
    public void IcebergOrder_AllOptionalSet_AllPresentInJson()
    {
      long start = 1700000000000L;
      long end   = 1700000000000L + 4L * 3600 * 1000;
      var o = PlaceOrderModel.BuildIcebergOrder(
          "ACC1", _contract, ActionType.BUY, 100, 150.0,
          20, 5, 30, PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT, start, end);
      string json = JsonConvert.SerializeObject(o, Settings);

      // All optional fields set — verify wire names present
      Assert.That(json, Does.Contain("display_size"));
      Assert.That(json, Does.Contain("min_display_size"));
      Assert.That(json, Does.Contain("check_intervals"),
          "CheckIntervals wire name must be 'check_intervals'");
    }

    [Test]
    public void IcebergOrder_SymbolAndAction_SerializedCorrectly()
    {
      var o = PlaceOrderModel.BuildIcebergOrder("ACC1", _contract, ActionType.SELL, 50, 200.0, 5);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("AAPL"),  "symbol value must be present");
      Assert.That(json, Does.Contain("SELL"),  "action value must be SELL");
    }
  }
}
