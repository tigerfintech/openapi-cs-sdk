using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Regression tests for the Authorization header handling in HttpUtil.
  /// HttpUtil.client is a shared static instance, so the token must travel on the individual
  /// request rather than on client.DefaultRequestHeaders: writing it to the shared default headers
  /// appends (not replaces) on every call, which made the second and later requests carry several
  /// comma-joined token values and the server reject them with "user token error".
  /// Uses a loopback HttpListener only - no credentials and no outbound network.
  /// </summary>
  [TestFixture]
  public class HttpUtilAuthorizationHeaderTest
  {
    private HttpListener? _listener;
    private string _url = string.Empty;
    private ConcurrentQueue<string[]> _received = new ConcurrentQueue<string[]>();

    /// <summary>Authorization values of every request the listener saw, "&lt;none&gt;" when absent.</summary>
    private IEnumerable<string[]> Received => _received;

    [SetUp]
    public void SetUp()
    {
      _received = new ConcurrentQueue<string[]>();
      _listener = StartListener(out _url);
    }

    [TearDown]
    public void TearDown()
    {
      try { _listener?.Stop(); } catch { /* ignore */ }
      try { _listener?.Close(); } catch { /* ignore */ }
      _listener = null;
    }

    private HttpListener StartListener(out string url)
    {
      // retry a few ports in case of a collision with another process
      for (int attempt = 0; ; attempt++)
      {
        int port = 18200 + attempt;
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add($"http://localhost:{port}/");
        try
        {
          listener.Start();
        }
        catch (HttpListenerException) when (attempt < 20)
        {
          continue;
        }
        url = $"http://localhost:{port}/";
        _ = Task.Run(() => Serve(listener));
        return listener;
      }
    }

    private void Serve(HttpListener listener)
    {
      while (true)
      {
        HttpListenerContext ctx;
        try
        {
          ctx = listener.GetContext();
        }
        catch
        {
          return; // listener stopped
        }

        string[] auth = ctx.Request.Headers.GetValues(TigerApiConstants.AUTHORIZATION) ?? new[] { "<none>" };
        _received.Enqueue(auth);
        try
        {
          byte[] body = Encoding.UTF8.GetBytes("{\"code\":0}");
          ctx.Response.ContentLength64 = body.Length;
          ctx.Response.OutputStream.Write(body, 0, body.Length);
          ctx.Response.Close();
        }
        catch { /* client gone */ }
      }
    }

    private void WaitForRequests(int expected)
    {
      DateTime deadline = DateTime.UtcNow.AddSeconds(30);
      while (_received.Count < expected && DateTime.UtcNow < deadline)
      {
        Thread.Sleep(20);
      }
      Assert.That(_received.Count, Is.EqualTo(expected), "listener did not observe every request");
    }

    private static void AssertSingleToken(string[] auth, string expected)
    {
      Assert.That(auth.Length, Is.EqualTo(1),
        $"Authorization must be sent exactly once, got: [{string.Join(" | ", auth)}]");
      Assert.That(auth[0], Is.EqualTo(expected));
      Assert.That(auth[0], Does.Not.Contain(","),
        $"Authorization must not carry comma-joined tokens, got: {auth[0]}");
    }

    [Test]
    public void HttpPost_DoesNotAccumulateAuthorization_AcrossRepeatedCalls()
    {
      // the accumulation happened even when the token never changed
      for (int i = 0; i < 4; i++)
      {
        HttpUtil.HttpPost(_url, "SAME_TOKEN");
      }

      WaitForRequests(4);
      foreach (string[] auth in Received)
      {
        AssertSingleToken(auth, "SAME_TOKEN");
      }
    }

    [Test]
    public async Task HttpPostAsync_SendsOnlyTheCurrentToken_WhenTokenRotates()
    {
      for (int i = 1; i <= 4; i++)
      {
        await HttpUtil.HttpPostAsync(_url, "TOKEN_" + i);
      }

      WaitForRequests(4);
      // the calls are serial, so the listener sees them in order: request i must carry TOKEN_i
      List<string[]> all = Received.ToList();
      for (int i = 0; i < all.Count; i++)
      {
        AssertSingleToken(all[i], "TOKEN_" + (i + 1));
      }
    }

    [Test]
    public async Task HttpPostAsync_IsThreadSafe_UnderConcurrentCallsWithDifferentTokens()
    {
      const int count = 200;
      // Task.Run matters: awaiting HttpPostAsync inline would run the header-setting part of every
      // call serially on this thread, hiding the race. Dispatching to the thread pool makes the
      // calls genuinely overlap, which is what a shared-header implementation fails on.
      await Task.WhenAll(Enumerable.Range(0, count)
        .Select(i => Task.Run(() => HttpUtil.HttpPostAsync(_url, "TOKEN_" + i))));

      WaitForRequests(count);
      List<string> sent = new List<string>();
      foreach (string[] auth in Received)
      {
        Assert.That(auth.Length, Is.EqualTo(1),
          $"concurrent calls must not share header state, got: [{string.Join(" | ", auth)}]");
        Assert.That(auth[0], Does.Not.Contain(","), $"got comma-joined tokens: {auth[0]}");
        Assert.That(auth[0], Is.Not.EqualTo("<none>"), "Authorization went missing on a concurrent call");
        sent.Add(auth[0]);
      }
      Assert.That(sent.OrderBy(v => v, StringComparer.Ordinal),
        Is.EqualTo(Enumerable.Range(0, count).Select(i => "TOKEN_" + i).OrderBy(v => v, StringComparer.Ordinal)),
        "every concurrent call must deliver exactly its own token");
    }

    [Test]
    public async Task HttpPostAsync_OmitsAuthorization_WhenTokenIsBlank_AndLeavesNoResidue()
    {
      await HttpUtil.HttpPostAsync(_url, "LEADING_TOKEN");
      // a blank token must not fall back to whatever the previous call put on the shared client
      await HttpUtil.HttpPostAsync(_url, string.Empty);
      await HttpUtil.HttpPostAsync(_url, "   ");

      WaitForRequests(3);
      List<string[]> all = Received.ToList();
      AssertSingleToken(all[0], "LEADING_TOKEN");
      Assert.That(all[1], Is.EqualTo(new[] { "<none>" }), "empty token must not reuse the previous token");
      Assert.That(all[2], Is.EqualTo(new[] { "<none>" }), "whitespace token must not reuse the previous token");
    }

    [Test]
    public void HttpGet_CarriesNoAuthorization_EvenAfterAuthenticatedPosts()
    {
      // NetworkUtil calls HttpUtil.HttpGet for domain discovery, which must stay credential-free
      HttpUtil.HttpPost(_url, "SECRET_TOKEN");
      HttpUtil.HttpGet(_url);

      WaitForRequests(2);
      List<string[]> all = _received.ToList();
      AssertSingleToken(all[0], "SECRET_TOKEN");
      Assert.That(all[1], Is.EqualTo(new[] { "<none>" }),
        "GET must not leak the token left behind by a previous POST");
    }
  }
}
