using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Extended unit tests for <see cref="PlaceOrderModel"/>: every public static factory,
  /// fluent attach methods, serialization wire-name contracts, null-handling, and edge cases.
  /// Also covers JSON deserialization for the trade response types that were not already
  /// exercised by <see cref="TradeResponseTest"/> / <see cref="ContractItemTest"/>.
  /// Zero network — pure model construction + serialization assertions.
  /// </summary>
  [TestFixture]
  public class PlaceOrderModelExtendedTest
  {
    // NOTE: build a fresh isolated JsonSerializerSettings per fixture rather than holding a
    // long-lived reference to the mutable shared `TigerClient.JsonSet` instance. Other test
    // fixtures mutate that shared object's state (contract cache / converters), which made the
    // serialization assertions in this fixture flaky depending on execution order. The values
    // below mirror TigerClient.JsonSet exactly so we still assert against production wire format.
    private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
      DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat,
      DateFormatString = DateUtil.FORMAT_DATETIME,
      NullValueHandling = NullValueHandling.Ignore,
      DefaultValueHandling = DefaultValueHandling.Ignore,
      MaxDepth = 10,
      ReferenceLoopHandling = ReferenceLoopHandling.Serialize
    };

    private ContractItem _stockContract = null!;

    [SetUp]
    public void SetUp()
    {
      _stockContract = ContractItem.BuildStockContract("AAPL", Currency.USD.ToString());
    }

    // ---------------------------------------------------------------------
    // BuildMarketOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildMarketOrder_DefaultScale_SetsMktOrderType()
    {
      var o = PlaceOrderModel.BuildMarketOrder("U123", _stockContract,
          ActionType.BUY, 100);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.MKT));
      Assert.That(o.Action, Is.EqualTo(ActionType.BUY));
      Assert.That(o.TotalQuantity, Is.EqualTo(100));
      Assert.That(o.TotalQuantityScale, Is.EqualTo(0));
      Assert.That(o.Account, Is.EqualTo("U123"));
      Assert.That(o.Symbol, Is.EqualTo("AAPL"));
      Assert.That(o.SecType, Is.EqualTo(SecType.STK));
      Assert.That(o.LimitPrice, Is.Null);
      Assert.That(o.TimeInForce, Is.EqualTo(TimeInForce.DAY));
    }

    [Test]
    public void BuildMarketOrder_WithExplicitScale_PropagatesScale()
    {
      var o = PlaceOrderModel.BuildMarketOrder("U123", _stockContract,
          ActionType.SELL, 50, totalQuantityScale: 3);

      Assert.That(o.TotalQuantity, Is.EqualTo(50));
      Assert.That(o.TotalQuantityScale, Is.EqualTo(3));
      Assert.That(o.OrderType, Is.EqualTo(OrderType.MKT));
    }

    [Test]
    public void BuildMarketOrder_SerializesRequiredWireNames()
    {
      var o = PlaceOrderModel.BuildMarketOrder("U123", _stockContract,
          ActionType.BUY, 100);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"order_type\":\"MKT\""));
      Assert.That(json, Does.Contain("\"action\":\"BUY\""));
      Assert.That(json, Does.Contain("\"total_quantity\":100"));
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"sec_type\":\"STK\""));
      Assert.That(json, Does.Contain("\"time_in_force\":\"DAY\""));
      Assert.That(json, Does.Not.Contain("\"limit_price\""), "MKT must omit limit_price");
    }

    // ---------------------------------------------------------------------
    // BuildAmountOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildAmountOrder_SetsCashAmountAndNullQuantity()
    {
      var o = PlaceOrderModel.BuildAmountOrder("U123", _stockContract,
          ActionType.BUY, 5000.0);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.MKT));
      Assert.That(o.CashAmount, Is.EqualTo(5000.0));
      Assert.That(o.TotalQuantity, Is.Null);
      Assert.That(o.Action, Is.EqualTo(ActionType.BUY));
    }

    [Test]
    public void BuildAmountOrder_SerializesCashAmountWireName()
    {
      var o = PlaceOrderModel.BuildAmountOrder("U123", _stockContract,
          ActionType.SELL, 2500.5);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"cash_amount\":2500.5"));
      // total_quantity is null — must be omitted (NullValueHandling.Ignore)
      Assert.That(json, Does.Not.Contain("\"total_quantity\""));
    }

    // ---------------------------------------------------------------------
    // BuildLimitOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildLimitOrder_DefaultAdjust_SetsLmtAndZeroAdjust()
    {
      var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.5);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.LMT));
      Assert.That(o.LimitPrice, Is.EqualTo(150.5));
      Assert.That(o.AdjustLimit, Is.EqualTo(0.0));
      Assert.That(o.TotalQuantityScale, Is.EqualTo(0));
    }

    [Test]
    public void BuildLimitOrder_WithAdjustAndScale_PropagatesValues()
    {
      var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.5, adjustLimit: 0.001, totalQuantityScale: 2);

      Assert.That(o.AdjustLimit, Is.EqualTo(0.001));
      Assert.That(o.TotalQuantityScale, Is.EqualTo(2));
    }

    [Test]
    public void BuildLimitOrder_SerializesLimitAndAdjustWireNames()
    {
      var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.5, adjustLimit: -0.001);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"order_type\":\"LMT\""));
      Assert.That(json, Does.Contain("\"limit_price\":150.5"));
      Assert.That(json, Does.Contain("\"adjust_limit\":-0.001"));
    }

    // ---------------------------------------------------------------------
    // BuildAuctionOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildAuctionOrder_Defaults_AreAlAndOpg()
    {
      var o = PlaceOrderModel.BuildAuctionOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.AL));
      Assert.That(o.TimeInForce, Is.EqualTo(TimeInForce.OPG));
      Assert.That(o.LimitPrice, Is.EqualTo(150.0));
    }

    [Test]
    public void BuildAuctionOrder_CustomOrderTypeAndTif_PropagatesValues()
    {
      var o = PlaceOrderModel.BuildAuctionOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0,
          orderType: OrderType.AM, timeInForce: TimeInForce.DAY, adjustLimit: 0.01);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.AM));
      Assert.That(o.TimeInForce, Is.EqualTo(TimeInForce.DAY));
      Assert.That(o.AdjustLimit, Is.EqualTo(0.01));
    }

    [Test]
    public void BuildAuctionOrder_SerializesAlAndOpg()
    {
      var o = PlaceOrderModel.BuildAuctionOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"order_type\":\"AL\""));
      Assert.That(json, Does.Contain("\"time_in_force\":\"OPG\""));
    }

    // ---------------------------------------------------------------------
    // BuildStopOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildStopOrder_SetsStpAndAuxPrice()
    {
      var o = PlaceOrderModel.BuildStopOrder("U123", _stockContract,
          ActionType.SELL, 100, 140.0);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.STP));
      Assert.That(o.AuxPrice, Is.EqualTo(140.0));
      Assert.That(o.AdjustLimit, Is.EqualTo(0.0));
    }

    [Test]
    public void BuildStopOrder_WithAdjust_PropagatesAdjust()
    {
      var o = PlaceOrderModel.BuildStopOrder("U123", _stockContract,
          ActionType.SELL, 100, 140.0, adjustLimit: 0.002, totalQuantityScale: 1);

      Assert.That(o.AdjustLimit, Is.EqualTo(0.002));
      Assert.That(o.TotalQuantityScale, Is.EqualTo(1));
    }

    [Test]
    public void BuildStopOrder_SerializesAuxPriceWireName()
    {
      var o = PlaceOrderModel.BuildStopOrder("U123", _stockContract,
          ActionType.SELL, 100, 140.0);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"order_type\":\"STP\""));
      Assert.That(json, Does.Contain("\"aux_price\":140"));
    }

    // ---------------------------------------------------------------------
    // BuildStopLimitOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildStopLimitOrder_SetsStpLmtWithLimitAndAux()
    {
      var o = PlaceOrderModel.BuildStopLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0, 140.0);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.STP_LMT));
      Assert.That(o.LimitPrice, Is.EqualTo(150.0));
      Assert.That(o.AuxPrice, Is.EqualTo(140.0));
    }

    [Test]
    public void BuildStopLimitOrder_SerializesStpLmtEnumValue()
    {
      var o = PlaceOrderModel.BuildStopLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0, 140.0);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"order_type\":\"STP_LMT\""));
      Assert.That(json, Does.Contain("\"limit_price\":150"));
      Assert.That(json, Does.Contain("\"aux_price\":140"));
    }

    // ---------------------------------------------------------------------
    // BuildTrailOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildTrailOrder_SetsTrailWithPercentAndAux()
    {
      var o = PlaceOrderModel.BuildTrailOrder("U123", _stockContract,
          ActionType.SELL, 100, 1.5, 1.0);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.TRAIL));
      Assert.That(o.TrailingPercent, Is.EqualTo(1.5));
      Assert.That(o.AuxPrice, Is.EqualTo(1.0));
    }

    [Test]
    public void BuildTrailOrder_SerializesTrailingPercentWireName()
    {
      var o = PlaceOrderModel.BuildTrailOrder("U123", _stockContract,
          ActionType.SELL, 100, 1.5, 1.0);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"order_type\":\"TRAIL\""));
      Assert.That(json, Does.Contain("\"trailing_percent\":1.5"));
    }

    // ---------------------------------------------------------------------
    // BuildTradeOrderModel — shared helper, incl. FUT branch logic
    // ---------------------------------------------------------------------

    [Test]
    public void BuildTradeOrderModel_NullContract_ThrowsArgumentNullException()
    {
      var ex = Assert.Throws<ArgumentNullException>(() =>
          PlaceOrderModel.BuildTradeOrderModel("U123", null!, ActionType.BUY, 100));
      Assert.That(ex.Message, Does.Contain("contract"));
    }

    [Test]
    public void BuildTradeOrderModel_NonGlobalFutAccount_ClearsExpiry()
    {
      // Non-global account (does not start with U/DU/F/DF) → Expiry forced to null
      var fut = ContractItem.BuildFutureContract("ES", "USD", "CME", "20240315", 50.0);
      var o = PlaceOrderModel.BuildTradeOrderModel("1234567", fut, ActionType.BUY, 1);

      Assert.That(o.SecType, Is.EqualTo(SecType.FUT));
      Assert.That(o.Expiry, Is.Null, "Expiry must be cleared for non-global futures account");
      Assert.That(o.Symbol, Is.EqualTo("ES"));
    }

    [Test]
    public void BuildTradeOrderModel_GlobalFutAccount_WithBlankExpiryAndLastTradingDate_UsesLastTradingDate()
    {
      var fut = ContractItem.BuildFutureContract("ES", "USD", "CME", "", 50.0);
      fut.LastTradingDate = "20240316";
      fut.Type = "ES"; // Type used as Symbol for global account when non-blank

      var o = PlaceOrderModel.BuildTradeOrderModel("U123456789", fut, ActionType.BUY, 1);

      Assert.That(o.Expiry, Is.EqualTo("20240316"));
      Assert.That(o.Symbol, Is.EqualTo("ES"));
    }

    [Test]
    public void BuildTradeOrderModel_GlobalFutAccount_WithBlankType_KeepsOriginalSymbol()
    {
      var fut = ContractItem.BuildFutureContract("ES", "USD", "CME", "20240315", 50.0);
      fut.Type = "";
      var o = PlaceOrderModel.BuildTradeOrderModel("DU123456", fut, ActionType.BUY, 1);

      Assert.That(o.Symbol, Is.EqualTo("ES"), "blank Type must not override Symbol");
      Assert.That(o.Expiry, Is.EqualTo("20240315"));
    }

    [Test]
    public void BuildTradeOrderModel_ZeroStrike_YieldsNullStrikeOnModel()
    {
      var opt = ContractItem.BuildOptionContract("AAPL", "20240119", 0.0, "CALL");
      var o = PlaceOrderModel.BuildTradeOrderModel("U123", opt, ActionType.BUY, 1);

      Assert.That(o.Strike, Is.Null, "strike <= 0 must serialize as null");
      Assert.That(o.Right, Is.EqualTo("CALL"));
    }

    [Test]
    public void BuildTradeOrderModel_NullCurrency_ResolvesToCurrencyNone()
    {
      var c = new ContractItem { Symbol = "AAPL", SecType = "STK" };
      var o = PlaceOrderModel.BuildTradeOrderModel("U123", c, ActionType.BUY, 1);

      Assert.That(o.Currency, Is.EqualTo(Currency.NONE));
      Assert.That(o.SecType, Is.EqualTo(SecType.STK));
      Assert.That(o.Market, Is.EqualTo(Market.NONE));
    }

    // ---------------------------------------------------------------------
    // BuildMultiLegOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildMultiLegOrder_NullContractLegs_Throws()
    {
      var ex = Assert.Throws<ArgumentException>(() =>
          PlaceOrderModel.BuildMultiLegOrder("U123", null, ComboType.VERTICAL,
              ActionType.BUY, 1, OrderType.LMT, 100.0, null, null));
      Assert.That(ex.Message, Does.Contain("contractLegs"));
    }

    [Test]
    public void BuildMultiLegOrder_NoneOrderType_Throws()
    {
      var legs = new List<ContractLeg> { new ContractLeg { Symbol = "AAPL", SecType = "STK" } };
      var ex = Assert.Throws<ArgumentException>(() =>
          PlaceOrderModel.BuildMultiLegOrder("U123", legs, ComboType.VERTICAL,
              ActionType.BUY, 1, OrderType.NONE, 100.0, null, null));
      Assert.That(ex.Message, Does.Contain("orderType"));
    }

    [Test]
    public void BuildMultiLegOrder_LmtCombo_SetsMlegSecTypeAndComboType()
    {
      var legs = new List<ContractLeg>
      {
        new ContractLeg { Symbol = "AAPL", SecType = "OPT", Action = "BUY",  Ratio = 1 },
        new ContractLeg { Symbol = "AAPL", SecType = "OPT", Action = "SELL", Ratio = 1 }
      };
      var o = PlaceOrderModel.BuildMultiLegOrder("U123", legs, ComboType.VERTICAL,
          ActionType.BUY, 1, OrderType.LMT, 100.0, null, null);

      Assert.That(o.SecType, Is.EqualTo(SecType.MLEG));
      Assert.That(o.ComboType, Is.EqualTo("VERTICAL"));
      Assert.That(o.OrderType, Is.EqualTo(OrderType.LMT));
      Assert.That(o.LimitPrice, Is.EqualTo(100.0));
      Assert.That(o.TimeInForce, Is.EqualTo(TimeInForce.DAY));
    }

    [Test]
    public void BuildMultiLegOrder_SerializesComboAndContractLegsWireNames()
    {
      var legs = new List<ContractLeg>
      {
        new ContractLeg { Symbol = "AAPL", SecType = "OPT", Ratio = 1 }
      };
      var o = PlaceOrderModel.BuildMultiLegOrder("U123", legs, ComboType.STRADDLE,
          ActionType.BUY, 1, OrderType.LMT, 100.0, null, null);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"sec_type\":\"MLEG\""));
      Assert.That(json, Does.Contain("\"combo_type\":\"STRADDLE\""));
      Assert.That(json, Does.Contain("\"contract_legs\""));
      Assert.That(json, Does.Contain("\"ratio\":1"));
    }

    // ---------------------------------------------------------------------
    // BuildTWAPOrder / BuildVWAPOrder / BuildWAPOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildTWAPOrder_SetsAlgoStrategyAndParams()
    {
      var o = PlaceOrderModel.BuildTWAPOrder("U123", "AAPL",
          ActionType.BUY, 100, 1700000000L, 1700003600L, 150.0);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.TWAP));
      Assert.That(o.AlgoStrategy, Is.EqualTo("TWAP"));
      Assert.That(o.OutsideRth, Is.False);
      Assert.That(o.SecType, Is.EqualTo(SecType.STK));
      Assert.That(o.Symbol, Is.EqualTo("AAPL"));
      Assert.That(o.LimitPrice, Is.EqualTo(150.0));
      Assert.That(o.AlgoParams, Is.Not.Null);
      // TWAP must NOT include participation_rate param
      Assert.That(o.AlgoParams.Find(p => p.Tag == PlaceOrderModel.WAP_PARTICIPATION_RATE),
          Is.Null);
    }

    [Test]
    public void BuildVWAPOrder_AddsParticipationRateParam()
    {
      var o = PlaceOrderModel.BuildVWAPOrder("U123", "AAPL",
          ActionType.BUY, 100, 1700000000L, 1700003600L, 0.5, 150.0);

      Assert.That(o.OrderType, Is.EqualTo(OrderType.VWAP));
      Assert.That(o.AlgoStrategy, Is.EqualTo("VWAP"));
      var pr = o.AlgoParams.Find(p => p.Tag == PlaceOrderModel.WAP_PARTICIPATION_RATE);
      Assert.That(pr, Is.Not.Null);
      Assert.That(pr!.Value, Is.EqualTo("0.5"));
    }

    [Test]
    public void BuildWAPOrder_InvalidOrderType_Throws()
    {
      var ex = Assert.Throws<ArgumentException>(() =>
          PlaceOrderModel.BuildWAPOrder("U123", "AAPL", ActionType.BUY, 100,
              OrderType.MKT, 1L, 2L, null, null));
      Assert.That(ex.Message, Does.Contain("TWAP").And.Contains("VWAP"));
    }

    [Test]
    public void BuildTWAPOrder_NullTimes_AreStillSerializedViaAddAlgoParamSkipped()
    {
      // TagValue.BuildTagValue returns null for null value → AddAlgoParam skips it
      // without ever initializing AlgoParams.
      var o = PlaceOrderModel.BuildTWAPOrder("U123", "AAPL",
          ActionType.BUY, 100, null, null, null);
      Assert.That(o.AlgoParams, Is.Null, "null start/end must not initialize AlgoParams");
    }

    [Test]
    public void BuildVWAPOrder_SerializesAlgoStrategyAndParamsWireNames()
    {
      var o = PlaceOrderModel.BuildVWAPOrder("U123", "AAPL",
          ActionType.BUY, 100, 1700000000L, 1700003600L, 0.5, 150.0);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"algo_strategy\":\"VWAP\""));
      Assert.That(json, Does.Contain("\"algo_params\""));
      Assert.That(json, Does.Contain("\"start_time\""));
      Assert.That(json, Does.Contain("\"end_time\""));
      Assert.That(json, Does.Contain("\"participation_rate\""));
    }

    // ---------------------------------------------------------------------
    // BuildIcebergOrder — edge cases (basic covered in IcebergOrderTest)
    // ---------------------------------------------------------------------

    [Test]
    public void BuildIcebergOrder_ZeroDisplaySize_Throws()
    {
      var ex = Assert.Throws<ArgumentException>(() =>
          PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
              ActionType.BUY, 100, 1.0, 0));
      Assert.That(ex.Message, Does.Contain("displaySize"));
    }

    [Test]
    public void BuildIcebergOrder_NegativeDisplaySize_Throws()
    {
      var ex = Assert.Throws<ArgumentException>(() =>
          PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
              ActionType.BUY, 100, 1.0, -5));
      Assert.That(ex.Message, Does.Contain("displaySize"));
    }

    [Test]
    public void BuildIcebergOrder_MinDisplayExceedsDisplay_Throws()
    {
      var ex = Assert.Throws<ArgumentException>(() =>
          PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
              ActionType.BUY, 100, 1.0, 10, 20, null, null, null, null));
      Assert.That(ex.Message, Does.Contain("minDisplaySize"));
    }

    [Test]
    public void BuildIcebergOrder_InvalidPriceType_Throws()
    {
      var ex = Assert.Throws<ArgumentException>(() =>
          PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
              ActionType.BUY, 100, 1.0, 10, null, null,
              "INVALID_PRICE", null, null));
      Assert.That(ex.Message, Does.Contain("priceType"));
    }

    [Test]
    public void BuildIcebergOrder_StartTimeGreaterOrEqualToEnd_Throws()
    {
      long t = 1700000000000L;
      Assert.Throws<ArgumentException>(() =>
          PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
              ActionType.BUY, 100, 1.0, 10, null, null, null, t, t));

      Assert.Throws<ArgumentException>(() =>
          PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
              ActionType.BUY, 100, 1.0, 10, null, null, null, t + 1, t));
    }

    [Test]
    public void BuildIcebergOrder_EachValidPriceType_Accepted()
    {
      string[] types = {
        PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT,
        PlaceOrderModel.ICEBERG_PRICE_TYPE_ASK,
        PlaceOrderModel.ICEBERG_PRICE_TYPE_BID,
        PlaceOrderModel.ICEBERG_PRICE_TYPE_LATEST
      };
      foreach (var pt in types)
      {
        var o = PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
            ActionType.BUY, 100, 1.0, 10, null, null, pt, null, null);
        Assert.That(o.PriceType, Is.EqualTo(pt));
      }
    }

    [Test]
    public void BuildIcebergOrder_ZeroStartTime_NotApplied()
    {
      // startTime=0 alone (no endTime) must not trigger the startTime>=endTime check,
      // and the >0 guard must skip assignment so StartTime stays null.
      var o = PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
          ActionType.BUY, 100, 1.0, 10, null, null, null, 0L, null);
      Assert.That(o.StartTime, Is.Null, "startTime==0 must not be applied");
      Assert.That(o.EndTime, Is.Null);
    }

    [Test]
    public void BuildIcebergOrder_NegativeEndTime_NotApplied()
    {
      var o = PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
          ActionType.BUY, 100, 1.0, 10, null, null, null, null, -1L);
      Assert.That(o.EndTime, Is.Null, "endTime<0 must not be applied");
      Assert.That(o.StartTime, Is.Null);
    }

    [Test]
    public void BuildIcebergOrder_FullForm_SerializesAllOptionalWireNames()
    {
      long start = 1700000000000L;
      long end = start + 3600_000L;
      var o = PlaceOrderModel.BuildIcebergOrder("U123", _stockContract,
          ActionType.BUY, 100, 1.0, 10, 5, 30,
          PlaceOrderModel.ICEBERG_PRICE_TYPE_ASK, start, end);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"order_type\":\"ICEBERG\""));
      Assert.That(json, Does.Contain("\"display_size\":10"));
      Assert.That(json, Does.Contain("\"min_display_size\":5"));
      Assert.That(json, Does.Contain("\"check_intervals\":30"));
      Assert.That(json, Does.Contain("\"price_type\":\"ASK_PRICE\""));
      Assert.That(json, Does.Contain("\"start_time\":1700000000000"));
      Assert.That(json, Does.Contain($"\"end_time\":{end}"));
    }

    // ---------------------------------------------------------------------
    // BuildOCABracketsOrder
    // ---------------------------------------------------------------------

    [Test]
    public void BuildOCABracketsOrder_WithoutStopLossLimit_ProducesStpAndLmtLegs()
    {
      var o = PlaceOrderModel.BuildOCABracketsOrder("U123", _stockContract,
          ActionType.BUY, 100,
          160.0, TimeInForce.DAY, true,
          140.0, TimeInForce.GTC, false);

      // OCA container itself has two children (OcaOrders is a private property)
      var ocaProp = typeof(PlaceOrderModel).GetProperty("OcaOrders",
          System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
      Assert.That(ocaProp, Is.Not.Null);
      var list = (List<PlaceOrderModel>?)ocaProp!.GetValue(o);
      Assert.That(list, Is.Not.Null);
      Assert.That(list!.Count, Is.EqualTo(2));

      var profit = list[0];
      Assert.That(profit.OrderType, Is.EqualTo(OrderType.LMT));
      Assert.That(profit.LimitPrice, Is.EqualTo(160.0));
      Assert.That(profit.TimeInForce, Is.EqualTo(TimeInForce.DAY));
      Assert.That(profit.OutsideRth, Is.True);

      var stop = list[1];
      Assert.That(stop.OrderType, Is.EqualTo(OrderType.STP));
      Assert.That(stop.AuxPrice, Is.EqualTo(140.0));
      Assert.That(stop.TimeInForce, Is.EqualTo(TimeInForce.GTC));
      Assert.That(stop.OutsideRth, Is.False);
    }

    [Test]
    public void BuildOCABracketsOrder_WithStopLossLimit_UsesStpLmt()
    {
      var o = PlaceOrderModel.BuildOCABracketsOrder("U123", _stockContract,
          ActionType.BUY, 100,
          160.0, TimeInForce.DAY, true,
          140.0, TimeInForce.DAY, false, stopLossLimitPrice: 138.0);

      var ocaProp = typeof(PlaceOrderModel).GetProperty("OcaOrders",
          System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
      var list = (List<PlaceOrderModel>?)ocaProp!.GetValue(o);
      Assert.That(list, Is.Not.Null);
      var stop = list![1];
      Assert.That(stop.OrderType, Is.EqualTo(OrderType.STP_LMT));
      Assert.That(stop.LimitPrice, Is.EqualTo(138.0));
      Assert.That(stop.AuxPrice, Is.EqualTo(140.0));
    }

    // ---------------------------------------------------------------------
    // Fluent attach methods
    // ---------------------------------------------------------------------

    [Test]
    public void AddProfitTakerOrder_SetsProfitAttachTypeAndFields()
    {
      var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0);
      var returned = o.AddProfitTakerOrder(160.0, TimeInForce.DAY, true);

      Assert.That(returned, Is.SameAs(o), "fluent API must return same instance");
      Assert.That(o.AttachType, Is.EqualTo(AttachType.PROFIT));
      Assert.That(o.ProfitTakerPrice, Is.EqualTo(160.0));
      Assert.That(o.ProfitTakerTif, Is.EqualTo(TimeInForce.DAY));
      Assert.That(o.ProfitTakerRth, Is.True);
    }

    [Test]
    public void AddStopLossOrder_SetsLossAttachTypeAndStpOrderType()
    {
      var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0);
      o.AddStopLossOrder(140.0, TimeInForce.GTC);

      Assert.That(o.AttachType, Is.EqualTo(AttachType.LOSS));
      Assert.That(o.StopLossOrderType, Is.EqualTo(OrderType.STP));
      Assert.That(o.StopLossPrice, Is.EqualTo(140.0));
      Assert.That(o.StopLossTif, Is.EqualTo(TimeInForce.GTC));
    }

    [Test]
    public void AddStopLossLimitOrder_SetsStpLmtAndLimitPrice()
    {
      var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0);
      o.AddStopLossLimitOrder(140.0, 138.0, TimeInForce.DAY);

      Assert.That(o.StopLossOrderType, Is.EqualTo(OrderType.STP_LMT));
      Assert.That(o.StopLossLimitPrice, Is.EqualTo(138.0));
      Assert.That(o.StopLossPrice, Is.EqualTo(140.0));
    }

    [Test]
    public void AddStopLossTrailOrder_SetsTrailAndTrailingFields()
    {
      var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0);
      o.AddStopLossTrailOrder(1.5, 1.0, TimeInForce.DAY);

      Assert.That(o.StopLossOrderType, Is.EqualTo(OrderType.TRAIL));
      Assert.That(o.StopLossTrailingPercent, Is.EqualTo(1.5));
      Assert.That(o.StopLossTrailingAmount, Is.EqualTo(1.0));
    }

    [Test]
    public void AddBracketsOrder_SetsBracketsAttachTypeWithAllFields()
    {
      var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0);
      o.AddBracketsOrder(160.0, TimeInForce.DAY, true, 140.0, TimeInForce.GTC, 138.0);

      Assert.That(o.AttachType, Is.EqualTo(AttachType.BRACKETS));
      Assert.That(o.ProfitTakerPrice, Is.EqualTo(160.0));
      Assert.That(o.ProfitTakerTif, Is.EqualTo(TimeInForce.DAY));
      Assert.That(o.ProfitTakerRth, Is.True);
      Assert.That(o.StopLossPrice, Is.EqualTo(140.0));
      Assert.That(o.StopLossTif, Is.EqualTo(TimeInForce.GTC));
      Assert.That(o.StopLossLimitPrice, Is.EqualTo(138.0));
    }

    [Test]
    public void AddBracketsOrder_AttachTypeSerializesAsEnumString()
    {
      var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract,
          ActionType.BUY, 100, 150.0);
      o.AddBracketsOrder(160.0, TimeInForce.DAY, true, 140.0, TimeInForce.GTC);
      string json = JsonConvert.SerializeObject(o, Settings);

      Assert.That(json, Does.Contain("\"attach_type\":\"BRACKETS\""));
      Assert.That(json, Does.Contain("\"profit_taker_price\":160"));
      Assert.That(json, Does.Contain("\"stop_loss_price\":140"));
      Assert.That(json, Does.Contain("\"profit_taker_tif\":\"DAY\""));
      Assert.That(json, Does.Contain("\"stop_loss_tif\":\"GTC\""));
    }

    // ---------------------------------------------------------------------
    // AddAlgoParam
    // ---------------------------------------------------------------------

    [Test]
    public void AddAlgoParam_NullParam_IsIgnored()
    {
      var o = PlaceOrderModel.BuildMarketOrder("U123", _stockContract,
          ActionType.BUY, 100);
      o.AddAlgoParam(null);
      Assert.That(o.AlgoParams, Is.Null, "null param must not initialize AlgoParams");
    }

    [Test]
    public void AddAlgoParam_FirstParam_InitializesList()
    {
      var o = PlaceOrderModel.BuildMarketOrder("U123", _stockContract,
          ActionType.BUY, 100);
      var tv = TagValue.BuildTagValue("k", "v");
      o.AddAlgoParam(tv);
      Assert.That(o.AlgoParams, Is.Not.Null);
      Assert.That(o.AlgoParams.Count, Is.EqualTo(1));
      Assert.That(o.AlgoParams[0].Tag, Is.EqualTo("k"));
      Assert.That(o.AlgoParams[0].Value, Is.EqualTo("v"));
    }

    // ---------------------------------------------------------------------
    // Enum serialization wire values (regression guard)
    // ---------------------------------------------------------------------

    [Test]
    public void Enum_ActionType_SerializesAsUppercaseString()
    {
      var oBuy = PlaceOrderModel.BuildMarketOrder("U123", _stockContract, ActionType.BUY, 1);
      var oSell = PlaceOrderModel.BuildMarketOrder("U123", _stockContract, ActionType.SELL, 1);
      Assert.That(JsonConvert.SerializeObject(oBuy, Settings), Does.Contain("\"action\":\"BUY\""));
      Assert.That(JsonConvert.SerializeObject(oSell, Settings), Does.Contain("\"action\":\"SELL\""));
    }

    [Test]
    public void Enum_OrderType_SerializesAsUppercaseString()
    {
      var cases = new (OrderType ot, string wire)[]
      {
        (OrderType.MKT,     "MKT"),
        (OrderType.LMT,     "LMT"),
        (OrderType.STP,     "STP"),
        (OrderType.STP_LMT, "STP_LMT"),
        (OrderType.TRAIL,   "TRAIL"),
        (OrderType.ICEBERG, "ICEBERG")
      };
      foreach (var (ot, wire) in cases)
      {
        var o = PlaceOrderModel.BuildLimitOrder("U123", _stockContract, ActionType.BUY, 1, 1.0);
        o.OrderType = ot;
        string json = JsonConvert.SerializeObject(o, Settings);
        Assert.That(json, Does.Contain($"\"order_type\":\"{wire}\""),
            $"OrderType.{ot} must serialize as {wire}");
      }
    }

    [Test]
    public void Enum_TimeInForce_DefaultIsDay()
    {
      var o = PlaceOrderModel.BuildMarketOrder("U123", _stockContract, ActionType.BUY, 1);
      Assert.That(o.TimeInForce, Is.EqualTo(TimeInForce.DAY));
      Assert.That(JsonConvert.SerializeObject(o, Settings),
          Does.Contain("\"time_in_force\":\"DAY\""));
    }

    // ---------------------------------------------------------------------
    // Serialization: required fields present + null optional omitted
    // ---------------------------------------------------------------------

    [Test]
    public void Serialize_DefaultModel_OmitsNullOptionalFields()
    {
      var o = PlaceOrderModel.BuildMarketOrder("U123", _stockContract,
          ActionType.BUY, 100);
      string json = JsonConvert.SerializeObject(o, Settings);

      // Option-specific null fields must be absent
      Assert.That(json, Does.Not.Contain("\"right\""));
      Assert.That(json, Does.Not.Contain("\"strike\""));
      Assert.That(json, Does.Not.Contain("\"expiry\""));
      // Iceberg-only null fields must be absent
      Assert.That(json, Does.Not.Contain("\"display_size\""));
      Assert.That(json, Does.Not.Contain("\"min_display_size\""));
      Assert.That(json, Does.Not.Contain("\"check_intervals\""));
      Assert.That(json, Does.Not.Contain("\"price_type\""));
      Assert.That(json, Does.Not.Contain("\"start_time\""));
      Assert.That(json, Does.Not.Contain("\"end_time\""));
      // Algo fields null → absent
      Assert.That(json, Does.Not.Contain("\"algo_strategy\""));
      Assert.That(json, Does.Not.Contain("\"algo_params\""));
    }

    [Test]
    public void Serialize_OutsideRth_DefaultsTrueAndSerialized()
    {
      var o = PlaceOrderModel.BuildMarketOrder("U123", _stockContract,
          ActionType.BUY, 100);
      Assert.That(o.OutsideRth, Is.True);
      string json = JsonConvert.SerializeObject(o, Settings);
      Assert.That(json, Does.Contain("\"outside_rth\":true"));
    }

    // ---------------------------------------------------------------------
    // JSON deserialization: ContractItem variations
    // ---------------------------------------------------------------------

    [Test]
    public void ContractItem_Deserialize_FullFuturesContract_AllMarginFieldsMapped()
    {
      string json = @"{""contractId"":42,""identifier"":""ESZ4"",""symbol"":""ES"",
        ""secType"":""FUT"",""expiry"":""20241220"",""strike"":0,
        ""multiplier"":50,""exchange"":""CME"",""currency"":""USD"",
        ""shortable"":true,""shortableCount"":1000,
        ""longInitialMargin"":5000,""longMaintenanceMargin"":4000,
        ""discountedDayInitialMargin"":4500,""discountedDayMaintenanceMargin"":3600,
        ""discountedTimeZoneCode"":""CST"",
        ""lastTradingDate"":""20241220"",""firstNoticeDate"":""20241218"",
        ""continuous"":false,""type"":""ES"",""ibCode"":""ES""}";
      var c = JsonConvert.DeserializeObject<ContractItem>(json, Settings);

      Assert.That(c.ContractId, Is.EqualTo(42));
      Assert.That(c.Symbol, Is.EqualTo("ES"));
      Assert.That(c.SecType, Is.EqualTo("FUT"));
      Assert.That(c.Multiplier, Is.EqualTo(50.0));
      Assert.That(c.Shortable, Is.True);
      Assert.That(c.LongInitialMargin, Is.EqualTo(5000.0));
      Assert.That(c.DiscountedDayInitialMargin, Is.EqualTo(4500.0));
      Assert.That(c.DiscountedTimeZoneCode, Is.EqualTo("CST"));
      Assert.That(c.LastTradingDate, Is.EqualTo("20241220"));
      Assert.That(c.Type, Is.EqualTo("ES"));
    }

    [Test]
    public void ContractItem_Deserialize_EtfFields_Mapped()
    {
      string json = @"{""symbol"":""TQQQ"",""secType"":""STK"",""isEtf"":true,""etfLeverage"":3}";
      var c = JsonConvert.DeserializeObject<ContractItem>(json, Settings);

      Assert.That(c.IsEtf, Is.True);
      Assert.That(c.EtfLeverage, Is.EqualTo(3));
    }

    [Test]
    public void ContractItem_Convert_FromFundContractItem_MapsSymbolAndCurrency()
    {
      var fund = new FundContractItem { Symbol = "SPY", Market = "US", Currency = "USD" };
      var c = ContractItem.Convert(fund);

      Assert.That(c.SecType, Is.EqualTo(SecType.FUND.ToString()));
      Assert.That(c.Symbol, Is.EqualTo("SPY"));
      Assert.That(c.Market, Is.EqualTo("US"));
      Assert.That(c.Currency, Is.EqualTo("USD"));
    }

    [Test]
    public void ContractItem_Convert_FromFutureContractItem_MapsAllFields()
    {
      var fc = new FutureContractItem
      {
        Type = "ES", ContractCode = "ESZ4", Name = "E-mini S&P",
        IbCode = "ES", ContractMonth = "202412", Exchange = "CME",
        Multiplier = 50m, MinTick = 0.25m,
        LastTradingDate = "20241220", FirstNoticeDate = "20241218",
        LastBiddingCloseTime = 1700000000L, Currency = "USD",
        Trade = true, Continuous = false
      };
      var c = ContractItem.Convert(fc);

      Assert.That(c.SecType, Is.EqualTo(SecType.FUT.ToString()));
      Assert.That(c.Symbol, Is.EqualTo("ESZ4"));
      Assert.That(c.Type, Is.EqualTo("ES"));
      Assert.That(c.Name, Is.EqualTo("E-mini S&P"));
      Assert.That(c.Multiplier, Is.EqualTo(50.0));
      Assert.That(c.MinTick, Is.EqualTo(0.25));
      Assert.That(c.Expiry, Is.EqualTo("20241220"));
      Assert.That(c.Tradeable, Is.True);
      Assert.That(c.Continuous, Is.False);
    }

    [Test]
    public void ContractItem_BuildCbbcContract_FieldsSetCorrectly()
    {
      var c = ContractItem.BuildCbbcContract("12345", "20241201", 10.0, "CALL");
      Assert.That(c.SecType, Is.EqualTo(SecType.IOPT.ToString()));
      Assert.That(c.LocalSymbol, Is.EqualTo("12345"));
      Assert.That(c.Currency, Is.EqualTo(Currency.HKD.ToString()));
      Assert.That(c.Market, Is.EqualTo(Market.HK.ToString()));
    }

    [Test]
    public void ContractItem_BuildOptionContract_FromIdentifier_PutsCorrectRight()
    {
      // 'P' at index 12 → PUT
      var c = ContractItem.BuildOptionContract("AAPL  190118P00160000");
      Assert.That(c.Symbol, Is.EqualTo("AAPL"));
      Assert.That(c.Right, Is.EqualTo("PUT"));
      Assert.That(c.Expiry, Is.EqualTo("2019-01-18"));
      Assert.That(c.Strike, Is.EqualTo(160.0));
    }

    [Test]
    public void ContractItem_BuildOptionContract_FromIdentifier_CallVariant()
    {
      // 'C' at index 12 → CALL
      var c = ContractItem.BuildOptionContract("AAPL  190118C00160000");
      Assert.That(c.Right, Is.EqualTo("CALL"));
      Assert.That(c.Strike, Is.EqualTo(160.0));
    }

    // ---------------------------------------------------------------------
    // JSON deserialization: TradeOrder (more fields)
    // ---------------------------------------------------------------------

    [Test]
    public void TradeOrder_Deserialize_CommissionAndOutsideRth()
    {
      string json = @"{""id"":1,""symbol"":""AAPL"",""commission"":1.5,
        ""outsideRth"":false,""gst"":0.1,""realizedPnl"":50.0,
        ""liquidation"":true,""source"":""API"",""discount"":5,
        ""canModify"":false,""canCancel"":true,""isOpen"":false}";
      var o = JsonConvert.DeserializeObject<TradeOrder>(json, Settings);

      Assert.That(o.Commission, Is.EqualTo(1.5));
      Assert.That(o.OutsideRth, Is.False);
      Assert.That(o.Gst, Is.EqualTo(0.1));
      Assert.That(o.RealizedPnl, Is.EqualTo(50.0));
      Assert.That(o.Liquidation, Is.True);
      Assert.That(o.Source, Is.EqualTo("API"));
      Assert.That(o.Discount, Is.EqualTo(5));
      Assert.That(o.CanModify, Is.False);
      Assert.That(o.CanCancel, Is.True);
      Assert.That(o.IsOpen, Is.False);
    }

    [Test]
    public void TradeOrder_Deserialize_TrailingAndAuxPrice()
    {
      string json = @"{""orderType"":""TRAIL"",""auxPrice"":1.0,""trailingPercent"":1.5,
        ""totalQuantity"":100,""filledQuantity"":10,""avgFillPrice"":150.0,
        ""lastFillPrice"":149.5}";
      var o = JsonConvert.DeserializeObject<TradeOrder>(json, Settings);

      Assert.That(o.OrderType, Is.EqualTo("TRAIL"));
      Assert.That(o.AuxPrice, Is.EqualTo(1.0));
      Assert.That(o.TrailingPercent, Is.EqualTo(1.5));
      Assert.That(o.AvgFillPrice, Is.EqualTo(150.0));
      Assert.That(o.LastFillPrice, Is.EqualTo(149.5));
    }

    [Test]
    public void TradeOrder_Deserialize_LegsAndCombo()
    {
      string json = @"{""comboType"":""VERTICAL"",""comboTypeDesc"":""Vertical Spread"",
        ""legs"":[{""market"":""US"",""currency"":""USD"",""multiplier"":100,
        ""totalQuantity"":1,""filledQuantity"":0,""avgFilledPrice"":0,
        ""createdAt"":1700000000,""updatedAt"":1700000001}]}";
      var o = JsonConvert.DeserializeObject<TradeOrder>(json, Settings);

      Assert.That(o.ComboType, Is.EqualTo("VERTICAL"));
      Assert.That(o.ComboTypeDesc, Is.EqualTo("Vertical Spread"));
      Assert.That(o.Legs, Is.Not.Null);
      Assert.That(o.Legs.Count, Is.EqualTo(1));
      var leg = o.Legs[0];
      Assert.That(leg.Market, Is.EqualTo("US"));
      Assert.That(leg.Currency, Is.EqualTo("USD"));
      Assert.That(leg.Multiplier, Is.EqualTo(100.0));
      Assert.That(leg.TotalQuantity, Is.EqualTo(1.0));
      Assert.That(leg.CreatedAt, Is.EqualTo(1700000000L));
    }

    [Test]
    public void TradeOrder_Deserialize_AlgoParametersWireName()
    {
      string json = @"{""algoStrategy"":""TWAP"",
        ""algoParameters"":[{""tag"":""start_time"",""value"":""1700000000""}]}";
      var o = JsonConvert.DeserializeObject<TradeOrder>(json, Settings);

      Assert.That(o.AlgoStrategy, Is.EqualTo("TWAP"));
      Assert.That(o.AlgoParameters, Is.Not.Null);
      Assert.That(o.AlgoParameters.Count, Is.EqualTo(1));
      Assert.That(o.AlgoParameters[0].Tag, Is.EqualTo("start_time"));
      Assert.That(o.AlgoParameters[0].Value, Is.EqualTo("1700000000"));
    }

    [Test]
    public void TradeOrder_Deserialize_StatusEnumSubmitted()
    {
      string json = @"{""status"":""Submitted""}";
      var o = JsonConvert.DeserializeObject<TradeOrder>(json, Settings);
      Assert.That(o.Status, Is.EqualTo(OrderStatus.Submitted));
    }

    [Test]
    public void TradeOrder_Deserialize_StatusEnumFilled()
    {
      var o = JsonConvert.DeserializeObject<TradeOrder>(
          @"{""status"":""Filled""}", Settings);
      Assert.That(o.Status, Is.EqualTo(OrderStatus.Filled));
    }

    // ---------------------------------------------------------------------
    // JSON deserialization: Segment
    // ---------------------------------------------------------------------

    [Test]
    public void Segment_Deserialize_AllFieldsMapped()
    {
      string json = @"{""capability"":""SEC"",""category"":""STK"",""currency"":""USD"",
        ""cashBalance"":1000.5,""cashAvailableForTrade"":800.0,
        ""cashAvailableForWithdrawal"":700.0,""grossPositionValue"":5000.0,
        ""equityWithLoan"":6000.0,""netLiquidation"":6500.0,""initMargin"":1000.0,
        ""maintainMargin"":800.0,""overnightMargin"":1200.0,""unrealizedPL"":50.0,
        ""realizedPL"":25.0,""excessLiquidation"":300.0,""overnightLiquidation"":250.0,
        ""buyingPower"":2000.0,""leverage"":2.0,
        ""unrealizedPLByCostOfCarry"":10.0,""totalTodayPL"":35.0,
        ""lockedFunds"":5.0,""uncollected"":3.0,
        ""consolidatedSegTypes"":[""SEC"",""FUT""]}";
      var s = JsonConvert.DeserializeObject<Segment>(json, Settings);

      Assert.That(s.Capability, Is.EqualTo("SEC"));
      Assert.That(s.Category, Is.EqualTo("STK"));
      Assert.That(s.Currency, Is.EqualTo("USD"));
      Assert.That(s.CashBalance, Is.EqualTo(1000.5));
      Assert.That(s.CashAvailableForTrade, Is.EqualTo(800.0));
      Assert.That(s.NetLiquidation, Is.EqualTo(6500.0));
      Assert.That(s.OvernightMargin, Is.EqualTo(1200.0));
      Assert.That(s.UnrealizedPL, Is.EqualTo(50.0));
      Assert.That(s.RealizedPL, Is.EqualTo(25.0));
      Assert.That(s.BuyingPower, Is.EqualTo(2000.0));
      Assert.That(s.Leverage, Is.EqualTo(2.0));
      Assert.That(s.UnrealizedPLByCostOfCarry, Is.EqualTo(10.0));
      Assert.That(s.TotalTodayPL, Is.EqualTo(35.0));
      Assert.That(s.LockedFunds, Is.EqualTo(5.0));
      Assert.That(s.Uncollected, Is.EqualTo(3.0));
      Assert.That(s.ConsolidatedSegTypes, Is.Not.Null);
      Assert.That(s.ConsolidatedSegTypes.Count, Is.EqualTo(2));
      Assert.That(s.ConsolidatedSegTypes[0], Is.EqualTo("SEC"));
    }

    // ---------------------------------------------------------------------
    // JSON deserialization: OrderTransactions / OrderTransactionsItem
    // ---------------------------------------------------------------------

    [Test]
    public void OrderTransactions_Deserialize_AllFieldsMapped()
    {
      string json = @"{""id"":100,""orderId"":200,""accountId"":""U123"",
        ""secType"":""STK"",""market"":""US"",""currency"":""USD"",
        ""symbol"":""AAPL"",""expiry"":"""",""strike"":"""",""right"":"""",
        ""action"":""BUY"",""filledQuantity"":10,""filledPrice"":150.5,
        ""filledAmount"":1505.0,""transactedAt"":""2024-01-01 10:00:00"",
        ""transactionTime"":1700000000}";
      var t = JsonConvert.DeserializeObject<OrderTransactions>(json, Settings);

      Assert.That(t.Id, Is.EqualTo(100));
      Assert.That(t.OrderId, Is.EqualTo(200));
      Assert.That(t.AccountId, Is.EqualTo("U123"));
      Assert.That(t.SecType, Is.EqualTo("STK"));
      Assert.That(t.Symbol, Is.EqualTo("AAPL"));
      Assert.That(t.Action, Is.EqualTo("BUY"));
      Assert.That(t.FilledQuantity, Is.EqualTo(10));
      Assert.That(t.FilledPrice, Is.EqualTo(150.5));
      Assert.That(t.FilledAmount, Is.EqualTo(1505.0));
      Assert.That(t.TransactionTime, Is.EqualTo(1700000000L));
      Assert.That(t.TransactedAt, Is.EqualTo("2024-01-01 10:00:00"));
    }

    [Test]
    public void OrderTransactionsItem_Deserialize_WrapsItemsList()
    {
      string json = @"{""items"":[{""id"":1,""orderId"":2,""symbol"":""AAPL"",
        ""action"":""BUY"",""filledQuantity"":1,""filledPrice"":100,
        ""filledAmount"":100,""transactionTime"":1700000000}]}";
      var item = JsonConvert.DeserializeObject<OrderTransactionsItem>(json, Settings);

      Assert.That(item, Is.Not.Null);
      Assert.That(item.Items, Is.Not.Null);
      Assert.That(item.Items.Count, Is.EqualTo(1));
      Assert.That(item.Items[0].Symbol, Is.EqualTo("AAPL"));
      Assert.That(item.Items[0].Action, Is.EqualTo("BUY"));
      Assert.That(item.Items[0].FilledQuantity, Is.EqualTo(1));
    }

    // ---------------------------------------------------------------------
    // JSON deserialization: OrderBatchItem / OrderBatchResponse
    // ---------------------------------------------------------------------

    [Test]
    public void OrderBatchItem_Deserialize_WithNextPageTokenAndItems()
    {
      string json = @"{""nextPageToken"":""abc123"",
        ""items"":[{""id"":1,""symbol"":""AAPL"",""action"":""BUY""},
                  {""id"":2,""symbol"":""MSFT"",""action"":""SELL""}]}";
      var item = JsonConvert.DeserializeObject<OrderBatchItem>(json, Settings);

      Assert.That(item, Is.Not.Null);
      Assert.That(item.NextPageToken, Is.EqualTo("abc123"));
      Assert.That(item.Items, Is.Not.Null);
      Assert.That(item.Items.Count, Is.EqualTo(2));
      Assert.That(item.Items[0].Id, Is.EqualTo(1));
      Assert.That(item.Items[1].Symbol, Is.EqualTo("MSFT"));
    }

    [Test]
    public void OrderBatchResponse_Deserialize_WrapsDataItem()
    {
      string json = @"{""code"":0,""data"":{""items"":[{""id"":1,""symbol"":""AAPL""}]}}";
      var resp = JsonConvert.DeserializeObject<OrderBatchResponse>(json, Settings);

      Assert.That(resp, Is.Not.Null);
      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data, Is.Not.Null);
      Assert.That(resp.Data.Items, Is.Not.Null);
      Assert.That(resp.Data.Items.Count, Is.EqualTo(1));
      Assert.That(resp.Data.Items[0].Id, Is.EqualTo(1));
    }

    [Test]
    public void OrderBatchResponse_Deserialize_WithNextPageToken()
    {
      string json = @"{""code"":0,""data"":{""nextPageToken"":""tok"",
        ""items"":[{""id"":7,""symbol"":""TSLA"",""status"":""Filled""}]}}";
      var resp = JsonConvert.DeserializeObject<OrderBatchResponse>(json, Settings);

      Assert.That(resp.IsSuccess(), Is.True);
      Assert.That(resp.Data.NextPageToken, Is.EqualTo("tok"));
      Assert.That(resp.Data.Items[0].Symbol, Is.EqualTo("TSLA"));
      Assert.That(resp.Data.Items[0].Status, Is.EqualTo(OrderStatus.Filled));
    }

    // ---------------------------------------------------------------------
    // JSON deserialization: OrderLeg
    // ---------------------------------------------------------------------

    [Test]
    public void OrderLeg_Deserialize_AllFieldsMapped()
    {
      string json = @"{""market"":""US"",""currency"":""USD"",""multiplier"":100,
        ""totalQuantity"":2,""filledQuantity"":1,""avgFilledPrice"":150.0,
        ""createdAt"":1700000000,""updatedAt"":1700000001}";
      var leg = JsonConvert.DeserializeObject<OrderLeg>(json, Settings);

      Assert.That(leg, Is.Not.Null);
      Assert.That(leg.Market, Is.EqualTo("US"));
      Assert.That(leg.Currency, Is.EqualTo("USD"));
      Assert.That(leg.Multiplier, Is.EqualTo(100.0));
      Assert.That(leg.TotalQuantity, Is.EqualTo(2.0));
      Assert.That(leg.FilledQuantity, Is.EqualTo(1.0));
      Assert.That(leg.AvgFilledPrice, Is.EqualTo(150.0));
      Assert.That(leg.CreatedAt, Is.EqualTo(1700000000L));
      Assert.That(leg.UpdatedAt, Is.EqualTo(1700000001L));
    }
  }
}
