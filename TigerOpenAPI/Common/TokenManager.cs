using System;
using DotNetty.Common.Utilities;
using System.IO;
using Newtonsoft.Json;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Common.Watch;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Pb;
using TigerOpenAPI.Quote.Response;

namespace TigerOpenAPI.Common
{
  public class TokenManager
  {
    private static readonly TokenManager instance = new TokenManager();

    // refresh every 5 days by default
    private const int defaultRefreshIntervalDays = 5;
    private long refreshIntervalMs = Convert.ToInt64(TimeSpan.FromDays(defaultRefreshIntervalDays).TotalMilliseconds);
    private System.Threading.Timer timer;
    private TigerClient? client;
    private TigerConfig config;
    private readonly List<IRefreshTokenCallback> callbackList = new List<IRefreshTokenCallback>();
    private readonly IRefreshTokenCallback defaultCallback = new DefaultRefreshTokenCallback();

    private TokenManager() { }

    public static TokenManager GetInstance()
    {
      return instance;
    }

    public void Destroy()
    {
      client = null;
      if (timer != null)
      {
        timer.Dispose();
      }
      callbackList.Clear();
    }

    public void Init(TigerConfig config, TigerClient client)
    {
      Monitor.Enter(this);
      try
      {
        if (config == null || this.client != null)
        {
          return;
        }
        this.client = client;
        this.config = config;
        Register(defaultCallback);
        LoadTokenFile(config);
        AddTokenFileWatch(config);

        if (!config.AutoRefreshToken)
        {
          return;
        }

        if (config.RefreshTokenIntervalDays > 0)
        {
          refreshIntervalMs = Convert.ToInt64(TimeSpan.FromDays(config.RefreshTokenIntervalDays).TotalMilliseconds);
        }
        long tokenCreateTime = ConfigFileUtil.TryGetCreateTime(config.Token);
        long initialDelay = tokenCreateTime + refreshIntervalMs - DateUtil.CurrentTimeMillis();
        if (initialDelay <= 0)
        {
          RefreshToken(null);
          tokenCreateTime = ConfigFileUtil.TryGetCreateTime(config.Token);
          initialDelay = tokenCreateTime + refreshIntervalMs - DateUtil.CurrentTimeMillis();
        }
        initialDelay = GetDelayTime(config.RefreshTokenTime, initialDelay);

        timer = new System.Threading.Timer(RefreshToken, null, initialDelay, refreshIntervalMs);
        if (!string.IsNullOrWhiteSpace(config.Token))
        {
          ApiLogger.Info($"init refresh token task success");
        }
      }
      finally { Monitor.Exit(this); }
    }

    public void Register(IRefreshTokenCallback callback)
    {
      if (callback != null)
      {
        if (!callbackList.Contains(callback))
        {
          callbackList.Add(callback);
        }
      }
    }

    public void Unregister(IRefreshTokenCallback callback)
    {
      if (callback != null)
      {
        callbackList.Remove(callback);
      }
    }

    public List<IRefreshTokenCallback> GetCallbackList()
    {
      return callbackList;
    }

    private void RefreshToken(object? state)
    {
      if (client is null || string.IsNullOrWhiteSpace(config.Token) && !LoadTokenFile(config))
      {
        return;
      }
      long tokenCreateTime = ConfigFileUtil.TryGetCreateTime(config.Token);
      if (tokenCreateTime == 0)
      {
        ApiLogger.Warn($"local token is invalid:{config.Token}, refreshToken ignore");
        return;
      }
      if (tokenCreateTime + refreshIntervalMs - DateUtil.CurrentTimeMillis() > 0)
      {
        ApiLogger.Info("refreshToken last update time:{}, ignore",
            DateUtil.PrintDateTime(tokenCreateTime, config.TimeZone));
        return;
      }

      int count = 5;
      String oldToken = config.Token;
      TigerRequest<UserTokenResponse> request = new TigerRequest<UserTokenResponse>()
      {
        ApiMethodName = QuoteApiService.USER_TOKEN_REFRESH
      };
      do
      {
        try
        {
          UserTokenResponse? response = client.Execute(request);
          if (response != null && response.IsSuccess())
          {
            ApiLogger.Info("refreshToken success. return:" + JsonConvert.SerializeObject(response.Data));
            foreach (IRefreshTokenCallback callback in callbackList) {
              callback.TokenChange(config, oldToken, response.Data);
            }
            break;
          }
          Thread.Sleep(TimeSpan.FromSeconds(5));
        }
        catch (Exception ex)
        {
          ApiLogger.Warn("refreshToken fail. " + ex.Message);
        }
        finally
        {
          count--;
        }
      } while (count > 0) ;
    }

