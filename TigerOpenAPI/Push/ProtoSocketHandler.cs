using System;
using DotNetty.Transport.Channels;
using Polly;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Push.Model;
using TigerOpenAPI.Quote.Pb;
using static TigerOpenAPI.Quote.Pb.Request.Types;

namespace TigerOpenAPI.Push
{
  public class ProtoSocketHandler : SimpleChannelInboundHandler<Response>
  {
    private ApiAuthentication authentication;
    private ApiCallbackDecoder decoder;
    private HeartBeatData heartBeatData;
    public const int HEART_BEAT_SPAN = 1000;

    public ProtoSocketHandler(ApiAuthentication authentication,
      IApiComposeCallback callback, HeartBeatData heartBeatData)
    {
      this.authentication = authentication;
      this.heartBeatData = heartBeatData;
      decoder = new ApiCallbackDecoder(callback);
    }

    public override void ChannelActive(IChannelHandlerContext context)
    {
      Request connect = ProtoMessageUtil.buildConnectMessage(authentication.TigerId, authentication.Sign,
        authentication.Version, this.heartBeatData.SendInterval + HEART_BEAT_SPAN,
        this.heartBeatData.ReceiveInterval - HEART_BEAT_SPAN);
      ApiLogger.Info($"netty channel active. channel:{context.Channel.Id.AsShortText()}" +
        $", preparing to send connect token:{connect.Connect}");

      context.WriteAndFlushAsync(connect).WaitAsync(TimeSpan.FromMilliseconds(PushClient.CONNECT_TIMEOUT_MS));
      ApiLogger.Info($"send connect token successfully. channel:{context.Channel.Id.AsShortText()}");
      base.ChannelActive(context);
    }

    public override void ChannelInactive(IChannelHandlerContext context)
    {
      ApiLogger.Info($"netty channel inactive! channel:{context.Channel.Id.AsShortText()}");
      PushClient client = PushClient.GetInstance();
      Monitor.Enter(client);
      if (client.isInitial)
      {
        ApiLogger.Info("try to reconnect...");
        new Thread( () => client.Connect() ).Start();
      }
      Monitor.Exit(client);
      base.ChannelInactive(context);
    }

    protected override void ChannelRead0(IChannelHandlerContext context, Response msg)
    {
      ApiLogger.Debug("received msg from server: {}", msg);
      try
      {
        ApiCallbackDecoderUtil.Executor(context, msg, decoder);
      }
      catch (Exception e)
      {
        ApiLogger.Error(e, "api callback fail. response:{}", msg);
      }
    }

    /// <summary>
    /// exception
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
    {
      ApiLogger.Error(exception, "handler exception caught, channel:{}", context.Channel.Id.AsShortText());
      context.CloseAsync();
    }
  }
}

