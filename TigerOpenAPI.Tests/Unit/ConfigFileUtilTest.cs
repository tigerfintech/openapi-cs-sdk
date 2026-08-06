using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Tests.TestSupport;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Unit tests for ConfigFileUtil property-file parsing and token helpers.
  /// Uses temp files for all I/O; cleaned up in TearDown.
  /// </summary>
  [TestFixture]
  public class ConfigFileUtilTest
  {
    private readonly List<string> _tempFiles = new();
    private readonly List<string> _tempDirs = new();

    private string NewTempDir()
    {
      string dir = Path.Combine(Path.GetTempPath(), "cs_sdk_test_" + Guid.NewGuid().ToString("N"));
      Directory.CreateDirectory(dir);
      _tempDirs.Add(dir);
      return dir;
    }

    private string NewTempFile(string dir, string fileName, string content)
    {
      string path = Path.Combine(dir, fileName);
      File.WriteAllText(path, content, new UTF8Encoding(false));
      _tempFiles.Add(path);
      return path;
    }

    [TearDown]
    public void TearDown()
    {
      foreach (var f in _tempFiles)
      {
        if (File.Exists(f)) File.Delete(f);
      }
      foreach (var d in _tempDirs)
      {
        if (Directory.Exists(d)) Directory.Delete(d, true);
      }
      _tempFiles.Clear();
      _tempDirs.Clear();
    }

    // ---------- CheckFile ----------

    [Test]
    public void CheckFile_EmptyDir_ReturnsFalse()
    {
      Assert.That(ConfigFileUtil.CheckFile(string.Empty, "any.properties"), Is.False);
    }

    [Test]
    public void CheckFile_WhitespaceDir_ReturnsFalse()
    {
      Assert.That(ConfigFileUtil.CheckFile("   ", "any.properties"), Is.False);
    }

    [Test]
    public void CheckFile_NonExistingDir_ReturnsFalse()
    {
      Assert.That(ConfigFileUtil.CheckFile(
        Path.Combine(Path.GetTempPath(), "definitely_missing_" + Guid.NewGuid()), "any"), Is.False);
    }

    [Test]
    public void CheckFile_ExistingDirMissingFile_ReturnsFalse()
    {
      string dir = NewTempDir();
      Assert.That(ConfigFileUtil.CheckFile(dir, "missing.properties"), Is.False);
    }

    [Test]
    public void CheckFile_ExistingDirAndFile_ReturnsTrue()
    {
      string dir = NewTempDir();
      NewTempFile(dir, "exists.properties", "a=b");
      Assert.That(ConfigFileUtil.CheckFile(dir, "exists.properties"), Is.True);
    }

    // ---------- LoadConfigFile ----------

    [Test]
    public void LoadConfigFile_NoFilePath_DoesNotThrow()
    {
      var config = new TigerConfig { ConfigFilePath = string.Empty };
      Assert.DoesNotThrow(() => ConfigFileUtil.LoadConfigFile(config));
      Assert.That(config.TigerId, Is.Null);
    }

    [Test]
    public void LoadConfigFile_ExistingTempFile_LoadsValues()
    {
      string dir = NewTempDir();
      string pk = TestClientFactory.PrivateKeyBase64;
      string content =
          "tiger_id=20150000001\n"
        + "account=testacct\n"
        + "license=TBNZ\n"
        + "private_key_pk8=" + pk + "\n"
        + "env=SANDBOX\n"
        + "# a comment line\n"
        + "notakey=ignored\n";
      NewTempFile(dir, TigerApiConstants.CONFIG_FILENAME, content);

      var config = new TigerConfig { ConfigFilePath = dir };
      ConfigFileUtil.LoadConfigFile(config);

      Assert.That(config.TigerId, Is.EqualTo("20150000001"));
      Assert.That(config.DefaultAccount, Is.EqualTo("testacct"));
      Assert.That(config.License, Is.EqualTo(License.TBNZ));
      Assert.That(config.PrivateKey, Is.EqualTo(pk));
      Assert.That(config.Environment, Is.EqualTo(Env.SANDBOX));
    }

    [Test]
    public void LoadConfigFile_LicenseNone_DoesNotOverwrite()
    {
      string dir = NewTempDir();
      NewTempFile(dir, TigerApiConstants.CONFIG_FILENAME, "license=NONE\n");
      var config = new TigerConfig { ConfigFilePath = dir, License = License.TBNZ };
      ConfigFileUtil.LoadConfigFile(config);
      Assert.That(config.License, Is.EqualTo(License.TBNZ));
    }

    // ---------- ReadPropertiesFile ----------

    [Test]
    public void ReadPropertiesFile_TempFile_ReturnsFilteredKeys()
    {
      string dir = NewTempDir();
      string path = NewTempFile(dir, "props.properties",
        "tiger_id=abc\n# comment\n\nempty=\nnoeq\nlicense=TBNZ\n");
      var includeKeys = new HashSet<string> { "tiger_id", "license" };
      var result = ConfigFileUtil.ReadPropertiesFile(path, includeKeys);

      Assert.That(result.Count, Is.EqualTo(2));
      Assert.That(result["tiger_id"], Is.EqualTo("abc"));
      Assert.That(result["license"], Is.EqualTo("TBNZ"));
    }

    [Test]
    public void ReadPropertiesFile_EmptyValue_Skipped()
    {
      string dir = NewTempDir();
      string path = NewTempFile(dir, "empty.properties", "key=\nother=val\n");
      var result = ConfigFileUtil.ReadPropertiesFile(path);
      Assert.That(result.ContainsKey("key"), Is.False);
      Assert.That(result["other"], Is.EqualTo("val"));
    }

    // ---------- ReadPrivateKey / ProcessPrivateKey ----------

    [Test]
    public void ReadPrivateKey_Pkcs8File_ReturnsStrippedContent()
    {
      string dir = NewTempDir();
      string keyBody = "MIIBfooBAR";
      string pem =
        "-----BEGIN " + "PRIVATE KEY-----\n" + keyBody + "\n-----END PRIVATE KEY-----\n";
      string path = NewTempFile(dir, "key.pem", pem);
      string result = ConfigFileUtil.ReadPrivateKey(path);
      Assert.That(result, Is.EqualTo(keyBody));
    }

    [Test]
    public void ProcessPrivateKey_WithHeaders_StripsHeadersAndWhitespace()
    {
      string content = "-----BEGIN " + "PRIVATE KEY-----\nAB CD\nEF\n-----END PRIVATE KEY-----";
      Assert.That(ConfigFileUtil.ProcessPrivateKey(content), Is.EqualTo("ABCDEF"));
    }

    [Test]
    public void ProcessPrivateKey_WithoutHeaders_StripsWhitespace()
    {
      string content = "AB CD\nEF\n";
      Assert.That(ConfigFileUtil.ProcessPrivateKey(content), Is.EqualTo("ABCDEF"));
    }

    [Test]
    public void ProcessPrivateKey_Base64Only_PreservesEquals()
    {
      // '=' is not in the trim set (only LF/CR/space), so it survives
      Assert.That(ConfigFileUtil.ProcessPrivateKey("MI BC=="), Is.EqualTo("MIBC=="));
    }

    // ---------- UpdateTokenFile ----------

    [Test]
    public void UpdateTokenFile_EmptyToken_ReturnsFalse()
    {
      string dir = NewTempDir();
      var config = new TigerConfig { ConfigFilePath = dir };
      Assert.That(ConfigFileUtil.UpdateTokenFile(config, ""), Is.False);
      Assert.That(ConfigFileUtil.UpdateTokenFile(config, "   "), Is.False);
    }

    [Test]
    public void UpdateTokenFile_NoTokenFile_ReturnsFalse()
    {
      string dir = NewTempDir();
      var config = new TigerConfig { ConfigFilePath = dir };
      Assert.That(ConfigFileUtil.UpdateTokenFile(config, "tok"), Is.False);
    }

    [Test]
    public void UpdateTokenFile_ExistingTokenFile_WritesToken()
    {
      string dir = NewTempDir();
      NewTempFile(dir, TigerApiConstants.TOKEN_FILENAME, "token=old");
      var config = new TigerConfig { ConfigFilePath = dir };
      bool ok = ConfigFileUtil.UpdateTokenFile(config, "newtoken123");
      Assert.That(ok, Is.True);

      string written = File.ReadAllText(Path.Combine(dir, TigerApiConstants.TOKEN_FILENAME));
      Assert.That(written, Is.EqualTo("token=newtoken123"));
    }

    // ---------- TryGetCreateTime / GetCreateTime / GetExpiredTime ----------

    private static string MakeToken(long createTime, long expiredTime)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{createTime},{expiredTime}"));
    }

    [Test]
    public void GetCreateTime_ValidToken_ReturnsTimestamp()
    {
      long createTime = 1700000000000L;
      string token = MakeToken(createTime, 1700086400000L);
      Assert.That(ConfigFileUtil.GetCreateTime(token), Is.EqualTo(createTime));
    }

    [Test]
    public void GetCreateTime_NoSeparator_ReturnsZero()
    {
      string token = Convert.ToBase64String(Encoding.UTF8.GetBytes("noseparator"));
      Assert.That(ConfigFileUtil.GetCreateTime(token), Is.EqualTo(0));
    }

    [Test]
    public void GetExpiredTime_ValidToken_ReturnsTimestamp()
    {
      long expiredTime = 1700086400000L;
      string token = MakeToken(1700000000000L, expiredTime);
      Assert.That(ConfigFileUtil.GetExpiredTime(token), Is.EqualTo(expiredTime));
    }

    [Test]
    public void GetExpiredTime_NoSeparator_ReturnsZero()
    {
      string token = Convert.ToBase64String(Encoding.UTF8.GetBytes("noseparator"));
      Assert.That(ConfigFileUtil.GetExpiredTime(token), Is.EqualTo(0));
    }

    [Test]
    public void TryGetCreateTime_ValidToken_ReturnsTimestamp()
    {
      long createTime = 1700000000000L;
      string token = MakeToken(createTime, 1700086400000L);
      Assert.That(ConfigFileUtil.TryGetCreateTime(token), Is.EqualTo(createTime));
    }

    [Test]
    public void TryGetCreateTime_InvalidToken_ReturnsZero()
    {
      // Not valid Base64 -> GetCreateTime throws -> caught -> 0
      Assert.That(ConfigFileUtil.TryGetCreateTime("!!!notbase64!!!"), Is.EqualTo(0));
    }

    [Test]
    public void TryGetCreateTime_EmptyToken_ReturnsZero()
    {
      Assert.That(ConfigFileUtil.TryGetCreateTime(string.Empty), Is.EqualTo(0));
      Assert.That(ConfigFileUtil.TryGetCreateTime("   "), Is.EqualTo(0));
    }
  }
}
