using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1.Pkcs;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;

namespace TigerOpenAPI.Tests.TestSupport
{
  /// <summary>
  /// Creates an offline TigerConfig backed by an in-memory RSA key pair.
  /// No file I/O, no network, no real credentials needed.
  /// Shared across all unit tests in the suite.
  /// </summary>
  public static class TestClientFactory
  {
    private static readonly Lazy<string> _privateKeyBase64 =
        new Lazy<string>(GeneratePrivateKeyBase64);

    public static string PrivateKeyBase64 => _privateKeyBase64.Value;

    /// <summary>
    /// Returns a TigerConfig that never touches the network.
    /// AutoGrabPermission=false prevents permission fetch on init.
    /// AutoRefreshToken=false prevents background token refresh.
    /// </summary>
    public static TigerConfig CreateOfflineConfig() => new TigerConfig
    {
      TigerId = "20150000001",
      License = License.TBNZ,
      PrivateKey = PrivateKeyBase64,
      ConfigFilePath = string.Empty,
      AutoGrabPermission = false,
      AutoRefreshToken = false,
    };

    private static string GeneratePrivateKeyBase64()
    {
      var generator = new RsaKeyPairGenerator();
      generator.Init(new KeyGenerationParameters(new SecureRandom(), 1024));
      AsymmetricCipherKeyPair keyPair = generator.GenerateKeyPair();
      PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair.Private);
      return Convert.ToBase64String(privateKeyInfo.GetDerEncoded());
    }
  }
}
