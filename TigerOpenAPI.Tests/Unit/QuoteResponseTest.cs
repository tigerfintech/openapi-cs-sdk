using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for Quote Response classes — JSON deserialization and field mapping.
  /// Zero network — pure deserialization assertions.
  /// </summary>
  [TestFixture]
  public class QuoteResponseTest
  {
    [Test]
    public void QuoteRealTimeQuoteResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""open"":190.0,""high"":195.0,
        ""low"":188.0,""close"":192.5,""preClose"":189.0,""latestPrice"":192.5,
        ""askPrice"":192.6,""askSize"":100,""bidPrice"":192.4,""bidSize"":200,
        ""volume"":1000000,""latestTime"":1700000000,""status"":""NORMAL""}]}";
      var resp = JsonConvert.DeserializeObject<QuoteRealTimeQuoteResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"));
      Assert.That(item.Open, Is.EqualTo(190.0));
      Assert.That(item.High, Is.EqualTo(195.0));
      Assert.That(item.Low, Is.EqualTo(188.0));
      Assert.That(item.Close, Is.EqualTo(192.5));
      Assert.That(item.PreClose, Is.EqualTo(189.0));
      Assert.That(item.LatestPrice, Is.EqualTo(192.5));
      Assert.That(item.AskPrice, Is.EqualTo(192.6));
      Assert.That(item.AskSize, Is.EqualTo(100));
      Assert.That(item.BidPrice, Is.EqualTo(192.4));
      Assert.That(item.BidSize, Is.EqualTo(200));
      Assert.That(item.Volume, Is.EqualTo(1000000));
      Assert.That(item.Status, Is.EqualTo(StockStatus.NORMAL));
    }

    [Test]
    public void QuoteKlineResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""period"":""day"",
        ""items"":[{""open"":190,""high"":195,""low"":188,""close"":192,""volume"":1000,""time"":1700000000}]}]}";
      var resp = JsonConvert.DeserializeObject<QuoteKlineResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      var item = resp.Data[0];
      Assert.That(item.Symbol, Is.EqualTo("AAPL"));
      Assert.That(item.Period, Is.EqualTo("day"));
      Assert.That(item.Items.Count, Is.EqualTo(1));
      var point = item.Items[0];
      Assert.That(point.Open, Is.EqualTo(190.0));
      Assert.That(point.High, Is.EqualTo(195.0));
      Assert.That(point.Low, Is.EqualTo(188.0));
      Assert.That(point.Close, Is.EqualTo(192.0));
      Assert.That(point.Volume, Is.EqualTo(1000));
    }

    [Test]
    public void QuoteTimelineResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""period"":""1m"",
        ""preClose"":190.0}]}";
      var resp = JsonConvert.DeserializeObject<QuoteTimelineResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data[0].Symbol, Is.EqualTo("AAPL"));
      Assert.That(resp.Data[0].Period, Is.EqualTo("1m"));
      Assert.That(resp.Data[0].PreClose, Is.EqualTo(190.0));
    }

    [Test]
    public void QuotePermissionResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""name"":""STK"",""expireAt"":1700000000}]}";
      var resp = JsonConvert.DeserializeObject<QuotePermissionResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Count, Is.EqualTo(1));
      Assert.That(resp.Data[0].Name, Is.EqualTo("STK"));
      Assert.That(resp.Data[0].ExpireAt, Is.EqualTo(1700000000));
    }

    [Test]
    public void UserTokenResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":{""token"":""abc123"",""expire_at"":1700000000}}";
      var resp = JsonConvert.DeserializeObject<UserTokenResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Token, Is.EqualTo("abc123"));
    }

    [Test]
    public void QuoteRealTimeQuoteResponse_ErrorResponse()
    {
      string json = "{\"code\":2100,\"message\":\"stock response error\"}";
      var resp = JsonConvert.DeserializeObject<QuoteRealTimeQuoteResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.False);
      Assert.That(resp.Code, Is.EqualTo(2100));
    }

    [Test]
    public void QuoteDelayResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""delay"":0}]}";
      var resp = JsonConvert.DeserializeObject<QuoteDelayResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data[0].Symbol, Is.EqualTo("AAPL"));
      Assert.That(resp.Data[0].Delay, Is.EqualTo(0));
    }

    [Test]
    public void QuoteOvernightResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":[{""symbol"":""AAPL"",""overnight"":false}]}";
      var resp = JsonConvert.DeserializeObject<QuoteOvernightResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data[0].Symbol, Is.EqualTo("AAPL"));
      Assert.That(resp.Data[0].Overnight, Is.EqualTo(false));
    }
  }
}
