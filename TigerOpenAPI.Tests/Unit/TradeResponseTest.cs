using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for Trade Response classes — JSON deserialization and field mapping.
  /// Zero network — pure deserialization assertions.
  /// </summary>
  [TestFixture]
  public class TradeResponseTest
  {
    [Test]
    public void AccountsResponse_Deserialize()
    {
      string json = "{\"code\":0,\"message\":\"ok\",\"data\":{\"SEC\":[{\"account\":\"U123\",\"capability\":\"STK\",\"accountType\":\"GLOBAL\",\"status\":\"ACTIVE\"}]}}";
      var resp = JsonConvert.DeserializeObject<AccountsResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.ContainsKey("SEC"), Is.True);
      Assert.That(resp.Data["SEC"][0].Account, Is.EqualTo("U123"));
      Assert.That(resp.Data["SEC"][0].Capability, Is.EqualTo("STK"));
      Assert.That(resp.Data["SEC"][0].AccountType, Is.EqualTo("GLOBAL"));
      Assert.That(resp.Data["SEC"][0].Status, Is.EqualTo("ACTIVE"));
    }

    [Test]
    public void PositionsResponse_Deserialize()
    {
      string json = "{\"code\":0,\"data\":{\"items\":[{\"account\":\"U123\",\"positionQty\":100,\"symbol\":\"AAPL\",\"secType\":\"STK\"}]}}";
      var resp = JsonConvert.DeserializeObject<PositionsResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Items.Count, Is.EqualTo(1));
      var pos = resp.Data.Items[0];
      Assert.That(pos.Account, Is.EqualTo("U123"));
      Assert.That(pos.PositionQty, Is.EqualTo(100.0));
      Assert.That(pos.Symbol, Is.EqualTo("AAPL"));
      Assert.That(pos.SecType, Is.EqualTo("STK"));
    }

    [Test]
    public void PositionDetail_ManyFields_Deserialize()
    {
      string json = @"{""account"":""U123"",""positionQty"":200,""salableQty"":100,
        ""averageCost"":150.5,""marketValue"":30000,""unrealizedPnl"":500,
        ""realizedPnl"":100,""unrealizedPnlPercent"":1.5,
        ""updateTimestamp"":1700000000,""symbol"":""AAPL"",""market"":""US"",
        ""secType"":""STK"",""currency"":""USD"",""multiplier"":1}";
      var detail = JsonConvert.DeserializeObject<PositionDetail>(json, TigerClient.JsonSet);
      Assert.That(detail.Account, Is.EqualTo("U123"));
      Assert.That(detail.PositionQty, Is.EqualTo(200.0));
      Assert.That(detail.SalableQty, Is.EqualTo(100.0));
      Assert.That(detail.AverageCost, Is.EqualTo(150.5));
      Assert.That(detail.MarketValue, Is.EqualTo(30000.0));
      Assert.That(detail.UnrealizedPnl, Is.EqualTo(500.0));
      Assert.That(detail.Symbol, Is.EqualTo("AAPL"));
      Assert.That(detail.Multiplier, Is.EqualTo(1.0));
    }

    [Test]
    public void PlaceOrderResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":{""id"":12345,""orders"":[{""id"":12345,""symbol"":""AAPL""}]}}";
      var resp = JsonConvert.DeserializeObject<PlaceOrderResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Id, Is.EqualTo(12345));
      Assert.That(resp.Data.Orders.Count, Is.EqualTo(1));
      Assert.That(resp.Data.Orders[0].Id, Is.EqualTo(12345));
    }

    [Test]
    public void SingleOrderResponse_Deserialize()
    {
      string json = @"{""code"":0,""data"":{""id"":99,""symbol"":""AAPL"",""orderType"":""LMT"",""limitPrice"":150.5,""status"":""Submitted""}}";
      var resp = JsonConvert.DeserializeObject<SingleOrderResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.Id, Is.EqualTo(99));
      Assert.That(resp.Data.Symbol, Is.EqualTo("AAPL"));
      Assert.That(resp.Data.OrderType, Is.EqualTo("LMT"));
      Assert.That(resp.Data.LimitPrice, Is.EqualTo(150.5));
    }

    [Test]
    public void TradeOrder_FullDeserialize()
    {
      string json = @"{""id"":1,""orderId"":100,""symbol"":""AAPL"",""market"":""US"",
        ""secType"":""STK"",""currency"":""USD"",""action"":""BUY"",""orderType"":""LMT"",
        ""limitPrice"":150.0,""totalQuantity"":100,""filledQuantity"":0,
        ""status"":""Submitted"",""openTime"":1700000000,""latestTime"":1700000001,
        ""canModify"":true,""canCancel"":true,""isOpen"":true}";
      var order = JsonConvert.DeserializeObject<TradeOrder>(json, TigerClient.JsonSet);
      Assert.That(order.Id, Is.EqualTo(1));
      Assert.That(order.OrderId, Is.EqualTo(100));
      Assert.That(order.Symbol, Is.EqualTo("AAPL"));
      Assert.That(order.Market, Is.EqualTo("US"));
      Assert.That(order.Action, Is.EqualTo("BUY"));
      Assert.That(order.OrderType, Is.EqualTo("LMT"));
      Assert.That(order.LimitPrice, Is.EqualTo(150.0));
      Assert.That(order.TotalQuantity, Is.EqualTo(100));
      Assert.That(order.Status, Is.EqualTo(OrderStatus.Submitted));
      Assert.That(order.CanModify, Is.True);
      Assert.That(order.CanCancel, Is.True);
    }

    [Test]
    public void AccountsResponse_ErrorResponse()
    {
      string json = "{\"code\":40001,\"message\":\"invalid tiger id\"}";
      var resp = JsonConvert.DeserializeObject<AccountsResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.False);
      Assert.That(resp.Code, Is.EqualTo(40001));
      Assert.That(resp.Message, Is.EqualTo("invalid tiger id"));
    }

    [Test]
    public void PlaceOrderResponse_ErrorResponse()
    {
      string json = "{\"code\":1100,\"message\":\"trade response error\"}";
      var resp = JsonConvert.DeserializeObject<PlaceOrderResponse>(json, TigerClient.JsonSet);
      Assert.That(resp.IsSuccess(), Is.False);
      Assert.That(resp.Code, Is.EqualTo(1100));
    }
  }
}