    public bool LoadTokenFile(TigerConfig tigerConfig)
    {
      if (!ConfigFileUtil.CheckFile(tigerConfig.ConfigFilePath, TigerApiConstants.TOKEN_FILENAME))
      {
        return false;
      }

      string tokenFile = Path.Combine(tigerConfig.ConfigFilePath.Trim(), TigerApiConstants.TOKEN_FILENAME);
      Dictionary<string, string> dataDict = ConfigFileUtil.ReadPropertiesFile(tokenFile);
      string token = dataDict[ConfigFileUtil.TOKEN_FILE_TOKEN];

      if (string.IsNullOrWhiteSpace(token))
      {
        return false;
      }
      tigerConfig.Token = token;
      return true;
    }

    /**
     * Update the hour, minute, and second for the specified timestamp
     * @param baseTimestamp the specified timestamp
     * @param time formate:HH:mm:ss, 16:30:00 etc
     * @return
     */
    private long GetTimeInMillis(long baseTimestamp, string time)
    {
      if (string.IsNullOrWhiteSpace(time))
      {
        return -1;
      }
      TimeZoneInfo timeZone = config.TimeZone;
      long timestamp = DateUtil.ConvertTimestamp(
        DateUtil.PrintDate(baseTimestamp, timeZone) + " " + time, timeZone);
      return timestamp <= 0 ? -1 : timestamp;
    }

    /**
     * get the delay time for refresh token
     * @param time formate:HH:mm:ss, 16:30:00 etc
     * @return
     */
    private long GetDelayTime(string time, long initialDelay)
    {
      initialDelay = initialDelay <= 0 ? 0 : initialDelay;
      long baseTimestamp = DateUtil.CurrentTimeMillis();
      long refreshTimestamp = GetTimeInMillis(baseTimestamp, time);
      if (refreshTimestamp < 0)
      {
        return initialDelay;
      }

      if (initialDelay > 0)
      {
        baseTimestamp += initialDelay;
        refreshTimestamp = GetTimeInMillis(baseTimestamp, time);
      }
      long delayTime = refreshTimestamp - baseTimestamp + initialDelay;
      if (delayTime < 0)
      {
        delayTime += Convert.ToInt64(TimeSpan.FromDays(1).TotalMilliseconds);
      }
      return delayTime;
    }

    public void AddTokenFileWatch(TigerConfig config)
    {
      try
      {
        if (null == config || string.IsNullOrWhiteSpace(config.ConfigFilePath))
        {
          return;
        }
        // if token file exists, add listener
        if (ConfigFileUtil.CheckFile(config.ConfigFilePath, TigerApiConstants.TOKEN_FILENAME))
        {
          IFileWatchedListener tokenFileListener = new TokenFileWatched(config);
          FileWatcher fileWatcher = new FileWatcher(config.ConfigFilePath,
            tokenFileListener, TigerApiConstants.TOKEN_FILENAME);
          fileWatcher.Watch();

          ApiLogger.Info("addTokenFileWatch success.");
        }
      }
      catch (Exception e)
      {
        ApiLogger.Error("addTokenFileWatch fail.", e);
      }
    }
  }
}

