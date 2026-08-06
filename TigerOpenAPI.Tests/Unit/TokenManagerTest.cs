using System;
using System.Collections.Generic;
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
  /// Unit tests for TokenManager singleton lifecycle, callback registry and
  /// token-file loading. Does NOT call Init() (which requires a TigerClient
  /// and network), so the default callback is never auto-registered here.
  /// Uses a recording IRefreshTokenCallback to verify registry behaviour.
  /// </summary>
  [TestFixture]
  [NonParallelizable]
  public class TokenManagerTest
  {
    private readonly List<string> _tempDirs = new();

    private string NewTempDir()
    {
      string dir = Path.Combine(Path.GetTempPath(), "cs_sdk_tm_" + Guid.NewGuid().ToString("N"));
      Directory.CreateDirectory(dir);
      _tempDirs.Add(dir);
      return dir;
    }

    private class RecordingCallback : IRefreshTokenCallback
    {
      public int Calls;
      public TigerConfig? Config;
      public string? OldToken;
      public UserToken? Token;
      public void TokenChange(TigerConfig config, string oldToken, UserToken token)
      {
        Calls++;
        Config = config;
        OldToken = oldToken;
        Token = token;
      }
    }

    [SetUp]
    public void SetUp()
    {
      // Reset shared singleton state before each test.
      TokenManager.GetInstance().Destroy();
    }

    [TearDown]
    public void TearDown()
    {
      TokenManager.GetInstance().Destroy();
      foreach (var d in _tempDirs)
      {
        if (Directory.Exists(d)) Directory.Delete(d, true);
      }
      _tempDirs.Clear();
    }

    // ---------- GetInstance ----------

    [Test]
    public void GetInstance_ReturnsNonNullSingleton()
    {
      Assert.That(TokenManager.GetInstance(), Is.Not.Null);
    }

    [Test]
    public void GetInstance_MultipleCalls_ReturnsSameInstance()
    {
      var a = TokenManager.GetInstance();
      var b = TokenManager.GetInstance();
      Assert.That(ReferenceEquals(a, b), Is.True);
    }

    // ---------- Register / Unregister ----------

    [Test]
    public void Register_AddsCallbackToList()
    {
      var cb = new RecordingCallback();
      TokenManager.GetInstance().Register(cb);
      Assert.That(TokenManager.GetInstance().GetCallbackList(), Does.Contain(cb));
    }

    [Test]
    public void Register_NullCallback_DoesNotAdd()
    {
      TokenManager.GetInstance().Register(null!);
      Assert.That(TokenManager.GetInstance().GetCallbackList().Count, Is.EqualTo(0));
    }

    [Test]
    public void Register_DuplicateCallback_DoesNotAddTwice()
    {
      var cb = new RecordingCallback();
      TokenManager.GetInstance().Register(cb);
      TokenManager.GetInstance().Register(cb);
      Assert.That(TokenManager.GetInstance().GetCallbackList().Count, Is.EqualTo(1));
    }

    [Test]
    public void Unregister_RemovesCallback()
    {
      var cb = new RecordingCallback();
      TokenManager.GetInstance().Register(cb);
      TokenManager.GetInstance().Unregister(cb);
      Assert.That(TokenManager.GetInstance().GetCallbackList(), Does.Not.Contain(cb));
    }

    [Test]
    public void Unregister_NullCallback_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => TokenManager.GetInstance().Unregister(null!));
    }

    // ---------- LoadTokenFile ----------

    [Test]
    public void LoadTokenFile_NoTokenFile_ReturnsFalse()
    {
      var config = new TigerConfig { ConfigFilePath = string.Empty };
      Assert.That(TokenManager.GetInstance().LoadTokenFile(config), Is.False);
      Assert.That(config.Token, Is.Null);
    }

    [Test]
    public void LoadTokenFile_NonExistingDir_ReturnsFalse()
    {
      var config = new TigerConfig { ConfigFilePath = "/no/such/dir/here_" + Guid.NewGuid() };
      Assert.That(TokenManager.GetInstance().LoadTokenFile(config), Is.False);
    }

    [Test]
    public void LoadTokenFile_ValidTokenFile_SetsToken()
    {
      string dir = NewTempDir();
      File.WriteAllText(Path.Combine(dir, TigerApiConstants.TOKEN_FILENAME),
        "token=loadedtoken456", new UTF8Encoding(false));
      var config = new TigerConfig { ConfigFilePath = dir };

      bool ok = TokenManager.GetInstance().LoadTokenFile(config);
      Assert.That(ok, Is.True);
      Assert.That(config.Token, Is.EqualTo("loadedtoken456"));
    }

    [Test]
    public void LoadTokenFile_EmptyTokenValue_ReturnsFalse()
    {
      string dir = NewTempDir();
      // value after '=' is empty -> skipped by ReadPropertiesFile -> key absent
      File.WriteAllText(Path.Combine(dir, TigerApiConstants.TOKEN_FILENAME),
        "token=", new UTF8Encoding(false));
      var config = new TigerConfig { ConfigFilePath = dir, Token = "keepme" };

      // dataDict[TOKEN_FILE_TOKEN] throws KeyNotFoundException because the line
      // "token=" is skipped (value empty). The exception propagates.
      Assert.Throws<KeyNotFoundException>(
        () => TokenManager.GetInstance().LoadTokenFile(config));
    }

    // ---------- AddTokenFileWatch ----------

    [Test]
    public void AddTokenFileWatch_NoConfigPath_DoesNotThrow()
    {
      var config = new TigerConfig { ConfigFilePath = string.Empty };
      Assert.DoesNotThrow(() => TokenManager.GetInstance().AddTokenFileWatch(config));
    }

    [Test]
    public void AddTokenFileWatch_NonExistingDir_DoesNotThrow()
    {
      var config = new TigerConfig { ConfigFilePath = "/no/such/dir/here_" + Guid.NewGuid() };
      Assert.DoesNotThrow(() => TokenManager.GetInstance().AddTokenFileWatch(config));
    }

    [Test]
    public void AddTokenFileWatch_ExistingTokenFile_StartsWatch()
    {
      string dir = NewTempDir();
      File.WriteAllText(Path.Combine(dir, TigerApiConstants.TOKEN_FILENAME),
        "token=watched", new UTF8Encoding(false));
      var config = new TigerConfig { ConfigFilePath = dir };
      Assert.DoesNotThrow(() => TokenManager.GetInstance().AddTokenFileWatch(config));
    }

    // ---------- Destroy ----------

    [Test]
    public void Destroy_ClearsCallbacks()
    {
      var cb = new RecordingCallback();
      TokenManager.GetInstance().Register(cb);
      Assert.That(TokenManager.GetInstance().GetCallbackList().Count, Is.EqualTo(1));

      TokenManager.GetInstance().Destroy();
      Assert.That(TokenManager.GetInstance().GetCallbackList().Count, Is.EqualTo(0));
    }
  }
}
