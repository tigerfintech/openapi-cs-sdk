using NUnit.Framework;
using TigerOpenAPI.Trade.Response;
using TigerOpenAPI.Common.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    public void OrderCapabilities_ParseSingleAndBatchResponses()
    {
      const string item = "{\"symbol\":\"AAPL\",\"secType\":\"OPT\",\"orderCapabilities\":["
          + "{\"orderType\":\"LMT\",\"timeInForce\":\"DAY\",\"tradingSessionType\":\"PRE_RTH_POST\"},"
          + "{\"orderType\":\"LMT\",\"timeInForce\":\"GTC\",\"tradingSessionType\":\"PRE_RTH_POST\"}]}";
      var single = JsonConvert.DeserializeObject<ContractResponse>("{\"code\":0,\"data\":" + item + "}")!.Data;
      var batch = JsonConvert.DeserializeObject<ContractsResponse>("{\"code\":0,\"data\":{\"items\":[" + item + "]}}")!.Data["items"][0];
      foreach (var contract in new[] { single, batch })
      {
        Assert.That(contract.OrderCapabilities, Has.Count.EqualTo(2));
        Assert.That(contract.OrderCapabilities![0].OrderType, Is.EqualTo("LMT"));
        Assert.That(contract.OrderCapabilities[0].TimeInForce, Is.EqualTo("DAY"));
        Assert.That(contract.OrderCapabilities[0].TradingSessionType, Is.EqualTo("PRE_RTH_POST"));
        Assert.That(contract.OrderCapabilities[1].TradingSessionType, Is.EqualTo("PRE_RTH_POST"));
        var serialized = JObject.Parse(JsonConvert.SerializeObject(contract));
        Assert.That(JToken.DeepEquals(serialized["orderCapabilities"], JObject.Parse(item)["orderCapabilities"]), Is.True);
      }
    }

    [TestCase("{}")]
    [TestCase("{\"orderCapabilities\":null}")]
    public void OrderCapabilities_MissingOrNullRemainNull(string json)
    {
      var contract = JsonConvert.DeserializeObject<ContractItem>(json)!;
      Assert.That(contract.OrderCapabilities, Is.Null);
      Assert.That(JObject.Parse(JsonConvert.SerializeObject(contract)).ContainsKey("orderCapabilities"), Is.False);
    }

    [Test]
    public void OrderCapabilities_EmptyListRemainsEmpty()
    {
      var contract = JsonConvert.DeserializeObject<ContractItem>("{\"orderCapabilities\":[]}")!;
      Assert.That(contract.OrderCapabilities, Is.Empty);
      Assert.That(JObject.Parse(JsonConvert.SerializeObject(contract))["orderCapabilities"], Is.TypeOf<JArray>());
    }

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
      // Positions: symbol(6) + date YYMMDD(6) + right(1) + price*1000 padded to 8 digits
      var c = ContractItem.BuildOptionContract("AAPL  190118P00160000");
      Assert.That(c.SecType, Is.EqualTo(SecType.OPT.ToString()));
      Assert.That(c.Symbol, Is.EqualTo("AAPL"));
      Assert.That(c.Right, Is.EqualTo("PUT"),
          "Right char 'P' should map to \"PUT\"");
      Assert.That(c.Expiry, Is.EqualTo("2019-01-18"),
          "Expiry should be formatted as yyyy-MM-dd");
      Assert.That(c.Strike, Is.EqualTo(160.0),
          "00160000 encodes strike 160.000");
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
