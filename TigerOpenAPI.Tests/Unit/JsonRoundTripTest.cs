using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// JSON serialization round-trip tests.
  /// Verifies that wire names in [JsonProperty] match the actual API JSON keys.
  /// A failing test here means a property cannot be deserialized from real API responses.
  /// </summary>
  [TestFixture]
  public class JsonRoundTripTest
  {
    private static readonly JsonSerializerSettings Settings = TigerClient.JsonSet;

    [Test]
    public void TradeCalendar_RoundTrip_AllFieldsPreserved()
    {
      const string json = @"{""date"":""2024-01-02"",""type"":""NORMAL""}";
      var obj = JsonConvert.DeserializeObject<TradeCalendar>(json, Settings);

      Assert.That(obj, Is.Not.Null);
      Assert.That(obj!.Date, Is.EqualTo("2024-01-02"),  @"[JsonProperty(""date"")] must map to Date");
      Assert.That(obj.Type, Is.EqualTo("NORMAL"),         @"[JsonProperty(""type"")] must map to Type");
    }

    [Test]
    public void MarketState_RoundTrip_AllFieldsPreserved()
    {
      const string json = @"{""market"":""US"",""marketStatus"":""Open"",""status"":""Trading"",""openTime"":""09:30""}";
      var obj = JsonConvert.DeserializeObject<MarketState>(json, Settings);

      Assert.That(obj, Is.Not.Null);
      Assert.That(obj!.Market,       Is.EqualTo("US"));
      Assert.That(obj.MarketStatus,  Is.EqualTo("Open"),     "marketStatus wire name");
      Assert.That(obj.Status,        Is.EqualTo("Trading"),  "status wire name");
      Assert.That(obj.OpenTime,      Is.EqualTo("09:30"),    "openTime wire name");
    }

    [Test]
    public void RealTimeQuoteItem_RoundTrip_AllNumericFieldsPreserved()
    {
      const string json = @"{
        ""symbol"":""AAPL"",
        ""open"":150.0,""high"":155.0,""low"":149.0,""close"":153.0,
        ""preClose"":152.0,""latestPrice"":154.0,
        ""askPrice"":154.1,""askSize"":100,
        ""bidPrice"":154.0,""bidSize"":200,
        ""volume"":50000000,""latestTime"":1700000000000
      }";
      var obj = JsonConvert.DeserializeObject<RealTimeQuoteItem>(json, Settings);

      Assert.That(obj, Is.Not.Null);
      Assert.That(obj!.Symbol,      Is.EqualTo("AAPL"));
      Assert.That(obj.Open,         Is.EqualTo(150.0));
      Assert.That(obj.High,         Is.EqualTo(155.0));
      Assert.That(obj.Low,          Is.EqualTo(149.0));
      Assert.That(obj.Close,        Is.EqualTo(153.0));
      Assert.That(obj.PreClose,     Is.EqualTo(152.0),     "preClose wire name");
      Assert.That(obj.LatestPrice,  Is.EqualTo(154.0),     "latestPrice wire name");
      Assert.That(obj.AskPrice,     Is.EqualTo(154.1),     "askPrice wire name");
      Assert.That(obj.AskSize,      Is.EqualTo(100L),      "askSize wire name");
      Assert.That(obj.BidPrice,     Is.EqualTo(154.0),     "bidPrice wire name");
      Assert.That(obj.BidSize,      Is.EqualTo(200L),      "bidSize wire name");
      Assert.That(obj.Volume,       Is.EqualTo(50000000L), "volume wire name");
      Assert.That(obj.LatestTime,   Is.EqualTo(1700000000000L), "latestTime wire name");
    }

    [Test]
    public void PositionDetail_RoundTrip_AllFieldsPreserved()
    {
      const string json = @"{
        ""account"":""DU123456"",
        ""symbol"":""AAPL"",
        ""secType"":""STK"",
        ""market"":""US"",
        ""currency"":""USD"",
        ""positionQty"":100.0,
        ""salableQty"":100.0,
        ""averageCost"":150.0,
        ""marketValue"":15400.0,
        ""latestPrice"":154.0,
        ""unrealizedPnl"":400.0,
        ""realizedPnl"":100.0,
        ""unrealizedPnlPercent"":2.67,
        ""updateTimestamp"":1700000000000
      }";
      var obj = JsonConvert.DeserializeObject<PositionDetail>(json, Settings);

      Assert.That(obj, Is.Not.Null);
      Assert.That(obj!.Account,          Is.EqualTo("DU123456"));
      Assert.That(obj.Symbol,            Is.EqualTo("AAPL"));
      Assert.That(obj.SecType,           Is.EqualTo("STK"));
      Assert.That(obj.Market,            Is.EqualTo("US"));
      Assert.That(obj.Currency,          Is.EqualTo("USD"));
      Assert.That(obj.PositionQty,       Is.EqualTo(100.0),      "positionQty wire name");
      Assert.That(obj.SalableQty,        Is.EqualTo(100.0),      "salableQty wire name");
      Assert.That(obj.AverageCost,       Is.EqualTo(150.0),      "averageCost wire name");
      Assert.That(obj.MarketValue,       Is.EqualTo(15400.0),    "marketValue wire name");
      Assert.That(obj.LatestPrice,       Is.EqualTo(154.0),      "latestPrice wire name");
      Assert.That(obj.UnrealizedPnl,     Is.EqualTo(400.0),      "unrealizedPnl wire name");
      Assert.That(obj.RealizedPnl,       Is.EqualTo(100.0),      "realizedPnl wire name");
      Assert.That(obj.UnrealizedPnlPercent, Is.EqualTo(2.67).Within(0.001), "unrealizedPnlPercent wire name");
      Assert.That(obj.UpdateTimestamp,   Is.EqualTo(1700000000000L), "updateTimestamp wire name");
    }

    [Test]
    public void TradeOrder_RoundTrip_CoreFieldsPreserved()
    {
      // Note: Status uses [JsonConverter(typeof(StringEnumConverter))] with OrderStatus enum.
      // Using "Filled" which maps to OrderStatus.Filled.
      // Note: ReplaceStatus and CancelStatus lack [JsonProperty] — known issue, not tested here.
      const string json = @"{
        ""symbol"":""AAPL"",
        ""market"":""US"",
        ""secType"":""STK"",
        ""currency"":""USD"",
        ""id"":12345678901,
        ""orderId"":1001,
        ""account"":""DU123456"",
        ""action"":""BUY"",
        ""orderType"":""LMT"",
        ""limitPrice"":150.0,
        ""totalQuantity"":100,
        ""filledQuantity"":50,
        ""status"":""Filled"",
        ""openTime"":1700000000000,
        ""latestPrice"":154.0
      }";
      var obj = JsonConvert.DeserializeObject<TradeOrder>(json, Settings);

      Assert.That(obj, Is.Not.Null);
      Assert.That(obj!.Symbol,        Is.EqualTo("AAPL"));
      Assert.That(obj.Market,         Is.EqualTo("US"));
      Assert.That(obj.SecType,        Is.EqualTo("STK"));
      Assert.That(obj.Currency,       Is.EqualTo("USD"));
      Assert.That(obj.Id,             Is.EqualTo(12345678901L),  "id wire name");
      Assert.That(obj.OrderId,        Is.EqualTo(1001),          "orderId wire name");
      Assert.That(obj.Account,        Is.EqualTo("DU123456"));
      Assert.That(obj.Action,         Is.EqualTo("BUY"));
      Assert.That(obj.OrderType,      Is.EqualTo("LMT"),         "orderType wire name");
      Assert.That(obj.LimitPrice,     Is.EqualTo(150.0),         "limitPrice wire name");
      Assert.That(obj.TotalQuantity,  Is.EqualTo(100L),          "totalQuantity wire name");
      Assert.That(obj.FilledQuantity, Is.EqualTo(50L),           "filledQuantity wire name");
      Assert.That(obj.Status,         Is.EqualTo(OrderStatus.Filled), "status wire name (enum)");
      Assert.That(obj.OpenTime,       Is.EqualTo(1700000000000L), "openTime wire name");
      Assert.That(obj.LatestPrice,    Is.EqualTo(154.0),         "latestPrice wire name");
    }

    [Test]
    public void ContractItem_RoundTrip_AllFieldsPreserved()
    {
      const string json = @"{
        ""symbol"":""AAPL"",
        ""secType"":""STK"",
        ""market"":""US"",
        ""currency"":""USD"",
        ""identifier"":""AAPL"",
        ""contractId"":12345,
        ""name"":""Apple Inc."",
        ""tradeable"":true,
        ""marginable"":true,
        ""minTick"":0.01,
        ""multiplier"":1.0,
        ""lotSize"":1.0,
        ""exchange"":""NASDAQ"",
        ""primaryExchange"":""NASDAQ"",
        ""isEtf"":true
      }";
      var obj = JsonConvert.DeserializeObject<ContractItem>(json, Settings);

      Assert.That(obj, Is.Not.Null);
      Assert.That(obj!.Symbol,         Is.EqualTo("AAPL"));
      Assert.That(obj.SecType,         Is.EqualTo("STK"));
      Assert.That(obj.Market,          Is.EqualTo("US"));
      Assert.That(obj.Currency,        Is.EqualTo("USD"));
      Assert.That(obj.Identifier,      Is.EqualTo("AAPL"),    "identifier wire name");
      Assert.That(obj.ContractId,      Is.EqualTo(12345),     "contractId wire name");
      Assert.That(obj.Name,            Is.EqualTo("Apple Inc."));
      Assert.That(obj.Tradeable,       Is.True,               "tradeable wire name");
      Assert.That(obj.Marginable,      Is.True,               "marginable wire name");
      Assert.That(obj.MinTick,         Is.EqualTo(0.01),      "minTick wire name");
      Assert.That(obj.Multiplier,      Is.EqualTo(1.0),       "multiplier wire name");
      Assert.That(obj.LotSize,         Is.EqualTo(1.0),       "lotSize wire name");
      Assert.That(obj.Exchange,        Is.EqualTo("NASDAQ"),  "exchange wire name");
      Assert.That(obj.PrimaryExchange, Is.EqualTo("NASDAQ"),  "primaryExchange wire name");
      Assert.That(obj.IsEtf,           Is.True,               "isEtf wire name");
    }

    [Test]
    public void TigerResponse_Serialize_NullValues_OmittedFromJson()
    {
      // NullValueHandling.Ignore means null properties must not appear in JSON
      var model = new TigerOpenAPI.Quote.Model.QuoteMarketModel { Market = Market.US };
      string json = JsonConvert.SerializeObject(model, Settings);
      Assert.That(json, Does.Not.Contain("\"trade_session\":null"),
          "Null properties must be omitted (NullValueHandling.Ignore)");
    }
  }
}
