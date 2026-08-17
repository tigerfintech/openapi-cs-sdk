using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Trade;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies that API name constants are non-empty and globally unique.
  /// A duplicate constant would mean two different APIs share the same name — a routing bug.
  /// </summary>
  [TestFixture]
  public class ApiConstantsTest
  {
    private static List<string> GetApiNames(Type serviceType)
    {
      return serviceType
          .GetFields(BindingFlags.Public | BindingFlags.Static)
          .Where(f => f.IsLiteral && f.FieldType == typeof(string))
          .Select(f => (string)f.GetRawConstantValue()!)
          .ToList();
    }

    [Test]
    public void QuoteApiService_AllConstants_NonEmpty()
    {
      var names = GetApiNames(typeof(QuoteApiService));
      Assert.That(names, Is.Not.Empty);
      foreach (var name in names)
      {
        Assert.That(name, Is.Not.Null.And.Not.Empty,
            $"QuoteApiService constant must be non-empty");
      }
    }

    [Test]
    public void TradeApiService_AllConstants_NonEmpty()
    {
      var names = GetApiNames(typeof(TradeApiService));
      Assert.That(names, Is.Not.Empty);
      foreach (var name in names)
      {
        Assert.That(name, Is.Not.Null.And.Not.Empty,
            $"TradeApiService constant must be non-empty");
      }
    }

    [Test]
    public void QuoteApiService_AllConstants_NoDuplicates()
    {
      var names = GetApiNames(typeof(QuoteApiService));
      var duplicates = names.GroupBy(n => n).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
      Assert.That(duplicates, Is.Empty,
          $"Duplicate QuoteApiService values: {string.Join(", ", duplicates)}");
    }

    [Test]
    public void TradeApiService_AllConstants_NoDuplicates()
    {
      var names = GetApiNames(typeof(TradeApiService));
      var duplicates = names.GroupBy(n => n).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
      Assert.That(duplicates, Is.Empty,
          $"Duplicate TradeApiService values: {string.Join(", ", duplicates)}");
    }

    [Test]
    public void QuoteAndTradeApiService_NoOverlappingNames()
    {
      var quoteNames = new HashSet<string>(GetApiNames(typeof(QuoteApiService)));
      var tradeNames = GetApiNames(typeof(TradeApiService));

      // USER_TOKEN_REFRESH is defined in QuoteApiService and is intentionally
      // shared / re-routed by TradeClient; it is NOT declared in TradeApiService.
      // Exclude it from the overlap check by its string value.
      var knownShared = new HashSet<string>
      {
        QuoteApiService.USER_TOKEN_REFRESH
      };

      var overlap = tradeNames.Where(n => quoteNames.Contains(n) && !knownShared.Contains(n)).ToList();
      Assert.That(overlap, Is.Empty,
          $"Unexpected overlapping API names between Quote and Trade: {string.Join(", ", overlap)}");
    }
  }
}
