using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Asn1.Ocsp;

namespace TigerOpenAPI.Common.Util
{
  public class HttpUtil
  {
    public const int TimeOut = 10;
    /// <summary>
    /// 发起POST同步请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="postData"></param>
    /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
    /// <param name="headers">填充消息头</param>
    /// <returns></returns>
    public static string HttpPost(string url, string postData = null,
      string contentType = "application/json", int timeOut = TimeOut, Dictionary<string, string> headers = null)
    {
      postData = postData ?? "";
      using (var handler = new HttpClientHandler() { Proxy = null })
      {
        handler.ServerCertificateCustomValidationCallback = delegate { return true; };
        using (HttpClient client = new HttpClient(handler))
        {
          client.Timeout = new TimeSpan(0, 0, timeOut);
          if (headers != null)
          {
            foreach (var header in headers)
              client.DefaultRequestHeaders.Add(header.Key, header.Value);
          }
          using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
          {
            if (contentType != null)
              httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

            HttpResponseMessage response = client.PostAsync(url, httpContent).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
          }
        }
      }
    }

    /// <summary>
    /// 发起POST异步请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="postData"></param>
    /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
    /// <param name="headers">填充消息头</param>
    /// <returns></returns>
    public static async Task<string> HttpPostAsync(string url, string postData = null,
      string contentType = "application/json;charset=UTF-8", int timeOut = TimeOut, Dictionary<string, string> headers = null)
    {
      postData = postData ?? "";
      using (var handler = new HttpClientHandler() { Proxy = null})
      using (HttpClient client = new HttpClient(handler))
      {
        client.Timeout = new TimeSpan(0, 0, timeOut);
        if (headers != null)
        {
          foreach (var header in headers)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
        using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
        {
          if (contentType != null)
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

          HttpResponseMessage response = await client.PostAsync(url, httpContent);
          response.EnsureSuccessStatusCode();
          return await response.Content.ReadAsStringAsync();
        }
      }
    }

    /// <summary>
    /// 发起GET同步请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public static string HttpGet(string url, string contentType = "application/json;charset=UTF-8", Dictionary<string, string> headers = null)
    {
      using (HttpClient client = new HttpClient())
      {
        if (contentType != null)
          client.DefaultRequestHeaders.Add("ContentType", contentType);
        if (headers != null)
        {
          foreach (var header in headers)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
        HttpResponseMessage response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result;
      }
    }

    /// <summary>
    /// 发起GET异步请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="headers"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public static async Task<string> HttpGetAsync(string url, string contentType = "application/json;charset=UTF-8", Dictionary<string, string> headers = null)
    {
      using (HttpClient client = new HttpClient())
      {
        if (contentType != null)
          client.DefaultRequestHeaders.Add("ContentType", contentType);
        if (headers != null)
        {
          foreach (var header in headers)
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
      }
    }

    ///// <summary>
    ///// 发起POST同步请求
    ///// </summary>
    ///// <param name="url"></param>
    ///// <param name="postData"></param>
    ///// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
    ///// <param name="headers">填充消息头</param>
    ///// <returns></returns>
    //public static T HttpPost<T>(string url, string postData = null,
    //  string contentType = "application/json", int timeOut = TimeOut, Dictionary<string, string> headers = null)
    //{
    //  return HttpPost(url, postData, contentType, timeOut, headers).ToEntity<T>();
    //}

    ///// <summary>
    ///// 发起POST异步请求
    ///// </summary>
    ///// <param name="url"></param>
    ///// <param name="postData"></param>
    ///// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
    ///// <param name="headers">填充消息头</param>
    ///// <returns></returns>
    //public static async Task<T> HttpPostAsync<T>(string url, string postData = null, string contentType = "application/json", int timeOut = 30, Dictionary<string, string> headers = null)
    //{
    //  var res = await HttpPostAsync(url, postData, contentType, timeOut, headers);
    //  return res.ToEntity<T>();
    //}

    ///// <summary>
    ///// 发起GET同步请求
    ///// </summary>
    ///// <param name="url"></param>
    ///// <param name="headers"></param>
    ///// <param name="contentType"></param>
    ///// <returns></returns>
    //public static T HttpGet<T>(string url, string contentType = "application/json", Dictionary<string, string> headers = null)
    //{
    //  return HttpGet(url, contentType, headers).ToEntity<T>();
    //}

    ///// <summary>
    ///// 发起GET异步请求
    ///// </summary>
    ///// <param name="url"></param>
    ///// <param name="headers"></param>
    ///// <param name="contentType"></param>
    ///// <returns></returns>
    //public static async Task<T> HttpGetAsync<T>(string url, string contentType = "application/json", Dictionary<string, string> headers = null)
    //{
    //  var res = await HttpGetAsync(url, contentType, headers);
    //  return res.ToEntity<T>();
    //}
  }
}

