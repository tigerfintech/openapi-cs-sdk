using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies that key enum wire values (the strings sent over the API) remain stable.
  /// If a wire value changes it breaks all existing integrations.
  /// </summary>
  [TestFixture]
  public class EnumWireValueTest
  {
    private static readonly JsonSerializerSettings EnumSettings = new JsonSerializerSettings
    {
      Converters = { new StringEnumConverter() }
    };

    // Helper: serialize an enum to its string wire representation via StringEnumConverter
    private static string Wire<T>(T value) where T : struct, System.Enum
    {
      return JsonConvert.SerializeObject(value, EnumSettings).Trim('"');
    }

    [Test]
    public void Market_WireValues_Stable()
    {
      Assert.That(Wire(Market.US), Is.EqualTo("US"));
      Assert.That(Wire(Market.HK), Is.EqualTo("HK"));
      Assert.That(Wire(Market.CN), Is.EqualTo("CN"));
    }

    [Test]
    public void Currency_WireValues_Stable()
    {
      Assert.That(Wire(Currency.USD), Is.EqualTo("USD"));
      Assert.That(Wire(Currency.HKD), Is.EqualTo("HKD"));
      Assert.That(Wire(Currency.CNH), Is.EqualTo("CNH"));
    }

    [Test]
    public void SecType_WireValues_Stable()
    {
      Assert.That(Wire(SecType.STK), Is.EqualTo("STK"));
      Assert.That(Wire(SecType.OPT), Is.EqualTo("OPT"));
      Assert.That(Wire(SecType.FUT), Is.EqualTo("FUT"));
      Assert.That(Wire(SecType.WAR), Is.EqualTo("WAR"));
      Assert.That(Wire(SecType.IOPT), Is.EqualTo("IOPT"));
      Assert.That(Wire(SecType.FUND), Is.EqualTo("FUND"));
    }

    [Test]
    public void ActionType_WireValues_Stable()
    {
      Assert.That(Wire(ActionType.BUY), Is.EqualTo("BUY"));
      Assert.That(Wire(ActionType.SELL), Is.EqualTo("SELL"));
    }

    [Test]
    public void OrderType_WireValues_Stable()
    {
      Assert.That(Wire(OrderType.LMT), Is.EqualTo("LMT"));
      Assert.That(Wire(OrderType.MKT), Is.EqualTo("MKT"));
      Assert.That(Wire(OrderType.STP), Is.EqualTo("STP"));
      Assert.That(Wire(OrderType.STP_LMT), Is.EqualTo("STP_LMT"));
      Assert.That(Wire(OrderType.TRAIL), Is.EqualTo("TRAIL"));
      Assert.That(Wire(OrderType.ICEBERG), Is.EqualTo("ICEBERG"));
    }

    [Test]
    public void Env_WireValues_Stable()
    {
      Assert.That(Wire(Env.PROD), Is.EqualTo("PROD"));
      Assert.That(Wire(Env.SANDBOX), Is.EqualTo("SANDBOX"));
    }

    [Test]
    public void License_WireValues_Stable()
    {
      Assert.That(Wire(License.TBNZ), Is.EqualTo("TBNZ"));
      Assert.That(Wire(License.TBSG), Is.EqualTo("TBSG"));
      Assert.That(Wire(License.TBHK), Is.EqualTo("TBHK"));
      Assert.That(Wire(License.TBAU), Is.EqualTo("TBAU"));
    }
  }
}
