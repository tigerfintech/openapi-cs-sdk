using System;
using DotNetty.Common.Utilities;
using System.Text.Json.Nodes;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Push.Model;
using TigerOpenAPI.Quote.Pb;

namespace TigerOpenAPI.Common.Util
{
  public class TradeTickUtil
  {
    private TradeTickUtil() { }

    private static readonly Dictionary<string, string> PART_CODE_NAME_DICT = InitPartCodeNameDict();

    private static readonly Dictionary<string, string> PART_CODE_SHORT_NAME_DICT = InitPartCodeShortNameDict();

    private static readonly Dictionary<char, string> usTradeCondDict = InitUsTradeCondDict();

    private static readonly Dictionary<char, string> hkTradeCondDict = InitHkTradeCondDict();

    private static Dictionary<string, string> InitPartCodeNameDict()
    {
      Dictionary<string, string> dict = new Dictionary<string, string>();
      dict.Add("a", "NYSE American, LLC (NYSE American)");
      dict.Add("b", "NASDAQ OMX BX, Inc. (NASDAQ OMX BX)");
      dict.Add("c", "NYSE National, Inc. (NYSE National)");
      dict.Add("d", "FINRA Alternative Display Facility (ADF)");
      dict.Add("h", "MIAX Pearl Exchange, LLC (MIAX)");
      dict.Add("i", "International Securities Exchange, LLC (ISE)");
      dict.Add("j", "Cboe EDGA Exchange, Inc. (Cboe EDGA)");
      dict.Add("k", "Cboe EDGX Exchange, Inc. (Cboe EDGX)");
      dict.Add("l", "Long-Term Stock Exchange, Inc. (LTSE)");
      dict.Add("m", "NYSE Chicago, Inc. (NYSE Chicago)");
      dict.Add("n", "New York Stock Exchange, LLC (NYSE)");
      dict.Add("p", "NYSE Arca, Inc. (NYSE Arca)");
      dict.Add("s", "Consolidated Tape System (CTS)");
      dict.Add("t", "NASDAQ Stock Market, LLC (NASDAQ)");
      dict.Add("u", "Members Exchange, LLC (MEMX)");
      dict.Add("v", "Investors’ Exchange, LLC. (IEX)");
      dict.Add("w", "CBOE Stock Exchange, Inc. (CBSX)");
      dict.Add("x", "NASDAQ OMX PSX, Inc. (NASDAQ OMX PSX)");
      dict.Add("y", "Cboe BYX Exchange, Inc. (Cboe BYX)");
      dict.Add("z", "Cboe BZX Exchange, Inc. (Cboe BZX)");
      return dict;
    }

    private static Dictionary<string, string> InitPartCodeShortNameDict()
    {
      Dictionary<string, string> dict = new Dictionary<string, string>();
      dict.Add("a", "AMEX");
      dict.Add("b", "BX");
      dict.Add("c", "NSX");
      dict.Add("d", "ADF");
      dict.Add("h", "MIAX");
      dict.Add("i", "ISE");
      dict.Add("j", "EDGA");
      dict.Add("k", "EDGX");
      dict.Add("l", "LTSE");
      dict.Add("m", "CHO");
      dict.Add("n", "NYSE");
      dict.Add("p", "ARCA");
      dict.Add("s", "CTS");
      dict.Add("t", "NSDQ");
      dict.Add("u", "MEMX");
      dict.Add("v", "IEX");
      dict.Add("w", "CBSX");
      dict.Add("x", "PSX");
      dict.Add("y", "BYX");
      dict.Add("z", "BZX");
      return dict;
    }

