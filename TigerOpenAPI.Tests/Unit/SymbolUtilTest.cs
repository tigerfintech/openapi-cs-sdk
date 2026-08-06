using NUnit.Framework;
using static TigerOpenAPI.Common.CustomTimeZone;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies the option/stock/future symbol helpers in <see cref="SymbolUtil"/>.
  /// </summary>
  [TestFixture]
  public class SymbolUtilTest
  {
    /// <summary>convertToOptionSymbol parses an OCC option identifier into its components.</summary>
    [Test]
    public void ConvertToOptionSymbol_ValidIdentifier_ParsesComponents()
    {
      // "AAPL  240119P00160000" = symbol(6, padded) + expiry(YYMMDD) + right(P/C) + strike(8 digits)
      var optionSymbol = SymbolUtil.convertToOptionSymbol("AAPL  240119P00160000");

      Assert.That(optionSymbol, Is.Not.Null);
      Assert.That(optionSymbol.Symbol, Is.EqualTo("AAPL"));
      Assert.That(optionSymbol.Expiry, Is.EqualTo("2024-01-19"));
      Assert.That(optionSymbol.Right, Is.EqualTo("PUT"));
      // Int32.Parse strips leading zeros from the 5-digit strike, so "00160" -> "160".
      Assert.That(optionSymbol.Strike, Is.EqualTo("160.000"));
    }

    /// <summary>convertToOptionSymbol parses a CALL identifier.</summary>
    [Test]
    public void ConvertToOptionSymbol_CallIdentifier_ParsesRightAsCall()
    {
      var optionSymbol = SymbolUtil.convertToOptionSymbol("AAPL  240119C00160000");

      Assert.That(optionSymbol.Right, Is.EqualTo("CALL"));
      Assert.That(optionSymbol.Symbol, Is.EqualTo("AAPL"));
      Assert.That(optionSymbol.Expiry, Is.EqualTo("2024-01-19"));
    }

    /// <summary>convertToOptionSymbol throws when the identifier is too short.</summary>
    [Test]
    public void ConvertToOptionSymbol_ShortIdentifier_ThrowsTigerApiException()
    {
      TigerApiException ex = Assert.Throws<TigerApiException>(
        () => SymbolUtil.convertToOptionSymbol("AAPL"));

      Assert.That(ex.ErrMsg, Is.EqualTo("option identifier format error"));
    }

    /// <summary>convertToOptionSymbol throws on null/empty input.</summary>
    [Test]
    public void ConvertToOptionSymbol_NullIdentifier_ThrowsTigerApiException()
    {
      Assert.Throws<TigerApiException>(() => SymbolUtil.convertToOptionSymbol(null!));
      Assert.Throws<TigerApiException>(() => SymbolUtil.convertToOptionSymbol(""));
    }

    /// <summary>isUsStockSymbol returns true for an all-uppercase US ticker.</summary>
    [Test]
    public void IsUsStockSymbol_AaplSymbol_ReturnsTrue()
    {
      Assert.That(SymbolUtil.isUsStockSymbol("AAPL"), Is.True);
    }

    /// <summary>isUsStockSymbol returns true for a Hong Kong ticker.
    /// NOTE: the source regex <c>[A-Z]+(.[A-Z0-9]+)?</c> is unanchored, so it
    /// matches the "HK" substring inside "0700.HK". This documents actual
    /// behavior; anchoring the pattern with ^...$ would make it return false.</summary>
    [Test]
    public void IsUsStockSymbol_HkSymbol_UnanchoredRegexReturnsTrue()
    {
      Assert.That(SymbolUtil.isUsStockSymbol("0700.HK"), Is.True);
    }

    /// <summary>isUsStockSymbol returns false for null/empty input.</summary>
    [Test]
    public void IsUsStockSymbol_NullOrEmpty_ReturnsFalse()
    {
      Assert.That(SymbolUtil.isUsStockSymbol(null!), Is.False);
      Assert.That(SymbolUtil.isUsStockSymbol(""), Is.False);
      Assert.That(SymbolUtil.isUsStockSymbol("   "), Is.False);
    }

    /// <summary>IsHkOptionSymbol returns true for a Hong Kong ticker.</summary>
    [Test]
    public void IsHkOptionSymbol_HkSymbol_ReturnsTrue()
    {
      Assert.That(SymbolUtil.IsHkOptionSymbol("0700.HK"), Is.True);
    }

    /// <summary>IsHkOptionSymbol returns false for a US ticker.</summary>
    [Test]
    public void IsHkOptionSymbol_UsSymbol_ReturnsFalse()
    {
      Assert.That(SymbolUtil.IsHkOptionSymbol("AAPL"), Is.False);
    }

    /// <summary>IsHkOptionSymbol returns false for null/empty input.</summary>
    [Test]
    public void IsHkOptionSymbol_NullOrEmpty_ReturnsFalse()
    {
      Assert.That(SymbolUtil.IsHkOptionSymbol(null!), Is.False);
      Assert.That(SymbolUtil.IsHkOptionSymbol(""), Is.False);
    }

    /// <summary>getZoneIdBySymbol returns the NY zone for a US ticker.</summary>
    [Test]
    public void GetZoneIdBySymbol_UsSymbol_ReturnsNyZone()
    {
      Assert.That(SymbolUtil.getZoneIdBySymbol("AAPL", HK_ZONE), Is.SameAs(NY_ZONE));
    }

    /// <summary>getZoneIdBySymbol returns the HK zone for a Hong Kong ticker.</summary>
    [Test]
    public void GetZoneIdBySymbol_HkSymbol_ReturnsHkZone()
    {
      Assert.That(SymbolUtil.getZoneIdBySymbol("0700.HK", NY_ZONE), Is.SameAs(HK_ZONE));
    }

    /// <summary>getZoneIdBySymbol returns the default zone for null input.</summary>
    [Test]
    public void GetZoneIdBySymbol_NullSymbol_ReturnsDefaultZone()
    {
      Assert.That(SymbolUtil.getZoneIdBySymbol(null!, NY_ZONE), Is.SameAs(NY_ZONE));
      Assert.That(SymbolUtil.getZoneIdBySymbol("", HK_ZONE), Is.SameAs(HK_ZONE));
    }

    /// <summary>isFutureSymbol returns true for a valid futures contract code.</summary>
    [Test]
    public void IsFutureSymbol_ValidContract_ReturnsTrue()
    {
      Assert.That(SymbolUtil.isFutureSymbol("CL2401"), Is.True);
    }

    /// <summary>isFutureSymbol returns false for a BK-prefixed code.</summary>
    [Test]
    public void IsFutureSymbol_BkPrefix_ReturnsFalse()
    {
      Assert.That(SymbolUtil.isFutureSymbol("BK1234"), Is.False);
    }

    /// <summary>isFutureSymbol returns false for a plain stock ticker.</summary>
    [Test]
    public void IsFutureSymbol_StockTicker_ReturnsFalse()
    {
      Assert.That(SymbolUtil.isFutureSymbol("AAPL"), Is.False);
    }

    /// <summary>isFutureSymbol returns false for an all-digit code.</summary>
    [Test]
    public void IsFutureSymbol_AllDigits_ReturnsFalse()
    {
      Assert.That(SymbolUtil.isFutureSymbol("12345"), Is.False);
    }

    /// <summary>isFutureSymbol returns false for null input.</summary>
    [Test]
    public void IsFutureSymbol_Null_ReturnsFalse()
    {
      Assert.That(SymbolUtil.isFutureSymbol(null!), Is.False);
    }
  }
}
