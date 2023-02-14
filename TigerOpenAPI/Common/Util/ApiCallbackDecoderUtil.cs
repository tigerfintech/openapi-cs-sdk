using System;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using DotNetty.Handlers.Timeout;
using Google.Protobuf;
using static TigerOpenAPI.Quote.Pb.Request.Types;
using System.Reflection.PortableExecutable;
using System.Text.Json.Nodes;
using TigerOpenAPI.Push;
using TigerOpenAPI.Quote.Pb;
using static TigerOpenAPI.Quote.Pb.SocketCommon.Types;
using Newtonsoft.Json;

namespace TigerOpenAPI.Common.Util
{
  public class ApiCallbackDecoderUtil
  {
    public const string IDLE_STATE_HANDLER = "idleStateHandler";
    public const string IDLE_TRIGGER_HANDLER = "idleTriggerHandler";

    private ApiCallbackDecoderUtil()
    {
    }

    public static void Executor(IChannelHandlerContext ctx, Response response, ApiCallbackDecoder decoder)
    {
      if (null == decoder || null == ctx || null == response || Command.Unknown == response.Command)
      {
        return;
      }

      switch (response.Command)
      {
        case Command.Connected:
          ApiLogger.Info("connect token validation success:{}", response);
          PushClient.GetInstance().ConnectSucceeded();
          ReceiveConnected(ctx, decoder, response.Msg);
          break;
        case Command.Heartbeat:
          decoder.ProcessHeartBeat(TigerApiConstants.HEART_BEAT);
          break;
        case Command.Message:
          decoder.Handle(response);
          break;
        case Command.Error:
          ProcessError(decoder, response);
          break;
        case Command.Disconnect:
          decoder.GetCallback()?.ConnectionClosed();
          ctx.CloseAsync();
          break;
        default:
          break;
      }
    }

    public static void ReceiveConnected(IChannelHandlerContext ctx,
      ApiCallbackDecoder decoder, string msg)
    {
      if (decoder.GetCallback() != null && !string.IsNullOrWhiteSpace(msg))
      {
        Dictionary<string, string>? dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(msg);
        if (dict is null || dict.Count == 0)
          return;
        // set version
        if (dict.TryGetValue(TigerApiConstants.VERSION, out string? version))
          PushClient.GetInstance().SetVersion(version);
        dict.TryGetValue(TigerApiConstants.HEART_BEAT, out string? value);
        if (!string.IsNullOrWhiteSpace(value))
        {
          string[] arrayValue = value.Split(",");
          if (null != arrayValue && arrayValue.Length >= 2)
          {
            int serverSendInterval = string.IsNullOrWhiteSpace(arrayValue[0]) ? 0 : Int32.Parse(arrayValue[0]);
            int serverReceiveInterval = string.IsNullOrWhiteSpace(arrayValue[1]) ? 0 : Int32.Parse(arrayValue[1]);
            if (serverSendInterval > 0 || serverReceiveInterval > 0)
            {
              if (null == ctx.Channel.Pipeline.Get(IDLE_STATE_HANDLER))
              {
                serverSendInterval =
                    serverSendInterval == 0 ? 0 : serverSendInterval + ProtoSocketHandler.HEART_BEAT_SPAN;
                serverReceiveInterval =
                    serverReceiveInterval == 0 ? 0 : serverReceiveInterval - ProtoSocketHandler.HEART_BEAT_SPAN;

                ctx.Channel.Pipeline.AddBefore(PushClient.SOCKET_ENCODER, IDLE_STATE_HANDLER,
                    new IdleStateHandler(TimeSpan.FromMilliseconds(serverSendInterval),
                    TimeSpan.FromMilliseconds(serverReceiveInterval),
                    TimeSpan.FromMilliseconds(0)));

                ctx.Channel.Pipeline.AddAfter(IDLE_STATE_HANDLER, IDLE_TRIGGER_HANDLER, new IdleTriggerHandler(decoder));
              }
            }
            decoder.GetCallback().ConnectionAck(serverSendInterval, serverReceiveInterval);
          }
          else
          {
            decoder.GetCallback().ConnectionAck();
          }
        }
        else
        {
          decoder.GetCallback().ConnectionAck();
        }
      }
    }

    public static void ProcessError(ApiCallbackDecoder decoder, Response response)
    {
      if (decoder.GetCallback() != null)
      {
        if (response.Code > 0)
        {
          try
          {
            if (response.Code == TigerApiCode.CONNECTION_KICK_OUT_ERROR.Code)
            {
              string errMessage = response.Msg ?? TigerApiCode.CONNECTION_KICK_OUT_ERROR.Message;
              ApiLogger.Info(errMessage);
              // close the connection(Do not send disconnect command)
              PushClient.GetInstance().CloseConnect(false);
              // callback
              decoder.GetCallback().ConnectionKickout(response.Code, errMessage);
              return;
            }
          }
          catch {
            // ignore...
          }
          decoder.GetCallback().Error((int)response.Id, response.Code, response.Msg);
        }
        else if (!string.IsNullOrWhiteSpace(response.Msg))
        {
          decoder.GetCallback().Error(response.Msg);
        }
        else
        {
          decoder.GetCallback().Error("unknown error");
        }
      }
    }
  }
}

