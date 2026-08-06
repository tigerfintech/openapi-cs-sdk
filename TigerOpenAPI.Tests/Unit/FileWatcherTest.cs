using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using TigerOpenAPI.Common.Watch;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for FileWatcher construction, Watch lifecycle and event
  /// dispatch. Uses a recording IFileWatchedListener and a temp directory;
  /// file-system events are polled with a generous timeout to stay robust
  /// across OSes (macOS FileSystemWatcher reports null Name for Created).
  /// </summary>
  [TestFixture]
  [NonParallelizable]
  public class FileWatcherTest
  {
    private string _tempDir = null!;

    private class RecordingListener : IFileWatchedListener
    {
      public int Created;
      public int Modified;
      public int Deleted;
      public FileSystemEventArgs? LastCreated;

      public void OnCreated(FileSystemEventArgs watchEvent)
      {
        Created++;
        LastCreated = watchEvent;
      }
      public void OnModified(FileSystemEventArgs watchEvent) { Modified++; }
      public void OnDeleted(FileSystemEventArgs watchEvent) { Deleted++; }
    }

    [SetUp]
    public void SetUp()
    {
      _tempDir = Path.Combine(Path.GetTempPath(), "cs_sdk_fw_" + Guid.NewGuid().ToString("N"));
      Directory.CreateDirectory(_tempDir);
    }

    [TearDown]
    public void TearDown()
    {
      if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, true);
    }

    [Test]
    public void Constructor_ValidArgs_DoesNotThrow()
    {
      var listener = new RecordingListener();
      Assert.DoesNotThrow(() => new FileWatcher(_tempDir, listener, "target.txt"));
    }

    [Test]
    public void Watch_OnExistingDirectory_DoesNotThrow()
    {
      var watcher = new FileWatcher(_tempDir, new RecordingListener(), "target.txt");
      Assert.DoesNotThrow(watcher.Watch);
      watcher.Dispose();
    }

    [Test]
    public void Dispose_AfterWatch_DoesNotThrow()
    {
      var watcher = new FileWatcher(_tempDir, new RecordingListener(), "target.txt");
      watcher.Watch();
      Assert.DoesNotThrow(watcher.Dispose);
    }

    [Test]
    public void Watch_FileCreated_DispatchesOnCreated()
    {
      var listener = new RecordingListener();
      var watcher = new FileWatcher(_tempDir, listener, "any.txt");
      watcher.Watch();
      try
      {
        string file = Path.Combine(_tempDir, "created-" + Guid.NewGuid() + ".txt");
        File.WriteAllText(file, "hi");

        // Poll up to 5 seconds for the Created event to fire.
        for (int i = 0; i < 50 && listener.Created == 0; i++)
        {
          Thread.Sleep(100);
        }
        Assert.That(listener.Created, Is.GreaterThan(0),
          "FileWatcher did not dispatch OnCreated within timeout");
      }
      finally
      {
        watcher.Dispose();
      }
    }
  }
}
