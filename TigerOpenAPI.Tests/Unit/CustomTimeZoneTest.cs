using System;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for CustomTimeZone static timezone fields and fallback dictionary.
  /// Zero network — pure timezone lookup assertions.
  /// </summary>
  [TestFixture]
  public class CustomTimeZoneTest
  {
    [Test]
    public void HK_ZONE_IsNotNull()
    {
      Assert.That(CustomTimeZone.HK_ZONE, Is.Not.Null);
      Assert.That(CustomTimeZone.HK_ZONE.Id, Is.Not.Empty);
    }

    [Test]
    public void SH_ZONE_IsNotNull()
    {
      Assert.That(CustomTimeZone.SH_ZONE, Is.Not.Null);
      Assert.That(CustomTimeZone.SH_ZONE.Id, Is.Not.Empty);
    }

    [Test]
    public void NY_ZONE_IsNotNull()
    {
      Assert.That(CustomTimeZone.NY_ZONE, Is.Not.Null);
      Assert.That(CustomTimeZone.NY_ZONE.Id, Is.Not.Empty);
    }

    [Test]
    public void CHI_ZONE_IsNotNull()
    {
      Assert.That(CustomTimeZone.CHI_ZONE, Is.Not.Null);
      Assert.That(CustomTimeZone.CHI_ZONE.Id, Is.Not.Empty);
    }

    [Test]
    public void SG_ZONE_IsNotNull()
    {
      Assert.That(CustomTimeZone.SG_ZONE, Is.Not.Null);
      Assert.That(CustomTimeZone.SG_ZONE.Id, Is.Not.Empty);
    }

    [Test]
    public void AU_ZONE_IsNotNull()
    {
      Assert.That(CustomTimeZone.AU_ZONE, Is.Not.Null);
      Assert.That(CustomTimeZone.AU_ZONE.Id, Is.Not.Empty);
    }

    [Test]
    public void NZ_ZONE_IsNotNull()
    {
      Assert.That(CustomTimeZone.NZ_ZONE, Is.Not.Null);
      Assert.That(CustomTimeZone.NZ_ZONE.Id, Is.Not.Empty);
    }

    [Test]
    public void LD_ZONE_IsNotNull()
    {
      Assert.That(CustomTimeZone.LD_ZONE, Is.Not.Null);
      Assert.That(CustomTimeZone.LD_ZONE.Id, Is.Not.Empty);
    }

    [Test]
    public void TimeZoneJsonDict_ContainsAllExpectedKeys()
    {
      Assert.That(CustomTimeZone.TimeZoneJsonDict, Is.Not.Null);
      Assert.That(CustomTimeZone.TimeZoneJsonDict.Count, Is.GreaterThanOrEqualTo(8));
      Assert.That(CustomTimeZone.TimeZoneJsonDict.ContainsKey("Asia/Hong_Kong"), Is.True);
      Assert.That(CustomTimeZone.TimeZoneJsonDict.ContainsKey("Asia/Shanghai"), Is.True);
      Assert.That(CustomTimeZone.TimeZoneJsonDict.ContainsKey("America/New_York"), Is.True);
      Assert.That(CustomTimeZone.TimeZoneJsonDict.ContainsKey("America/Chicago"), Is.True);
      Assert.That(CustomTimeZone.TimeZoneJsonDict.ContainsKey("Asia/Singapore"), Is.True);
      Assert.That(CustomTimeZone.TimeZoneJsonDict.ContainsKey("Australia/Sydney"), Is.True);
      Assert.That(CustomTimeZone.TimeZoneJsonDict.ContainsKey("Pacific/Auckland"), Is.True);
      Assert.That(CustomTimeZone.TimeZoneJsonDict.ContainsKey("Europe/London"), Is.True);
    }

    [Test]
    public void TimeZoneJsonDict_ValuesAreNonEmptyJson()
    {
      foreach (var kv in CustomTimeZone.TimeZoneJsonDict)
      {
        Assert.That(kv.Value, Is.Not.Empty);
        Assert.That(kv.Value, Does.Contain("Id"));
      }
    }

    [Test]
    public void NY_ZONE_HasExpectedUtcOffset()
    {
      // New York is UTC-5 (EST) or UTC-4 (EDT)
      TimeSpan offset = CustomTimeZone.NY_ZONE.GetUtcOffset(DateTime.UtcNow);
      Assert.That(offset.TotalHours, Is.EqualTo(-5).Or.EqualTo(-4));
    }

    [Test]
    public void HK_ZONE_HasExpectedUtcOffset()
    {
      // Hong Kong is UTC+8
      TimeSpan offset = CustomTimeZone.HK_ZONE.GetUtcOffset(DateTime.UtcNow);
      Assert.That(offset.TotalHours, Is.EqualTo(8));
    }
  }
}