    private static Dictionary<char, string> InitUsTradeCondDict()
    {
      Dictionary<char, string> dict = new Dictionary<char, string>();
      dict.Add(' ', "US_REGULAR_SALE"); // 自动对盘
      dict.Add('B', "US_BUNCHED_TRADE");     // 批量交易
      dict.Add('C', "US_CASH_TRADE");        // 现金交易
      dict.Add('F', "US_INTERMARKET_SWEEP"); // 跨市场交易
      dict.Add('G', "US_BUNCHED_SOLD_TRADE");// 批量卖出
      dict.Add('H', "US_PRICE_VARIATION_TRADE");// 离价交易
      dict.Add('I', "US_ODD_LOT_TRADE");        // 碎股交易
      dict.Add('K', "US_RULE_127_OR_155_TRADE");// 纽交所 第127条交易 或 第155条交易
      dict.Add('L', "US_SOLD_LAST");            // 延迟交易
      dict.Add('M', "US_MARKET_CENTER_CLOSE_PRICE");  // 中央收市价
      dict.Add('N', "US_NEXT_DAY_TRADE");             // 隔日交易
      dict.Add('O', "US_MARKET_CENTER_OPENING_TRADE");// 中央开盘价交易
      dict.Add('P', "US_PRIOR_REFERENCE_PRICE");      // 前参考价
      dict.Add('Q', "US_MARKET_CENTER_OPEN_PRICE");   // 中央开盘价
      dict.Add('R', "US_SELLER");                // 卖方
      dict.Add('T', "US_FORM_T");                // 盘前盘后交易
      dict.Add('U', "US_EXTENDED_TRADING_HOURS");// 延长交易时段
      dict.Add('V', "US_CONTINGENT_TRADE");      // 合单交易
      dict.Add('W', "US_AVERAGE_PRICE_TRADE");   // 均价交易
      dict.Add('X', "US_CROSS_TRADE");           //
      dict.Add('Z', "US_SOLD_OUT_OF_SEQUENCE");  // 场外售出
      dict.Add('0', "US_ODD_LOST_CROSS_TRADE");  // 碎股跨市场交易
      dict.Add('4', "US_DERIVATIVELY_PRICED");   // 衍生工具定价
      dict.Add('5', "US_MARKET_CENTER_RE_OPENING_TRADE");// 再开盘定价
      dict.Add('6', "US_MARKET_CENTER_CLOSING_TRADE");   // 收盘定价
      dict.Add('7', "US_QUALIFIED_CONTINGENT_TRADE");    // 合单交易
      dict.Add('9', "US_CONSOLIDATED_LAST_PRICE_PER_LISTING_PACKET");// 综合延迟价格

      return dict;
    }

    private static Dictionary<char, string> InitHkTradeCondDict()
    {
      Dictionary<char, string> dict = new Dictionary<char, string>();
      dict.Add(' ', "HK_AUTOMATCH_NORMAL"); // 自动对盘
      dict.Add('D', "HK_ODD_LOT_TRADE");    // 碎股交易
      dict.Add('U', "HK_AUCTION_TRADE");    // 竞价交易
      dict.Add('*', "HK_OVERSEAS_TRADE");   // 场外交易
      dict.Add('P', "HK_LATE_TRADE_OFF_EXCHG");       // 开市前成交
      dict.Add('M', "HK_NON_DIRECT_OFF_EXCHG_TRADE"); // 非自动对盘
      dict.Add('X', "HK_DIRECT_OFF_EXCHG_TRADE");     // 同券商自动对盘
      dict.Add('Y', "HK_AUTOMATIC_INTERNALIZED");     // 同券商非自动对盘
      return dict;
    }

    private static string GetPartNameByCode(string code)
    {
      if (string.IsNullOrWhiteSpace(code))
      {
        return code;
      }
      PART_CODE_NAME_DICT.TryGetValue(code, out string? name);
      return name ?? code;
    }

    private static string GetPartShortNameByCode(string code)
    {
      if (string.IsNullOrWhiteSpace(code))
      {
        return code;
      }
      PART_CODE_SHORT_NAME_DICT.TryGetValue(code, out string? name);
      return name ?? code;
    }

    private static string GetTradeCondByCode(bool isUsStockSymbol, char code)
    {
      if (code == 0)
      {
        code = ' ';
      }
      string? tradeCond;
      if (isUsStockSymbol)
      {
        usTradeCondDict.TryGetValue(code, out tradeCond);
      }
      else
      {
        hkTradeCondDict.TryGetValue(code, out tradeCond);
      }

      return tradeCond ?? code.ToString();
    }

