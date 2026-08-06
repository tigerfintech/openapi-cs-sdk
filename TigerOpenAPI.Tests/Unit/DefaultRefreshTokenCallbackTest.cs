using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for DefaultRefreshTokenCallback. Verifies that TokenChange
  /// updates config.Token and persists the new token to the token file when
  /// one exists. Uses temp directories for file I/O.
  /// </summary>
  [TestFixture]
  public class DefaultRefreshTokenCallbackTest
  {
    private readonly List<string> _tempDirs = new();

    private string NewTempDir()
    {
      string dir = Path.Combine(Path.GetTempPath(), "cs_sdk_cb_" + Guid.NewGuid().ToString("N"));
      Directory.CreateDirectory(dir);
      _tempDirs.Add(dir);
      return dir;
    }

    private static UserToken NewUserToken(string token, long createTime = 1700000000000L,
      long expiredTime = 1700086400000L)
    {
      return new UserToken
      {
        TigerId = "20150000001",
        License = "TBNZ",
        Token = token,
        CreateTime = createTime,
        ExpiredTime = expiredTime
      };
    }

    [TearDown]
    public void TearDown()
    {
      foreach (var d in _tempDirs)
      {
        if (Directory.Exists(d)) Directory.Delete(d, true);
      }
      _tempDirs.Clear();
    }

    [Test]
    public void TokenChange_UpdatesConfigToken()
    {
      var config = new TigerConfig { ConfigFilePath = string.Empty, Token = "old-token" };
      var callback = new DefaultRefreshTokenCallback();

      callback.TokenChange(config, "old-token", NewUserToken("new-token-789"));

      Assert.That(config.Token, Is.EqualTo("new-token-789"));
    }

    [Test]
    public void TokenChange_WritesToTokenFile()
    {
      string dir = NewTempDir();
      // Pre-create the token file so CheckFile passes and UpdateTokenFile writes.
      string tokenPath = Path.Combine(dir, TigerApiConstants.TOKEN_FILENAME);
      File.WriteAllText(tokenPath, "token=stale", new UTF8Encoding(false));

      var config = new TigerConfig { ConfigFilePath = dir, Token = "stale" };
      var callback = new DefaultRefreshTokenCallback();

      callback.TokenChange(config, "stale", NewUserToken("persisted-token"));

      Assert.That(config.Token, Is.EqualTo("persisted-token"));
      string written = File.ReadAllText(tokenPath);
      Assert.That(written, Is.EqualTo("token=persisted-token"));
    }

    [Test]
    public void TokenChange_NoConfigPath_DoesNotThrow()
    {
      var config = new TigerConfig { ConfigFilePath = string.Empty, Token = "old" };
      var callback = new DefaultRefreshTokenCallback();

      // UpdateTokenFile returns false (no file), but config.Token is still set
      Assert.DoesNotThrow(() =>
        callback.TokenChange(config, "old", NewUserToken("fresh")));
      Assert.That(config.Token, Is.EqualTo("fresh"));
    }

    [Test]
    public void TokenChange_NonExistingTokenFile_DoesNotThrow()
    {
      string dir = NewTempDir();
      var config = new TigerConfig { ConfigFilePath = dir, Token = "old" };
      var callback = new DefaultRefreshTokenCallback();

      // No token file in dir -> CheckFile false -> UpdateTokenFile false
      Assert.DoesNotThrow(() =>
        callback.TokenChange(config, "old", NewUserToken("fresh")));
      Assert.That(config.Token, Is.EqualTo("fresh"));
    }

    [Test]
    public void TokenChange_NullUserToken_ThrowsAndIsCaught()
    {
      // A null UserToken would NPE inside the callback; the try/catch swallows it.
      var config = new TigerConfig { ConfigFilePath = string.Empty, Token = "keep" };
      var callback = new DefaultRefreshTokenCallback();

      Assert.DoesNotThrow(() => callback.TokenChange(config, "keep", null!));
      // config.Token not updated because the NPE happened before the assignment
      // is uncertain; we only assert no exception escapes.
    }
  }
}
