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
  /// Contract test: every public read-write property on Model/Item/Response classes
  /// must carry a [JsonProperty] annotation, matching the wire-name used in the API.
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

    [Test]
    public void AllModelProperties_HaveJsonPropertyAnnotation()
    {
      var failures = new List<string>();

      var candidateTypes = SdkAssembly.GetTypes()
          .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic)
          .Where(t => t.Name.EndsWith("Model") || t.Name.EndsWith("Item") || t.Name.EndsWith("Response"))
          .Where(t => !t.Namespace!.Contains(".Pb"))
          .OrderBy(t => t.FullName);

      foreach (var type in candidateTypes)
      {
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(p => p.CanRead && p.CanWrite);

        foreach (var prop in props)
        {
          // Skip properties inherited from object / base classes already checked
          if (prop.DeclaringType != type) continue;

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

      var candidateTypes = SdkAssembly.GetTypes()
          .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic)
          .Where(t => t.Name.EndsWith("Model") || t.Name.EndsWith("Item") || t.Name.EndsWith("Response"))
          .Where(t => !t.Namespace!.Contains(".Pb"))
          .OrderBy(t => t.FullName);

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
