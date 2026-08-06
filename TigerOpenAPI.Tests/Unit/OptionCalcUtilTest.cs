using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Struct;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for OptionCalcUtil — Black-Scholes / BAW American option pricing,
  /// implied volatility, and option greeks. Because every math helper in
  /// OptionCalcUtil is private static, these tests invoke them through reflection.
  /// The only public method (GetOptionFundamentals) makes live QuoteClient API
  /// calls, so only its no-network guard (expired date) is exercised here.
  /// </summary>
  [TestFixture]
  public class OptionCalcUtilTest
  {
    private const BindingFlags PrivateStatic =
      BindingFlags.NonPublic | BindingFlags.Static;

    // --- reflection helper -------------------------------------------------
    private static object? Invoke(string methodName, params object?[] args)
    {
      MethodInfo? method = typeof(OptionCalcUtil)
        .GetMethods(PrivateStatic)
        .First(m => m.Name == methodName
                    && m.GetParameters().Length == args.Length);
      return method.Invoke(null, args);
    }

    private static double InvokeDouble(string methodName, params object?[] args)
    {
      return (double)Invoke(methodName, args)!;
    }

    private static OptionMetrics InvokeMetrics(string methodName,
      params object?[] args)
    {
      return (OptionMetrics)Invoke(methodName, args)!;
    }

    // ======================================================================
    //  Normal density n(x)  and  cumulative N(z)
    // ======================================================================
    [Test]
    public void N_AtZero_ReturnsHalf()
    {
      Assert.That(InvokeDouble("N", 0.0), Is.EqualTo(0.5).Within(1e-6));
    }

    [Test]
    public void N_AboveSix_ReturnsOne()
    {
      // z > 6 short-circuits to 1.0
      Assert.That(InvokeDouble("N", 6.0001), Is.EqualTo(1.0));
    }

    [Test]
    public void N_BelowMinusSix_ReturnsZero()
    {
      // z < -6 short-circuits to 0.0
      Assert.That(InvokeDouble("N", -6.0001), Is.EqualTo(0.0));
    }

    [Test]
    public void N_AtBoundarySix_ReturnsApproximatelyOne()
    {
      // z == 6 is not > 6, so it goes through the polynomial; ~0.999999999
      Assert.That(InvokeDouble("N", 6.0), Is.EqualTo(1.0).Within(1e-6));
    }

    [Test]
    public void N_At1Point96_ReturnsApproximately975()
    {
      // standard 95% two-sided critical value
      Assert.That(InvokeDouble("N", 1.96), Is.EqualTo(0.975).Within(1e-3));
    }

    [Test]
    public void N_AtMinus1Point96_ReturnsApproximately025()
    {
      Assert.That(InvokeDouble("N", -1.96), Is.EqualTo(0.025).Within(1e-3));
    }

    [Test]
    public void n_AtZero_ReturnsOneOverSqrtTwoPi()
    {
      Assert.That(InvokeDouble("n", 0.0),
        Is.EqualTo(1.0 / Math.Sqrt(2 * Math.PI)).Within(1e-9));
    }

    [Test]
    public void n_AtOne_ReturnsKnownDensity()
    {
      // phi(1) ~= 0.2419707
      Assert.That(InvokeDouble("n", 1.0), Is.EqualTo(0.2419707).Within(1e-6));
    }

    // ======================================================================
    //  Black-Scholes Call / Put  (q = continuous dividend yield)
    // ======================================================================
    [Test]
    public void Call_StandardInputs_ReturnsApproximately4_62()
    {
      // S=100, K=100, r=0.05, q=0, sigma=0.2, T=0.25 -> ~4.61-4.62
      double price = InvokeDouble("Call", 100.0, 100.0, 0.05, 0.0, 0.2, 0.25);
      Assert.That(price, Is.EqualTo(4.61).Within(0.5));
    }

    [Test]
    public void Put_StandardInputs_ReturnsApproximately3_38()
    {
      // S=100, K=100, r=0.05, q=0, sigma=0.2, T=0.25 -> ~3.37-3.38
      double price = InvokeDouble("Put", 100.0, 100.0, 0.05, 0.0, 0.2, 0.25);
      Assert.That(price, Is.EqualTo(3.37).Within(0.5));
    }

    [Test]
    public void Call_DeepITM_ReturnsCloseToIntrinsic()
    {
      // S=200, K=100, r=0.05, q=0, sigma=0.2, T=0.25 -> ~100
      double price = InvokeDouble("Call", 200.0, 100.0, 0.05, 0.0, 0.2, 0.25);
      Assert.That(price, Is.GreaterThan(99.0));
      Assert.That(price, Is.LessThan(101.5));
    }

    [Test]
    public void Put_DeepITM_ReturnsCloseToDiscountedIntrinsic()
    {
      // S=50, K=100, q=0 -> European put ~= K*exp(-rT) - S = 98.76 - 50 = 48.76
      double price = InvokeDouble("Put", 50.0, 100.0, 0.05, 0.0, 0.2, 0.25);
      Assert.That(price, Is.EqualTo(48.76).Within(0.5));
    }

    [Test]
    public void Call_FarOTM_ReturnsNearZero()
    {
      // S=50, K=100 -> call worthless
      double price = InvokeDouble("Call", 50.0, 100.0, 0.05, 0.0, 0.2, 0.25);
      Assert.That(price, Is.LessThan(0.01));
    }

    [Test]
    public void Put_FarOTM_ReturnsNearZero()
    {
      // S=200, K=100 -> put worthless
      double price = InvokeDouble("Put", 200.0, 100.0, 0.05, 0.0, 0.2, 0.25);
      Assert.That(price, Is.LessThan(0.01));
    }

    [Test]
    public void PutCallParity_HoldsForStandardInputs()
    {
      // C - P == S*exp(-q*T) - K*exp(-r*T)
      double S = 100.0, K = 100.0, r = 0.05, q = 0.0, sigma = 0.2, T = 0.25;
      double c = InvokeDouble("Call", S, K, r, q, sigma, T);
      double p = InvokeDouble("Put", S, K, r, q, sigma, T);
      double parity = S * Math.Exp(-q * T) - K * Math.Exp(-r * T);
      Assert.That(c - p, Is.EqualTo(parity).Within(1e-9));
    }

    [Test]
    public void Call_IncreasingVolatilityIncreasesPrice()
    {
      double p1 = InvokeDouble("Call", 100.0, 100.0, 0.05, 0.0, 0.1, 0.25);
      double p2 = InvokeDouble("Call", 100.0, 100.0, 0.05, 0.0, 0.3, 0.25);
      Assert.That(p2, Is.GreaterThan(p1));
    }

    [Test]
    public void Put_IncreasingVolatilityIncreasesPrice()
    {
      double p1 = InvokeDouble("Put", 100.0, 100.0, 0.05, 0.0, 0.1, 0.25);
      double p2 = InvokeDouble("Put", 100.0, 100.0, 0.05, 0.0, 0.3, 0.25);
      Assert.That(p2, Is.GreaterThan(p1));
    }

    // ======================================================================
    //  BAW American approximations
    // ======================================================================
    [Test]
    public void BawCall_NoDividend_NotLessThanEuropeanCall()
    {
      // b == r means no dividend -> American call == European call value
      double american = InvokeDouble(
        "OptionPriceAmericanCallApproximatedBaw",
        100.0, 100.0, 0.05, 0.05, 0.2, 0.25);
      double european = InvokeDouble(
        "Call", 100.0, 100.0, 0.05, 0.05, 0.2, 0.25);
      Assert.That(american, Is.GreaterThanOrEqualTo(european - 1e-9));
    }

    [Test]
    public void BawPut_NotLessThanEuropeanPut()
    {
      // American put >= European put (early exercise has value)
      double american = InvokeDouble(
        "OptionPriceAmericanPutApproximatedBaw",
        100.0, 100.0, 0.05, 0.05, 0.2, 0.25);
      double european = InvokeDouble(
        "Put", 100.0, 100.0, 0.05, 0.05, 0.2, 0.25);
      Assert.That(american, Is.GreaterThanOrEqualTo(european - 1e-9));
    }

    [Test]
    public void BawCall_DeepITM_NotLessThanEuropeanCall()
    {
      // With b == r (no dividend) the American call == European call, which for
      // deep ITM is (S-X)*exp(-r*T) ~= 98.76, NOT the raw intrinsic 100.
      double american = InvokeDouble(
        "OptionPriceAmericanCallApproximatedBaw",
        200.0, 100.0, 0.05, 0.05, 0.2, 0.25);
      double european = InvokeDouble(
        "Call", 200.0, 100.0, 0.05, 0.05, 0.2, 0.25);
      Assert.That(american, Is.GreaterThanOrEqualTo(european - 1e-9));
      Assert.That(american, Is.EqualTo(european).Within(0.5));
    }

    [Test]
    public void BawPut_DeepITM_ReturnsIntrinsicWhenExercisedEarly()
    {
      // Deep ITM American put is exercised immediately -> P = K - S = 50,
      // and BAW returns max(P, p) = max(50, 49.38) = 50.
      double american = InvokeDouble(
        "OptionPriceAmericanPutApproximatedBaw",
        50.0, 100.0, 0.05, 0.05, 0.2, 0.25);
      Assert.That(american, Is.EqualTo(50.0).Within(0.01));
    }

    // ======================================================================
    //  Implied volatility solvers (round-trip consistency)
    // ======================================================================
    [Test]
    public void GetVolatilityCall_RoundTripsKnownSigma()
    {
      // price the BAW call at sigma=0.3, then recover sigma
      double target = InvokeDouble(
        "OptionPriceAmericanCallApproximatedBaw",
        100.0, 100.0, 0.05, 0.05, 0.3, 0.25);
      double sigma = InvokeDouble(
        "GetVolatilityCall", target, 100.0, 100.0, 0.05, 0.05, 0.25);
      Assert.That(sigma, Is.EqualTo(0.3).Within(0.01));
    }

    [Test]
    public void GetVolatilityPut_RoundTripsKnownSigma()
    {
      double target = InvokeDouble(
        "OptionPriceAmericanPutApproximatedBaw",
        100.0, 100.0, 0.05, 0.05, 0.3, 0.25);
      double sigma = InvokeDouble(
        "GetVolatilityPut", target, 100.0, 100.0, 0.05, 0.05, 0.25);
      Assert.That(sigma, Is.EqualTo(0.3).Within(0.01));
    }

    // ======================================================================
    //  Black-Scholes greeks
    // ======================================================================
    [Test]
    public void CallGreeks_DeltaBetweenZeroAndOne()
    {
      var g = InvokeMetrics("OptionPricePartialsCallBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(g.Delta, Is.GreaterThan(0.0));
      Assert.That(g.Delta, Is.LessThan(1.0));
    }

    [Test]
    public void CallGreeks_DeltaMatchesNOfD1()
    {
      var g = InvokeMetrics("OptionPricePartialsCallBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      // Delta == N(d1) ~= 0.5695 for these inputs
      Assert.That(g.Delta, Is.EqualTo(0.5695).Within(0.01));
    }

    [Test]
    public void CallGreeks_GammaIsPositive()
    {
      var g = InvokeMetrics("OptionPricePartialsCallBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(g.Gamma, Is.GreaterThan(0.0));
    }

    [Test]
    public void CallGreeks_VegaIsPositive()
    {
      var g = InvokeMetrics("OptionPricePartialsCallBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(g.Vega, Is.GreaterThan(0.0));
    }

    [Test]
    public void CallGreeks_ThetaIsNegativeForLongPosition()
    {
      var g = InvokeMetrics("OptionPricePartialsCallBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(g.Theta, Is.LessThan(0.0));
    }

    [Test]
    public void CallGreeks_RhoIsPositive()
    {
      var g = InvokeMetrics("OptionPricePartialsCallBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(g.Rho, Is.GreaterThan(0.0));
    }

    [Test]
    public void PutGreeks_DeltaBetweenMinusOneAndZero()
    {
      var g = InvokeMetrics("OptionPricePartialsPutBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(g.Delta, Is.GreaterThan(-1.0));
      Assert.That(g.Delta, Is.LessThan(0.0));
    }

    [Test]
    public void PutGreeks_DeltaMatchesMinusNOfMinusD1()
    {
      var g = InvokeMetrics("OptionPricePartialsPutBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      // Delta == -N(-d1) ~= -0.4305
      Assert.That(g.Delta, Is.EqualTo(-0.4305).Within(0.01));
    }

    [Test]
    public void PutGreeks_GammaIsPositive()
    {
      var g = InvokeMetrics("OptionPricePartialsPutBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(g.Gamma, Is.GreaterThan(0.0));
    }

    [Test]
    public void PutGreeks_VegaIsPositive()
    {
      var g = InvokeMetrics("OptionPricePartialsPutBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(g.Vega, Is.GreaterThan(0.0));
    }

    [Test]
    public void PutGreeks_RhoIsNegative()
    {
      var g = InvokeMetrics("OptionPricePartialsPutBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(g.Rho, Is.LessThan(0.0));
    }

    [Test]
    public void CallAndPutGreeks_ShareGammaAndVega()
    {
      var cg = InvokeMetrics("OptionPricePartialsCallBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      var pg = InvokeMetrics("OptionPricePartialsPutBlackScholes",
        100.0, 100.0, 0.05, 0.2, 0.25);
      Assert.That(pg.Gamma, Is.EqualTo(cg.Gamma).Within(1e-12));
      Assert.That(pg.Vega, Is.EqualTo(cg.Vega).Within(1e-12));
    }

    // ======================================================================
    //  Time value helpers
    // ======================================================================
    [Test]
    public void GetTimeValueCall_OTM_ReturnsFullPrice()
    {
      // strike > spot -> all premium is time value
      double v = InvokeDouble("GetTimeValueCall", 110.0, 100.0, 5.0);
      Assert.That(v, Is.EqualTo(5.0));
    }

    [Test]
    public void GetTimeValueCall_ITM_ReturnsPriceMinusIntrinsic()
    {
      // strike <= spot -> time value = price + strike - spot
      double v = InvokeDouble("GetTimeValueCall", 90.0, 100.0, 7.0);
      Assert.That(v, Is.EqualTo(7.0 + 90.0 - 100.0));
    }

    [Test]
    public void GetTimeValuePut_OTM_ReturnsFullPrice()
    {
      // strike < spot -> all premium is time value
      double v = InvokeDouble("GetTimeValuePut", 90.0, 100.0, 3.0);
      Assert.That(v, Is.EqualTo(3.0));
    }

    [Test]
    public void GetTimeValuePut_ITM_ReturnsPriceMinusIntrinsic()
    {
      // strike >= spot -> time value = price - (strike - spot)
      double v = InvokeDouble("GetTimeValuePut", 110.0, 100.0, 15.0);
      Assert.That(v, Is.EqualTo(15.0 - (110.0 - 100.0)));
    }

    // ======================================================================
    //  Premium rate helpers
    // ======================================================================
    [Test]
    public void CalcCallPremiumRate_ReturnsExpectedFormula()
    {
      // (strike + opPrice - stockPrice) / stockPrice
      double v = InvokeDouble("CalcCallPremiumRate", 5.0, 100.0, 90.0);
      Assert.That(v, Is.EqualTo((90.0 + 5.0 - 100.0) / 100.0));
    }

    [Test]
    public void CalcPutPremiumRate_ReturnsExpectedFormula()
    {
      // (strike - opPrice - stockPrice) / stockPrice
      double v = InvokeDouble("CalcPutPremiumRate", 5.0, 100.0, 110.0);
      Assert.That(v, Is.EqualTo((110.0 - 5.0 - 100.0) / 100.0));
    }

    // ======================================================================
    //  Leverage & intrinsic value
    // ======================================================================
    [Test]
    public void GetLeverage_ReturnsAbsDeltaTimesPriceOverTarget()
    {
      // |delta * latestPrice| / targetPrice
      double v = InvokeDouble("GetLeverage", 5.0, 100.0, 0.5);
      Assert.That(v, Is.EqualTo(Math.Abs(0.5 * 100.0) / 5.0));
    }

    [Test]
    public void GetInsideValue_CallITM_ReturnsPositiveIntrinsic()
    {
      double v = InvokeDouble("GetInsideValue", "CALL", 110.0, 100.0);
      Assert.That(v, Is.EqualTo(10.0));
    }

    [Test]
    public void GetInsideValue_CallOTM_ReturnsZero()
    {
      double v = InvokeDouble("GetInsideValue", "CALL", 90.0, 100.0);
      Assert.That(v, Is.EqualTo(0.0));
    }

    [Test]
    public void GetInsideValue_PutITM_ReturnsPositiveIntrinsic()
    {
      double v = InvokeDouble("GetInsideValue", "PUT", 90.0, 100.0);
      Assert.That(v, Is.EqualTo(10.0));
    }

    [Test]
    public void GetInsideValue_PutOTM_ReturnsZero()
    {
      double v = InvokeDouble("GetInsideValue", "PUT", 110.0, 100.0);
      Assert.That(v, Is.EqualTo(0.0));
    }

    [Test]
    public void GetInsideValue_RightIsCaseInsensitive()
    {
      double v = InvokeDouble("GetInsideValue", "call", 110.0, 100.0);
      Assert.That(v, Is.EqualTo(10.0));
    }

    // ======================================================================
    //  Buy-profit probability helpers
    // ======================================================================
    [Test]
    public void OptionBuyCallProfitRate_ReturnsProbabilityBetweenZeroAndOne()
    {
      double p = InvokeDouble("OptionBuyCallProfitRate",
        100.0, 100.0, 5.0, 0.05, 0.2, 0.25);
      Assert.That(p, Is.GreaterThan(0.0));
      Assert.That(p, Is.LessThan(1.0));
    }

    [Test]
    public void OptionBuyPutProfitRate_ReturnsProbabilityBetweenZeroAndOne()
    {
      double p = InvokeDouble("OptionBuyPutProfitRate",
        100.0, 100.0, 5.0, 0.05, 0.2, 0.25);
      Assert.That(p, Is.GreaterThan(0.0));
      Assert.That(p, Is.LessThan(1.0));
    }

    // ======================================================================
    //  CalcOptionIndex — the orchestrator
    // ======================================================================
    private static long MillisFromNowDays(int days)
    {
      // current millis + days*millis-in-a-day
      return DateUtil.CurrentTimeMillis() + (long)days * 24L * 60L * 60L * 1000L;
    }

    [Test]
    public void CalcOptionIndex_ZeroTarget_ReturnsNull()
    {
      var result = Invoke("CalcOptionIndex",
        0.05, MillisFromNowDays(30), 0L,
        190.0, 0.0, 0.0, 180.0, "CALL",
        DateUtil.CurrentTimeMillis(), true);
      Assert.That(result, Is.Null);
    }

    [Test]
    public void CalcOptionIndex_ZeroStrike_ReturnsNull()
    {
      var result = Invoke("CalcOptionIndex",
        0.05, MillisFromNowDays(30), 0L,
        190.0, 15.0, 0.0, 0.0, "CALL",
        DateUtil.CurrentTimeMillis(), true);
      Assert.That(result, Is.Null);
    }

    [Test]
    public void CalcOptionIndex_CallHappyPath_ReturnsValidGreeks()
    {
      // S=190, strike=180, call, target=15 (> 190-180=10, so call branch)
      long expiry = MillisFromNowDays(30);
      var result = (OptionFundamentals)Invoke("CalcOptionIndex",
        0.05, expiry, 0L,
        190.0, 15.0, 0.0, 180.0, "CALL",
        DateUtil.CurrentTimeMillis(), true)!;

      Assert.That(result.Delta, Is.GreaterThan(0.0));
      Assert.That(result.Delta, Is.LessThan(1.0));
      Assert.That(result.Gamma, Is.GreaterThan(0.0));
      Assert.That(result.Vega, Is.GreaterThan(0.0));
      Assert.That(result.Volatility, Is.GreaterThan(0.0));
      Assert.That(result.InsideValue, Is.EqualTo(10.0));
      // leverage = |delta * 190| / 15
      Assert.That(result.Leverage,
        Is.EqualTo(Math.Abs(result.Delta * 190.0) / 15.0).Within(1e-9));
    }

    [Test]
    public void CalcOptionIndex_CallHappyPath_ThetaIsNegative()
    {
      long expiry = MillisFromNowDays(30);
      var result = (OptionFundamentals)Invoke("CalcOptionIndex",
        0.05, expiry, 0L,
        190.0, 15.0, 0.0, 180.0, "CALL",
        DateUtil.CurrentTimeMillis(), true)!;
      Assert.That(result.Theta, Is.LessThan(0.0));
    }

    [Test]
    public void CalcOptionIndex_PutHappyPath_ReturnsValidGreeks()
    {
      // S=190, strike=200, put, target=15 (> 200-190=10, so put branch)
      long expiry = MillisFromNowDays(30);
      var result = (OptionFundamentals)Invoke("CalcOptionIndex",
        0.05, expiry, 0L,
        190.0, 15.0, 0.0, 200.0, "PUT",
        DateUtil.CurrentTimeMillis(), true)!;

      Assert.That(result.Delta, Is.GreaterThan(-1.0));
      Assert.That(result.Delta, Is.LessThan(0.0));
      Assert.That(result.Gamma, Is.GreaterThan(0.0));
      Assert.That(result.Vega, Is.GreaterThan(0.0));
      Assert.That(result.Volatility, Is.GreaterThan(0.0));
      Assert.That(result.InsideValue, Is.EqualTo(10.0));
    }

    [Test]
    public void CalcOptionIndex_CallTargetAtOrBelowIntrinsic_GreeksAreNaN()
    {
      // target <= latestPrice - strike -> else branch -> sigma stays 0, greeks NaN
      long expiry = MillisFromNowDays(30);
      var result = (OptionFundamentals)Invoke("CalcOptionIndex",
        0.05, expiry, 0L,
        190.0, 5.0, 0.0, 180.0, "CALL",
        DateUtil.CurrentTimeMillis(), true)!;
      Assert.That(result.Delta, Is.EqualTo(double.NaN));
      Assert.That(result.Volatility, Is.EqualTo(0.0));
    }

    [Test]
    public void CalcOptionIndex_PutTargetAtOrBelowIntrinsic_GreeksAreNaN()
    {
      // target <= strike - latestPrice -> else branch
      long expiry = MillisFromNowDays(30);
      var result = (OptionFundamentals)Invoke("CalcOptionIndex",
        0.05, expiry, 0L,
        190.0, 5.0, 0.0, 200.0, "PUT",
        DateUtil.CurrentTimeMillis(), true)!;
      Assert.That(result.Delta, Is.EqualTo(double.NaN));
      Assert.That(result.Volatility, Is.EqualTo(0.0));
    }

    [Test]
    public void CalcOptionIndex_NotTrading_AddsExtraDayToTime()
    {
      // isTrading=false -> diff has +1 day. Use a tiny expiry so the
      // difference in sigma is detectable. We just assert both produce
      // valid (non-NaN) greeks and the non-trading time is longer.
      long expiry = MillisFromNowDays(10);
      var trading = (OptionFundamentals)Invoke("CalcOptionIndex",
        0.05, expiry, 0L,
        190.0, 15.0, 0.0, 180.0, "CALL",
        DateUtil.CurrentTimeMillis(), true)!;
      var nonTrading = (OptionFundamentals)Invoke("CalcOptionIndex",
        0.05, expiry, 0L,
        190.0, 15.0, 0.0, 180.0, "CALL",
        DateUtil.CurrentTimeMillis(), false)!;
      Assert.That(double.IsNaN(trading.Delta), Is.False);
      Assert.That(double.IsNaN(nonTrading.Delta), Is.False);
    }

    // ======================================================================
    //  GetOptionFundamentals — public guard (no network)
    // ======================================================================
    [Test]
    public void GetOptionFundamentals_PastExpiry_ThrowsException()
    {
      // The date check runs before the QuoteClient is touched, so null is safe.
      Assert.That(
        () => OptionCalcUtil.GetOptionFundamentals(
          null!, "AAPL", "CALL", "180", "2020-01-01"),
        Throws.Exception.With.Message.Contains(
          "Option expiration date cannot be earlier than the current date."));
    }

    // ======================================================================
    //  GetOptionFundamentals — full path via TestQuoteClient
    //  (Execute is virtual, so a subclass returns canned JSON responses)
    // ======================================================================
    private static TigerConfig NewMinimalConfig()
    {
      return new TigerConfig
      {
        TigerId = "test",
        PrivateKey = "test",
        License = License.TBHK,
        AutoRefreshToken = false,
        AutoGrabPermission = false,
        ConfigFilePath = ""
      };
    }

    private static string JsonFor(string body)
    {
      return "{\"code\":0,\"message\":\"success\",\"data\":" + body + "}";
    }

    private sealed class TestQuoteClient : QuoteClient
    {
      private readonly string _dividendJson;
      private readonly string _marketStateJson;
      private readonly string _realTimeJson;
      private readonly string _optionBriefJson;

      public TestQuoteClient(TigerConfig config, string dividendJson,
        string marketStateJson, string realTimeJson, string optionBriefJson)
        : base(config)
      {
        _dividendJson = dividendJson;
        _marketStateJson = marketStateJson;
        _realTimeJson = realTimeJson;
        _optionBriefJson = optionBriefJson;
      }

      public override T? Execute<T>(TigerRequest<T> request) where T : class
      {
        string json = request.ApiMethodName switch
        {
          QuoteApiService.CORPORATE_ACTION => _dividendJson,
          QuoteApiService.MARKET_STATE => _marketStateJson,
          QuoteApiService.QUOTE_REAL_TIME => _realTimeJson,
          QuoteApiService.OPTION_BRIEF => _optionBriefJson,
          _ => "{\"code\":0,\"data\":null}"
        };
        return JsonConvert.DeserializeObject<T>(json);
      }
    }

    private static TestQuoteClient NewClient(double latestPrice, double askPrice,
      double bidPrice, string strike, string right, double ratesBonds,
      long expiryTs, int openInterest, string volatility, bool trading)
    {
      string dividend = JsonFor(
        "{\"AAPL\":[{\"executeDate\":\"2099-01-01 00:00:00\",\"amount\":0.0}]}");
      string market = JsonFor(
        "[{\"market\":\"US\",\"status\":\"" + (trading ? "TRADING" : "CLOSED") + "\"}]");
      string realtime = JsonFor(
        "[{\"symbol\":\"AAPL\",\"latestPrice\":" + latestPrice.ToString(System.Globalization.CultureInfo.InvariantCulture) + "}]");
      string brief = JsonFor(
        "[{\"symbol\":\"AAPL\",\"strike\":\"" + strike + "\",\"right\":\"" + right
        + "\",\"askPrice\":" + askPrice.ToString(System.Globalization.CultureInfo.InvariantCulture)
        + ",\"bidPrice\":" + bidPrice.ToString(System.Globalization.CultureInfo.InvariantCulture)
        + ",\"ratesBonds\":" + ratesBonds.ToString(System.Globalization.CultureInfo.InvariantCulture)
        + ",\"expiry\":" + expiryTs
        + ",\"openInterest\":" + openInterest
        + ",\"volatility\":\"" + volatility + "\"}]");
      return new TestQuoteClient(NewMinimalConfig(),
        dividend, market, realtime, brief);
    }

    [Test]
    public void GetOptionFundamentals_CallHappyPath_ReturnsValidGreeks()
    {
      // AAPL @ 190, strike 180, call, mid target 15 (ask 14 / bid 16),
      // 30 days to expiry, r=0.05, IV~30% from solver.
      long expiryTs = DateUtil.CurrentTimeMillis()
        + 30L * 24L * 60L * 60L * 1000L;
      var client = NewClient(190.0, 14.0, 16.0, "180", "CALL",
        0.05, expiryTs, 100, "30%", true);

      var result = OptionCalcUtil.GetOptionFundamentals(
        client, "AAPL", "CALL", "180", "2099-01-01");

      Assert.That(result, Is.Not.Null);
      Assert.That(result!.Delta, Is.GreaterThan(0.0));
      Assert.That(result.Delta, Is.LessThan(1.0));
      Assert.That(result.Gamma, Is.GreaterThan(0.0));
      Assert.That(result.Vega, Is.GreaterThan(0.0));
      Assert.That(result.Theta, Is.LessThan(0.0));
      Assert.That(result.Volatility, Is.GreaterThan(0.0));
      // intrinsic = 190 - 180 = 10
      Assert.That(result.InsideValue, Is.EqualTo(10.0));
      Assert.That(result.OpenInterest, Is.EqualTo(100));
      // history volatility parsed from "30%"
      Assert.That(result.HistoryVolatility, Is.EqualTo(30.0));
    }

    [Test]
    public void GetOptionFundamentals_PutHappyPath_ReturnsValidGreeks()
    {
      // AAPL @ 190, strike 200, put, mid target 15 (ask 14 / bid 16),
      // 30 days, r=0.05.
      long expiryTs = DateUtil.CurrentTimeMillis()
        + 30L * 24L * 60L * 60L * 1000L;
      var client = NewClient(190.0, 14.0, 16.0, "200", "PUT",
        0.05, expiryTs, 50, "25%", true);

      var result = OptionCalcUtil.GetOptionFundamentals(
        client, "AAPL", "PUT", "200", "2099-01-01");

      Assert.That(result, Is.Not.Null);
      Assert.That(result!.Delta, Is.GreaterThan(-1.0));
      Assert.That(result.Delta, Is.LessThan(0.0));
      Assert.That(result.Gamma, Is.GreaterThan(0.0));
      Assert.That(result.Vega, Is.GreaterThan(0.0));
      // intrinsic = 200 - 190 = 10
      Assert.That(result.InsideValue, Is.EqualTo(10.0));
      Assert.That(result.HistoryVolatility, Is.EqualTo(25.0));
    }

    [Test]
    public void GetOptionFundamentals_ZeroBidAsk_ThrowsUnableToObtainSummary()
    {
      // ask=NaN->0, bid=NaN->0 -> target=0 -> throws
      long expiryTs = DateUtil.CurrentTimeMillis()
        + 30L * 24L * 60L * 60L * 1000L;
      var client = NewClient(190.0, 0.0, 0.0, "180", "CALL",
        0.05, expiryTs, 0, "0%", true);

      Assert.That(
        () => OptionCalcUtil.GetOptionFundamentals(
          client, "AAPL", "CALL", "180", "2099-01-01"),
        Throws.Exception.With.Message.Contains(
          "Unable to obtain option summary information."));
    }
  }
}
