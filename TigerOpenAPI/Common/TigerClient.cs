using System;
using Newtonsoft.Json;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;

namespace TigerOpenAPI.Common
{
  public class TigerClient
  {

    protected string TigerId { get; set; }
    protected string PrivateKey { get; set; }
    protected License License { get; set; }
    protected string TigerPublicKey { get; set; }
    protected string AccessToken { get; set; }
    protected string TradeToken { get; set; }
    protected string AccountType { get; set; }
    protected string DeviceId { get; set; }
    protected Env Environment { get; set; } = Env.PROD;
    protected TigerConfig Config { get; private set; }
    protected ApiModel EmptyModel { get; private set; }

    private const string ONLINE_PUBLIC_KEY =
      "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDNF3G8SoEcCZh2rshUbayDgLLrj6rKgzNMxDL2HSnKcB0+GPOsndqSv+a4IBu9+I3fyBp5hkyMMG2+AXugd9pMpy6VxJxlNjhX1MYbNTZJUT4nudki4uh+LMOkIBHOceGNXjgB+cXqmlUnjlqha/HgboeHSnSgpM3dKSJQlIOsDwIDAQAB";
    private const string SANDBOX_PUBLIC_KEY =
      "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCbm21i11hgAENGd3/f280PSe4g9YGkS3TEXBYMidihTvHHf+tJ0PYD0o3PruI0hl3qhEjHTAxb75T5YD3SGK4IBhHn/Rk6mhqlGgI+bBrBVYaXixmHfRo75RpUUuWACyeqQkZckgR0McxuW9xRMIa2cXZOoL1E4SL4lXKGhKoWbwIDAQAB";
    private const string signType = TigerApiConstants.SIGN_TYPE_RSA;
    private const string charset = TigerApiConstants.CHARSET_UTF8;

    private static readonly JsonSerializerSettings JsonSet = new JsonSerializerSettings
    {
      DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat,
      DateFormatString = DateUtil.FORMAT_DATETIME,// "yyyy-MM-dd HH:mm:ss",
      NullValueHandling = NullValueHandling.Ignore,
      MaxDepth = 10,
      // 指定如何处理循环引用，None--不序列化，Error-抛出异常，Serialize--仍要序列化
      ReferenceLoopHandling = ReferenceLoopHandling.Serialize
    };

    public TigerClient(TigerConfig config)
    {
      if (null == config)
      {
        throw new ArgumentNullException("TigerConfig is empty.");
      }
      if (string.IsNullOrWhiteSpace(config.TigerId))
      {
        throw new ArgumentNullException("TigerId is empty.");
      }
      if (string.IsNullOrWhiteSpace(config.PrivateKey))
      {
        throw new ArgumentNullException("PrivateKey is empty.");
      }
      if (License.NONE == config.License)
      {
        throw new ArgumentNullException("License is empty.");
      }

      TigerId = config.TigerId;
      PrivateKey = config.PrivateKey;
      License = config.License;
      Environment = config.Environment;
      TigerPublicKey = config.Environment == Env.PROD ? ONLINE_PUBLIC_KEY : SANDBOX_PUBLIC_KEY;
      Config = config;
      DeviceId = NetworkUtil.GetDeviceId();
      EmptyModel = new ApiModel() { Lang = config.Language };
    }

    public virtual string GetServerUri<T>(TigerRequest<T> request) where T : TigerResponse { return default; }
    public virtual bool Validate<T>(TigerRequest<T> request, out string errorMsg) where T : TigerResponse
    {
      errorMsg = string.Empty;
      return true;
    }

