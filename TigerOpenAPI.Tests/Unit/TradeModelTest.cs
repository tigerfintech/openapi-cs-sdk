using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for Trade Model classes — serialization wire names, default values, and null handling.
  /// Zero network — pure model construction and JSON assertions.
  /// </summary>
  [TestFixture]
  public class TradeModelTest
  {
    [Test]
    public void TradeModel_InheritsApiModel_HasSecretKey()
    {
      var model = new CancelOrderModel();
      model.SecretKey = "secret";
      model.Lang = Language.en_US;
      Assert.That(model.SecretKey, Is.EqualTo("secret"));
      Assert.That(model.Lang, Is.EqualTo(Language.en_US));
    }

    [Test]
    public void CancelOrderModel_Serialization_HasId()
    {
      var model = new CancelOrderModel { Id = 12345, SecretKey = "key" };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"id\":12345"));
      Assert.That(json, Does.Contain("\"secret_key\":\"key\""));
    }

    [Test]
    public void ModifyOrderModel_Serialization_NullOptionalsOmitted()
    {
      var model = new ModifyOrderModel { Id = 100 };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"id\":100"));
      Assert.That(json, Does.Not.Contain("order_type"));
      Assert.That(json, Does.Not.Contain("limit_price"));
      Assert.That(json, Does.Not.Contain("display_size"));
    }

    [Test]
    public void ModifyOrderModel_Serialization_AllFieldsSet()
    {
      var model = new ModifyOrderModel
      {
        Id = 200,
        OrderType = OrderType.LMT,
        TotalQuantity = 100,
        LimitPrice = 150.5,
        DisplaySize = 50,
        PriceType = "LIMIT_PRICE"
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"id\":200"));
      Assert.That(json, Does.Contain("\"order_type\":\"LMT\""));
      Assert.That(json, Does.Contain("\"total_quantity\":100"));
      Assert.That(json, Does.Contain("\"limit_price\":150.5"));
      Assert.That(json, Does.Contain("\"display_size\":50"));
      Assert.That(json, Does.Contain("\"price_type\":\"LIMIT_PRICE\""));
    }

    [Test]
    public void QueryOrderModel_DefaultLimit_Is100()
    {
      var model = new QueryOrderModel();
      Assert.That(model.Limit, Is.EqualTo(100));
    }

    [Test]
    public void QueryOrderModel_Serialization_IncludesAllFields()
    {
      var model = new QueryOrderModel
      {
        Id = 1,
        IsShowCharges = true,
        SegType = SegmentType.SEC,
        SecType = SecType.STK,
        Market = Market.US,
        Symbol = "AAPL",
        StartDate = 1000,
        EndDate = 2000,
        SortBy = OrderSortBy.LATEST_CREATED
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"id\":1"));
      Assert.That(json, Does.Contain("\"show_charges\":true"));
      Assert.That(json, Does.Contain("\"seg_type\":\"SEC\""));
      Assert.That(json, Does.Contain("\"sec_type\":\"STK\""));
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"limit\":100"));
    }

    [Test]
    public void PositionsModel_Serialization_WireNames()
    {
      var model = new PositionsModel
      {
        SecType = SecType.STK,
        Market = Market.US,
        Currency = Currency.USD,
        Symbol = "AAPL"
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"sec_type\":\"STK\""));
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"currency\":\"USD\""));
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
    }

    [Test]
    public void GlobalAssetsModel_Serialization_WireNames()
    {
      var model = new GlobalAssetsModel { Segment = true, MarketValue = false };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"segment\":true"));
      Assert.That(json, Does.Not.Contain("\"market_value\":false")); // false + DefaultValueHandling.Ignore
    }

    [Test]
    public void BaseContractModel_DefaultSecType_IsSTK()
    {
      var model = new ContractModel();
      Assert.That(model.SecType, Is.EqualTo("STK"));
    }

    [Test]
    public void ContractModel_Serialization_HasSymbol()
    {
      var model = new ContractModel { Symbol = "AAPL", Currency = "USD" };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"currency\":\"USD\""));
      Assert.That(json, Does.Contain("\"sec_type\":\"STK\""));
    }

    [Test]
    public void ContractsModel_Serialization_HasSymbols()
    {
      var model = new ContractsModel { Symbols = new List<string> { "AAPL", "GOOG" } };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("AAPL"));
      Assert.That(json, Does.Contain("GOOG"));
    }

    [Test]
    public void TagValue_BuildTagValue_ReturnsInstance_WhenBothNonNull()
    {
      var tv = TagValue.BuildTagValue("tag1", "value1");
      Assert.That(tv, Is.Not.Null);
      Assert.That(tv.Tag, Is.EqualTo("tag1"));
      Assert.That(tv.Value, Is.EqualTo("value1"));
    }

    [Test]
    public void TagValue_BuildTagValue_ReturnsNull_WhenTagEmpty()
    {
      var tv = TagValue.BuildTagValue("", "value");
      Assert.That(tv, Is.Null);
    }

    [Test]
    public void TagValue_BuildTagValue_ReturnsNull_WhenValueNull()
    {
      var tv = TagValue.BuildTagValue("tag", null);
      Assert.That(tv, Is.Null);
    }

    [Test]
    public void ContractLeg_DefaultRatio_Is1()
    {
      var leg = new ContractLeg();
      Assert.That(leg.Ratio, Is.EqualTo(1));
    }

    [Test]
    public void OptionExerciseSubmitModel_Serialization_IncludesRequiredFields()
    {
      var model = new OptionExerciseSubmitModel
      {
        ContractId = 12345,
        Type = "Exercise",
        Quantity = 10,
        ExecutingDate = "2024-01-19",
        IsForce = true
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"contract_id\":12345"));
      Assert.That(json, Does.Contain("\"type\":\"Exercise\""));
      Assert.That(json, Does.Contain("\"quantity\":10"));
      Assert.That(json, Does.Contain("\"executing_date\":\"2024-01-19\""));
      Assert.That(json, Does.Contain("\"is_force\":true"));
    }

    [Test]
    public void OptionExerciseSubmitModel_NullOptionals_Omitted()
    {
      var model = new OptionExerciseSubmitModel { ContractId = 1, Type = "Expire", Quantity = 5 };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Not.Contain("executing_date"));
      Assert.That(json, Does.Not.Contain("is_force"));
      Assert.That(json, Does.Not.Contain("itm_rate"));
    }

    [Test]
    public void OptionExerciseCheckModel_Serialization()
    {
      var model = new OptionExerciseCheckModel { ContractId = 99, Type = "Exercise", Quantity = 1 };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"contract_id\":99"));
      Assert.That(json, Does.Contain("\"quantity\":1"));
    }

    [Test]
    public void OptionExerciseCancelModel_Serialization()
    {
      var model = new OptionExerciseCancelModel { Id = 8888 };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"id\":8888"));
    }

    [Test]
    public void OptionExercisePositionModel_Serialization()
    {
      var model = new OptionExercisePositionModel { Type = "Exercise" };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"type\":\"Exercise\""));
    }

    [Test]
    public void OptionExerciseRecordModel_Defaults_Page1Size20()
    {
      var model = new OptionExerciseRecordModel();
      Assert.That(model.Page, Is.EqualTo(1));
      Assert.That(model.Size, Is.EqualTo(20));
    }

    [Test]
    public void OrderTransactionsModel_DefaultLimit_Is20()
    {
      var model = new OrderTransactionsModel();
      Assert.That(model.Limit, Is.EqualTo(20));
    }

    [Test]
    public void OrderTransactionsModel_Serialization()
    {
      var model = new OrderTransactionsModel
      {
        OrderId = 123,
        SecType = SecType.OPT,
        Symbol = "AAPL",
        StartDate = 1000,
        EndDate = 2000
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"order_id\":123"));
      Assert.That(json, Does.Contain("\"sec_type\":\"OPT\""));
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"limit\":20"));
    }

    [Test]
    public void EstimateTradableQuantityModel_Serialization()
    {
      var model = new EstimateTradableQuantityModel
      {
        Symbol = "AAPL",
        SecType = SecType.STK,
        Action = ActionType.BUY,
        OrderType = OrderType.LMT,
        LimitPrice = 150.0
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"sec_type\":\"STK\""));
      Assert.That(json, Does.Contain("\"action\":\"BUY\""));
      Assert.That(json, Does.Contain("\"order_type\":\"LMT\""));
      Assert.That(json, Does.Contain("\"limit_price\":150"));
    }

    [Test]
    public void DepositWithdrawModel_Serialization_NullOptionalsOmitted()
    {
      var model = new DepositWithdrawModel { Currency = "USD" };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"currency\":\"USD\""));
      Assert.That(json, Does.Not.Contain("seg_type"));
      Assert.That(json, Does.Not.Contain("limit"));
      Assert.That(json, Does.Not.Contain("page"));
    }

    [Test]
    public void ForexTradeOrderModel_DefaultTimeInForce_IsDAY()
    {
      var model = new ForexTradeOrderModel();
      Assert.That(model.TimeInForce, Is.EqualTo(TimeInForce.DAY));
    }

    [Test]
    public void ForexTradeOrderModel_Serialization()
    {
      var model = new ForexTradeOrderModel
      {
        SourceCurrency = Currency.USD,
        SourceAmount = 1000.0,
        TargetCurrency = Currency.HKD,
        SegType = SegmentType.SEC
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"source_currency\":\"USD\""));
      Assert.That(json, Does.Contain("\"source_amount\":1000"));
      Assert.That(json, Does.Contain("\"target_currency\":\"HKD\""));
      Assert.That(json, Does.Contain("\"time_in_force\":\"DAY\""));
    }

    [Test]
    public void SegmentFundModel_Serialization()
    {
      var model = new SegmentFundModel
      {
        FromSegment = SegmentType.SEC,
        ToSegment = SegmentType.FUT,
        Currency = Currency.USD,
        Amount = 5000.0
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"from_segment\":\"SEC\""));
      Assert.That(json, Does.Contain("\"to_segment\":\"FUT\""));
      Assert.That(json, Does.Contain("\"currency\":\"USD\""));
      Assert.That(json, Does.Contain("\"amount\":5000"));
    }
  }
}
