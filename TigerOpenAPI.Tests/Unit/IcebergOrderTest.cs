using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for iceberg order model construction.
  /// Zero network required — pure model-building assertions.
  /// Migrated from Sample/IcebergUnitTest.cs (was a console-exit program).
  /// </summary>
  [TestFixture]
  public class IcebergOrderTest
  {
    private ContractItem _contract = null!;

    [SetUp]
    public void SetUp()
    {
      _contract = ContractItem.BuildStockContract("AAPL", Currency.USD.ToString());
    }

    [Test]
    public void BasicIceberg_FieldsSetCorrectly()
    {
      var o = PlaceOrderModel.BuildIcebergOrder("ACC1", _contract, ActionType.BUY, 100, 1.5, 10);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.ICEBERG));
      Assert.That(o.DisplaySize, Is.EqualTo(10));
      Assert.That(o.LimitPrice, Is.EqualTo(1.5));
      Assert.That(o.TotalQuantity, Is.EqualTo(100));
      Assert.That(o.Action, Is.EqualTo(ActionType.BUY));
      Assert.That(o.Symbol, Is.EqualTo("AAPL"));
      Assert.That(o.MinDisplaySize, Is.EqualTo(10));
      Assert.That(o.CheckIntervals, Is.Null);
      Assert.That(o.PriceType, Is.EqualTo(PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT));
      Assert.That(o.StartTime, Is.Null);
      Assert.That(o.EndTime, Is.Null);
    }

    [Test]
    public void FullIceberg_AllOptionalFieldsSet()
    {
      long start = 1700000000000L;
      long end   = 1700000000000L + 4L * 3600 * 1000;
      var o = PlaceOrderModel.BuildIcebergOrder(
          "ACC1", _contract, ActionType.SELL, 200, 2.0,
          20, 5, 30, PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT, start, end);

      Assert.That(o.OrderType,      Is.EqualTo(OrderType.ICEBERG));
      Assert.That(o.DisplaySize,    Is.EqualTo(20));
      Assert.That(o.MinDisplaySize, Is.EqualTo(5));
      Assert.That(o.CheckIntervals, Is.EqualTo(30));
      Assert.That(o.PriceType,      Is.EqualTo(PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT));
      Assert.That(o.StartTime,      Is.EqualTo(start));
      Assert.That(o.EndTime,        Is.EqualTo(end));
      Assert.That(o.Action,         Is.EqualTo(ActionType.SELL));
      Assert.That(o.TotalQuantity,  Is.EqualTo(200));
    }

    [Test]
    public void OptionalNullsNotPassed_FieldsRemainNull()
    {
      var o = PlaceOrderModel.BuildIcebergOrder(
          "ACC1", _contract, ActionType.BUY, 50, 1.0,
          5, null, null, null, null, null);

      Assert.That(o.MinDisplaySize, Is.Null, "MinDisplaySize");
      Assert.That(o.CheckIntervals, Is.Null, "CheckIntervals");
      Assert.That(o.PriceType,      Is.Null, "PriceType");
    }

    [Test]
    public void PriceTypeConstants_HaveExpectedWireValues()
    {
      Assert.That(PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT,  Is.EqualTo("LIMIT_PRICE"));
      Assert.That(PlaceOrderModel.ICEBERG_PRICE_TYPE_ASK,    Is.EqualTo("ASK_PRICE"));
      Assert.That(PlaceOrderModel.ICEBERG_PRICE_TYPE_BID,    Is.EqualTo("BID_PRICE"));
      Assert.That(PlaceOrderModel.ICEBERG_PRICE_TYPE_LATEST, Is.EqualTo("LATEST_PRICE"));
    }
  }
}
