using System;
using NUnit.Framework;
using TigerOpenAPI.Common;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for ApiLogger. ApiLogger holds static Enabled/DebugEnabled
  /// flags that are saved in SetUp and restored in TearDown to avoid leaking
  /// state across fixtures.
  /// </summary>
  [TestFixture]
  public class ApiLoggerTest
  {
    private bool _enabled;
    private bool _debugEnabled;
    private bool _infoEnabled;
    private bool _warnEnabled;
    private bool _errorEnabled;

    [SetUp]
    public void SetUp()
    {
      _enabled = ApiLogger.Enabled;
      _debugEnabled = ApiLogger.DebugEnabled;
      _infoEnabled = ApiLogger.InfoEnabled;
      _warnEnabled = ApiLogger.WarnEnabled;
      _errorEnabled = ApiLogger.ErrorEnabled;

      ApiLogger.Enabled = true;
      ApiLogger.DebugEnabled = true;
      ApiLogger.InfoEnabled = true;
      ApiLogger.WarnEnabled = true;
      ApiLogger.ErrorEnabled = true;
    }

    [TearDown]
    public void TearDown()
    {
      ApiLogger.Enabled = _enabled;
      ApiLogger.DebugEnabled = _debugEnabled;
      ApiLogger.InfoEnabled = _infoEnabled;
      ApiLogger.WarnEnabled = _warnEnabled;
      ApiLogger.ErrorEnabled = _errorEnabled;
    }

    // ---------- Simple message overloads ----------

    [Test]
    public void Debug_SimpleMessage_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Debug("debug message"));
    }

    [Test]
    public void Info_SimpleMessage_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Info("info message"));
    }

    [Test]
    public void Warn_SimpleMessage_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Warn("warn message"));
    }

    [Test]
    public void Error_SimpleMessage_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Error("error message"));
    }

    // ---------- Format-arg overloads ----------

    [Test]
    public void Debug_WithFormatArgs_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Debug("value={0}, id={1}", 42, "abc"));
    }

    [Test]
    public void Info_WithFormatArgs_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Info("user={0}", "sukai"));
    }

    [Test]
    public void Warn_WithFormatArgs_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Warn("retry {0}/{1}", 1, 3));
    }

    [Test]
    public void Error_WithFormatArgs_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Error("code={0} msg={1}", 500, "boom"));
    }

    // ---------- Exception overloads ----------

    [Test]
    public void Debug_WithException_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Debug(new InvalidOperationException("boom"), "debug with ex"));
    }

    [Test]
    public void Error_WithException_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Error(new InvalidOperationException("boom"), "error with ex"));
    }

    [Test]
    public void Warn_WithException_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Warn(new InvalidOperationException("boom"), "warn with ex"));
    }

    [Test]
    public void Info_WithException_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Info(new InvalidOperationException("boom"), "info with ex"));
    }

    // ---------- Flag gating ----------

    [Test]
    public void Enabled_False_SuppressesLoggingWithoutThrowing()
    {
      ApiLogger.Enabled = false;
      ApiLogger.DebugEnabled = true;
      Assert.DoesNotThrow(() =>
      {
        ApiLogger.Debug("d");
        ApiLogger.Info("i");
        ApiLogger.Warn("w");
        ApiLogger.Error("e");
      });
    }

    [Test]
    public void DebugEnabled_False_SuppressesDebugWithoutThrowing()
    {
      ApiLogger.Enabled = true;
      ApiLogger.DebugEnabled = false;
      Assert.DoesNotThrow(() => ApiLogger.Debug("should be skipped"));
    }

    [Test]
    public void InfoEnabled_False_SuppressesInfoWithoutThrowing()
    {
      ApiLogger.Enabled = true;
      ApiLogger.InfoEnabled = false;
      Assert.DoesNotThrow(() => ApiLogger.Info("should be skipped"));
    }

    [Test]
    public void Warn_StringExceptionOverload_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Warn("msg", new InvalidOperationException("ex")));
    }

    [Test]
    public void Error_StringExceptionOverload_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => ApiLogger.Error("msg", new InvalidOperationException("ex")));
    }
  }
}
