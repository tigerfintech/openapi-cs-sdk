using System;
using System.Collections.Generic;
using NUnit.Framework;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Push;
using TigerOpenAPI.Push.Model;
using TigerOpenAPI.Quote.Pb;
using static TigerOpenAPI.Quote.Pb.SocketCommon.Types;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for ApiCallbackDecoderUtil constants, the Executor null-guard,
  /// the ReceiveConnected empty-msg early return, and ProcessError dispatch logic.
  /// The Connected/Heartbeat/Message/Disconnect command branches that require a
  /// live IChannelHandlerContext are intentionally out of scope for unit tests.
  /// </summary>
  [TestFixture]
  public class ApiCallbackDecoderUtilTest
  {
    private sealed class FakeCallback : IApiComposeCallback
    {
      public List<string> Errors { get; } = new();
      public List<(int id, int code, string msg)> CodedErrors { get; } = new();
      public List<string> KickOuts { get; } = new();
      public bool ConnectionAckCalled;
      public (int send, int recv)? ConnectionAckArgs;
      public bool ConnectionClosedCalled;
      public List<string> HeartBeats { get; } = new();

      public void Error(string errorMsg) => Errors.Add(errorMsg);
      public void Error(int id, int errorCode, string errorMsg) =>
          CodedErrors.Add((id, errorCode, errorMsg));
      public void ConnectionClosed() => ConnectionClosedCalled = true;
      public void ConnectionKickout(int errorCode, string errorMsg) =>
          KickOuts.Add($"{errorCode}:{errorMsg}");
      public void ConnectionAck() => ConnectionAckCalled = true;
      public void ConnectionAck(int serverSendInterval, int serverReceiveInterval)
        => ConnectionAckArgs = (serverSendInterval, serverReceiveInterval);
      public void HearBeat(string heartBeatContent) => HeartBeats.Add(heartBeatContent);
      public void ServerHeartBeatTimeOut(string channelId) { }
      public void OrderStatusChange(OrderStatusData data) { }
      public void OrderTransactionChange(OrderTransactionData data) { }
      public void PositionChange(PositionData data) { }
      public void AssetChange(AssetData data) { }
      public void TradeTickChange(TradeTick data) { }
      public void FullTickChange(TickData data) { }
      public void QuoteChange(QuoteBasicData data) { }
      public void QuoteAskBidChange(QuoteBBOData data) { }
      public void OptionChange(QuoteBasicData data) { }
      public void OptionAskBidChange(QuoteBBOData data) { }
      public void FutureChange(QuoteBasicData data) { }
      public void FutureAskBidChange(QuoteBBOData data) { }
      public void DepthQuoteChange(QuoteDepthData data) { }
      public void KlineChange(KlineData data) { }
      public void StockTopPush(StockTopData data) { }
      public void OptionTopPush(OptionTopData data) { }
      public void SubscribeEnd(int id, string subject, string result) { }
      public void CancelSubscribeEnd(int id, string subject, string result) { }
      public void GetSubscribedSymbolEnd(SubscribedSymbol subscribedSymbol) { }
    }

    // ---------- constants ----------

    [Test]
    public void IdleStateHandlerConstant_IsNonEmptyExpectedValue()
    {
      Assert.That(ApiCallbackDecoderUtil.IDLE_STATE_HANDLER, Is.Not.Null);
      Assert.That(ApiCallbackDecoderUtil.IDLE_STATE_HANDLER, Is.Not.Empty);
      Assert.That(ApiCallbackDecoderUtil.IDLE_STATE_HANDLER, Is.EqualTo("idleStateHandler"));
    }

    [Test]
    public void IdleTriggerHandlerConstant_IsNonEmptyExpectedValue()
    {
      Assert.That(ApiCallbackDecoderUtil.IDLE_TRIGGER_HANDLER, Is.Not.Null);
      Assert.That(ApiCallbackDecoderUtil.IDLE_TRIGGER_HANDLER, Is.Not.Empty);
      Assert.That(ApiCallbackDecoderUtil.IDLE_TRIGGER_HANDLER, Is.EqualTo("idleTriggerHandler"));
    }

    [Test]
    public void HandlerConstants_AreDistinct()
    {
      Assert.That(ApiCallbackDecoderUtil.IDLE_STATE_HANDLER,
          Is.Not.EqualTo(ApiCallbackDecoderUtil.IDLE_TRIGGER_HANDLER));
    }

    // ---------- Executor null-guard ----------

    [Test]
    public void Executor_WhenDecoderNull_ReturnsEarly_NoException()
    {
      // guard short-circuits before touching ctx/response
      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.Executor(null!, null!, null!));
    }

    [Test]
    public void Executor_WhenContextNull_ReturnsEarly_NoException()
    {
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(new FakeCallback());
      Response response = new Response { Command = Command.Heartbeat };

      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.Executor(null!, response, decoder));
    }

    [Test]
    public void Executor_WhenResponseNull_ReturnsEarly_NoException()
    {
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(new FakeCallback());

      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.Executor(null!, null!, decoder));
    }

    [Test]
    public void Executor_WhenCommandUnknown_ReturnsEarly_NoException()
    {
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(new FakeCallback());
      Response response = new Response { Command = Command.Unknown };

      // ctx null but guard checks command -> still returns before touching ctx
      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.Executor(null!, response, decoder));
    }

    // ---------- ReceiveConnected empty-msg early return ----------

    [Test]
    public void ReceiveConnected_WhenMsgEmpty_ReturnsEarly_NoException()
    {
      // null callback -> guard `decoder.GetCallback() != null` false -> returns
      ApiCallbackDecoder nullCbDecoder = new ApiCallbackDecoder(null!);
      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.ReceiveConnected(null!, nullCbDecoder, ""));
    }

    [Test]
    public void ReceiveConnected_WhenCallbackSetButMsgWhitespace_ReturnsEarly_NoException()
    {
      // callback non-null but msg whitespace -> short-circuits before touching ctx
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(new FakeCallback());
      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.ReceiveConnected(null!, decoder, "   "));
    }

    [Test]
    public void ReceiveConnected_WhenCallbackSetButMsgNull_ReturnsEarly_NoException()
    {
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(new FakeCallback());
      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.ReceiveConnected(null!, decoder, null!));
    }

    /// <summary>
    /// JSON dict without version/heart-beat keys: SetVersion is skipped (avoids the
    /// null-authentication NRE in PushClient), value stays null, and the else branch
    /// invokes ConnectionAck() without ever touching ctx.
    /// </summary>
    [Test]
    public void ReceiveConnected_WhenMsgWithoutHeartBeat_CallsConnectionAck()
    {
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);
      // no "version" key -> SetVersion skipped; no "heart-beat" key -> else branch
      string msg = "{\"foo\":\"bar\"}";

      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.ReceiveConnected(null!, decoder, msg));

      Assert.That(cb.ConnectionAckCalled, Is.True);
      Assert.That(cb.ConnectionAckArgs.HasValue, Is.False);
    }

    /// <summary>
    /// Empty heart-beat value (whitespace) takes the else branch -> ConnectionAck()
    /// without touching ctx.
    /// </summary>
    [Test]
    public void ReceiveConnected_WhenHeartBeatValueEmpty_CallsConnectionAck()
    {
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);
      // TigerApiConstants.HEART_BEAT == "heart-beat"
      string msg = "{\"heart-beat\":\"\"}";

      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.ReceiveConnected(null!, decoder, msg));

      Assert.That(cb.ConnectionAckCalled, Is.True);
      Assert.That(cb.ConnectionAckArgs.HasValue, Is.False);
    }

    /// <summary>
    /// Empty JSON object -> dict.Count == 0 -> returns early, no callback invoked.
    /// </summary>
    [Test]
    public void ReceiveConnected_WhenMsgEmptyDict_ReturnsEarlyWithoutCallback()
    {
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);

      Assert.DoesNotThrow(() =>
          ApiCallbackDecoderUtil.ReceiveConnected(null!, decoder, "{}"));

      Assert.That(cb.ConnectionAckCalled, Is.False);
    }

    // ---------- ProcessError dispatch ----------

    [Test]
    public void ProcessError_WhenCallbackNull_ReturnsEarly_NoException()
    {
      // decoder with null callback -> outer guard skips the whole block
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(null!);
      Response response = new Response { Code = 0, Msg = "boom" };

      Assert.DoesNotThrow(() => ApiCallbackDecoderUtil.ProcessError(decoder, response));
    }

    [Test]
    public void ProcessError_WhenZeroCodeAndMsg_PropagatesErrorMsg()
    {
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);
      Response response = new Response { Code = 0, Msg = "transient failure" };

      ApiCallbackDecoderUtil.ProcessError(decoder, response);

      Assert.That(cb.Errors, Has.Count.EqualTo(1));
      Assert.That(cb.Errors[0], Is.EqualTo("transient failure"));
      Assert.That(cb.CodedErrors, Is.Empty);
    }

    [Test]
    public void ProcessError_WhenZeroCodeAndEmptyMsg_PropagatesUnknownError()
    {
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);
      Response response = new Response { Code = 0, Msg = "" };

      ApiCallbackDecoderUtil.ProcessError(decoder, response);

      Assert.That(cb.Errors, Has.Count.EqualTo(1));
      Assert.That(cb.Errors[0], Is.EqualTo("unknown error"));
    }

    [Test]
    public void ProcessError_WhenZeroCodeAndWhitespaceMsg_PropagatesUnknownError()
    {
      // IsNullOrWhiteSpace covers null, empty, and whitespace-only
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);
      Response response = new Response { Code = 0, Msg = "   " };

      ApiCallbackDecoderUtil.ProcessError(decoder, response);

      Assert.That(cb.Errors, Has.Count.EqualTo(1));
      Assert.That(cb.Errors[0], Is.EqualTo("unknown error"));
    }

    [Test]
    public void ProcessError_WhenPositiveNonKickOutCode_PropagatesCodedError()
    {
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);
      Response response = new Response
      {
        Id = 42,
        // 1001 is not the kick-out code (4001) -> falls through to Error(id, code, msg)
        Code = 1001,
        Msg = "param invalid"
      };

      ApiCallbackDecoderUtil.ProcessError(decoder, response);

      Assert.That(cb.CodedErrors, Has.Count.EqualTo(1));
      Assert.That(cb.CodedErrors[0].id, Is.EqualTo(42));
      Assert.That(cb.CodedErrors[0].code, Is.EqualTo(1001));
      Assert.That(cb.CodedErrors[0].msg, Is.EqualTo("param invalid"));
      Assert.That(cb.Errors, Is.Empty);
      Assert.That(cb.KickOuts, Is.Empty);
    }

    [Test]
    public void ProcessError_WhenPositiveCodeMsgUnset_PropagatesEmptyMsg()
    {
      // Response.Msg is a non-nullable protobuf string (default "") — it cannot be
      // set to null. ProcessError with Code>0 always calls Error(id, code, msg)
      // regardless of msg content, so an unset msg propagates as "".
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);
      Response response = new Response
      {
        Id = 7,
        Code = 1001
        // Msg intentionally unset -> defaults to ""
      };

      ApiCallbackDecoderUtil.ProcessError(decoder, response);

      Assert.That(cb.CodedErrors, Has.Count.EqualTo(1));
      Assert.That(cb.CodedErrors[0].id, Is.EqualTo(7));
      Assert.That(cb.CodedErrors[0].code, Is.EqualTo(1001));
      Assert.That(cb.CodedErrors[0].msg, Is.EqualTo(""));
    }

    /// <summary>
    /// Kick-out code (TigerApiCode.CONNECTION_KICK_OUT_ERROR.Code == 4001):
    /// ProcessError closes the PushClient connection (CloseConnect(false) is a no-op
    /// on an uninitialized singleton) and invokes ConnectionKickout, then returns
    /// without falling through to Error(id, code, msg).
    /// </summary>
    [Test]
    public void ProcessError_WhenKickOutCode_InvokesConnectionKickout()
    {
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);
      Response response = new Response
      {
        Id = 9,
        Code = 4001,
        Msg = "kick out by a new connection"
      };

      ApiCallbackDecoderUtil.ProcessError(decoder, response);

      Assert.That(cb.KickOuts, Has.Count.EqualTo(1));
      Assert.That(cb.KickOuts[0], Is.EqualTo("4001:kick out by a new connection"));
      // kick-out returns early -> no coded error, no plain error
      Assert.That(cb.CodedErrors, Is.Empty);
      Assert.That(cb.Errors, Is.Empty);
    }

    /// <summary>
    /// Kick-out with empty Msg: response.Msg is "" (protobuf default, never null),
    /// so errMessage becomes "" via the ?? short-circuit on a non-null empty string.
    /// </summary>
    [Test]
    public void ProcessError_WhenKickOutCodeAndEmptyMsg_UsesEmptyErrMessage()
    {
      FakeCallback cb = new FakeCallback();
      ApiCallbackDecoder decoder = new ApiCallbackDecoder(cb);
      Response response = new Response
      {
        Id = 9,
        Code = 4001
        // Msg unset -> ""
      };

      ApiCallbackDecoderUtil.ProcessError(decoder, response);

      Assert.That(cb.KickOuts, Has.Count.EqualTo(1));
      Assert.That(cb.KickOuts[0], Is.EqualTo("4001:"));
    }
  }
}
