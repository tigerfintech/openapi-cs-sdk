// Unit tests for iceberg order model construction — no network required.
using System;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

namespace Sample
{
  static class IcebergUnitTest
  {
    static int pass = 0, fail = 0;

    static void Assert(string name, bool cond, string detail = "")
    {
      string status = cond ? "[OK]" : "[FAIL]";
      Console.WriteLine($"  {status} {name}{(string.IsNullOrEmpty(detail) ? "" : ": " + detail)}");
      if (cond) pass++; else { fail++; }
    }

    public static int Run()
    {
      Console.WriteLine("=== C# SDK Iceberg Order Unit Tests ===");

      ContractItem contract = ContractItem.BuildStockContract("AAPL", Currency.USD.ToString());

      // --- Test 1: basic iceberg ---
      {
        var o = PlaceOrderModel.BuildIcebergOrder("ACC1", contract, ActionType.BUY, 100, 1.5, 10);
        Assert("basic: OrderType == ICEBERG", o.OrderType == OrderType.ICEBERG);
        Assert("basic: DisplaySize == 10", o.DisplaySize == 10);
        Assert("basic: LimitPrice == 1.5", o.LimitPrice == 1.5);
        Assert("basic: TotalQuantity == 100", o.TotalQuantity == 100);
        Assert("basic: Action == BUY", o.Action == ActionType.BUY);
        Assert("basic: Symbol == AAPL", o.Symbol == "AAPL");
        Assert("basic: MinDisplaySize is null", o.MinDisplaySize == null);
        Assert("basic: CheckIntervals is null", o.CheckIntervals == null);
        Assert("basic: PriceType is null", o.PriceType == null);
        Assert("basic: StartTime is null", o.StartTime == null);
        Assert("basic: EndTime is null", o.EndTime == null);
      }

      // --- Test 2: full iceberg ---
      {
        long start = 1700000000000L;
        long end   = 1700000000000L + 4 * 3600 * 1000L;
        var o = PlaceOrderModel.BuildIcebergOrder(
            "ACC1", contract, ActionType.SELL, 200, 2.0,
            20, 5, 30, PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT, start, end);
        Assert("full: OrderType == ICEBERG", o.OrderType == OrderType.ICEBERG);
        Assert("full: DisplaySize == 20", o.DisplaySize == 20);
        Assert("full: MinDisplaySize == 5", o.MinDisplaySize == 5);
        Assert("full: CheckIntervals == 30", o.CheckIntervals == 30);
        Assert("full: PriceType == LIMIT_PRICE", o.PriceType == PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT);
        Assert("full: StartTime set", o.StartTime == start);
        Assert("full: EndTime set", o.EndTime == end);
        Assert("full: Action == SELL", o.Action == ActionType.SELL);
        Assert("full: TotalQuantity == 200", o.TotalQuantity == 200);
      }

      // --- Test 3: optional zeros are omitted ---
      {
        var o = PlaceOrderModel.BuildIcebergOrder(
            "ACC1", contract, ActionType.BUY, 50, 1.0,
            5, null, null, null, null, null);
        Assert("zero-omit: MinDisplaySize null when not passed", o.MinDisplaySize == null);
        Assert("zero-omit: CheckIntervals null when not passed", o.CheckIntervals == null);
        Assert("zero-omit: PriceType null when not passed", o.PriceType == null);
      }

      // --- Test 4: price type constant values ---
      Assert("const LIMIT_PRICE",    PlaceOrderModel.ICEBERG_PRICE_TYPE_LIMIT    == "LIMIT_PRICE");
      Assert("const OPPONENT_PRICE", PlaceOrderModel.ICEBERG_PRICE_TYPE_OPPONENT == "OPPONENT_PRICE");

      Console.WriteLine();
      Console.WriteLine($"=== Result: {pass} passed, {fail} failed ===");
      return fail;
    }
  }
}
