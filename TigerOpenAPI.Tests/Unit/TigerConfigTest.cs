using System;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Config;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for TigerConfig default values and property assignment.
  /// Pure data object — zero network, zero file I/O.
  /// </summary>
  [TestFixture]
  public class TigerConfigTest
  {
    [Test]
    public void DefaultConstructor_SetsEnvironmentToProd()
    {
      var config = new TigerConfig();
      Assert.That(config.Environment, Is.EqualTo(Env.PROD));
      Assert.That(config.Environment, Is.EqualTo(TigerConfig.DEFAULT_ENV));
    }

    [Test]
    public void DefaultConstructor_SetsTimeZoneToHongKong()
    {
      var config = new TigerConfig();
      Assert.That(config.TimeZone, Is.Not.Null);
      Assert.That(config.TimeZone.Id, Is.EqualTo("Asia/Hong_Kong"),
          "default time zone should be Hong Kong");
      Assert.That(config.TimeZone, Is.EqualTo(CustomTimeZone.HK_ZONE));
    }

    [Test]
    public void DefaultConstructor_SetsLanguageToEnUs()
    {
      var config = new TigerConfig();
      Assert.That(config.Language, Is.EqualTo(Language.en_US));
    }

    [Test]
    public void DefaultConstructor_SetsFailRetryCountsToDefault()
    {
      var config = new TigerConfig();
      Assert.That(config.FailRetryCounts, Is.EqualTo(TigerApiConstants.DefaultRetryCount));
      Assert.That(config.FailRetryCounts, Is.EqualTo(2));
    }

    [Test]
    public void DefaultConstructor_SetsAutoRefreshTokenToFalse()
    {
      var config = new TigerConfig();
      Assert.That(config.AutoRefreshToken, Is.False);
    }

    [Test]
    public void DefaultConstructor_SetsAutoGrabPermissionToTrue()
    {
      var config = new TigerConfig();
      Assert.That(config.AutoGrabPermission, Is.True);
    }

    [Test]
    public void DefaultConstructor_SetsIsSslSocketToTrue()
    {
      var config = new TigerConfig();
      Assert.That(config.IsSslSocket, Is.True);
    }

    [Test]
    public void DefaultConstructor_SetsUseFullTickToFalse()
    {
      var config = new TigerConfig();
      Assert.That(config.UseFullTick, Is.False);
    }

    [Test]
    public void DefaultConstructor_SetsEmptyConfigFilePath()
    {
      var config = new TigerConfig();
      Assert.That(config.ConfigFilePath, Is.EqualTo(string.Empty));
    }

    [Test]
    public void DefaultConstructor_LeavesRequiredFieldsUnset()
    {
      var config = new TigerConfig();
      Assert.That(config.TigerId, Is.Null);
      Assert.That(config.PrivateKey, Is.Null);
      Assert.That(config.License, Is.EqualTo(License.NONE));
      Assert.That(config.DefaultAccount, Is.Null);
      Assert.That(config.Token, Is.Null);
      Assert.That(config.SecretKey, Is.Null);
    }

    [Test]
    public void DefaultConstructor_SetsZeroRefreshTokenIntervalDays()
    {
      var config = new TigerConfig();
      Assert.That(config.RefreshTokenIntervalDays, Is.EqualTo(0));
    }

    [Test]
    public void SetProperties_AssignsValuesToAllFields()
    {
      var config = new TigerConfig
      {
        TigerId = "20150000001",
        License = License.TBNZ,
        PrivateKey = "PRIVATE_KEY_BYTES",
        DefaultAccount = "12345678901234567",
        Token = "Bearer abc123",
        SecretKey = "INST_SECRET",
        Environment = Env.SANDBOX,
        Language = Language.zh_CN,
        FailRetryCounts = 5,
        AutoRefreshToken = true,
        AutoGrabPermission = false,
        IsSslSocket = false,
        UseFullTick = true,
        RefreshTokenIntervalDays = 7,
        RefreshTokenTime = "03:00:00",
        ConfigFilePath = "/tmp/tiger",
      };

      Assert.That(config.TigerId, Is.EqualTo("20150000001"));
      Assert.That(config.License, Is.EqualTo(License.TBNZ));
      Assert.That(config.PrivateKey, Is.EqualTo("PRIVATE_KEY_BYTES"));
      Assert.That(config.DefaultAccount, Is.EqualTo("12345678901234567"));
      Assert.That(config.Token, Is.EqualTo("Bearer abc123"));
      Assert.That(config.SecretKey, Is.EqualTo("INST_SECRET"));
      Assert.That(config.Environment, Is.EqualTo(Env.SANDBOX));
      Assert.That(config.Language, Is.EqualTo(Language.zh_CN));
      Assert.That(config.FailRetryCounts, Is.EqualTo(5));
      Assert.That(config.AutoRefreshToken, Is.True);
      Assert.That(config.AutoGrabPermission, Is.False);
      Assert.That(config.IsSslSocket, Is.False);
      Assert.That(config.UseFullTick, Is.True);
      Assert.That(config.RefreshTokenIntervalDays, Is.EqualTo(7));
      Assert.That(config.RefreshTokenTime, Is.EqualTo("03:00:00"));
      Assert.That(config.ConfigFilePath, Is.EqualTo("/tmp/tiger"));
    }

    [Test]
    public void TimeZone_CanBeOverriddenToNewYork()
    {
      var config = new TigerConfig { TimeZone = CustomTimeZone.NY_ZONE };
      Assert.That(config.TimeZone, Is.EqualTo(CustomTimeZone.NY_ZONE));
      Assert.That(config.TimeZone.Id, Is.EqualTo("America/New_York"));
    }

    [Test]
    public void FailRetryCounts_CanBeSetToZero()
    {
      var config = new TigerConfig { FailRetryCounts = 0 };
      Assert.That(config.FailRetryCounts, Is.EqualTo(0));
    }

    [Test]
    public void Language_CanBeCycledThroughAllValues()
    {
      foreach (Language lang in Enum.GetValues(typeof(Language)))
      {
        var config = new TigerConfig { Language = lang };
        Assert.That(config.Language, Is.EqualTo(lang));
      }
    }

    [Test]
    public void Environment_CanBeCycledThroughAllValues()
    {
      foreach (Env env in Enum.GetValues(typeof(Env)))
      {
        var config = new TigerConfig { Environment = env };
        Assert.That(config.Environment, Is.EqualTo(env));
      }
    }
  }
}
