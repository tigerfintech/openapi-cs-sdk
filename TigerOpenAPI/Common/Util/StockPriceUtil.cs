using System;
using System.Collections.Generic;
using System.Linq;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Common.Util
{
  public class StockPriceUtil
  {
    public const string INFINITY = "Infinity";
    public const decimal MAX_PRICE = decimal.MaxValue;

    private StockPriceUtil()
    {
    }

    public static bool MatchTickSize(decimal price, List<TickSizeItem>? tickSizes)
    {
      if (price == 0)
      {
        return false;
      }

      decimal fixedPrice = FixPriceByTickSize(price, tickSizes);
      decimal difference = price - fixedPrice;
      return Math.Round(difference, 6, MidpointRounding.AwayFromZero) == 0;
    }

    public static decimal FixPriceByTickSize(decimal price, List<TickSizeItem>? tickSizes)
    {
      return FixPriceByTickSize(price, tickSizes, false);
    }

    /**  
     * fix price by min tick  
     * @param price  
     * @param tickSizes  
     * @param up default value:false. true:increments the price, false:decrease the price  
     * @return  
     */
    public static decimal FixPriceByTickSize(decimal price, List<TickSizeItem>? tickSizes, bool up)
    {
      if (price == 0)
      {
        return 0;
      }

      TickSizeItem? curItem = FindTickSize(price, tickSizes);
      if (curItem == null)
      {
        return price;
      }

      decimal minTick = (decimal)curItem.TickSize;
      decimal begin = decimal.Parse(curItem.Begin);
      decimal multiple = (price - begin) / minTick;

      if (multiple == Math.Truncate(multiple)) // Check if multiple is an integer  
      {
        return price;  
      }

      if (up)
      {
        multiple = Math.Ceiling(multiple);
      }
      else
      {
        multiple = Math.Floor(multiple);
      }

      return minTick * multiple + begin;
    }

    private static TickSizeItem? FindTickSize(decimal price, List<TickSizeItem>? tickSizes)
    {
      if (price == 0 || tickSizes == null || !tickSizes.Any())
      {
        return null;
      }

      foreach (TickSizeItem item in tickSizes)
      {
        decimal begin = decimal.Parse(item.Begin);
        decimal end = INFINITY.Equals(item.End) ? MAX_PRICE : decimal.Parse(item.End);
        TickSizeType type = item.Type;

        if (type == TickSizeType.OPEN)
        {
          if (begin < price && price < end)
          {
            return item;
          }
        }
        else if (type == TickSizeType.OPEN_CLOSED)
        {
          if (begin < price && price <= end)
          {
            return item;
          }
        }
        else if (type == TickSizeType.CLOSED_OPEN)
        {
          if (begin <= price && price < end)
          {
            return item;
          }
        }
        else if (type == TickSizeType.CLOSED)
        {
          if (begin <= price && price <= end)
          {
            return item;
          }
        }
      }

      return null;
    }
  }
}
