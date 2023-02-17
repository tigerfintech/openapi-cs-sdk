using System;
using Newtonsoft.Json;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;
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

    private long REFRESH_INTERVAL_MS = Convert.ToInt64(TimeSpan.FromDays(1).TotalMilliseconds);
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
        if (config == null || !config.AutoRefreshToken)
        {
          return;
        }
        if (this.client != null)
        {
          return;
        }
        this.client = client;
        this.config = config;
        bool result = ConfigUtil.LoadTokenFile(config);
        long tokenCreateTime = 0;
        try
        {
          tokenCreateTime = ConfigUtil.GetCreateTime(config.Token);
        }
        catch
        {
          // ignore
        }

        Register(defaultCallback);
        if (result && tokenCreateTime > 0)
        {
          long initialDelay = tokenCreateTime + REFRESH_INTERVAL_MS - DateUtil.CurrentTimeMillis();
          initialDelay = initialDelay < 0 ? 0 : initialDelay;
          timer = new System.Threading.Timer(RefreshToken, null, initialDelay, REFRESH_INTERVAL_MS);
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
      if (client is null)
      {
        return;
      }
      long tokenCreateTime = 0;
      try
      {
        tokenCreateTime = ConfigUtil.GetCreateTime(config.Token);
      }
      catch
      {
        // ignore
      }
      if (tokenCreateTime + REFRESH_INTERVAL_MS - DateUtil.CurrentTimeMillis() > 0)
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
  }
}

