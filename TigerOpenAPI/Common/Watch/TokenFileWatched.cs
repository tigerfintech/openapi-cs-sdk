using System;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;

namespace TigerOpenAPI.Common.Watch
{
  public class TokenFileWatched : IFileWatchedListener
  {
    private TigerConfig config;
    public TokenFileWatched(TigerConfig config)
    {
      this.config = config;
    }

    public void OnCreated(FileSystemEventArgs watchEvent)
    {
      string? fileName = Path.GetFileName(watchEvent.Name);
      if (!string.Equals(TigerApiConstants.TOKEN_FILENAME, fileName))
      {
        return;
      }
      bool load = TokenManager.GetInstance().LoadTokenFile(config);
      ApiLogger.Info($"{fileName} is created, reload token " + (load ? "success" : "fail"));
    }

    public void OnModified(FileSystemEventArgs watchEvent)
    {
      string? fileName = Path.GetFileName(watchEvent.Name);
      if (!string.Equals(TigerApiConstants.TOKEN_FILENAME, fileName))
      {
        return;
      }
      bool load = TokenManager.GetInstance().LoadTokenFile(config);
      ApiLogger.Info($"{fileName} is modifed, reload token " + (load ? "success" : "fail"));
    }

    public void OnDeleted(FileSystemEventArgs watchEvent)
    {
      string? fileName = Path.GetFileName(watchEvent.Name);
      if (!string.Equals(TigerApiConstants.TOKEN_FILENAME, fileName))
      {
        return;
      }
      ApiLogger.Info($"{fileName} is deleted, ignore");
    }
  }
}

