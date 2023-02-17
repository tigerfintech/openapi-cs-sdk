using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Threading.Channels;
using DotNetty.Codecs;
using DotNetty.Codecs.Protobuf;
using DotNetty.Common.Utilities;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Org.BouncyCastle.Utilities.Net;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Push;
using TigerOpenAPI.Push.Model;
using TigerOpenAPI.Quote.Pb;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TigerOpenAPI.Push
{
  public class PushClient : ISubscribeAsyncApi
  {
    public const string SOCKET_ENCODER = "socketEncoder";
    public const string SOCKET_DECODER = "socketDecoder";
    public const int CONNECT_TIMEOUT_MS = 5000;
    public const int RETRY_INTERVAL_MS = 2000;

    private TigerConfig tigerConfig;
    private string url;
    private ApiAuthentication authentication;
    private HeartBeatData heartBeatData = new HeartBeatData();
    private IApiComposeCallback apiComposeCallback;

    private volatile CountdownEvent connectCountdown = new CountdownEvent(1);
    internal volatile bool isInitial = false;

    private IEventLoopGroup group;
    private Bootstrap bootstrap;
    private volatile IChannel? channel;

    private PushClient() { }
    private static class SingletonInner
    {
      public static readonly PushClient INSTANCE = new PushClient();
    }
    public static PushClient GetInstance() => SingletonInner.INSTANCE;

    public PushClient Config(TigerConfig config)
    {
      ConfigUtil.LoadConfigFile(config);
      this.tigerConfig = config;
      this.authentication = ApiAuthentication.build(config.TigerId, config.PrivateKey);
      GetSocketUrL();
      return this;
    }

    public PushClient ApiComposeCallback(in IApiComposeCallback apiComposeCallback)
    {
      this.apiComposeCallback = apiComposeCallback;
      return this;
    }

    public PushClient HeartBeatData(in HeartBeatData heartBeatData)
    {
      if (heartBeatData is not null)
        this.heartBeatData = heartBeatData;
      return this;
    }

    private Uri GetSocketUrL()
    {
      Protocol protocol = tigerConfig.IsSslSocket ? Protocol.SECURE_SOCKET : Protocol.WEB_SOCKET;
      Dictionary<UriType, string> uriDict = NetworkUtil.GetServerAddress(protocol, tigerConfig.License, tigerConfig.Environment);
      string newUrl = (uriDict.ContainsKey(UriType.SOCKET) && !string.IsNullOrWhiteSpace(uriDict[UriType.SOCKET]))
        ? uriDict[UriType.SOCKET] : uriDict.ContainsKey(UriType.COMMON) ? uriDict[UriType.COMMON] : string.Empty;
      this.url = string.IsNullOrWhiteSpace(newUrl) ? this.url : newUrl;
      return new Uri(this.url);
    }

    private void CheckArgument()
    {
      if (this.authentication == null)
      {
        throw new ArgumentException("authentication info is missing.");
      }
      if (this.apiComposeCallback == null)
      {
        throw new ArgumentException("apiComposeCallback is missing.");
      }
      if (connectCountdown.CurrentCount == 0)
      {
        connectCountdown.Reset();
      }
    }

    private void Init()
    {
      if (isInitial) {
        return;
      }
      Uri uri = GetSocketUrL();
      ApiLogger.Info($"Init url:{url}");

      group = new MultithreadEventLoopGroup();
      bootstrap = new Bootstrap()
        .RemoteAddress(new DnsEndPoint(uri.Host, uri.Port))
        .Group(group)
        .Option(ChannelOption.TcpNodelay, true)
        .Option(ChannelOption.SoKeepalive, true)
        .Channel<TcpSocketChannel>()
        .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
        {
          IChannelPipeline pipeline = channel.Pipeline;
          if (tigerConfig.IsSslSocket)
          {
            pipeline.AddLast(TigerApiConstants.SSL_HANDLER_NAME, new TlsHandler(
              stream => new SslStream(stream, false, (sender, certificate, chain, errors) => true),
                new ClientTlsSettings(uri.Host)));
          }
          ProtoSocketHandler handler = new ProtoSocketHandler(authentication, apiComposeCallback, heartBeatData);

          pipeline.AddLast(SOCKET_DECODER, new ProtobufVarint32FrameDecoder());
          pipeline.AddLast(new ProtobufDecoder(Response.Parser));
          pipeline.AddLast(new ProtobufVarint32LengthFieldPrepender());
          pipeline.AddLast(SOCKET_ENCODER, new ProtobufEncoder());
          pipeline.AddLast("webSocketHandler", handler);
        }));
      isInitial = true;
    }

    /**
     * create the connection (The same tigerId has only one active connection)
     */
    public bool Connect()
    {
      Monitor.Enter(this);
      try
      {
        if (IsConnected())
        {
          return true;
        }
        CheckArgument();
        DoConnect();
        ApiLogger.Info($"{(IsConnected() ? "Success" : "Failed")} connect to server," +
          $" channel is: {(channel?.Id?.AsShortText() ?? string.Empty)}");
      }
      catch (Exception e)
      {
        ApiLogger.Error("Failed connect to server, cause: ", e);
      }
      finally { Monitor.Exit(this); }
      return IsConnected();
    }

    private void DoConnect()
    {
      bool connected = false;
      int count = 0;
      do
      {
        try
        {
          count++;
          Init();

          Task<IChannel> task = bootstrap.ConnectAsync();
          int millisecondsTimeout = CONNECT_TIMEOUT_MS;
          while (!task.IsCompleted && millisecondsTimeout > 0)
          {
            millisecondsTimeout -= 100;
            Thread.Sleep(100);
          }

          if (task.IsCompletedSuccessfully)
          {
            channel = task.Result;
            connected = true;
          }
          else
          {
            if (count % 10 == 0 && !NetworkUtil.TryConnectUri(new Uri(this.url)))
            {
              CloseConnect(false);
            }
            ApiLogger.Warn($"fail count:{count}, reconnect after {RETRY_INTERVAL_MS}ms, taskId:{task.Id}, isDone:{task.IsCompleted}," +
              $" status:{task.Status} {task.Exception?.Message}");
          }
        }
        catch (Exception e)
        {
          ApiLogger.Warn($"fail count:{count}, reconnect after {RETRY_INTERVAL_MS}ms, {e.Message}");
        }
        finally
        {
          if (!connected)
            Thread.Sleep(RETRY_INTERVAL_MS);
        }
      } while (!connected);
      connectCountdown.Wait(CONNECT_TIMEOUT_MS);
    }

    public void Disconnect() => CloseConnect(true);

    internal void CloseConnect(bool sendDisconnectCommand)
    {
      ApiLogger.Info("CloseConnect start");
      Monitor.Enter(this);
      if (sendDisconnectCommand)
      {
        SendDisconnectData();
      }
      try
      {
        if (channel != null && channel.IsWritable)
        {
          channel.DisconnectAsync();
        }
        channel = null;
      }
      catch (Exception e)
      {
        ApiLogger.Error(e.Message, e);
      }
      try
      {
        if (group != null && !group.IsShutdown)
        {
          group.ShutdownGracefullyAsync();
        }
      }
      catch (Exception e)
      {
        ApiLogger.Error(e.Message);
      }
      finally { isInitial = false; }
      Monitor.Exit(this);
      ApiLogger.Info("CloseConnect end");
    }

    internal void ConnectSucceeded() => connectCountdown.Signal();

    public bool IsConnected()
    {
      return channel != null && connectCountdown.CurrentCount == 0 && channel.Active;
    }

    public string GetUrl() => this.url;

    internal void SetVersion(string version) => this.authentication.Version = version;

    private void SendDisconnectData()
    {
      if (channel is null || !IsConnected())
      {
        NotConnect();
        return;
      }

      Request request = ProtoMessageUtil.buildDisconnectMessage();
      Task task = channel.WriteAndFlushAsync(request);
      try
      {
        task.Wait();
        ApiLogger.Info($"sendDisconnect finished, tigerId:{tigerConfig.TigerId}");
      }
      catch (Exception e)
      {
        ApiLogger.Error(e, $"sendDisconnect error, tigerId:{tigerConfig.TigerId}");
      }
    }

    public uint Subscribe(Subject subject)
    {
      return Subscribe(subject, null);
    }

    public uint Subscribe(Subject subject, string? account)
    {
      if (channel is null || !IsConnected())
      {
        NotConnect();
        return 0;
      }
      Request request = ProtoMessageUtil.buildSubscribeMessage(account, subject);

      channel.WriteAndFlushAsync(request).Wait();
      return request.Id;
    }

    public uint CancelSubscribe(Subject subject)
    {
      if (channel is null || !IsConnected())
      {
        NotConnect();
        return 0;
      }
      Request request = ProtoMessageUtil.buildUnSubscribeMessage(subject);
      channel.WriteAndFlushAsync(request).Wait();
      return request.Id;
    }

    public uint SubscribeQuote(ISet<string> symbols)
    {
      return SubscribeQuote(symbols, QuoteSubject.Quote);
    }

    public uint CancelSubscribeQuote(ISet<string> symbols)
    {
      return CancelSubscribeQuote(symbols, QuoteSubject.Quote);
    }

    public uint SubscribeTradeTick(ISet<string> symbols)
    {
      return SubscribeQuote(symbols, QuoteSubject.TradeTick);
    }

    public uint CancelSubscribeTradeTick(ISet<string> symbols)
    {
      return CancelSubscribeQuote(symbols, QuoteSubject.TradeTick);
    }

    public uint SubscribeOption(ISet<string> symbols)
    {
      return SubscribeQuote(symbols, QuoteSubject.Option);
    }

    public uint CancelSubscribeOption(ISet<string> symbols)
    {
      return CancelSubscribeQuote(symbols, QuoteSubject.Option);
    }

    public uint SubscribeFuture(ISet<string> symbols)
    {
      return SubscribeQuote(symbols, QuoteSubject.Future);
    }

    public uint CancelSubscribeFuture(ISet<string> symbols)
    {
      return CancelSubscribeQuote(symbols, QuoteSubject.Future);
    }

    public uint SubscribeDepthQuote(ISet<string> symbols)
    {
      return SubscribeQuote(symbols, QuoteSubject.QuoteDepth);
    }

    public uint CancelSubscribeDepthQuote(ISet<string> symbols)
    {
      return CancelSubscribeQuote(symbols, QuoteSubject.QuoteDepth);
    }

    private uint SubscribeQuote(ISet<string> symbols, QuoteSubject subject)
    {
      if (channel is null || !IsConnected())
      {
        NotConnect();
        return 0;
      }
      Request request = ProtoMessageUtil.buildSubscribeMessage(symbols, subject);
      channel.WriteAndFlushAsync(request).Wait();
      ApiLogger.Info("send subscribe [{}] message, symbols:{}", subject, symbols);
      return request.Id;
    }

    private uint CancelSubscribeQuote(ISet<string> symbols, QuoteSubject subject)
    {
      if (channel is null || !IsConnected())
      {
        NotConnect();
        return 0;
      }

      Request request = ProtoMessageUtil.buildUnSubscribeMessage(symbols, subject);
      channel.WriteAndFlushAsync(request).Wait();
      ApiLogger.Info("send cancel subscribe [{}] message, symbols:{}.", subject, symbols);
      return request.Id;
    }

    public uint SubscribeMarketQuote(Market market, QuoteSubject subject)
    {
      if (channel is null || !IsConnected())
      {
        NotConnect();
        return 0;
      }

      Request request = ProtoMessageUtil.buildSubscribeMessage(market, subject);
      channel.WriteAndFlushAsync(request).Wait();

      ApiLogger.Info("send subscribe [{}] message, market:{}", subject, market);
      return request.Id;
    }

    public uint CancelSubscribeMarketQuote(Market market, QuoteSubject subject)
    {
      if (channel is null || !IsConnected())
      {
        NotConnect();
        return 0;
      }

      Request request = ProtoMessageUtil.buildUnSubscribeMessage(market, subject);
      channel.WriteAndFlushAsync(request).Wait();

      ApiLogger.Info("send cancel subscribe [{}] message, market:{}", subject, market);
      return request.Id;
    }

    public uint GetSubscribedSymbols()
    {
      if (channel is null || !IsConnected())
      {
        NotConnect();
        return 0;
      }
      ApiLogger.Info("send getSubscribedSymbols message");

      Request request = ProtoMessageUtil.buildSendMessage();
      channel.WriteAndFlushAsync(request).Wait();
      return request.Id;
    }

    private void NotConnect()
    {
      ApiLogger.Info("connection is closed.");
    }
  }
}
