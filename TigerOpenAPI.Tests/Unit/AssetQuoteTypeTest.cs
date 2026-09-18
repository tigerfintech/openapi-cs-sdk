using System;
using System.Linq;
using Google.Protobuf;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Quote.Pb;
using TigerOpenAPI.Trade.Model;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// asset_quote_type coverage on both channels:
  ///
  ///  1. HTTP pull side — assets / prime_assets / aggregate_assets / positions carry
  ///     the optional "asset_quote_type" field (enum name, not ordinal).
  ///     Regression: the property must be nullable. TigerClient.JsonSet sets
  ///     DefaultValueHandling.Ignore, so a non-nullable enum drops ETH (ordinal 0)
  ///     from the request even when the user sets it explicitly.
  ///
  ///  2. WebSocket push side — Request.Subscribe.assetQuoteType (field number 5,
  ///     optional string) carries the enum name for Asset / Position subscriptions.
  ///
  /// Zero network — pure serialization assertions.
  /// </summary>
  [TestFixture]
  public class AssetQuoteTypeTest
  {
    private static string Serialize(object model) =>
        JsonConvert.SerializeObject(model, TigerClient.JsonSet);

    // -----------------------------------------------------------------
    // 1. HTTP pull side
    // -----------------------------------------------------------------

    [Test]
    public void GlobalAssetsModel_AssetQuoteType_SerializedAsEnumName()
    {
      var model = new GlobalAssetsModel { Account = "U123456", AssetQuoteType = AssetQuoteType.OVERNIGHT };
      Assert.That(Serialize(model), Does.Contain("\"asset_quote_type\":\"OVERNIGHT\""));
    }

    [Test]
    public void GlobalAssetsModel_AssetQuoteTypeUnset_FieldOmitted()
    {
      var model = new GlobalAssetsModel { Account = "U123456" };
      Assert.That(Serialize(model), Does.Not.Contain("asset_quote_type"));
    }

    [Test]
    public void PrimeAssetsModel_AssetQuoteType_SerializedAsEnumName()
    {
      var model = new PrimeAssetsModel { Account = "U123456", AssetQuoteType = AssetQuoteType.RTH };
      Assert.That(Serialize(model), Does.Contain("\"asset_quote_type\":\"RTH\""));
    }

    [Test]
    public void PrimeAssetsModel_AssetQuoteTypeUnset_FieldOmitted()
    {
      var model = new PrimeAssetsModel { Account = "U123456" };
      Assert.That(Serialize(model), Does.Not.Contain("asset_quote_type"));
    }

    [Test]
    public void AggregateAssetModel_AssetQuoteType_SerializedAsEnumName()
    {
      var model = new AggregateAssetModel("U123456", "SEC") { AssetQuoteType = AssetQuoteType.RTH };
      Assert.That(Serialize(model), Does.Contain("\"asset_quote_type\":\"RTH\""));
    }

    [Test]
    public void AggregateAssetModel_AssetQuoteTypeUnset_FieldOmitted()
    {
      var model = new AggregateAssetModel("U123456", "SEC");
      Assert.That(Serialize(model), Does.Not.Contain("asset_quote_type"));
    }

    [Test]
    public void PositionsModel_AssetQuoteType_SerializedAsEnumName()
    {
      var model = new PositionsModel { Account = "U123456", AssetQuoteType = AssetQuoteType.OVERNIGHT };
      Assert.That(Serialize(model), Does.Contain("\"asset_quote_type\":\"OVERNIGHT\""));
    }

    [Test]
    public void PositionsModel_AssetQuoteTypeUnset_FieldOmitted()
    {
      var model = new PositionsModel { Account = "U123456" };
      Assert.That(Serialize(model), Does.Not.Contain("asset_quote_type"));
    }

    /// <summary>
    /// ETH is ordinal 0. With a non-nullable enum property, DefaultValueHandling.Ignore
    /// drops the field and the server falls back to its own default — the bug this
    /// nullable property fixes. Covers every model carrying asset_quote_type.
    /// </summary>
    [Test]
    public void AllModels_EthExplicitlySet_IsSent()
    {
      Assert.Multiple(() =>
      {
        Assert.That(Serialize(new GlobalAssetsModel { AssetQuoteType = AssetQuoteType.ETH }),
            Does.Contain("\"asset_quote_type\":\"ETH\""), "assets");
        Assert.That(Serialize(new PrimeAssetsModel { AssetQuoteType = AssetQuoteType.ETH }),
            Does.Contain("\"asset_quote_type\":\"ETH\""), "prime_assets");
        Assert.That(Serialize(new AggregateAssetModel { AssetQuoteType = AssetQuoteType.ETH }),
            Does.Contain("\"asset_quote_type\":\"ETH\""), "aggregate_assets");
        Assert.That(Serialize(new PositionsModel { AssetQuoteType = AssetQuoteType.ETH }),
            Does.Contain("\"asset_quote_type\":\"ETH\""), "positions");
      });
    }

    /// <summary>
    /// analytics_asset / prime_analytics_assets do not accept asset_quote_type —
    /// the server ignores it, so the SDK must not offer a dead parameter.
    /// </summary>
    [Test]
    public void PrimeAnalyticsAssetModel_HasNoAssetQuoteType()
    {
      Assert.That(typeof(PrimeAnalyticsAssetModel).GetProperty("AssetQuoteType"), Is.Null);
    }

    // -----------------------------------------------------------------
    // 2. WebSocket push side
    // -----------------------------------------------------------------

    [Test]
    public void BuildSubscribeMessage_AssetQuoteType_SetsEnumName()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage("U123456", Subject.Asset, AssetQuoteType.RTH);
      Assert.That(req.Subscribe.HasAssetQuoteType, Is.True);
      Assert.That(req.Subscribe.AssetQuoteType, Is.EqualTo("RTH"));
      Assert.That(req.Subscribe.Account, Is.EqualTo("U123456"));
    }

    [Test]
    public void BuildSubscribeMessage_AssetQuoteTypeEth_IsSent()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage(null, Subject.Position, AssetQuoteType.ETH);
      Assert.That(req.Subscribe.HasAssetQuoteType, Is.True);
      Assert.That(req.Subscribe.AssetQuoteType, Is.EqualTo("ETH"));
    }

    [Test]
    public void BuildSubscribeMessage_NullAssetQuoteType_FieldNotSet()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage("U123456", Subject.Asset, null);
      Assert.That(req.Subscribe.HasAssetQuoteType, Is.False);
    }

    [Test]
    public void BuildSubscribeMessage_LegacyOverload_KeepsFieldUnset()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage("U123456", Subject.Asset);
      Assert.That(req.Subscribe.HasAssetQuoteType, Is.False);
      Assert.That(req.Subscribe.Account, Is.EqualTo("U123456"));
    }

    /// <summary>
    /// Wire-format assertion: assetQuoteType must be field number 5, wire type 2
    /// (tag byte 0x2A), holding the raw enum name.
    /// </summary>
    [Test]
    public void SubscribeMessage_AssetQuoteType_WireFormatIsField5LengthDelimited()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage(null, Subject.Asset, AssetQuoteType.OVERNIGHT);
      byte[] bytes = req.Subscribe.ToByteArray();

      byte[] expected = new byte[] { 0x2A, 0x09 }
          .Concat(System.Text.Encoding.UTF8.GetBytes("OVERNIGHT"))
          .ToArray();
      Assert.That(IndexOf(bytes, expected), Is.GreaterThanOrEqualTo(0),
          "expected tag 0x2A (field 5, length-delimited) followed by the enum name");

      // round-trip through the parser
      var parsed = Request.Types.Subscribe.Parser.ParseFrom(bytes);
      Assert.That(parsed.AssetQuoteType, Is.EqualTo("OVERNIGHT"));
      Assert.That(Request.Types.Subscribe.AssetQuoteTypeFieldNumber, Is.EqualTo(5));
    }

    [Test]
    public void SubscribeMessage_NoAssetQuoteType_NoField5OnWire()
    {
      var req = ProtoMessageUtil.BuildSubscribeMessage("U123456", Subject.Asset);
      byte[] bytes = req.Subscribe.ToByteArray();
      Assert.That(bytes.Contains((byte)0x2A), Is.False,
          "field 5 must not appear on the wire when assetQuoteType is unset");
    }

    private static int IndexOf(byte[] haystack, byte[] needle)
    {
      for (int i = 0; i + needle.Length <= haystack.Length; i++)
      {
        bool match = true;
        for (int j = 0; j < needle.Length; j++)
        {
          if (haystack[i + j] != needle[j]) { match = false; break; }
        }
        if (match) return i;
      }
      return -1;
    }
  }
}
