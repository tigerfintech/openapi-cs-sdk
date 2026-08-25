using System;
using System.Collections.Generic;
using NUnit.Framework;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Tests.TestSupport;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for SignatureUtil sign/verify and GetSignContent.
  /// Reuses the shared offline private key from TestClientFactory and derives
  /// the matching public key via BouncyCastle (no network, no real credentials).
  /// </summary>
  [TestFixture]
  public class SignatureUtilTest
  {
    private string _privateKey = null!;
    private string _publicKey = null!;

    [SetUp]
    public void SetUp()
    {
      _privateKey = TestClientFactory.PrivateKeyBase64;
      _publicKey = DerivePublicKeyBase64(_privateKey);
    }

    /// <summary>
    /// Derives the SubjectPublicKeyInfo (Base64) corresponding to a PKCS#8
    /// private key, so SignatureUtil.Verify can be exercised end-to-end.
    /// </summary>
    private static string DerivePublicKeyBase64(string privateKeyBase64)
    {
      AsymmetricKeyParameter privParam =
        PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKeyBase64));
      var rsaPriv = (RsaPrivateCrtKeyParameters)privParam;
      var pubParam = new RsaKeyParameters(false, rsaPriv.Modulus, rsaPriv.PublicExponent);
      SubjectPublicKeyInfo info = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubParam);
      return Convert.ToBase64String(info.GetDerEncoded());
    }

    // ---------- Sign ----------

    [Test]
    public void Sign_WithData_ReturnsNonEmptyBase64()
    {
      string sign = SignatureUtil.Sign("hello world", _privateKey, "UTF-8");
      Assert.That(sign, Is.Not.Empty);
      // Must be valid Base64
      byte[] decoded = Convert.FromBase64String(sign);
      Assert.That(decoded.Length, Is.GreaterThan(0));
    }

    [Test]
    public void Sign_EmptyCharset_UsesUtf8AndSucceeds()
    {
      string sign = SignatureUtil.Sign("payload", _privateKey, "");
      Assert.That(sign, Is.Not.Empty);
      Assert.That(Convert.FromBase64String(sign).Length, Is.GreaterThan(0));
    }

    // ---------- Verify ----------

    [Test]
    public void Verify_WithValidSign_ReturnsTrue()
    {
      string data = "verify-me";
      string sign = SignatureUtil.Sign(data, _privateKey, "UTF-8");
      Assert.That(SignatureUtil.Verify(data, sign, _publicKey, "UTF-8"), Is.True);
    }

    [Test]
    public void Verify_WithWrongData_ReturnsFalse()
    {
      string sign = SignatureUtil.Sign("original", _privateKey, "UTF-8");
      Assert.That(SignatureUtil.Verify("tampered", sign, _publicKey, "UTF-8"), Is.False);
    }

    [Test]
    public void Verify_WithWrongPublicKey_ReturnsFalse()
    {
      string sign = SignatureUtil.Sign("original", _privateKey, "UTF-8");
      // Generate a second, unrelated key pair for the public side
      var gen = new RsaKeyPairGenerator();
      gen.Init(new KeyGenerationParameters(new SecureRandom(), 1024));
      var otherPair = gen.GenerateKeyPair();
      var otherPub = new RsaKeyParameters(false,
        ((RsaPrivateCrtKeyParameters)otherPair.Private).Modulus,
        ((RsaPrivateCrtKeyParameters)otherPair.Private).PublicExponent);
      string otherPubB64 = Convert.ToBase64String(
        SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(otherPub).GetDerEncoded());

      Assert.That(SignatureUtil.Verify("original", sign, otherPubB64, "UTF-8"), Is.False);
    }

    // ---------- GetSignContent ----------

    [Test]
    public void GetSignContent_SortsKeysAlphabetically()
    {
      var dict = new Dictionary<string, object>
      {
      { "charlie", 3 },
      { "alpha", 1 },
      { "bravo", 2 },
      };
      Assert.That(SignatureUtil.GetSignContent(dict), Is.EqualTo("alpha=1&bravo=2&charlie=3"));
    }

    [Test]
    public void GetSignContent_SkipsNullAndEmptyValues()
    {
      var dict = new Dictionary<string, object?>
      {
      { "a", 1 },
      { "b", null },
      { "c", "" },
      { "d", "   " },
      { "e", 5 },
      };
      Assert.That(SignatureUtil.GetSignContent(dict), Is.EqualTo("a=1&e=5"));
    }

    [Test]
    public void GetSignContent_SingleEntry_NoAmpersand()
    {
      var dict = new Dictionary<string, object> { { "only", "val" } };
      Assert.That(SignatureUtil.GetSignContent(dict), Is.EqualTo("only=val"));
    }

    [Test]
    public void GetSignContent_EmptyDict_ReturnsEmptyString()
    {
      Assert.That(SignatureUtil.GetSignContent(new Dictionary<string, object>()), Is.EqualTo(""));
    }
  }
}
