using System;
using System.Collections.Generic;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Quote.Pb;
using static TigerOpenAPI.Quote.Pb.SocketCommon.Types;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for ProtoMessageUtil (protobuf message builders) and AtomicInteger.
  /// Zero network — pure message construction assertions.
  /// </summary>
  [TestFixture]
  public class ProtoMessageUtilTest
  {
    [Test]
    public void BuildConnectMessage_SetsAllFields()
    {
      var req = ProtoMessageUtil.BuildConnectMessage("tiger123", "sign456", "3", 10, 20);
      Assert.That(req, Is.Not.Null);
      Assert.That(req.Command, Is.EqualTo(Command.Connect));
      Assert.That(req.Id, Is.GreaterThan(0));
      Assert.That(req.Connect.TigerId, Is.EqualTo("tiger123"));
      Assert.That(req.Connect.Sign, Is.EqualTo("sign456"));
      Assert.That(req.Connect.AcceptVersion, Is.EqualTo("3"));
      Assert.That(req.Connect.SendInterval, Is.EqualTo(10u));
      Assert.That(req.Connect.ReceiveInterval, Is.EqualTo(20u));
      Assert.That(req.Connect.SdkVersion, Does.StartWith("csharp-"));
    }

    [Test]
    public void BuildConnectMessage_UseFullTick_DefaultFalse()
    {
      var req = ProtoMessageUtil.BuildConnectMessage("id", "sign", "3", 5, 5);
      Assert.That(req.Connect.UseFullTick, Is.False);
    }

    [Test]
    public void BuildConnectMessage_UseFullTick_True()
    {
      var req = ProtoMessageUtil.BuildConnectMessage("id", "sign", "3", 5, 5, true);
      Assert.That(req.Connect.UseFullTick, Is.True);
    }

    [Test]
    public void BuildSendMessage_SetsCommand()
    {
      var req = ProtoMessageUtil.BuildSendMessage();
      Assert.That(req.Command, Is.EqualTo(Command.Send));
      Assert.That(req.Id, Is.GreaterThan(0));
    }

    [Test]
    public void BuildHeartBeatMessage_SetsCommand()
    {
      var req = ProtoMessageUtil.BuildHeartBeatMessage();
      Assert.That(req.Command, Is.EqualTo(Command.Heartbeat));
      Assert.That(req.Id, Is.GreaterThan(0));
    }

    [Test]
    public void BuildSubscribeMessage_Subject_SetsCommand()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage(Subject.Asset);
      Assert.That(req.Command, Is.EqualTo(Command.Subscribe));
      Assert.That(req.Id, Is.GreaterThan(0));
    }

    [Test]
    public void BuildSubscribeMessage_AccountSubject_SetsAccount()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage("U123456", Subject.Asset);
      Assert.That(req.Command, Is.EqualTo(Command.Subscribe));
      Assert.That(req.Subscribe.Account, Is.EqualTo("U123456"));
    }

    [Test]
    public void BuildSubscribeMessage_AccountSubject_NullAccount_OmitsAccount()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage(null, Subject.Asset);
      Assert.That(req.Subscribe.Account, Is.EqualTo(string.Empty));
    }

    [Test]
    public void BuildSubscribeMessage_Symbols_SetsSymbols()
    {
      var symbols = new HashSet<string> { "AAPL", "GOOG" };
      var req = ProtoMessageUtil.BuildSubscribeMessage(symbols, QuoteSubject.Quote);
      Assert.That(req.Command, Is.EqualTo(Command.Subscribe));
      Assert.That(req.Subscribe.Symbols, Does.Contain("AAPL"));
      Assert.That(req.Subscribe.Symbols, Does.Contain("GOOG"));
    }

    [Test]
    public void BuildSubscribeMessage_Market_SetsMarket()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage(Market.US, QuoteSubject.Quote);
      Assert.That(req.Command, Is.EqualTo(Command.Subscribe));
      Assert.That(req.Subscribe.Market, Is.EqualTo("US"));
    }

    [Test]
    public void BuildSubscribeMessage_MarketWithIndicators()
    {
      var indicators = new HashSet<string> { "MA5", "MA10" };
      var req = ProtoMessageUtil.BuildSubscribeMessage(Market.US, QuoteSubject.Quote, indicators);
      Assert.That(req.Subscribe.Market, Is.EqualTo("US"));
      Assert.That(req.Subscribe.Symbols, Does.Contain("MA5"));
      Assert.That(req.Subscribe.Symbols, Does.Contain("MA10"));
    }

    [Test]
    public void BuildUnSubscribeMessage_Subject_SetsCommand()
    {
      var req = ProtoMessageUtil.BuildUnSubscribeMessage(Subject.Asset);
      Assert.That(req.Command, Is.EqualTo(Command.Unsubscribe));
    }

    [Test]
    public void BuildUnSubscribeMessage_Symbols_SetsSymbols()
    {
      var symbols = new HashSet<string> { "AAPL" };
      var req = ProtoMessageUtil.BuildUnSubscribeMessage(symbols, QuoteSubject.Quote);
      Assert.That(req.Command, Is.EqualTo(Command.Unsubscribe));
      Assert.That(req.Subscribe.Symbols, Does.Contain("AAPL"));
    }

    [Test]
    public void BuildUnSubscribeMessage_Market_SetsMarket()
    {
      var req = ProtoMessageUtil.BuildUnSubscribeMessage(Market.HK, QuoteSubject.Quote);
      Assert.That(req.Command, Is.EqualTo(Command.Unsubscribe));
      Assert.That(req.Subscribe.Market, Is.EqualTo("HK"));
    }

    [Test]
    public void BuildDisconnectMessage_SetsCommand()
    {
      var req = ProtoMessageUtil.BuildDisconnectMessage();
      Assert.That(req.Command, Is.EqualTo(Command.Disconnect));
      Assert.That(req.Id, Is.GreaterThan(0));
    }

    [Test]
    public void BuildMessages_IncrementingIds()
    {
      var req1 = ProtoMessageUtil.BuildHeartBeatMessage();
      var req2 = ProtoMessageUtil.BuildHeartBeatMessage();
      Assert.That(req2.Id, Is.GreaterThan(req1.Id));
    }

    // --- AtomicInteger ---

    [Test]
    public void AtomicInteger_DefaultConstructor_StartsAt0()
    {
      var ai = new AtomicInteger();
      Assert.That(ai.Get(), Is.EqualTo(0));
    }

    [Test]
    public void AtomicInteger_InitialValueConstructor()
    {
      var ai = new AtomicInteger(42);
      Assert.That(ai.Get(), Is.EqualTo(42));
    }

    [Test]
    public void AtomicInteger_SetChangesValue()
    {
      var ai = new AtomicInteger(0);
      ai.Set(99);
      Assert.That(ai.Get(), Is.EqualTo(99));
    }

    [Test]
    public void AtomicInteger_GetAndSet_ReturnsOldSetsNew()
    {
      var ai = new AtomicInteger(10);
      int old = ai.GetAndSet(20);
      Assert.That(old, Is.EqualTo(10));
      Assert.That(ai.Get(), Is.EqualTo(20));
    }

    [Test]
    public void AtomicInteger_CompareAndSet_MatchingReturnsTrue()
    {
      var ai = new AtomicInteger(5);
      bool result = ai.CompareAndSet(5, 10);
      Assert.That(result, Is.True);
      Assert.That(ai.Get(), Is.EqualTo(10));
    }

    [Test]
    public void AtomicInteger_CompareAndSet_NonMatchingReturnsFalse()
    {
      var ai = new AtomicInteger(5);
      bool result = ai.CompareAndSet(99, 10);
      Assert.That(result, Is.False);
      Assert.That(ai.Get(), Is.EqualTo(5));
    }

    [Test]
    public void AtomicInteger_GetAndIncrement_ReturnsOldIncrements()
    {
      var ai = new AtomicInteger(10);
      int old = ai.GetAndIncrement();
      Assert.That(old, Is.EqualTo(10));
      Assert.That(ai.Get(), Is.EqualTo(11));
    }

    [Test]
    public void AtomicInteger_GetAndDecrement_ReturnsOldDecrements()
    {
      var ai = new AtomicInteger(10);
      int old = ai.GetAndDecrement();
      Assert.That(old, Is.EqualTo(10));
      Assert.That(ai.Get(), Is.EqualTo(9));
    }

    [Test]
    public void AtomicInteger_GetAndAdd_ReturnsOldAddsDelta()
    {
      var ai = new AtomicInteger(10);
      int old = ai.GetAndAdd(5);
      Assert.That(old, Is.EqualTo(10));
      Assert.That(ai.Get(), Is.EqualTo(15));
    }

    [Test]
    public void AtomicInteger_IncrementAndGet_ReturnsNew()
    {
      var ai = new AtomicInteger(10);
      int result = ai.IncrementAndGet();
      Assert.That(result, Is.EqualTo(11));
      Assert.That(ai.Get(), Is.EqualTo(11));
    }

    [Test]
    public void AtomicInteger_DecrementAndGet_ReturnsNew()
    {
      var ai = new AtomicInteger(10);
      int result = ai.DecrementAndGet();
      Assert.That(result, Is.EqualTo(9));
    }

    [Test]
    public void AtomicInteger_AddAndGet_ReturnsNew()
    {
      var ai = new AtomicInteger(10);
      int result = ai.AddAndGet(7);
      Assert.That(result, Is.EqualTo(17));
    }

    [Test]
    public void AtomicInteger_ToString_ReturnsStringValue()
    {
      var ai = new AtomicInteger(42);
      Assert.That(ai.ToString(), Is.EqualTo("42"));
    }
  }
}
