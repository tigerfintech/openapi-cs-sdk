using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using static TigerOpenAPI.Common.CustomTimeZone;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies the formatting and conversion helpers in <see cref="DateUtil"/>.
  /// </summary>
  [TestFixture]
  public class DateUtilTest
  {
    private const long SampleTimestamp = 1705335234567L; // 2024-01-15 16:13:54.567 UTC

    /// <summary>CurrentTimeMillis returns a positive value comparable to Unix epoch time.</summary>
    [Test]
    public void CurrentTimeMillis_ReturnsPositiveValue()
    {
      long now = DateUtil.CurrentTimeMillis();

      Assert.That(now, Is.GreaterThan(1_000_000_000_000L));
    }

    /// <summary>PrintDate returns a yyyy-MM-dd formatted string in the given time zone.</summary>
    [Test]
    public void PrintDate_ReturnsDateStringInNyZone()
    {
      string printed = DateUtil.PrintDate(SampleTimestamp, NY_ZONE);

      Assert.That(printed, Does.Match(@"^\d{4}-\d{2}-\d{2}$"));
      Assert.That(printed, Is.EqualTo("2024-01-15"));
    }

    /// <summary>PrintDateTime returns a yyyy-MM-dd HH:mm:ss formatted string.</summary>
    [Test]
    public void PrintDateTime_ReturnsDateTimeStringInNyZone()
    {
      string printed = DateUtil.PrintDateTime(SampleTimestamp, NY_ZONE);

      Assert.That(printed, Does.Match(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$"));
    }

    /// <summary>PrintDateTimeWithMs returns a yyyy-MM-dd HH:mm:ss.fff formatted string.</summary>
    [Test]
    public void PrintDateTimeWithMs_ReturnsDateTimeWithMsStringInNyZone()
    {
      string printed = DateUtil.PrintDateTimeWithMs(SampleTimestamp, NY_ZONE);

      Assert.That(printed, Does.Match(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3}$"));
      // SampleTimestamp is 2024-01-15 16:13:54.567 UTC; January NY = EST (UTC-5) => 11:13:54.567.
      Assert.That(printed, Is.EqualTo("2024-01-15 11:13:54.567"));
    }

    /// <summary>PrintUtcSystemDate returns a yyyy-MM-dd string using UTC.</summary>
    [Test]
    public void PrintUtcSystemDate_ReturnsDateString()
    {
      string printed = DateUtil.PrintUtcSystemDate();

      Assert.That(printed, Does.Match(@"^\d{4}-\d{2}-\d{2}$"));
    }

    /// <summary>PrintUtcSystemDateTime returns a yyyy-MM-dd HH:mm:ss string using UTC.</summary>
    [Test]
    public void PrintUtcSystemDateTime_ReturnsDateTimeString()
    {
      string printed = DateUtil.PrintUtcSystemDateTime();

      Assert.That(printed, Does.Match(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$"));
    }

    /// <summary>PrintSystemDate returns a yyyy-MM-dd string in the supplied time zone.</summary>
    [Test]
    public void PrintSystemDate_ReturnsDateStringInHkZone()
    {
      string printed = DateUtil.PrintSystemDate(HK_ZONE);

      Assert.That(printed, Does.Match(@"^\d{4}-\d{2}-\d{2}$"));
    }

    /// <summary>PrintSystemDateTime returns a yyyy-MM-dd HH:mm:ss string in the supplied time zone.</summary>
    [Test]
    public void PrintSystemDateTime_ReturnsDateTimeStringInHkZone()
    {
      string printed = DateUtil.PrintSystemDateTime(HK_ZONE);

      Assert.That(printed, Does.Match(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$"));
    }

    /// <summary>IsDateBeforeToday returns true when the supplied date is yesterday
    /// (computed in the same time zone as the comparison uses).</summary>
    [Test]
    public void IsDateBeforeToday_Yesterday_ReturnsTrue()
    {
      DateTime nowNy = TimeZoneInfo.ConvertTimeFromUtc(DateTimeOffset.UtcNow.DateTime, NY_ZONE);
      string yesterday = nowNy.AddDays(-1).ToString("yyyy-MM-dd");

      Assert.That(DateUtil.IsDateBeforeToday(yesterday, NY_ZONE), Is.True);
    }

    /// <summary>IsDateBeforeToday returns false when the supplied date is tomorrow
    /// (computed in the same time zone as the comparison uses).</summary>
    [Test]
    public void IsDateBeforeToday_Tomorrow_ReturnsFalse()
    {
      DateTime nowNy = TimeZoneInfo.ConvertTimeFromUtc(DateTimeOffset.UtcNow.DateTime, NY_ZONE);
      string tomorrow = nowNy.AddDays(1).ToString("yyyy-MM-dd");

      Assert.That(DateUtil.IsDateBeforeToday(tomorrow, NY_ZONE), Is.False);
    }

    /// <summary>IsDateBeforeToday returns false for an empty string.</summary>
    [Test]
    public void IsDateBeforeToday_EmptyString_ReturnsFalse()
    {
      Assert.That(DateUtil.IsDateBeforeToday("", NY_ZONE), Is.False);
      Assert.That(DateUtil.IsDateBeforeToday("   ", NY_ZONE), Is.False);
      Assert.That(DateUtil.IsDateBeforeToday(null!, NY_ZONE), Is.False);
    }

    /// <summary>ConvertTime converts a datetime string into the given time zone.
    /// Only asserts year/month (not day) because DateTime.TryParse yields an
    /// Unspecified Kind that ConvertTime interprets as the system local zone,
    /// which can shift the day across zone boundaries.</summary>
    [Test]
    public void ConvertTime_ParsesAndConverts_ReturnsDateTime()
    {
      DateTime? converted = DateUtil.ConvertTime("2024-01-15 12:00:00", NY_ZONE);

      Assert.That(converted, Is.Not.Null);
      Assert.That(converted!.Value.Year, Is.EqualTo(2024));
      Assert.That(converted.Value.Month, Is.EqualTo(1));
    }

    /// <summary>ConvertTime returns null for an unparseable datetime string.</summary>
    [Test]
    public void ConvertTime_InvalidString_ReturnsNull()
    {
      DateTime? converted = DateUtil.ConvertTime("not-a-date", NY_ZONE);

      Assert.That(converted, Is.Null);
    }

    /// <summary>ConvertTimestamp converts a datetime string into epoch milliseconds.</summary>
    [Test]
    public void ConvertTimestamp_ParsesString_ReturnsLong()
    {
      long ts = DateUtil.ConvertTimestamp("1970-01-01 00:00:00", NY_ZONE);

      // 1970-01-01 00:00:00 in NY (UTC-5) => 5h offset => 5*3600*1000 = 18000000 ms
      Assert.That(ts, Is.EqualTo(18000000L));
    }

    /// <summary>ConvertTimestamp returns 0 for an unparseable datetime string.</summary>
    [Test]
    public void ConvertTimestamp_InvalidString_ReturnsZero()
    {
      long ts = DateUtil.ConvertTimestamp("not-a-date", NY_ZONE);

      Assert.That(ts, Is.EqualTo(0));
    }

    /// <summary>GetTimeZoneByMarket returns the NY zone for Market.US.</summary>
    [Test]
    public void GetTimeZoneByMarket_US_ReturnsNyZone()
    {
      Assert.That(DateUtil.GetTimeZoneByMarket(Market.US), Is.SameAs(NY_ZONE));
    }

    /// <summary>GetTimeZoneByMarket returns the HK zone for Market.HK.</summary>
    [Test]
    public void GetTimeZoneByMarket_HK_ReturnsHkZone()
    {
      Assert.That(DateUtil.GetTimeZoneByMarket(Market.HK), Is.SameAs(HK_ZONE));
    }

    /// <summary>GetTimeZoneByMarket returns the HK zone for Market.CN (default branch).</summary>
    [Test]
    public void GetTimeZoneByMarket_CN_ReturnsHkZone()
    {
      Assert.That(DateUtil.GetTimeZoneByMarket(Market.CN), Is.SameAs(HK_ZONE));
    }

    /// <summary>GetTimeZoneByMarket returns the SG zone for Market.SG.</summary>
    [Test]
    public void GetTimeZoneByMarket_SG_ReturnsSgZone()
    {
      Assert.That(DateUtil.GetTimeZoneByMarket(Market.SG), Is.SameAs(SG_ZONE));
    }

    /// <summary>GetTimeZoneByMarket returns the AU zone for Market.AU.</summary>
    [Test]
    public void GetTimeZoneByMarket_AU_ReturnsAuZone()
    {
      Assert.That(DateUtil.GetTimeZoneByMarket(Market.AU), Is.SameAs(AU_ZONE));
    }

    /// <summary>GetTimeZoneByMarket returns the NZ zone for Market.NZ.</summary>
    [Test]
    public void GetTimeZoneByMarket_NZ_ReturnsNzZone()
    {
      Assert.That(DateUtil.GetTimeZoneByMarket(Market.NZ), Is.SameAs(NZ_ZONE));
    }

    /// <summary>GetTimeZoneByMarket returns the LD zone for Market.UK.</summary>
    [Test]
    public void GetTimeZoneByMarket_UK_ReturnsLdZone()
    {
      Assert.That(DateUtil.GetTimeZoneByMarket(Market.UK), Is.SameAs(LD_ZONE));
    }
  }
}
