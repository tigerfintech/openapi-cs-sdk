using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Struct;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Common.Util
{
  public class OptionCalcUtil
  {
    private OptionCalcUtil() { }

    private const int TIME_MILLIS_IN_ONE_HOUR = 60 * 60 * 1000;
    private const double ACCURACY = 1.0e-6;
    private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private static TaskFactory taskFactory = new TaskFactory(cancellationTokenSource.Token);

    private static double n(double x)
    {
      return (1 / Math.Sqrt(2 * Math.PI)) * Math.Exp(-x * x / 2);
    }

    private static double N(double z)
    {
      if (z > 6.0)
      {
        return 1.0;
      }
      if (z < -6.0)
      {
        return 0.0;
      }

      double b1 = 0.31938153;
      double b2 = -0.356563782;
      double b3 = 1.781477937;
      double b4 = -1.821255978;
      double b5 = 1.330274429;
      double p = 0.2316419;
      double c2 = 0.3989423;

      double a = Math.Abs(z);
      double t = 1.0 / (1.0 + a * p);
      double b = c2 * Math.Exp((-z) * (z / 2.0));
      double n = ((((b5 * t + b4) * t + b3) * t + b2) * t + b1) * t;
      n = 1.0 - b * n;
      if (z < 0.0)
      {
        n = 1.0 - n;
      }
      return n;
    }

    private static double Call(double S, double X, double r, double q, double sigma, double time)
    {
      double sigma_sqr = Math.Pow(sigma, 2);
      double time_sqrt = Math.Sqrt(time);
      double d1 = (Math.Log(S / X) + (r - q + 0.5 * sigma_sqr) * time) / (sigma * time_sqrt);
      double d2 = d1 - (sigma * time_sqrt);
      return S * Math.Exp(-q * time) * N(d1) - X * Math.Exp(-r * time) * N(d2);
    }

    private static double Put(double S, double K, double r, double q, double sigma, double time)
    {
      double sigma_sqr = Math.Pow(sigma, 2);
      double time_sqrt = Math.Sqrt(time);
      double d1 = (Math.Log(S / K) + (r - q + 0.5 * sigma_sqr) * time) / (sigma * time_sqrt);
      double d2 = d1 - (sigma * time_sqrt);
      return K * Math.Exp(-r * time) * N(-d2) - S * Math.Exp(-q * time) * N(-d1);
    }

    private static double OptionPriceAmericanCallApproximatedBaw(double S, double X, double r, double b, double sigma,
        double time)
    {
      double sigma_sqr = sigma * sigma;
      double time_sqrt = Math.Sqrt(time);
      double nn = 2.0 * b / sigma_sqr;
      double m = 2.0 * r / sigma_sqr;
      double K = 1.0 - Math.Exp(-r * time);
      double q2 = (-(nn - 1) + Math.Sqrt(Math.Pow((nn - 1), 2.0) + (4 * m / K))) * 0.5;

      // seed value from paper
      double q2_inf = 0.5 * (-(nn - 1.0) + Math.Sqrt(Math.Pow((nn - 1), 2.0) + 4.0 * m));
      double S_star_inf = X / (1.0 - 1.0 / q2_inf);
      double h2 = -(b * time + 2.0 * sigma * time_sqrt) * (X / (S_star_inf - X));
      double S_seed = X + (S_star_inf - X) * (1.0 - Math.Exp(h2));

      int no_iterations = 0; // iterate on S to find S_star, using Newton steps
      double Si = S_seed;
      double g = 1;
      double gprime = 1.0;
      while ((Math.Abs(g) > ACCURACY) && (Math.Abs(gprime) > ACCURACY) // to avoid exploding Newton's
          && (no_iterations++ < 500) && (Si > 0.0) && !Double.IsNaN(Si))
      {
        double cc = Call(Si, X, r, b, sigma, time);
        double d1 = (Math.Log(Si / X) + (b + 0.5 * sigma_sqr) * time) / (sigma * time_sqrt);
        g = (1.0 - 1.0 / q2) * Si - X - cc + (1.0 / q2) * Si * Math.Exp((b - r) * time) * N(d1);
        gprime = (1.0 - 1.0 / q2) * (1.0 - Math.Exp((b - r) * time) * N(d1)) +
            (1.0 / q2) * Math.Exp((b - r) * time) * n(d1) * (1.0 / (sigma * time_sqrt));
        Si = Si - (g / gprime);
      }
      double S_star = 0;
      if (Math.Abs(g) > ACCURACY)
      {
        S_star = S_seed;
      } // did not converge
      else
      {
        S_star = Si;
      }
      double C = 0;
      double c = Call(S, X, r, b, sigma, time);
      if (S >= S_star)
      {
        C = S - X;
      }
      else
      {
        double d1 = (Math.Log(S_star / X) + (b + 0.5 * sigma_sqr) * time) / (sigma * time_sqrt);
        double A2 = (1.0 - Math.Exp((b - r) * time) * N(d1)) * (S_star / q2);
        C = c + A2 * Math.Pow((S / S_star), q2);
      }
      return Double.IsNaN(C) ? c : Math.Max(C, c); // know value will never be less than BS value
    }

    private static double OptionPriceAmericanPutApproximatedBaw(double S, double X, double r, double b, double sigma,
        double time)
    {
      double sigma_sqr = sigma * sigma;
      double time_sqrt = Math.Sqrt(time);
      double M = 2.0 * r / sigma_sqr;
      double NN = 2.0 * b / sigma_sqr;
      double K = 1.0 - Math.Exp(-r * time);
      double q1 = 0.5 * (-(NN - 1) - Math.Sqrt(Math.Pow((NN - 1), 2.0) + (4.0 * M / K)));

      // now find initial S value
      double q1_inf = 0.5 * (-(NN - 1) - Math.Sqrt(Math.Pow((NN - 1), 2.0) + 4.0 * M));
      double S_star_star_inf = X / (1.0 - 1.0 / q1_inf);
      double h1 = (b * time - 2 * sigma * time_sqrt) * (X / (X - S_star_star_inf));
      double S_seed = S_star_star_inf + (X - S_star_star_inf) * Math.Exp(h1);

      double Si = S_seed;  // now do Newton iterations to solve for S**
      int no_iterations = 0;
      double g = 1;
      double gprime = 1;
      while ((Math.Abs(g) > ACCURACY) && (Math.Abs(gprime) > ACCURACY) // to avoid non-convergence
          && (no_iterations++ < 500) && Si > 0.0 && !Double.IsNaN(Si))
      {
        double pp = Put(Si, X, r, b, sigma, time);
        double d1 = (Math.Log(Si / X) + (b + 0.5 * sigma_sqr) * time) / (sigma * time_sqrt);
        g = X - Si - pp + (1 - Math.Exp((b - r) * time) * N(-d1)) * Si / q1;
        gprime = (1.0 / q1 - 1.0) * (1.0 - Math.Exp((b - r) * time) * N(-d1)) +
            (1.0 / q1) * Math.Exp((b - r) * time) * (1.0 / (sigma * time_sqrt)) * n(-d1);
        Si = Si - (g / gprime);
      }
      double S_star_star = Si;
      if (g > ACCURACY)
      {
        S_star_star = S_seed;
      }
      double P = 0;
      double p = Put(S, X, r, b, sigma, time);
      if (S > S_star_star)
      {
        double d1 = (Math.Log(S_star_star / X) + (b + 0.5 * sigma_sqr) * time) / (sigma * time_sqrt);
        double A1 = -(S_star_star / q1) * (1 - Math.Exp((b - r) * time) * N(-d1));
        P = p + A1 * Math.Pow((S / S_star_star), q1);
      }
      else
      {
        P = X - S;
      }
      return Math.Max(P, p);  // should not be lower than Black Scholes value
    }

    /**
     * Buy Profit Probability
     */
    private static double OptionBuyCallProfitRate(double S, double K, double p, double r, double sigma, double time)
    {
      double time_sqrt = Math.Sqrt(time);
      double d1 = Math.Log(S / (K + p)) + r * time;
      double d2 = sigma * time_sqrt;
      return N(d1 / d2);
    }

    /**
     * Buy Profit Probability
     */
    private static double OptionBuyPutProfitRate(double S, double K, double p, double r, double sigma, double time)
    {
      double time_sqrt = Math.Sqrt(time);
      double d1 = Math.Log(S / (K - p)) + r * time;
      double d2 = sigma * time_sqrt;
      double result = N(d1 / d2);
      return 1 - result;
    }

    /**
     * 求call的隐含波动率
     *
     * @param target 目标价
     * @param S 股票 价格
     * @param X 行权价格
     * @param r 国债利率
     * @param b =r
     * @param time 时间，以年为单位半年是0.5
     */
    private static double GetVolatilityCall(double target, double S, double X, double r, double b, double time)
    {
      double min = 0;
      double max = 2.5;
      double sig = min;
      double x = 1000;
      int maxCount = 100;
      while (Math.Abs(x - target) > 0.001 && maxCount-- > 0)
      {
        sig = (min + max) / 2;
        x = OptionPriceAmericanCallApproximatedBaw(S, X, r, b, sig, time);
        if (x < target)
        {
          min = sig;
        }
        else if (x > target)
        {
          max = sig;
        }
      }
      return sig;
    }

    private static double GetTimeValueCall(double strike, double spot, double price)
    {
      double res = 0;
      if (strike > spot)
      {
        res = price;
      }
      else
      {
        res = price + strike - spot;
      }
      return res;
    }

    private static double GetTimeValuePut(double strike, double spot, double price)
    {
      double res = 0;
      if (strike < spot)
      {
        res = price;
      }
      else
      {
        res = price - (strike - spot);
      }
      return res;
    }

    /**
     * 求put的隐含波动率
     *
     * @param target 目标价
     * @param S 股票 价格
     * @param X 行权价格
     * @param r 国债利率
     * @param b =r
     * @param time 时间，以年为单位半年是0.5
     */
    private static double GetVolatilityPut(double target, double S, double X, double r, double b, double time)
    {
      double min = 0;
      double max = 2.5;
      double sig = min;
      double x = 1000;
      int maxCount = 100;
      while (Math.Abs(x - target) > 0.001 && maxCount-- > 0)
      {
        sig = (min + max) / 2;
        x = OptionPriceAmericanPutApproximatedBaw(S, X, r, b, sig, time);
        if (x < target)
        {
          min = sig;
        }
        else if (x > target)
        {
          max = sig;
        }
      }
      return sig;
    }

    private static OptionMetrics OptionPricePartialsCallBlackScholes(double S,     // spot price
                                                                     double K,     // Strike (exercise) price,
                                                                     double r,     // interest rate
                                                                     double sigma, // volatility
                                                                     double time)  // partial wrt r
    {
      double time_sqrt = Math.Sqrt(time);
      double d1 = (Math.Log(S / K) + r * time) / (sigma * time_sqrt) + 0.5 * sigma * time_sqrt;
      double d2 = d1 - (sigma * time_sqrt);
      double Delta = N(d1);
      double Gamma = n(d1) / (S * sigma * time_sqrt);
      double Theta = (-(S * sigma * n(d1)) / (2 * time_sqrt) - r * K * Math.Exp(-r * time) * N(d2)) / 365f;
      double Vega = S * time_sqrt * n(d1) / 100f;
      double Rho = K * time * Math.Exp(-r * time) * N(d2);
      return new OptionMetrics(Delta, Gamma, Theta, Vega, Rho);
    }

    private static OptionMetrics OptionPricePartialsPutBlackScholes(double S, // spot price
                                                                    double K, // Strike (exercise) price,
                                                                    double r,  // interest rate
                                                                    double sigma,
                                                                    double time)    // partial wrt r
    {
      double time_sqrt = Math.Sqrt(time);
      double d1 = (Math.Log(S / K) + r * time) / (sigma * time_sqrt) + 0.5 * sigma * time_sqrt;
      double d2 = d1 - (sigma * time_sqrt);
      double Delta = -N(-d1);
      double Gamma = n(d1) / (S * sigma * time_sqrt);
      double Theta = (-(S * sigma * n(d1)) / (2 * time_sqrt) + r * K * Math.Exp(-r * time) * N(-d2)) / 365f;
      double Vega = S * time_sqrt * n(d1) / 100f;
      double Rho = -K * time * Math.Exp(-r * time) * N(-d2);
      return new OptionMetrics(Delta, Gamma, Theta, Vega, Rho);
    }

    /**
     * 溢价率
     */
    private static double CalcPutPremiumRate(double opPrice, double stockPrice, double strike)
    {
      return (strike - opPrice - stockPrice) / stockPrice;
    }

    /**
     * 溢价率
     */
    private static double CalcCallPremiumRate(double opPrice, double stockPrice, double strike)
    {
      return (strike + opPrice - stockPrice) / stockPrice;
    }

    /**
     * 杠杆率
     */
    private static double GetLeverage(double targetPrice, double latestPrice, double delta)
    {
      return Math.Abs(delta * latestPrice) / targetPrice;
    }

    /**
     * 内在价值
     */
    private static double GetInsideValue(String right, double latestPrice, double strike)
    {
      double value = Right.CALL.ToString().Equals(right, StringComparison.CurrentCultureIgnoreCase) ? latestPrice - strike : strike - latestPrice;
      if (value < 0)
      {
        value = 0;
      }
      return value;
    }

    /**
     * 计算期权基本面信息
     *
     * @param r 国债利率
     * @param expiryLong 过期日（long类型）
     * @param executeDateLong 除权除息日（long类型），与expiryLong保持时区一致
     * @param latestPrice 最新正股价格
     * @param targetPrice 期权目标价格
     * @param dividendAmount 分红
     * @param strike 行权价
     * @param type CALL or PUT
     * @param currentTime 当前时间（long类型），与expiryLong保持时区一致
     * @param isTrading 是否在正股交易时间
     */
    private static OptionFundamentals? CalcOptionIndex(double r, long expiryLong, long executeDateLong,
        double latestPrice, double targetPrice, double dividendAmount, double strike, String type, long currentTime,
        bool isTrading)
    {

      OptionFundamentals result = new OptionFundamentals();
      double diff = ((expiryLong - currentTime) /
          (24.0f * TIME_MILLIS_IN_ONE_HOUR) + 1 + (isTrading ? 0 : 1)) / 365.0f;
      bool needDividend =
          executeDateLong != 0 && expiryLong > executeDateLong && currentTime < executeDateLong;
      latestPrice = latestPrice - (needDividend ? dividendAmount : 0);

      if (targetPrice == 0 || strike == 0)
      {
        return null;
      }
      double sigma = 0;
      OptionMetrics optionMetrics;
      if (Right.PUT.ToString().Equals(type, StringComparison.CurrentCultureIgnoreCase) && targetPrice > strike - latestPrice)
      {
        result.TimeValue = GetTimeValuePut(strike, latestPrice, targetPrice);
        sigma = GetVolatilityPut(targetPrice, latestPrice, strike, r, r, diff);
        optionMetrics = OptionPricePartialsPutBlackScholes(latestPrice, strike, r, sigma, diff);
        result.PremiumRate = CalcPutPremiumRate(targetPrice, latestPrice, strike) * 100;
        result.ProfitRate = OptionBuyPutProfitRate(latestPrice, strike, targetPrice, r, sigma, diff) * 100;
      }
      else if (Right.CALL.ToString().Equals(type, StringComparison.CurrentCultureIgnoreCase) && targetPrice > latestPrice - strike)
      {
        result.TimeValue = GetTimeValueCall(strike, latestPrice, targetPrice);
        sigma = GetVolatilityCall(targetPrice, latestPrice, strike, r, r, diff);
        result.PremiumRate = CalcCallPremiumRate(targetPrice, latestPrice, strike) * 100;
        result.ProfitRate = OptionBuyCallProfitRate(latestPrice, strike, targetPrice, r, sigma, diff) * 100;
        optionMetrics = OptionPricePartialsCallBlackScholes(latestPrice, strike, r, sigma, diff);
      }
      else
      {
        optionMetrics = new OptionMetrics(Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN);
      }
      result.Delta = optionMetrics.Delta;
      result.Gamma = optionMetrics.Gamma;
      result.Theta = optionMetrics.Theta;
      result.Vega = optionMetrics.Vega;
      result.Rho = optionMetrics.Rho;
      result.Volatility = sigma * 100;
      result.Leverage = GetLeverage(targetPrice, latestPrice, result.Delta);
      result.InsideValue = GetInsideValue(type, latestPrice, strike);
      return result;
    }

    /**
     * Get option fundamental information（include option greek values）
     *
     * @param client QuoteClient
     * @param symbol Stock code
     * @param right CALL or PUT
     * @param strike strike price
     * @param expiry Expiration date（yyyy-MM-dd）
     * @return option fundamental information
     * @throws Exception runtime exception
     */
    public static OptionFundamentals? GetOptionFundamentals(QuoteClient client,
      string symbol, string right, string strike, string expiry)
    {
      return GetOptionFundamentals(client, symbol, right, strike, expiry, null);
    }

    /**
     * Get option fundamental information（include option greek values）
     *
     * @param client QuoteClient
     * @param symbol Stock code
     * @param right CALL or PUT
     * @param strike strike price
     * @param expiry Expiration date（yyyy-MM-dd）
     * @param underlyingSymbol underlying symbol（if null, If empty, defaults to the same value as 'symbol'）
     * @return option fundamental information
     * @throws Exception runtime exception
     */
    public static OptionFundamentals? GetOptionFundamentals(QuoteClient client,
      string symbol, string right, string strike, string expiry, string? underlyingSymbol)
    {
      TimeZoneInfo timeZoneInfo = CustomTimeZone.NY_ZONE;
      if (DateUtil.IsDateBeforeToday(expiry, timeZoneInfo))
      {
        throw new Exception("Option expiration date cannot be earlier than the current date.");
      }
      if (string.IsNullOrWhiteSpace(underlyingSymbol))
      {
        underlyingSymbol = symbol;
      }

      Task<CorporateDividendItem> dividendTask = GetCorporateDividendTask(client, underlyingSymbol);
      Task<Boolean> marketStateTask = GetMarketStateTask(client);
      Task<Double> latestPriceTask = GetLatestPriceTask(client, underlyingSymbol);
      Task<OptionBriefItem> optionBriefTask = GetOptionBriefTask(client, symbol,
        right, strike, DateUtil.ConvertTimestamp(expiry, timeZoneInfo));

      OptionBriefItem optionBriefItem = optionBriefTask.Result;
      if (Double.IsNaN(optionBriefItem.AskPrice))
      {
        optionBriefItem.AskPrice = 0D;
      }
      if (Double.IsNaN(optionBriefItem.BidPrice))
      {
        optionBriefItem.BidPrice = 0D;
      }
      double target = (optionBriefItem.AskPrice + optionBriefItem.BidPrice) / 2;
      if (optionBriefItem.Strike is null || target <= 0)
      {
        throw new Exception("Unable to obtain option summary information.");
      }

      OptionFundamentals? result = CalcOptionIndex(optionBriefItem.RatesBonds,
        optionBriefItem.Expiry,
        DateUtil.ConvertTimestamp(dividendTask.Result.ExecuteDate, timeZoneInfo),
        latestPriceTask.Result,
        target,
        dividendTask.Result.Amount,
        double.Parse(optionBriefItem.Strike),
        optionBriefItem.Right,
        DateUtil.CurrentTimeMillis(),
        marketStateTask.Result
      );
      if (result is null)
      {
        return result;
      }

      result.OpenInterest = optionBriefItem.OpenInterest;
      string? volatility = null;
      if (optionBriefItem.Volatility != null && optionBriefItem.Volatility.Contains("%"))
      {
        volatility = optionBriefItem.Volatility.Replace("%", "");
      }
      double historyVolatility = string.IsNullOrEmpty(volatility) ? 0.0 : double.Parse(volatility);
      result.HistoryVolatility = historyVolatility;
      return result;
    }

    private static async Task<CorporateDividendItem> GetCorporateDividendTask(QuoteClient client, string symbol)
    {
      return await taskFactory.StartNew(() =>
      {
        List<string> symbols = new List<string>();
        symbols.Add(symbol);
        TimeZoneInfo timeZone = CustomTimeZone.HK_ZONE;
        Int64 date = DateUtil.ConvertTimestamp(DateUtil.PrintDate(DateUtil.CurrentTimeMillis(), timeZone), timeZone);

        TigerRequest<CorporateDividendResponse> request = new TigerRequest<CorporateDividendResponse>()
        {
          ApiMethodName = QuoteApiService.CORPORATE_ACTION,
          ModelValue = new CorporateActionModel()
          {
            ActionType = CorporateActionType.DIVIDEND,
            Symbols = symbols,
            Market = Market.US,
            BeginDate = date,
            EndDate = date
          }
        };
        CorporateDividendResponse? corporateDividendResponse = client.Execute(request);
        if (corporateDividendResponse != null && corporateDividendResponse.IsSuccess())
        {
          List<CorporateDividendItem>? corporateDividendItems;
          if (corporateDividendResponse.Data.TryGetValue(symbol, out corporateDividendItems)
            && corporateDividendItems != null && corporateDividendItems.Count > 0)
          {
            return corporateDividendItems[0];
          }
        }
        CorporateDividendItem item = new CorporateDividendItem();
        item.Amount = 0D;
        return item;
      });
    }

    private static async Task<Boolean> GetMarketStateTask(QuoteClient client)
    {
      return await taskFactory.StartNew(() =>
      {
        TigerRequest<MarketStateResponse> request = new TigerRequest<MarketStateResponse>()
        {
          ApiMethodName = QuoteApiService.MARKET_STATE,
          ModelValue = new QuoteMarketModel() { Market = Market.US, Lang = Language.en_US }
        };
        MarketStateResponse? marketStateResponse = client.Execute(request);
        if (marketStateResponse != null && marketStateResponse.IsSuccess())
        {
          List<MarketState> marketItems = marketStateResponse.Data;
          if (marketItems != null && marketItems.Count > 0)
          {
            return "TRADING".Equals(marketItems[0].Status);
          }
        }
        return false;
      });
    }

    private static async Task<Double> GetLatestPriceTask(QuoteClient client, string symbol)
    {
      return await taskFactory.StartNew(() =>
      {
        TigerRequest<QuoteRealTimeQuoteResponse> request = new TigerRequest<QuoteRealTimeQuoteResponse>()
        {
          ApiMethodName = QuoteApiService.QUOTE_REAL_TIME,
          ModelValue = new QuoteSymbolModel()
          {
            Symbols = new List<string> { symbol },
            IncludeHourTrading = false
          }
        };
        QuoteRealTimeQuoteResponse? quoteRealTimeQuoteResponse = client.Execute(request);
        if (quoteRealTimeQuoteResponse != null && quoteRealTimeQuoteResponse.IsSuccess())
        {
          List<RealTimeQuoteItem> realTimeQuoteItems = quoteRealTimeQuoteResponse.Data;
          if (realTimeQuoteItems != null && realTimeQuoteItems.Count > 0 && realTimeQuoteItems[0] is not null)
          {
            return realTimeQuoteItems[0].LatestPrice;
          }
        }
        if (quoteRealTimeQuoteResponse == null)
        {
          throw new Exception("Failed to get the latest price of the stock.");
        }
        throw new Exception("Get realtime-quotes return error description: " + quoteRealTimeQuoteResponse.Message);
      });
    }

    private static async Task<OptionBriefItem> GetOptionBriefTask(QuoteClient client,
      string symbol, string right, string strike, long expiryDate)
    {
      return await taskFactory.StartNew(() =>
      {
        TigerRequest<OptionBriefResponse> request = new TigerRequest<OptionBriefResponse>()
        {
          ApiMethodName = QuoteApiService.OPTION_BRIEF,
          ModelValue = new BatchApiModel<OptionCommonModel>()
          {
            Items = new List<OptionCommonModel>()
            {
              new OptionCommonModel()
              {
                Symbol = symbol,
                Right = right,
                Strike = strike,
                Expiry = expiryDate
              }
            }
          }
        };
        OptionBriefResponse? optionBriefResponse = client.Execute(request);
        List<OptionBriefItem> briefItems;
        if (optionBriefResponse != null && optionBriefResponse.IsSuccess())
        {
          briefItems = optionBriefResponse.Data;
          if (briefItems != null && briefItems.Count > 0)
          {
            return briefItems[0];
          }
        }
        if (optionBriefResponse == null)
        {
          throw new Exception("Unable to obtain option summary info.");
        }
        throw new Exception("Obtain option brief summary info return error description: " + optionBriefResponse.Message);
      });
    }
  }
}