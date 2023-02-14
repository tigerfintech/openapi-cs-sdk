using System;
namespace TigerOpenAPI.Push
{
  public interface IApiComposeCallback : ISubscribeApiCallback
  {
    void Error(string errorMsg);

    void Error(int id, int errorCode, string errorMsg);

    void ConnectionClosed();

    /**
     * kicked out by a new connection. recommend to terminate the connection.
     * The same tigerId has only one active connection
     * @param errorCode errorCode
     * @param errorMsg errorMsg
     */
    void ConnectionKickout(int errorCode, string errorMsg);

    void ConnectionAck();

    /**
     * @param serverSendInterval The minimum interval(ms) for the client to send heartbeats
     * @param serverReceiveInterval The client expects to receive the heartbeat interval(ms) of the server
     */
    void ConnectionAck(int serverSendInterval, int serverReceiveInterval);

    void HearBeat(string heartBeatContent);

    void ServerHeartBeatTimeOut(string channelId);
  }
}

