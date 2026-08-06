using NUnit.Framework;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies the account-type classifiers in <see cref="AccountUtil"/>.
  /// </summary>
  [TestFixture]
  public class AccountUtilTest
  {
    // ---- IsOmnibusAccount ----

    /// <summary>An all-digit account shorter than 17 chars is omnibus.</summary>
    [Test]
    public void IsOmnibusAccount_AllDigitsShorterThan17_ReturnsTrue()
    {
      Assert.That(AccountUtil.IsOmnibusAccount("123456"), Is.True);
    }

    /// <summary>An account that contains non-digit characters is not omnibus.</summary>
    [Test]
    public void IsOmnibusAccount_NonDigitPrefix_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsOmnibusAccount("U123456"), Is.False);
    }

    /// <summary>Null input is not an omnibus account.</summary>
    [Test]
    public void IsOmnibusAccount_Null_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsOmnibusAccount(null), Is.False);
    }

    /// <summary>An empty string is not an omnibus account.</summary>
    [Test]
    public void IsOmnibusAccount_EmptyString_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsOmnibusAccount(""), Is.False);
      Assert.That(AccountUtil.IsOmnibusAccount("   "), Is.False);
    }

    /// <summary>A 17-digit account is not omnibus (it is a virtual account).</summary>
    [Test]
    public void IsOmnibusAccount_SeventeenDigits_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsOmnibusAccount("12345678901234567"), Is.False);
    }

    // ---- IsVirtualAccount ----

    /// <summary>A 17-digit account is a virtual (paper) account.</summary>
    [Test]
    public void IsVirtualAccount_SeventeenDigits_ReturnsTrue()
    {
      Assert.That(AccountUtil.IsVirtualAccount("12345678901234567"), Is.True);
    }

    /// <summary>An account shorter than 17 digits is not a virtual account.</summary>
    [Test]
    public void IsVirtualAccount_ShorterThanSeventeen_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsVirtualAccount("12345"), Is.False);
    }

    /// <summary>Null input is not a virtual account.</summary>
    [Test]
    public void IsVirtualAccount_Null_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsVirtualAccount(null), Is.False);
    }

    /// <summary>An account with non-digit chars of length 17 is not a virtual account.</summary>
    [Test]
    public void IsVirtualAccount_SeventeenCharsWithLetters_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsVirtualAccount("U1234567890123456"), Is.False);
    }

    // ---- IsGlobalAccount ----

    /// <summary>An account starting with "U" is a global account.</summary>
    [Test]
    public void IsGlobalAccount_StartsWithU_ReturnsTrue()
    {
      Assert.That(AccountUtil.IsGlobalAccount("U123456"), Is.True);
    }

    /// <summary>An account starting with "DU" is a global account.</summary>
    [Test]
    public void IsGlobalAccount_StartsWithDu_ReturnsTrue()
    {
      Assert.That(AccountUtil.IsGlobalAccount("DU123456"), Is.True);
    }

    /// <summary>An account starting with "F" is a global account.</summary>
    [Test]
    public void IsGlobalAccount_StartsWithF_ReturnsTrue()
    {
      Assert.That(AccountUtil.IsGlobalAccount("F123456"), Is.True);
    }

    /// <summary>An account starting with "DF" is a global account.</summary>
    [Test]
    public void IsGlobalAccount_StartsWithDf_ReturnsTrue()
    {
      Assert.That(AccountUtil.IsGlobalAccount("DF123456"), Is.True);
    }

    /// <summary>An all-digit account is not a global account.</summary>
    [Test]
    public void IsGlobalAccount_AllDigits_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsGlobalAccount("123456"), Is.False);
    }

    /// <summary>Null input is not a global account.</summary>
    [Test]
    public void IsGlobalAccount_Null_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsGlobalAccount(null), Is.False);
    }

    /// <summary>An empty string is not a global account.</summary>
    [Test]
    public void IsGlobalAccount_EmptyString_ReturnsFalse()
    {
      Assert.That(AccountUtil.IsGlobalAccount(""), Is.False);
    }
  }
}
