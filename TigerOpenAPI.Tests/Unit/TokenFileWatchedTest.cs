using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Watch;
using TigerOpenAPI.Config;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for TokenFileWatched listener logic. Verifies that only events
  /// whose name matches TOKEN_FILENAME trigger a token reload, and that
  /// non-matching files are ignored. Uses temp directories with real token
  /// files so TokenManager.GetInstance().LoadTokenFile() works end-to-end.
  /// </summary>
  [TestFixture]
  [NonParallelizable]
  public class TokenFileWatchedTest
  {
    private string _tempDir = null!;

    [SetUp]
    public void SetUp()
    {
      _tempDir = Path.Combine(Path.GetTempPath(), "cs_sdk_tw_" + Guid.NewGuid().ToString("N"));
      Directory.CreateDirectory(_tempDir);
      // Reset singleton state so LoadTokenFile runs cleanly.
      Common.TokenManager.GetInstance().Destroy();
    }

    [TearDown]
    public void TearDown()
    {
      Common.TokenManager.GetInstance().Destroy();
      if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, true);
    }

    private void WriteTokenFile(string content)
    {
      File.WriteAllText(Path.Combine(_tempDir, TigerApiConstants.TOKEN_FILENAME),
        content, new UTF8Encoding(false));
    }

    private static FileSystemEventArgs Args(WatcherChangeTypes type, string dir, string name)
      => new FileSystemEventArgs(type, dir, name);

    // ---------- OnCreated ----------

    [Test]
    public void OnCreated_MatchingFilename_LoadsToken()
    {
      WriteTokenFile("token=created-token-abc");
      var config = new TigerConfig { ConfigFilePath = _tempDir };
      var watched = new TokenFileWatched(config);

      watched.OnCreated(Args(WatcherChangeTypes.Created, _tempDir, TigerApiConstants.TOKEN_FILENAME));

      Assert.That(config.Token, Is.EqualTo("created-token-abc"));
    }

    [Test]
    public void OnCreated_NonMatchingFilename_DoesNotLoad()
    {
      WriteTokenFile("token=should-not-load");
      var config = new TigerConfig { ConfigFilePath = _tempDir, Token = "untouched" };
      var watched = new TokenFileWatched(config);

      watched.OnCreated(Args(WatcherChangeTypes.Created, _tempDir, "other.txt"));

      Assert.That(config.Token, Is.EqualTo("untouched"));
    }

    // ---------- OnModified ----------

    [Test]
    public void OnModified_MatchingFilename_ReloadsToken()
    {
      var config = new TigerConfig { ConfigFilePath = _tempDir, Token = "stale" };
      WriteTokenFile("token=fresh-token-xyz");
      var watched = new TokenFileWatched(config);

      watched.OnModified(Args(WatcherChangeTypes.Changed, _tempDir, TigerApiConstants.TOKEN_FILENAME));

      Assert.That(config.Token, Is.EqualTo("fresh-token-xyz"));
    }

    [Test]
    public void OnModified_NonMatchingFilename_DoesNotLoad()
    {
      WriteTokenFile("token=should-not-load");
      var config = new TigerConfig { ConfigFilePath = _tempDir, Token = "untouched" };
      var watched = new TokenFileWatched(config);

      watched.OnModified(Args(WatcherChangeTypes.Changed, _tempDir, "notes.txt"));

      Assert.That(config.Token, Is.EqualTo("untouched"));
    }

    // ---------- OnDeleted ----------

    [Test]
    public void OnDeleted_MatchingFilename_DoesNotThrow()
    {
      var config = new TigerConfig { ConfigFilePath = _tempDir, Token = "keep" };
      var watched = new TokenFileWatched(config);

      Assert.DoesNotThrow(() =>
        watched.OnDeleted(Args(WatcherChangeTypes.Deleted, _tempDir, TigerApiConstants.TOKEN_FILENAME)));
      // Token is not reloaded on delete
      Assert.That(config.Token, Is.EqualTo("keep"));
    }

    [Test]
    public void OnDeleted_NonMatchingFilename_DoesNotThrow()
    {
      var config = new TigerConfig { ConfigFilePath = _tempDir };
      var watched = new TokenFileWatched(config);
      Assert.DoesNotThrow(() =>
        watched.OnDeleted(Args(WatcherChangeTypes.Deleted, _tempDir, "unrelated.txt")));
    }

    // ---------- Missing token file ----------

    [Test]
    public void OnCreated_MatchingNameButNoTokenFile_DoesNotThrow()
    {
      // No token file exists in the dir; LoadTokenFile returns false, no throw
      var config = new TigerConfig { ConfigFilePath = _tempDir };
      var watched = new TokenFileWatched(config);
      Assert.DoesNotThrow(() =>
        watched.OnCreated(Args(WatcherChangeTypes.Created, _tempDir, TigerApiConstants.TOKEN_FILENAME)));
      Assert.That(config.Token, Is.Null);
    }
  }
}
