using System;
namespace TigerOpenAPI.Common.Watch
{
  public interface IFileWatchedListener
  {
    void OnCreated(FileSystemEventArgs watchEvent);

    void OnModified(FileSystemEventArgs watchEvent);

    void OnDeleted(FileSystemEventArgs watchEvent);
  }
}

