using NUnit.Framework;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies the version helpers in <see cref="SdkVersionUtil"/>.
  /// </summary>
  [TestFixture]
  public class SdkVersionUtilTest
  {
    /// <summary>GetSdkVersion returns a value prefixed with the openapi-cs-sdk prefix.</summary>
    [Test]
    public void GetSdkVersion_ReturnsValueWithOpenApiPrefix()
    {
      string version = SdkVersionUtil.GetSdkVersion();

      Assert.That(version.StartsWith("openapi-cs-sdk-"), Is.True,
        $"Actual: {version}");
    }

    /// <summary>GetSdkVersion resolves to a real version, not the unknown sentinel.</summary>
    [Test]
    public void GetSdkVersion_IsNotUnknown()
    {
      string version = SdkVersionUtil.GetSdkVersion();

      Assert.That(version, Is.Not.EqualTo("openapi-cs-sdk-unknown"));
    }

    /// <summary>GetSdkVersion caches the result and returns the same value on subsequent calls.</summary>
    [Test]
    public void GetSdkVersion_SecondCall_ReturnsSameCachedValue()
    {
      string first = SdkVersionUtil.GetSdkVersion();
      string second = SdkVersionUtil.GetSdkVersion();

      Assert.That(second, Is.EqualTo(first));
    }

    /// <summary>GetPushSdkVersion returns a value prefixed with the csharp prefix.</summary>
    [Test]
    public void GetPushSdkVersion_ReturnsValueWithCsharpPrefix()
    {
      string version = SdkVersionUtil.GetPushSdkVersion();

      Assert.That(version.StartsWith("csharp-"), Is.True,
        $"Actual: {version}");
    }
  }
}
