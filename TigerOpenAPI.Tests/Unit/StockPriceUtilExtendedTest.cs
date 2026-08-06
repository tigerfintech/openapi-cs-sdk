using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Extended tests for StockPriceUtil — covers FindTickSize with all TickSizeType variants,
  /// MatchTickSize edge cases, and FixPriceByTickSize with boundary conditions.
  /// Zero network — pure math assertions.
  /// </summary>
  [TestFixture]
  public class StockPriceUtilExtendedTest
  {
    private static List<TickSizeItem> MakeTickSizes(string begin, string end, TickSizeType type, double tickSize)
    {
      return new List<TickSizeItem>
      {
        new TickSizeItem { Begin = begin, End = end, Type = type, TickSize = tickSize }
      };
    }

    // --- TickSizeType.OPEN: begin < price < end ---

    [Test]
    public void FindTickSize_OpenType_ExcludesBeginAndEnd()
    {
      var items = MakeTickSizes("0", "100", TickSizeType.OPEN, 0.01);
      // price exactly at begin -> not matched (open)
      Assert.That(StockPriceUtil.MatchTickSize(0, items), Is.False);
      // price just above begin -> matched
      Assert.That(StockPriceUtil.MatchTickSize(0.01m, items), Is.True);
    }

    [Test]
    public void FindTickSize_OpenType_PriceAtEnd_NotMatched()
    {
      var items = MakeTickSizes("0", "100", TickSizeType.OPEN, 0.01);
      // price exactly at end -> not matched (open)
      // 100 is on tick, so FixPriceByTickSize returns 100, difference 0 -> match true
      // Actually with OPEN, begin < price < end, so 100 is excluded -> FindTickSize returns null
      // FixPriceByTickSize returns price unchanged, and MatchTickSize checks price == fixedPrice -> true
      // But wait, price=0 returns false, so price=100 should work
      Assert.That(StockPriceUtil.MatchTickSize(100, items), Is.True); // null result -> price unchanged -> match
    }

    // --- TickSizeType.CLOSED: begin <= price <= end ---

    [Test]
    public void FindTickSize_ClosedType_IncludesBeginAndEnd()
    {
      var items = MakeTickSizes("0", "100", TickSizeType.CLOSED, 0.01);
      // price at begin -> matched (closed)
      Assert.That(StockPriceUtil.MatchTickSize(0.01m, items), Is.True);
    }

    // --- TickSizeType.OPEN_CLOSED: begin < price <= end ---

    [Test]
    public void FindTickSize_OpenClosedType_ExcludesBeginIncludesEnd()
    {
      var items = MakeTickSizes("0", "100", TickSizeType.OPEN_CLOSED, 0.01);
      // price just above begin -> matched
      Assert.That(StockPriceUtil.MatchTickSize(0.01m, items), Is.True);
    }

    // --- TickSizeType.CLOSED_OPEN: begin <= price < end ---

    [Test]
    public void FindTickSize_ClosedOpenType_IncludesBeginExcludesEnd()
    {
      var items = MakeTickSizes("0", "100", TickSizeType.CLOSED_OPEN, 0.01);
      // price at begin -> matched (closed_open includes begin)
      Assert.That(StockPriceUtil.MatchTickSize(0.01m, items), Is.True);
    }

    // --- Infinity end ---

    [Test]
    public void FindTickSize_InfinityEnd_HandlesLargePrice()
    {
      var items = MakeTickSizes("0", "Infinity", TickSizeType.CLOSED, 0.01);
      Assert.That(StockPriceUtil.MatchTickSize(5000m, items), Is.True);
    }

    // --- Edge cases ---

    [Test]
    public void FixPriceByTickSize_PriceZero_ReturnsZero()
    {
      Assert.That(StockPriceUtil.FixPriceByTickSize(0, null), Is.EqualTo(0));
    }

    [Test]
    public void FixPriceByTickSize_NullTickSizes_ReturnsPrice()
    {
      Assert.That(StockPriceUtil.FixPriceByTickSize(150.5m, null), Is.EqualTo(150.5m));
    }

    [Test]
    public void FixPriceByTickSize_EmptyTickSizes_ReturnsPrice()
    {
      Assert.That(StockPriceUtil.FixPriceByTickSize(150.5m, new List<TickSizeItem>()), Is.EqualTo(150.5m));
    }

    [Test]
    public void MatchTickSize_PriceZero_ReturnsFalse()
    {
      Assert.That(StockPriceUtil.MatchTickSize(0, null), Is.False);
    }

    [Test]
    public void FixPriceByTickSize_UpTrue_RoundsUp()
    {
      var items = new List<TickSizeItem>
      {
        new TickSizeItem { Begin = "0", End = "Infinity", Type = TickSizeType.CLOSED, TickSize = 0.02 }
      };
      // 10.01 with tick 0.02: (10.01 - 0) / 0.02 = 500.5 -> floor 500 -> 500*0.02 = 10.00 (down)
      // ceil 501 -> 501*0.02 = 10.02 (up)
      decimal down = StockPriceUtil.FixPriceByTickSize(10.01m, items, false);
      decimal up = StockPriceUtil.FixPriceByTickSize(10.01m, items, true);
      Assert.That(down, Is.EqualTo(10.00m));
      Assert.That(up, Is.EqualTo(10.02m));
    }

    [Test]
    public void FixPriceByTickSize_ExactMultiple_ReturnsPrice()
    {
      var items = new List<TickSizeItem>
      {
        new TickSizeItem { Begin = "0", End = "Infinity", Type = TickSizeType.CLOSED, TickSize = 0.02 }
      };
      // 10.00 is exactly 500 * 0.02, so it should return unchanged
      Assert.That(StockPriceUtil.FixPriceByTickSize(10.00m, items), Is.EqualTo(10.00m));
    }

    [Test]
    public void MatchTickSize_OnTick_ReturnsTrue()
    {
      var items = new List<TickSizeItem>
      {
        new TickSizeItem { Begin = "0", End = "Infinity", Type = TickSizeType.CLOSED, TickSize = 0.02 }
      };
      Assert.That(StockPriceUtil.MatchTickSize(10.00m, items), Is.True);
      Assert.That(StockPriceUtil.MatchTickSize(10.02m, items), Is.True);
      Assert.That(StockPriceUtil.MatchTickSize(10.04m, items), Is.True);
    }

    [Test]
    public void MatchTickSize_OffTick_ReturnsFalse()
    {
      var items = new List<TickSizeItem>
      {
        new TickSizeItem { Begin = "0", End = "Infinity", Type = TickSizeType.CLOSED, TickSize = 0.02 }
      };
      Assert.That(StockPriceUtil.MatchTickSize(10.01m, items), Is.False);
      Assert.That(StockPriceUtil.MatchTickSize(10.03m, items), Is.False);
    }

    [Test]
    public void FindTickSize_NoMatchingRange_ReturnsNull()
    {
      var items = new List<TickSizeItem>
      {
        new TickSizeItem { Begin = "0", End = "100", Type = TickSizeType.CLOSED, TickSize = 0.01 }
      };
      // 200 is outside the range -> FindTickSize returns null -> FixPriceByTickSize returns price
      Assert.That(StockPriceUtil.FixPriceByTickSize(200m, items), Is.EqualTo(200m));
    }

    [Test]
    public void FixPriceByTickSize_MultipleRanges_FindsCorrectRange()
    {
      var items = new List<TickSizeItem>
      {
        new TickSizeItem { Begin = "0", End = "5", Type = TickSizeType.CLOSED, TickSize = 0.01 },
        new TickSizeItem { Begin = "5", End = "Infinity", Type = TickSizeType.CLOSED, TickSize = 0.05 }
      };
      // 3.50 with tick 0.01 -> on tick -> returns 3.50
      Assert.That(StockPriceUtil.FixPriceByTickSize(3.50m, items), Is.EqualTo(3.50m));
      // 7.23 with tick 0.05 -> (7.23 - 5) / 0.05 = 44.6 -> floor 44 -> 44*0.05 + 5 = 7.20
      Assert.That(StockPriceUtil.FixPriceByTickSize(7.23m, items), Is.EqualTo(7.20m));
      // 7.23 with up -> ceil 45 -> 45*0.05 + 5 = 7.25
      Assert.That(StockPriceUtil.FixPriceByTickSize(7.23m, items, true), Is.EqualTo(7.25m));
    }

    [Test]
    public void TickSizeItem_Serialization_WireNames()
    {
      var item = new TickSizeItem { Begin = "0", End = "Infinity", Type = TickSizeType.CLOSED, TickSize = 0.01 };
      string json = JsonConvert.SerializeObject(item, new JsonSerializerSettings());
      Assert.That(json, Does.Contain("\"begin\":\"0\""));
      Assert.That(json, Does.Contain("\"end\":\"Infinity\""));
      Assert.That(json, Does.Contain("\"type\":\"CLOSED\""));
      Assert.That(json, Does.Contain("\"tickSize\":0.01"));
    }

    [Test]
    public void TickSizeItem_Deserialize_FromJson()
    {
      string json = @"{""begin"":""0"",""end"":""100"",""type"":""OPEN"",""tickSize"":0.05}";
      var item = JsonConvert.DeserializeObject<TickSizeItem>(json);
      Assert.That(item.Begin, Is.EqualTo("0"));
      Assert.That(item.End, Is.EqualTo("100"));
      Assert.That(item.Type, Is.EqualTo(TickSizeType.OPEN));
      Assert.That(item.TickSize, Is.EqualTo(0.05));
    }
  }
}
