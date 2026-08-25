using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Trade.Model;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Contract test: every public read-write property on serializable SDK types
  /// must carry a [JsonProperty] annotation, matching the wire-name used in the API.
  ///
  /// Candidate types are identified by two complementary rules:
  ///   1. Name suffix: ends with "Model", "Item", or "Response" (existing convention).
  ///   2. Namespace heuristic: any public, concrete, non-Pb class in a namespace
  ///      that contains ".Quote.", ".Trade.", ".Common.Struct", or ".Model" that
  ///      already declares at least one [JsonProperty] on its properties.
  /// Rule 2 catches helper classes such as OptionFundamentals that are embedded in
  /// serialized responses but do not follow the naming convention.
  ///
  /// This is a ratchet test. If it fails, add [JsonProperty] to the flagged property
  /// rather than loosening this check — missing annotations caused two production bugs
  /// in the Java SDK (OptionAnalysisModel and OptionTimelineModel).
  /// </summary>
  [TestFixture]
  public class SerializationContractTest
  {
    private static readonly Assembly SdkAssembly =
        typeof(PlaceOrderModel).Assembly; // TigerOpenAPI.dll

    /// <summary>
    /// Collect all candidate serializable types from the SDK assembly.
    /// Includes name-suffix types AND any concrete public class in a
    /// serialization-relevant namespace that already carries at least one
    /// [JsonProperty] annotation (catches helpers like OptionFundamentals).
    /// </summary>
    private static IEnumerable<Type> GetCandidateTypes()
    {
      return SdkAssembly.GetTypes()
          .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic)
          .Where(t => !t.Namespace!.Contains(".Pb"))
          .Where(t =>
          {
            // Rule 1: conventional name suffix
            if (t.Name.EndsWith("Model") || t.Name.EndsWith("Item") || t.Name.EndsWith("Response"))
              return true;
            // Rule 2: lives in a serialization-relevant namespace and has at least
            // one property already annotated with [JsonProperty] — meaning it is
            // intentionally serialized but did not follow the naming convention.
            bool inSerializationNs = t.Namespace!.Contains(".Quote.") ||
                                     t.Namespace!.Contains(".Trade.") ||
                                     t.Namespace!.Contains(".Common.Struct") ||
                                     t.Namespace!.Contains(".Model");
            if (!inSerializationNs) return false;
            return t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Any(p => p.GetCustomAttribute<JsonPropertyAttribute>() != null);
          })
          .OrderBy(t => t.FullName);
    }

    [Test]
    public void AllModelProperties_HaveJsonPropertyAnnotation()
    {
      var failures = new List<string>();

      foreach (var type in GetCandidateTypes())
      {
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(p => p.CanRead && p.CanWrite);

        foreach (var prop in props)
        {
          // Skip properties inherited from object / base classes already checked
          if (prop.DeclaringType != type) continue;

          // Properties explicitly opted out of serialization don't need [JsonProperty]
          if (prop.GetCustomAttribute<JsonIgnoreAttribute>() != null) continue;

          var attr = prop.GetCustomAttribute<JsonPropertyAttribute>();
          if (attr == null)
          {
            failures.Add($"{type.FullName}.{prop.Name}");
          }
        }
      }

      Assert.That(failures, Is.Empty,
          "The following properties are missing [JsonProperty] annotation:\n" +
          string.Join("\n", failures.Select(f => "  " + f)));
    }

    [Test]
    public void AllModels_DefaultConstructorExists()
    {
      // Newtonsoft.Json requires a parameterless constructor for deserialization.
      var failures = new List<string>();

      var candidateTypes = GetCandidateTypes();

      foreach (var type in candidateTypes)
      {
        var ctor = type.GetConstructor(
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
            null, Type.EmptyTypes, null);
        if (ctor == null)
        {
          failures.Add(type.FullName!);
        }
      }

      Assert.That(failures, Is.Empty,
          "The following types lack a no-arg constructor (required for JSON deserialization):\n" +
          string.Join("\n", failures.Select(f => "  " + f)));
    }
  }
}
