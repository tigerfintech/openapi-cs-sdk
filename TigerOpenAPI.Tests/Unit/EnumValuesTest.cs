using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies the <c>Values</c> enumeration property of the enum-like classes
  /// (FinancialField, StockField, AccumulateField, MultiTagField, AccumulatePeriod)
  /// and the <see cref="EnumNameConverter.WriteJson"/> serialization behavior.
  /// </summary>
  [TestFixture]
  public class EnumValuesTest
  {
    // ---- FinancialField ----

    /// <summary>FinancialField.Values should enumerate every declared instance.</summary>
    [Test]
    public void FinancialField_Values_ReturnsAllDeclaredInstances()
    {
      List<FinancialField> values = FinancialField.Values.ToList();

      Assert.That(values.Count, Is.GreaterThan(0));
      Assert.That(values.Count, Is.EqualTo(68));

      FinancialField first = values[0];
      Assert.That(first.Index, Is.EqualTo(1));
      Assert.That(first.Value, Is.EqualTo("grossMarginVal"));

      FinancialField last = values[values.Count - 1];
      Assert.That(last, Is.Not.Null);
    }

    /// <summary>Values re-enumeration should be idempotent and return identical references.</summary>
    [Test]
    public void FinancialField_Values_IsIdempotent()
    {
      FinancialField firstA = FinancialField.Values.First();
      FinancialField firstB = FinancialField.Values.First();
      Assert.That(ReferenceEquals(firstA, firstB), Is.True);
    }

    // ---- StockField ----

    /// <summary>StockField.Values should enumerate every declared instance.</summary>
    [Test]
    public void StockField_Values_ReturnsAllDeclaredInstances()
    {
      List<StockField> values = StockField.Values.ToList();

      Assert.That(values.Count, Is.GreaterThan(0));
      Assert.That(values.Count, Is.EqualTo(57));

      StockField first = values[0];
      Assert.That(first.Index, Is.EqualTo(2));
      Assert.That(first.Value, Is.EqualTo("latestPrice"));
    }

    // ---- AccumulateField ----

    /// <summary>AccumulateField.Values should enumerate every declared instance.</summary>
    [Test]
    public void AccumulateField_Values_ReturnsAllDeclaredInstances()
    {
      List<AccumulateField> values = AccumulateField.Values.ToList();

      Assert.That(values.Count, Is.GreaterThan(0));
      Assert.That(values.Count, Is.EqualTo(26));

      AccumulateField first = values[0];
      Assert.That(first.Index, Is.EqualTo(1));
      Assert.That(first.Value, Is.EqualTo("changeRate"));
    }

    // ---- MultiTagField ----

    /// <summary>MultiTagField.Values should enumerate every declared instance.</summary>
    [Test]
    public void MultiTagField_Values_ReturnsAllDeclaredInstances()
    {
      List<MultiTagField> values = MultiTagField.Values.ToList();

      Assert.That(values.Count, Is.GreaterThan(0));
      Assert.That(values.Count, Is.EqualTo(22));

      MultiTagField first = values[0];
      Assert.That(first.Index, Is.EqualTo(1));
      Assert.That(first.Value, Is.EqualTo("industry"));
    }

    // ---- AccumulatePeriod ----

    /// <summary>AccumulatePeriod.Values should enumerate every declared instance.</summary>
    [Test]
    public void AccumulatePeriod_Values_ReturnsAllDeclaredInstances()
    {
      List<AccumulatePeriod> values = AccumulatePeriod.Values.ToList();

      Assert.That(values.Count, Is.GreaterThan(0));
      Assert.That(values.Count, Is.EqualTo(14));

      AccumulatePeriod first = values[0];
      Assert.That(first.Value, Is.EqualTo(0));
      Assert.That(first.Suffix, Is.EqualTo("_5_min"));
      Assert.That(first.Range, Is.EqualTo("changeRate"));
    }

    // ---- EnumNameConverter.WriteJson ----
    // The converter's CanConvert() throws NotImplementedException, so it cannot
    // be used via JsonConvert.SerializeObject(value, converter). Instead we
    // invoke WriteJson directly with a JsonTextWriter, exactly as the SDK's
    // JsonSettings attribute path would.

    /// <summary>Helper: call WriteJson directly and return the raw output.</summary>
    private static string WriteJsonDirect(EnumNameConverter converter, object? value)
    {
      var sb = new StringBuilder();
      using (var sw = new StringWriter(sb))
      using (var jw = new JsonTextWriter(sw))
      {
        JsonSerializer serializer = JsonSerializer.CreateDefault();
        converter.WriteJson(jw, value, serializer);
      }
      return sb.ToString();
    }

    /// <summary>WriteJson should serialize a StockField to its static field name.</summary>
    [Test]
    public void EnumNameConverter_WriteJson_StockField_WritesFieldName()
    {
      EnumNameConverter converter = new EnumNameConverter();
      string json = WriteJsonDirect(converter, StockField.StockField_CurPrice);

      Assert.That(json, Is.EqualTo("\"StockField_CurPrice\""));
    }

    /// <summary>WriteJson should serialize an AccumulateField to its static field name.</summary>
    [Test]
    public void EnumNameConverter_WriteJson_AccumulateField_WritesFieldName()
    {
      EnumNameConverter converter = new EnumNameConverter();
      string json = WriteJsonDirect(converter, AccumulateField.AccumulateField_Eps);

      Assert.That(json, Is.EqualTo("\"AccumulateField_Eps\""));
    }

    /// <summary>WriteJson should serialize an AccumulatePeriod to its static field name.</summary>
    [Test]
    public void EnumNameConverter_WriteJson_AccumulatePeriod_WritesFieldName()
    {
      EnumNameConverter converter = new EnumNameConverter();
      string json = WriteJsonDirect(converter, AccumulatePeriod.Last_Year);

      Assert.That(json, Is.EqualTo("\"Last_Year\""));
    }

    /// <summary>WriteJson should be a no-op for types not in the converter's set.</summary>
    [Test]
    public void EnumNameConverter_WriteJson_UnsupportedType_WritesNothing()
    {
      EnumNameConverter converter = new EnumNameConverter();
      // FinancialField is not in the converter's set, so nothing is written.
      string json = WriteJsonDirect(converter, FinancialField.FinancialField_GrossProfitRate);

      Assert.That(json, Is.EqualTo(""));
    }

    /// <summary>WriteJson should be a no-op for a null value.</summary>
    [Test]
    public void EnumNameConverter_WriteJson_NullValue_WritesNothing()
    {
      EnumNameConverter converter = new EnumNameConverter();
      StockField? field = null;
      string json = WriteJsonDirect(converter, field);

      Assert.That(json, Is.EqualTo(""));
    }
  }
}