    public TigerRequest<T> BuildParams<T>(TigerRequest<T> request) where T : TigerResponse
    {
      request.Timestamp = DateUtil.PrintSystemDateTime(Config.TimeZone);
      request.TigerId = TigerId;
      request.DeviceId = DeviceId;

      if (!string.IsNullOrWhiteSpace(TigerId) && !string.IsNullOrWhiteSpace(PrivateKey))
      {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add(TigerApiConstants.METHOD, request.ApiMethodName);
        dic.Add(TigerApiConstants.VERSION, request.ApiVersion);
        dic.Add(TigerApiConstants.SDK_VERSION, request.SdkVersion);
        dic.Add(TigerApiConstants.TIMESTAMP, request.Timestamp);
        dic.Add(TigerApiConstants.CHARSET, request.Charset);
        dic.Add(TigerApiConstants.TIGER_ID, request.TigerId);
        dic.Add(TigerApiConstants.SIGN_TYPE, request.SignType);

        if (string.IsNullOrWhiteSpace(request.BizContent))
        {
          request.ModelValue = request.ModelValue ?? EmptyModel;
          if (request.ModelValue.Lang == Language.None)
          {
            request.ModelValue.Lang = Config.Language;
          }
          request.BizContent = JsonConvert.SerializeObject(request.ModelValue, JsonSet);
        }
        dic.Add(TigerApiConstants.BIZ_CONTENT, request.BizContent);

        if (!string.IsNullOrEmpty(DeviceId))
        {
          dic.Add(TigerApiConstants.DEVICE_ID, DeviceId);
        }

        if (!string.IsNullOrEmpty(AccessToken))
        {
          dic.Add(TigerApiConstants.ACCESS_TOKEN, AccessToken);
        }
        if (!string.IsNullOrEmpty(TradeToken))
        {
          dic.Add(TigerApiConstants.TRADE_TOKEN, TradeToken);
        }
        if (!string.IsNullOrEmpty(AccountType))
        {
          dic.Add(TigerApiConstants.ACCOUNT_TYPE, AccountType);
        }
        string content = SignatureUtil.GetSignContent (dic);
        request.Sign = SignatureUtil.Sign(content, PrivateKey, charset);
      }
      return request;
    }

    //public virtual async Task<T?> executeAsync<T>(TigerRequest<T> request) where T : TigerResponse
    public virtual T? Execute<T>(TigerRequest<T> request) where T : TigerResponse
    {
      T? response;
      string param = string.Empty;
      string data = string.Empty;
      try
      {
        if (!Validate(request, out string errorMsg))
        {
          throw new TigerApiException(TigerApiCode.HTTP_COMMON_PARAM_ERROR, errorMsg);
        }
        BuildParams(request);
        param = JsonConvert.SerializeObject(request, JsonSet);
        ApiLogger.Debug("request param:{}", param);

        // data = HttpUtils.post(getServerUrl(request), param);
        data = HttpUtil.HttpPost(GetServerUri(request), param);

        ApiLogger.Debug("response result:{}", data);
        if (string.IsNullOrWhiteSpace(data))
        {
          throw new TigerApiException(TigerApiCode.EMPTY_DATA_ERROR);
        }
        response = JsonConvert.DeserializeObject<T>(data);
        if (string.IsNullOrEmpty(TigerPublicKey) || string.IsNullOrEmpty(response?.Sign))
        {
          return response;
        }
        bool signSuccess = SignatureUtil.Verify(request.Timestamp, response.Sign, TigerPublicKey, charset);
        if (!signSuccess)
        {
          throw new TigerApiException(TigerApiCode.SIGN_CHECK_FAILED);
        }
        return response;
      }
      catch (TigerApiException e)
      {
        ApiLogger.Error(e, "request fail. tigerId:{}, method:{}, param:{}, response:{}",
            TigerId, request?.ApiMethodName ?? string.Empty, param, data);
        return ErrorResponse(TigerId, request, e);
      }
      catch (Exception e)
      {
        ApiLogger.Error(e, "request fail. tigerId:{}, method:{}, param:{}, response:{}",
            TigerId, request?.ApiMethodName ?? string.Empty, param, data);
        return ErrorResponse(TigerId, request, e);
      }
    }

    private T? ErrorResponse<T>(string tigerId, TigerRequest<T>? request, Exception e) where T : TigerResponse
    {
      try
      {
        T response = System.Activator.CreateInstance<T>();
        response.Code = TigerApiCode.CLIENT_API_ERROR.Code;
        response.Message = TigerApiCode.CLIENT_API_ERROR.Message + "(" + e.Message + ")";
        return response;
      }
      catch (Exception exp)
      {
        ApiLogger.Error("errorResponse fail. tigerId:{}, {}", tigerId, exp.Message);
        return null;
      }
    }
  }
}
