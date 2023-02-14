using System;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Push
{
  public class IdleTriggerHandler : ChannelHandlerAdapter
  {
    private ApiCallbackDecoder apiCallbackDecoder;

    public IdleTriggerHandler(ApiCallbackDecoder decoder)
    {
      this.apiCallbackDecoder = decoder;
    }

    /// <summary>
    /// heart beat trigger
    /// </summary>
    /// <param name="context"></param>
    /// <param name="evt"></param>
    public override void UserEventTriggered(IChannelHandlerContext context, object evt)
    {
      var eventState = evt as IdleStateEvent;
      if (eventState != null)
      {
        if (IdleState.WriterIdle == eventState.State)
        {
          context.Channel.WriteAndFlushAsync(ProtoMessageUtil.buildHeartBeatMessage());
        }
        else if (IdleState.ReaderIdle == eventState.State)
        {
          ApiLogger.Warn("server timeout:{}", context.Channel.Id.AsShortText());
          if (!NetworkUtil.TryConnectUri(new Uri(PushClient.GetInstance().GetUrl())))
          {
            context.CloseAsync();
          }
          if (this.apiCallbackDecoder != null)
          {
            this.apiCallbackDecoder.ServerHeartBeatTimeOut(context.Channel.Id.AsShortText());
          }
        }
      }
      else
      {
        base.UserEventTriggered(context, evt);
      }
    }
  }
}

