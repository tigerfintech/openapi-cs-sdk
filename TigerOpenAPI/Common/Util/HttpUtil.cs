using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Org.BouncyCastle.Asn1.Ocsp;

namespace TigerOpenAPI.Common.Util
{
  public class HttpUtil
  {
    // default timeout 10s
    public const int DefaultTimeOutSeconds = 10;
    public static readonly TimeSpan DefaultTimeOutSpan = TimeSpan.FromSeconds(DefaultTimeOutSeconds);
    public static readonly HttpClient client;
    // Handle both exceptions and return values in one policy
    public static readonly HashSet<HttpStatusCode> FailRetryStatusCodes = new HashSet<HttpStatusCode>() {
      HttpStatusCode.RequestTimeout, // 408
      HttpStatusCode.InternalServerError, // 500
      HttpStatusCode.BadGateway, // 502
      HttpStatusCode.ServiceUnavailable, // 503
      HttpStatusCode.GatewayTimeout // 504
    };

    static HttpUtil ()
    {
      var handler = new HttpClientHandler() { Proxy = null };
      handler.ServerCertificateCustomValidationCallback = delegate { return true; };
      client = new HttpClient(handler);
      client.Timeout = DefaultTimeOutSpan;
      client.DefaultRequestHeaders.Add("ContentType", TigerApiConstants.CONTENT_TYPE_JSON);
    }

    /// <summary>
    /// send http post request
    /// </summary>
    /// <param name="url"></param>
    /// <param name="token"></param>
    /// <param name="postData"></param>
    /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
    /// <returns></returns>
    public static string HttpPost(string url, string token, string? postData = null,
      string contentType = TigerApiConstants.CONTENT_TYPE_JSON)
    {
      postData = postData ?? "{}";
      // the token is set on the request instead of client.DefaultRequestHeaders: the client is a shared
      // static instance, so writing the header there accumulates values across calls and is not thread safe
      using (HttpRequestMessage request = BuildPostRequest(url, token, postData, contentType))
      {
        HttpResponseMessage response = client.SendAsync(request).Result;
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result;
      }
    }

    /// <summary>
    /// send http post request
    /// </summary>
    /// <param name="url"></param>
    /// <param name="token"></param>
    /// <param name="postData"></param>
    /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
    /// <returns></returns>
    public static async Task<string> HttpPostAsync(string url, string token, string? postData = null,
      string contentType = TigerApiConstants.CONTENT_TYPE_JSON)
    {
      postData = postData ?? "{}";
      using (HttpRequestMessage request = BuildPostRequest(url, token, postData, contentType))
      {
        HttpResponseMessage response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
      }
    }

    /// <summary>
    /// build a post request carrying its own headers, so that concurrent calls never share header state
    /// </summary>
    private static HttpRequestMessage BuildPostRequest(string url, string token, string postData,
      string contentType)
    {
      HttpContent httpContent = new StringContent(postData, Encoding.UTF8);
      if (!string.IsNullOrWhiteSpace(contentType))
        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url) { Content = httpContent };
      if (!string.IsNullOrWhiteSpace(token))
        request.Headers.TryAddWithoutValidation(TigerApiConstants.AUTHORIZATION, token);
      return request;
    }

    /// <summary>
    /// send http get request
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string HttpGet(string url)
    {
      HttpResponseMessage response = client.GetAsync(url).Result;
      response.EnsureSuccessStatusCode();
      return response.Content.ReadAsStringAsync().Result;
    }

    /// <summary>
    /// send http get request
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<string> HttpGetAsync(string url)
    {
      HttpResponseMessage response = await client.GetAsync(url);
      response.EnsureSuccessStatusCode();
      return await response.Content.ReadAsStringAsync();
    }
  }
}
