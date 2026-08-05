using NUnit.Framework;
using TigerOpenAPI.Trade.Response;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for ContractItem factory methods.
  /// Zero network — pure model construction.
  /// </summary>
  [TestFixture]
  public class ContractItemTest
  {
    [Test]
    public void BuildStockContract_FieldsSetCorrectly()
    {
      var c = ContractItem.BuildStockContract("AAPL", "USD");
      Assert.That(c.SecType, Is.EqualTo(SecType.STK.ToString()));
      Assert.That(c.Symbol, Is.EqualTo("AAPL"));
      Assert.That(c.Currency, Is.EqualTo("USD"));
    }

    [Test]
    public void BuildOptionContract_FromIdentifier_FieldsSetCorrectly()
    {
      // Standard OCC identifier: "AAPL  190118P00160000"
      var c = ContractItem.BuildOptionContract("AAPL  190118P00160000");
      Assert.That(c.SecType, Is.EqualTo(SecType.OPT.ToString()));
      Assert.That(c.Symbol, Is.EqualTo("AAPL"));
      Assert.That(c.Expiry, Is.Not.Null.And.Not.Empty);
      Assert.That(c.Strike, Is.GreaterThan(0));
      Assert.That(c.Right, Is.EqualTo("PUT").Or.EqualTo("P"));
    }

    [Test]
    public void BuildOptionContract_Explicit_FieldsSetCorrectly()
    {
      var c = ContractItem.BuildOptionContract("AAPL", "20240119", 180.0, "CALL");
      Assert.That(c.SecType, Is.EqualTo(SecType.OPT.ToString()));
      Assert.That(c.Symbol, Is.EqualTo("AAPL"));
      Assert.That(c.Expiry, Is.EqualTo("20240119"));
      Assert.That(c.Strike, Is.EqualTo(180.0));
      Assert.That(c.Right, Is.EqualTo("CALL"));
    }

    [Test]
    public void BuildFutureContract_Full_FieldsSetCorrectly()
    {
      var c = ContractItem.BuildFutureContract("ES", "USD", "CME", "20240315", 50.0);
      Assert.That(c.SecType, Is.EqualTo(SecType.FUT.ToString()));
      Assert.That(c.Symbol, Is.EqualTo("ES"));
      Assert.That(c.Currency, Is.EqualTo("USD"));
      Assert.That(c.Exchange, Is.EqualTo("CME"));
      Assert.That(c.Expiry, Is.EqualTo("20240315"));
      Assert.That(c.Multiplier, Is.EqualTo(50.0));
    }

    [Test]
    public void BuildFutureContract_Short_FieldsSetCorrectly()
    {
      var c = ContractItem.BuildFutureContract("CL", "USD");
      Assert.That(c.SecType, Is.EqualTo(SecType.FUT.ToString()));
      Assert.That(c.Symbol, Is.EqualTo("CL"));
      Assert.That(c.Currency, Is.EqualTo("USD"));
    }

    [Test]
    public void BuildWarrantContract_FieldsSetCorrectly()
    {
      var c = ContractItem.BuildWarrantContract("12345", "20241201", 10.0, "CALL");
      Assert.That(c.SecType, Is.EqualTo(SecType.WAR.ToString()));
      Assert.That(c.Currency, Is.EqualTo(Currency.HKD.ToString()));
      Assert.That(c.Market, Is.EqualTo(Market.HK.ToString()));
      Assert.That(c.LocalSymbol, Is.EqualTo("12345"));
    }

    [Test]
    public void BuildFundContract_FieldsSetCorrectly()
    {
      var c = ContractItem.BuildFundContract("SPY", "USD");
      Assert.That(c.SecType, Is.EqualTo(SecType.FUND.ToString()));
      Assert.That(c.Symbol, Is.EqualTo("SPY"));
      Assert.That(c.Currency, Is.EqualTo("USD"));
    }
  }
}