    public static TradeTick Convert(TradeTickData source)
    {
      string secType = source.SecType;
      if (SecType.FUT.ToString().Equals(secType))
      {
        return ConvertFutureData(source);
      }
      else
      {
        return ConvertStockData(source);
      }
    }

    public static TradeTick ConvertStockData(TradeTickData source)
    {
      TradeTick tradeTick = new TradeTick();

      tradeTick.SecType = SecType.STK;
      tradeTick.Symbol = source.Symbol;
      tradeTick.QuoteLevel = source.QuoteLevel;
      tradeTick.Timestamp = (long)source.Timestamp;
      IList<long> timeList = source.Time;
      IList<long> pricesList = source.Price;
      IList<string> partCodeList = source.PartCode;
      IList<long> volumeList = source.Volume;
      string cond = source.Cond;
      string tickType = source.Type;
      long startSn = source.Sn;
      bool isUsStockSymbol = SymbolUtil.isUsStockSymbol(source.Symbol);

      List<Tick> ticks = new List<Tick>(timeList.Count);
      tradeTick.Ticks = ticks;
      if (timeList != null && timeList.Count > 0)
      {
        long currentTime = 0;
        long priceBase = source.PriceBase;
        double denominator = Math.Pow(10, source.PriceOffset);
        for (int i = 0; i < timeList.Count; i++)
        {
          Tick tickData = new Tick();
          ticks.Add(tickData);

          // recover time: time[i] = time[i] + time[i-1]
          currentTime += timeList[i];
          tickData.Time = currentTime;
          // recover price: price[i] = (priceBase + price[i]) / 10^priceOffset
          tickData.Price = ((priceBase + pricesList[i]) / denominator);

          tickData.Sn = startSn++;
          tickData.Volume = volumeList[i];
          if (i < partCodeList.Count)
          {
            tickData.PartCode = GetPartShortNameByCode(partCodeList[i]);
            tickData.PartName = GetPartNameByCode(partCodeList[i]);
          }

          char condChar = '\0';
          if (cond != null && cond.Length > i)
          {
            condChar = cond[i];
          }
          tickData.Cond = GetTradeCondByCode(isUsStockSymbol, condChar);
          if (tickType != null && tickType.Length > i)
          {
            tickData.TickType = tickType.Substring(i, 1);
          }
        }
      }

      return tradeTick;
    }

    public static TradeTick ConvertFutureData(TradeTickData source)
    {
      TradeTick tradeTick = new TradeTick();

      tradeTick.SecType = SecType.FUT;
      tradeTick.Symbol = source.Symbol;
      tradeTick.Timestamp = (long)source.Timestamp;
      IList<long> timeList = source.Time;
      IList<long> pricesList = source.Price;
      long startSn = source.Sn;
      IList<TradeTickData.Types.MergedVol> mergedVolsArray = source.MergedVols;
      int totalCount = 0;
      foreach (TradeTickData.Types.MergedVol item in mergedVolsArray)
      {
        totalCount += item.MergeTimes;
      }

      List<Tick> ticks = new List<Tick>(totalCount);
      tradeTick.Ticks = ticks;
      if (timeList != null && timeList.Count > 0)
      {
        long currentTime = 0;
        long priceBase = source.PriceBase;
        double denominator = Math.Pow(10, source.PriceOffset);
        for (int i = 0; i < timeList.Count; i++)
        {

          // recover time: time[i] = time[i] + time[i-1]
          currentTime += timeList[i];
          // recover price: price[i] = (priceBase + price[i]) / 10^priceOffset
          double curPrices = (priceBase + pricesList[i]) / denominator;

          IList<long> volsList = mergedVolsArray[i].Vol;
          int mergeTimes = mergedVolsArray[i].MergeTimes;
          for (int j = 0; j < mergeTimes; j++)
          {
            Tick tickData = new Tick();
            ticks.Add(tickData);
            tickData.Sn = startSn * 10 + j;
            tickData.Volume = volsList[j];
            tickData.Time = currentTime;
            tickData.Price = curPrices;
          }
          startSn++;
        }
      }

      return tradeTick;
    }
  }
}

