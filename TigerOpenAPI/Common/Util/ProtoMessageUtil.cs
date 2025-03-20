﻿using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using Org.BouncyCastle.Cms;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Quote.Pb;
using static TigerOpenAPI.Quote.Pb.SocketCommon.Types;

namespace TigerOpenAPI.Common.Util
{
  public class ProtoMessageUtil
  {
    private static readonly AtomicInteger increment = new AtomicInteger(0);
    private ProtoMessageUtil()
    {
    }

    public static Request BuildConnectMessage(string tigerId, string sign,
      string version, int sendInterval, int receiveInterval, bool useFullTick = false)
    {
      Request request = new Request()
      {
        Command = Command.Connect,
        Id = (uint)increment.IncrementAndGet(),
        Connect = new Request.Types.Connect()
        {
          AcceptVersion = version,
          TigerId = tigerId,
          Sign = sign,
          SendInterval = (uint)sendInterval,
          UseFullTick = useFullTick,
          ReceiveInterval = (uint)receiveInterval
        }
      };

      return request;
    }

    public static Request BuildSendMessage()
    {
      Request request = new Request()
      {
        Command = Command.Send,
        Id = (uint)increment.IncrementAndGet()
      };
      return request;
    }

    public static Request BuildHeartBeatMessage()
    {
      Request request = new Request()
      {
        Command = Command.Heartbeat,
        Id = (uint)increment.IncrementAndGet()
      };
      return request;
    }

    public static Request BuildSubscribeMessage(Subject subject)
    {
      Request request = new Request()
      {
        Command = Command.Subscribe,
        Id = (uint)increment.IncrementAndGet(),
        Subscribe = new Request.Types.Subscribe()
        {
          DataType = (DataType)System.Enum.Parse(typeof(DataType), subject.ToString())
        }
      };
      return request;
    }

    public static Request BuildSubscribeMessage(string? account, Subject subject)
    {
      Request request = new Request()
      {
        Command = Command.Subscribe,
        Id = (uint)increment.IncrementAndGet(),
        Subscribe = new Request.Types.Subscribe()
        {
          DataType = (DataType)System.Enum.Parse(typeof(DataType), subject.ToString())
        }
      };
      if (!string.IsNullOrWhiteSpace(account))
        request.Subscribe.Account = account;
      return request;
    }

    public static Request BuildSubscribeMessage(ISet<string> symbols, QuoteSubject subject)
    {
      Request request = new Request()
      {
        Command = Command.Subscribe,
        Id = (uint)increment.IncrementAndGet(),
        Subscribe = new Request.Types.Subscribe()
        {
          DataType = (DataType)System.Enum.Parse(typeof(DataType), subject.ToString()),
          Symbols = symbols is null ? string.Empty : string.Join(',', symbols)
        }
      };
      return request;
    }

    public static Request BuildSubscribeMessage(Market market, QuoteSubject subject, ISet<string>? indicatorNames = null)
    {
      Request request = new Request()
      {
        Command = Command.Subscribe,
        Id = (uint)increment.IncrementAndGet(),
        Subscribe = new Request.Types.Subscribe()
        {
          DataType = (DataType)System.Enum.Parse(typeof(DataType), subject.ToString()),
          Market = market.ToString(),
          Symbols = indicatorNames is null ? string.Empty : string.Join(',', indicatorNames)
        }
      };
      return request;
    }

    public static Request BuildUnSubscribeMessage(Subject subject)
    {
      Request request = new Request()
      {
        Command = Command.Unsubscribe,
        Id = (uint)increment.IncrementAndGet(),
        Subscribe = new Request.Types.Subscribe()
        {
          DataType = (DataType)System.Enum.Parse(typeof(DataType), subject.ToString())
        }
      };
      return request;
    }

    public static Request BuildUnSubscribeMessage(ISet<string>? symbols, QuoteSubject subject)
    {
      Request request = new Request()
      {
        Command = Command.Unsubscribe,
        Id = (uint)increment.IncrementAndGet(),
        Subscribe = new Request.Types.Subscribe()
        {
          DataType = (DataType)System.Enum.Parse(typeof(DataType), subject.ToString()),
          Symbols = symbols is null ? string.Empty : string.Join(',', symbols)
        }
      };
      return request;
    }

    public static Request BuildUnSubscribeMessage(Market market, QuoteSubject subject, ISet<string>? indicatorNames = null)
    {
      Request request = new Request()
      {
        Command = Command.Unsubscribe,
        Id = (uint)increment.IncrementAndGet(),
        Subscribe = new Request.Types.Subscribe()
        {
          DataType = (DataType)System.Enum.Parse(typeof(DataType), subject.ToString()),
          Market = market.ToString(),
          Symbols = indicatorNames is null ? string.Empty : string.Join(',', indicatorNames)
        }
      };
      return request;
    }

    public static Request BuildDisconnectMessage()
    {
      Request request = new Request()
      {
        Command = Command.Disconnect,
        Id = (uint)increment.IncrementAndGet()
      };
      return request;
    }
  }

  public class AtomicInteger
  {
    private int value;

    public AtomicInteger(int initialValue)
    {
      value = initialValue;
    }

    public AtomicInteger() : this(0) { }

    public int Get() => value;

    public void Set(int newValue)
    {
      value = newValue;
    }

    public int GetAndSet(int newValue)
    {
      while (true)
      {
        int current = Get();
        if (CompareAndSet(current, newValue))
          return current;
      }
    }

    public bool CompareAndSet(int expect, int update)
    {
      return Interlocked.CompareExchange(ref value, update, expect) == expect;
    }

    public int GetAndIncrement()
    {
      while (true)
      {
        int current = Get();
        int next = current + 1;
        if (CompareAndSet(current, next))
          return current;
      }
    }

    public int GetAndDecrement()
    {
      while (true)
      {
        int current = Get();
        int next = current - 1;
        if (CompareAndSet(current, next))
          return current;
      }
    }

    public int GetAndAdd(int delta)
    {
      while (true)
      {
        int current = Get();
        int next = current + delta;
        if (CompareAndSet(current, next))
          return current;
      }
    }

    public int IncrementAndGet()
    {
      while (true)
      {
        int current = Get();
        int next = current + 1;
        if (CompareAndSet(current, next))
          return next;
      }
    }

    public int DecrementAndGet()
    {
      while (true)
      {
        int current = Get();
        int next = current - 1;
        if (CompareAndSet(current, next))
          return next;
      }
    }

    public int AddAndGet(int delta)
    {
      while (true)
      {
        int current = Get();
        int next = current + delta;
        if (CompareAndSet(current, next))
          return next;
      }
    }

    public override string ToString()
    {
      return Convert.ToString(Get());
    }
  }
}

