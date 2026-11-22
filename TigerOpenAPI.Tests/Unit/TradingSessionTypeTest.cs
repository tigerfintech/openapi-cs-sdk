using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Trade.Model;

namespace TigerOpenAPI.Tests.Unit
{
  [TestFixture]
  public class TradingSessionTypeTest
  {
    private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore,
      DefaultValueHandling = DefaultValueHandling.Ignore
    };

    [TestCase(TradingSessionType.PRE_RTH)]
    [TestCase(TradingSessionType.RTH_POST)]
    public void NewSessionsSerializeAndDeserializeAsNames(TradingSessionType session)
    {
      var model = new PlaceOrderModel().SetTradingSessionType(session);
      var json = JsonConvert.SerializeObject(model, Settings);
      var payload = JObject.Parse(json);
      Assert.That(payload["trading_session_type"]?.Value<string>(), Is.EqualTo(session.ToString()));
      Assert.That(payload["OrderTradingSessionType"], Is.Null);
      Assert.That(payload["TradingSessionType"], Is.Null);
      var restored = JsonConvert.DeserializeObject<PlaceOrderModel>(json, Settings);
      Assert.That(restored!.OrderTradingSessionType, Is.EqualTo(session));
    }

    [TestCase(TradeSession.OverNight)]
    [TestCase(TradeSession.AfterHours)]
    public void LegacyAssignmentsKeepTheirWireNames(TradeSession session)
    {
      var model = new PlaceOrderModel { TradingSessionType = session };
      var json = JsonConvert.SerializeObject(model, Settings);
      Assert.That(JObject.Parse(json)["trading_session_type"]?.Value<string>(), Is.EqualTo(session.ToString()));
      var restored = JsonConvert.DeserializeObject<PlaceOrderModel>(json, Settings);
      Assert.That(restored!.TradingSessionType, Is.EqualTo(session));
    }

    [Test]
    public void DefaultSessionRemainsOmitted()
    {
      Assert.That(JObject.Parse(JsonConvert.SerializeObject(new PlaceOrderModel(), Settings))["trading_session_type"], Is.Null);
      Assert.That(JObject.Parse(JsonConvert.SerializeObject(new PlaceOrderModel()))["trading_session_type"]?.Value<string>(), Is.EqualTo("Regular"));
    }

    [Test]
    public void LegacySetterReplacesExplicitSession()
    {
      var model = new PlaceOrderModel().SetTradingSessionType(TradingSessionType.PRE_RTH);
      model.TradingSessionType = TradeSession.OverNight;
      Assert.That(model.OrderTradingSessionType, Is.Null);
      Assert.That(JObject.Parse(JsonConvert.SerializeObject(model, Settings))["trading_session_type"]?.Value<string>(), Is.EqualTo("OverNight"));
    }

    [Test]
    public void TypedPropertyCanSetSessionDirectly()
    {
      var model = new PlaceOrderModel { OrderTradingSessionType = TradingSessionType.RTH_POST };
      Assert.That(JObject.Parse(JsonConvert.SerializeObject(model, Settings))["trading_session_type"]?.Value<string>(), Is.EqualTo("RTH_POST"));
    }

    [Test]
    public void LegacyDeserializationKeepsCaseInsensitiveNamesAndNumericValues()
    {
      var name = JsonConvert.DeserializeObject<PlaceOrderModel>("{\"trading_session_type\":\"overnight\"}", Settings);
      Assert.That(name!.TradingSessionType, Is.EqualTo(TradeSession.OverNight));
      var number = JsonConvert.DeserializeObject<PlaceOrderModel>("{\"trading_session_type\":1}", Settings);
      Assert.That(number!.TradingSessionType, Is.EqualTo(TradeSession.PreMarket));
    }
  }
}
