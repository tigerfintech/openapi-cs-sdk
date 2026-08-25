using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for TigerResponse base class deserialization and IsSuccess() semantics.
  /// </summary>
  [TestFixture]
  public class TigerResponseTest
  {
    private static readonly JsonSerializerSettings Settings = TigerClient.JsonSet;

    [Test]
    public void IsSuccess_ReturnTrue_WhenCode0()
    {
      var resp = new TigerResponse { Code = TigerApiCode.SUCCESS.Code };
      Assert.That(resp.IsSuccess(), Is.True);
    }

    [Test]
    public void IsSuccess_ReturnFalse_WhenCodeNonZero()
    {
      var resp = new TigerResponse { Code = 40001 };
      Assert.That(resp.IsSuccess(), Is.False);
    }

    [Test]
    public void TigerResponse_Deserialize_CodeAndMessage()
    {
      const string json = @"{""code"":0,""message"":""success"",""timestamp"":1700000000000}";
      var resp = JsonConvert.DeserializeObject<TigerResponse>(json, Settings);

      Assert.That(resp, Is.Not.Null);
      Assert.That(resp!.Code,      Is.EqualTo(0),             "code wire name");
      Assert.That(resp.Message,    Is.EqualTo("success"),      "message wire name");
      Assert.That(resp.Timestamp,  Is.EqualTo(1700000000000L), "timestamp wire name");
      Assert.That(resp.IsSuccess(), Is.True);
    }

    [Test]
    public void TigerResponse_Deserialize_ErrorCode()
    {
      const string json = @"{""code"":40001,""message"":""param error""}";
      var resp = JsonConvert.DeserializeObject<TigerResponse>(json, Settings);

      Assert.That(resp, Is.Not.Null);
      Assert.That(resp!.Code,     Is.EqualTo(40001));
      Assert.That(resp.Message,   Is.EqualTo("param error"));
      Assert.That(resp.IsSuccess(), Is.False);
    }

    [Test]
    public void TradeCalendarResponse_Deserialize_FullPayload()
    {
      const string json = @"{""code"":0,""message"":""success"",""timestamp"":1700000000," +
          @"""data"":[{""date"":""2024-01-02"",""type"":""NORMAL""}]}";
      var resp = JsonConvert.DeserializeObject<TradeCalendarResponse>(json, Settings);

      Assert.That(resp, Is.Not.Null);
      Assert.That(resp!.IsSuccess(), Is.True);
      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0));
      Assert.That(resp.Data[0].Date, Is.EqualTo("2024-01-02"));
      Assert.That(resp.Data[0].Type, Is.EqualTo("NORMAL"));
    }

    [Test]
    public void MarketStateResponse_Deserialize_FullPayload()
    {
      const string json = @"{""code"":0,""message"":""success""," +
          @"""data"":[{""market"":""US"",""marketStatus"":""Open"",""status"":""Trading"",""openTime"":""09:30""}]}";
      var resp = JsonConvert.DeserializeObject<MarketStateResponse>(json, Settings);

      Assert.That(resp, Is.Not.Null);
      Assert.That(resp!.IsSuccess(), Is.True);
      Assert.That(resp.Data, Is.Not.Null.And.Count.GreaterThan(0));
      Assert.That(resp.Data[0].Market,       Is.EqualTo("US"));
      Assert.That(resp.Data[0].MarketStatus, Is.EqualTo("Open"));
      Assert.That(resp.Data[0].Status,       Is.EqualTo("Trading"));
      Assert.That(resp.Data[0].OpenTime,     Is.EqualTo("09:30"));
    }
  }
}
