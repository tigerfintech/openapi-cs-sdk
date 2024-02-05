using System;
using Newtonsoft.Json;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Model
{
  public class TigerRequest<T> where T : TigerResponse
  {

    [JsonProperty(PropertyName = "tiger_id")]
    public string TigerId { get; set; }
    [JsonProperty(PropertyName = "version")]
    public string ApiVersion { get; set; } = TigerApiConstants.DEFAULT_VERSION;
    [JsonProperty(PropertyName = "method")]
    public string ApiMethodName { get; set; }
    [JsonProperty(PropertyName = "timestamp")]
    public string Timestamp { get; set; }
    [JsonProperty(PropertyName = "sign")]
    public string Sign { get; set; }

    [JsonProperty(PropertyName = "device_id")]
    public string DeviceId { get; set; }
    [JsonProperty(PropertyName = "charset")]
    public string Charset { get; set; } = TigerApiConstants.CHARSET_UTF8;
    [JsonProperty(PropertyName = "sign_type")]
    public string SignType { get; set; } = TigerApiConstants.SIGN_TYPE_RSA;
    [JsonProperty(PropertyName = "sdk-version")]
    public string SdkVersion { get; set; } = SdkVersionUtil.GetSdkVersion();

    [JsonProperty(PropertyName = "biz_content")]
    public string BizContent { get; set; }
    [JsonIgnore]
    public ApiModel ModelValue { get; set; }

    public TigerRequest()
    {
    }

    public virtual Type GetResponseType()
    {
      return typeof(T);
    }
  }
}

