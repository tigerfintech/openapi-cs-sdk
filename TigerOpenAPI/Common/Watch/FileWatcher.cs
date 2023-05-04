using System;
using System.IO;
using Newtonsoft.Json;

namespace TigerOpenAPI.Common.Watch
{
  public class FileWatcher
  {
    private FileSystemWatcher watcher;
    string Directory { set; get; }
    string TargetFile { set; get; }
    IFileWatchedListener Listener { set; get; }

    public FileWatcher(string path, IFileWatchedListener listener, string targetFile)
    {
      Directory = path;
      TargetFile = targetFile;
      Listener = listener;
    }

    public void Watch()
    {
      watcher = new FileSystemWatcher();
      watcher.Path = this.Directory;
      watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
        | NotifyFilters.Size;
      watcher.IncludeSubdirectories = true;
      watcher.Created += OnFileCreated;
      watcher.Deleted += OnFileDeleted;
      watcher.Changed += OnFileChanged;
      watcher.Renamed += OnFileRenamed;
      watcher.EnableRaisingEvents = true;
    }

    public void Dispose()
    {
      if (watcher != null)
      {
        watcher.EnableRaisingEvents = false;
        watcher.Created -= OnFileCreated;
        watcher.Deleted -= OnFileDeleted;
        watcher.Changed -= OnFileChanged;
        watcher.Renamed -= OnFileRenamed;
        watcher.Dispose();
      }
    }

    void OnFileCreated(object sender, FileSystemEventArgs e)
    {
      ApiLogger.Debug($"OnFileCreated:" + JsonConvert.SerializeObject(e));
      if (string.IsNullOrWhiteSpace(e.Name))
      {
        // On MacOS systems, the obtained file name may be null
        string? fileName = filterFileName();
        e = new FileSystemEventArgs(e.ChangeType, e.FullPath, fileName);
      }
      Listener.OnCreated(e);
    }

    void OnFileChanged(object sender, FileSystemEventArgs e)
    {
      ApiLogger.Debug($"OnFileChanged:" + JsonConvert.SerializeObject(e));
      if (string.IsNullOrWhiteSpace(e.Name))
      {
        // On MacOS systems, the obtained file name may be null
        string? fileName = filterFileName();
        e = new FileSystemEventArgs(e.ChangeType, e.FullPath, fileName);
      }
      Listener.OnModified(e);
    }

    void OnFileRenamed(object sender, RenamedEventArgs e)
    {
      ApiLogger.Debug($"RenamedEventArgs:" + JsonConvert.SerializeObject(e));
      Listener.OnModified(e);
    }

    void OnFileDeleted(object sender, FileSystemEventArgs e)
    {
      ApiLogger.Debug($"FileSystemEventArgs:" + JsonConvert.SerializeObject(e));
      Listener.OnDeleted(e);
    }

    private string? filterFileName()
    {
      DirectoryInfo directory = new DirectoryInfo(Directory);
      FileInfo[] files = directory.GetFiles();
      DateTime now = DateTime.Now;
      foreach (FileInfo file in files)
      {
        if (string.Equals(TargetFile, file.Name)
          && (now - file.LastWriteTime).TotalSeconds <= 2)
        {
          return file.Name;
        }
      }
      return null;
    }
  }
}
