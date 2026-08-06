using System.Collections.Generic;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Push.Model;
using TigerOpenAPI.Quote.Pb;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for TradeTickUtil.Convert / ConvertStockData / ConvertFutureData.
  /// Covers time recovery (cumulative sum), price recovery (priceBase + delta) / 10^offset,
  /// part-code short/full name lookup, US/HK trade-condition lookup, and SN sequencing.
  /// </summary>
  [TestFixture]
  public class TradeTickUtilTest
  {
    /// <summary>
    /// US stock: verify cumulative time, price recovery, SN increment,
    /// part-code short & full name mapping, US trade-condition translation, tickType passthrough.
    /// </summary>
    [Test]
    public void ConvertStockData_WhenUsSymbolWithFullInput_RecoveresTicks()
    {
      // price = (priceBase + delta) / 10^offset => (10000 + delta) / 100
      // delta 50 -> 100.50 ; delta 25 -> 100.25 ; delta 75 -> 100.75
      TradeTickData source = new TradeTickData
      {
        Symbol = "AAPL",
        SecType = SecType.STK.ToString(),
        QuoteLevel = "1",
        Timestamp = 1700000000000L,
        Sn = 100L,
        PriceBase = 10000L,
        PriceOffset = 2
      };
      source.Time.Add(1000L);
      source.Time.Add(2000L);
      source.Time.Add(3000L);
      source.Price.Add(50L);
      source.Price.Add(25L);
      source.Price.Add(75L);
      source.Volume.Add(10L);
      source.Volume.Add(20L);
      source.Volume.Add(30L);
      source.PartCode.Add("n");
      source.PartCode.Add("t");
      source.PartCode.Add("z");
      source.Cond = "BF ";
      source.Type = "+-+";

      TradeTick result = TradeTickUtil.ConvertStockData(source);

      Assert.That(result, Is.Not.Null);
      Assert.That(result.Symbol, Is.EqualTo("AAPL"));
      Assert.That(result.SecType, Is.EqualTo(SecType.STK));
      Assert.That(result.QuoteLevel, Is.EqualTo("1"));
      Assert.That(result.Timestamp, Is.EqualTo(1700000000000L));
      Assert.That(result.Ticks, Is.Not.Null);
      Assert.That(result.Ticks.Count, Is.EqualTo(3));

      // cumulative time: 1000, 3000, 6000
      Assert.That(result.Ticks[0].Time, Is.EqualTo(1000L));
      Assert.That(result.Ticks[1].Time, Is.EqualTo(3000L));
      Assert.That(result.Ticks[2].Time, Is.EqualTo(6000L));

      // recovered prices
      Assert.That(result.Ticks[0].Price, Is.EqualTo(100.50).Within(1e-9));
      Assert.That(result.Ticks[1].Price, Is.EqualTo(100.25).Within(1e-9));
      Assert.That(result.Ticks[2].Price, Is.EqualTo(100.75).Within(1e-9));

      // SN increments from start
      Assert.That(result.Ticks[0].Sn, Is.EqualTo(100L));
      Assert.That(result.Ticks[1].Sn, Is.EqualTo(101L));
      Assert.That(result.Ticks[2].Sn, Is.EqualTo(102L));

      // volumes
      Assert.That(result.Ticks[0].Volume, Is.EqualTo(10L));
      Assert.That(result.Ticks[1].Volume, Is.EqualTo(20L));
      Assert.That(result.Ticks[2].Volume, Is.EqualTo(30L));

      // part-code short & full name (NYSE / NASDAQ / Cboe BZX)
      Assert.That(result.Ticks[0].PartCode, Is.EqualTo("NYSE"));
      Assert.That(result.Ticks[0].PartName, Is.EqualTo("New York Stock Exchange, LLC (NYSE)"));
      Assert.That(result.Ticks[1].PartCode, Is.EqualTo("NSDQ"));
      Assert.That(result.Ticks[1].PartName, Is.EqualTo("NASDAQ Stock Market, LLC (NASDAQ)"));
      Assert.That(result.Ticks[2].PartCode, Is.EqualTo("BZX"));
      Assert.That(result.Ticks[2].PartName, Is.EqualTo("Cboe BZX Exchange, Inc. (Cboe BZX)"));

      // US trade-condition lookup
      Assert.That(result.Ticks[0].Cond, Is.EqualTo("US_BUNCHED_TRADE"));
      Assert.That(result.Ticks[1].Cond, Is.EqualTo("US_INTERMARKET_SWEEP"));
      Assert.That(result.Ticks[2].Cond, Is.EqualTo("US_REGULAR_SALE"));

      // tickType per-position substring
      Assert.That(result.Ticks[0].TickType, Is.EqualTo("+"));
      Assert.That(result.Ticks[1].TickType, Is.EqualTo("-"));
      Assert.That(result.Ticks[2].TickType, Is.EqualTo("+"));
    }

    /// <summary>
    /// HK symbol: trade-condition dictionary switches to HK set (isUsStockSymbol false).
    /// HK stock symbols are purely numeric (e.g. "00700"), which isUsStockSymbol's
    /// [A-Z]+ regex does not match, so the HK dict is selected.
    /// Empty part-code list leaves PartCode/PartName untouched.
    /// </summary>
    [Test]
    public void ConvertStockData_WhenHkSymbol_UsesHkTradeCondDict()
    {
      TradeTickData source = new TradeTickData
      {
        Symbol = "00700",
        SecType = SecType.STK.ToString(),
        Timestamp = 1700000000000L,
        Sn = 1L,
        PriceBase = 30000L,
        PriceOffset = 2
      };
      source.Time.Add(500L);
      source.Price.Add(0L);
      source.Volume.Add(100L);
      source.Cond = "D";

      TradeTick result = TradeTickUtil.ConvertStockData(source);

      Assert.That(result.Symbol, Is.EqualTo("00700"));
      Assert.That(result.Ticks.Count, Is.EqualTo(1));
      Assert.That(result.Ticks[0].Price, Is.EqualTo(300.0).Within(1e-9));
      // HK odd-lot trade
      Assert.That(result.Ticks[0].Cond, Is.EqualTo("HK_ODD_LOT_TRADE"));
      // no part code -> defaults
      Assert.That(result.Ticks[0].PartCode, Is.Null);
      Assert.That(result.Ticks[0].PartName, Is.Null);
    }

    /// <summary>
    /// Empty time list yields a non-null TradeTick with an empty ticks list.
    /// </summary>
    [Test]
    public void ConvertStockData_WhenTimeListEmpty_ReturnsEmptyTicks()
    {
      TradeTickData source = new TradeTickData
      {
        Symbol = "AAPL",
        SecType = SecType.STK.ToString(),
        Timestamp = 1L,
        Sn = 0L,
        PriceBase = 0L,
        PriceOffset = 0
      };

      TradeTick result = TradeTickUtil.ConvertStockData(source);

      Assert.That(result, Is.Not.Null);
      Assert.That(result.Symbol, Is.EqualTo("AAPL"));
      Assert.That(result.SecType, Is.EqualTo(SecType.STK));
      Assert.That(result.Ticks, Is.Not.Null);
      Assert.That(result.Ticks, Is.Empty);
    }

    /// <summary>
    /// Missing cond char (position beyond cond string) maps to regular sale for US,
    /// because GetTradeCondByCode converts code 0 -> ' '.
    /// </summary>
    [Test]
    public void ConvertStockData_WhenCondShorterThanTicks_DefaultsToRegularSale()
    {
      TradeTickData source = new TradeTickData
      {
        Symbol = "AAPL",
        SecType = SecType.STK.ToString(),
        Timestamp = 1L,
        Sn = 0L,
        PriceBase = 0L,
        PriceOffset = 0
      };
      source.Time.Add(1L);
      source.Time.Add(2L);
      source.Price.Add(0L);
      source.Price.Add(0L);
      source.Volume.Add(1L);
      source.Volume.Add(2L);
      // cond only has one char for two ticks
      source.Cond = "B";

      TradeTick result = TradeTickUtil.ConvertStockData(source);

      Assert.That(result.Ticks.Count, Is.EqualTo(2));
      Assert.That(result.Ticks[0].Cond, Is.EqualTo("US_BUNCHED_TRADE"));
      // second tick: cond.Length(1) > 1 is false -> condChar '\0' -> ' ' -> US_REGULAR_SALE
      Assert.That(result.Ticks[1].Cond, Is.EqualTo("US_REGULAR_SALE"));
    }

    /// <summary>
    /// Future: each time/price entry expands into MergeTimes ticks; SN = startSn*10 + j.
    /// </summary>
    [Test]
    public void ConvertFutureData_WhenMergedVols_ExpandsTicksByMergeTimes()
    {
      // price = (100000 + delta) / 10^1
      // delta 500  -> 10050 ; delta 1000 -> 10100
      TradeTickData source = new TradeTickData
      {
        Symbol = "ESZ3",
        SecType = SecType.FUT.ToString(),
        Timestamp = 1700000000000L,
        Sn = 50L,
        PriceBase = 100000L,
        PriceOffset = 1
      };
      source.Time.Add(100L);
      source.Time.Add(200L);
      source.Price.Add(500L);
      source.Price.Add(1000L);

      TradeTickData.Types.MergedVol mv0 = new TradeTickData.Types.MergedVol { MergeTimes = 2 };
      mv0.Vol.Add(5L);
      mv0.Vol.Add(15L);
      source.MergedVols.Add(mv0);

      TradeTickData.Types.MergedVol mv1 = new TradeTickData.Types.MergedVol { MergeTimes = 1 };
      mv1.Vol.Add(25L);
      source.MergedVols.Add(mv1);

      TradeTick result = TradeTickUtil.ConvertFutureData(source);

      Assert.That(result, Is.Not.Null);
      Assert.That(result.Symbol, Is.EqualTo("ESZ3"));
      Assert.That(result.SecType, Is.EqualTo(SecType.FUT));
      Assert.That(result.Timestamp, Is.EqualTo(1700000000000L));
      // total = 2 + 1
      Assert.That(result.Ticks.Count, Is.EqualTo(3));

      // i=0: time=100, price=10050, startSn=50 -> sn 500, 501
      Assert.That(result.Ticks[0].Sn, Is.EqualTo(500L));
      Assert.That(result.Ticks[0].Time, Is.EqualTo(100L));
      Assert.That(result.Ticks[0].Price, Is.EqualTo(10050.0).Within(1e-9));
      Assert.That(result.Ticks[0].Volume, Is.EqualTo(5L));

      Assert.That(result.Ticks[1].Sn, Is.EqualTo(501L));
      Assert.That(result.Ticks[1].Time, Is.EqualTo(100L));
      Assert.That(result.Ticks[1].Price, Is.EqualTo(10050.0).Within(1e-9));
      Assert.That(result.Ticks[1].Volume, Is.EqualTo(15L));

      // i=1: time=100+200=300, price=10100, startSn=51 -> sn 510
      Assert.That(result.Ticks[2].Sn, Is.EqualTo(510L));
      Assert.That(result.Ticks[2].Time, Is.EqualTo(300L));
      Assert.That(result.Ticks[2].Price, Is.EqualTo(10100.0).Within(1e-9));
      Assert.That(result.Ticks[2].Volume, Is.EqualTo(25L));
    }

    /// <summary>
    /// Convert routes to ConvertFutureData when SecType is FUT.
    /// </summary>
    [Test]
    public void Convert_WhenSecTypeFut_RoutesToFuturePath()
    {
      TradeTickData source = new TradeTickData
      {
        Symbol = "CLZ3",
        SecType = SecType.FUT.ToString(),
        Timestamp = 1L,
        Sn = 1L,
        PriceBase = 0L,
        PriceOffset = 0
      };
      source.Time.Add(1L);
      source.Price.Add(0L);
      TradeTickData.Types.MergedVol mv = new TradeTickData.Types.MergedVol { MergeTimes = 1 };
      mv.Vol.Add(1L);
      source.MergedVols.Add(mv);

      TradeTick result = TradeTickUtil.Convert(source);

      Assert.That(result.SecType, Is.EqualTo(SecType.FUT));
      Assert.That(result.Ticks.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// Convert routes to ConvertStockData when SecType is not FUT.
    /// </summary>
    [Test]
    public void Convert_WhenSecTypeStk_RoutesToStockPath()
    {
      TradeTickData source = new TradeTickData
      {
        Symbol = "AAPL",
        SecType = SecType.STK.ToString(),
        Timestamp = 1L,
        Sn = 0L,
        PriceBase = 0L,
        PriceOffset = 0
      };
      source.Time.Add(1L);
      source.Price.Add(0L);
      source.Volume.Add(1L);

      TradeTick result = TradeTickUtil.Convert(source);

      Assert.That(result.SecType, Is.EqualTo(SecType.STK));
      Assert.That(result.Ticks.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// Convert routes to ConvertStockData when SecType is empty (not equal to FUT).
    /// </summary>
    [Test]
    public void Convert_WhenSecTypeEmpty_RoutesToStockPath()
    {
      TradeTickData source = new TradeTickData
      {
        Symbol = "AAPL",
        Timestamp = 1L,
        Sn = 0L,
        PriceBase = 0L,
        PriceOffset = 0
      };
      source.Time.Add(1L);
      source.Price.Add(0L);
      source.Volume.Add(1L);

      TradeTick result = TradeTickUtil.Convert(source);

      Assert.That(result.SecType, Is.EqualTo(SecType.STK));
      Assert.That(result.Ticks.Count, Is.EqualTo(1));
    }
  }
}
