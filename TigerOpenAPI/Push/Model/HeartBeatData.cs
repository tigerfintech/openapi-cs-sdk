using System;
using Newtonsoft.Json;

namespace TigerOpenAPI.Push.Model
{
  public class HeartBeatData
  {
    private int clientSendInterval = 10000;
    private int clientReceiveInterval = 10000;
    public const int CLIENT_SEND_INTERVAL_MIN = 10000;
    public const int CLIENT_RECEIVE_INTERVAL_MIN = 10000;

    /**
     * The minimum interval(ms) for the client to send heartbeats
     */
    [JsonProperty(PropertyName = "sendInterval")]
    public int SendInterval {
      get => clientSendInterval;
      set {
        clientSendInterval = value >= CLIENT_SEND_INTERVAL_MIN ? value : CLIENT_SEND_INTERVAL_MIN;
      }
    }
    /**
     * The client expects to receive the heartbeat interval(ms) of the server
     */
    [JsonProperty(PropertyName = "receiveInterval")]
    public int ReceiveInterval {
      get => clientReceiveInterval;
      set {
        clientReceiveInterval = value >= CLIENT_RECEIVE_INTERVAL_MIN ? value : CLIENT_RECEIVE_INTERVAL_MIN;
      }
    }

    public HeartBeatData()
    {
    }
  }
}

