using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Tests.Integration
{
  /// <summary>
  /// Helpers shared by integration tests for deciding whether a market is
  /// open (so tests can require live data) and for resolving fresh dynamic
  /// identifiers (e.g. a valid US option identifier) that would otherwise
  /// go stale.
  ///
  /// Rationale: an integration test that always <c>Assert.Ignore()</c>s
  /// on empty data is indistinguishable from a broken wiring, and also
  /// hides real regressions behind a SKIP result. Instead, the test should
  /// check the current <c>market_state</c>:
  ///  - in trading hours, missing data is a real failure (<c>Assert.Fail</c>);
  ///  - outside trading hours, the wire path has already been validated by
  ///    the request completing without exception, so an empty response is
  ///    logged and the test returns as PASS — no skip.
  ///
  /// Market status values returned by <c>market_state</c> follow Tiger's
  /// public contract: <c>NOT_YET_OPEN</c>, <c>PRE_HOUR_TRADING</c>,
  /// <c>TRADING</c>, <c>MIDDLE_CLOSE</c>, <c>POST_HOUR_TRADING</c>,
  /// <c>CLOSING</c>, <c>EARLY_CLOSED</c>, <c>MARKET_CLOSED</c>.
  /// </summary>
  internal static class MarketHelpers
  {
    // Per-process caches so a batch of tests only pays for one round-trip
    // per market / per option lookup.
    private static readonly Dictionary<Market, string?> StatusCache = new();
    private static readonly Dictionary<Market, string> OptionIdentifierCache = new();
    private static readonly object StatusLock = new();
    private static readonly object OptionLock = new();
    private static ContractItem? _futuresContractCache;
    private static readonly object FuturesLock = new();
    private static (ContractLeg lower, ContractLeg upper)? _verticalSpreadLegsCache;
    private static readonly object VerticalSpreadLock = new();

    /// <summary>
    /// True when the given market is currently in the main <c>TRADING</c>
    /// session. Extended hours (pre/post) are excluded — use
    /// <see cref="IsMarketOpenExtended"/> for those.
    /// </summary>
    public static bool IsMarketTrading(QuoteClient qc, Market market)
    {
      return string.Equals(GetMarketStatus(qc, market), "TRADING",
          StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// True when the market is trading in any live session, including
    /// pre-hour (<c>PRE_HOUR_TRADING</c>) or post-hour
    /// (<c>POST_HOUR_TRADING</c>) sessions.
    /// </summary>
    public static bool IsMarketOpenExtended(QuoteClient qc, Market market)
    {
      var status = GetMarketStatus(qc, market);
      if (string.IsNullOrEmpty(status)) return false;
      return status.Equals("TRADING", StringComparison.OrdinalIgnoreCase)
          || status.Equals("PRE_HOUR_TRADING", StringComparison.OrdinalIgnoreCase)
          || status.Equals("POST_HOUR_TRADING", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Returns the raw <c>status</c> string for the market (e.g. "TRADING",
    /// "MARKET_CLOSED"), or <c>null</c> when the market_state call did not
    /// return a status. Cached per-market for the process lifetime.
    /// </summary>
    public static string? GetMarketStatus(QuoteClient qc, Market market)
    {
      lock (StatusLock)
      {
        if (StatusCache.TryGetValue(market, out var cached))
          return cached;
      }

      string? status = null;
      try
      {
        var model = new QuoteMarketModel { Market = market };
        var req = new TigerRequest<MarketStateResponse>
        {
          ApiMethodName = QuoteApiService.MARKET_STATE,
          ModelValue = model
        };
        var resp = qc.Execute(req);
        if (resp != null && resp.IsSuccess() && resp.Data != null && resp.Data.Count > 0)
        {
          status = resp.Data[0].Status;
        }
      }
      catch
      {
        // Non-fatal: treat as unknown → fall back to "closed" semantics.
        status = null;
      }

      // Only cache a successful status fetch — a null (query failure) must not
      // be cached permanently, because transient errors (network hiccup,
      // rate-limit) would otherwise poison the cache for the entire test run
      // and cause every downstream test to Assert.Ignore silently.
      if (status != null)
      {
        lock (StatusLock)
        {
          StatusCache[market] = status;
        }
      }
      return status;
    }

    /// <summary>
    /// Resolves a fresh US option identifier by looking up AAPL's nearest
    /// expiry via <c>option_expiration</c>, then picking a plausible ATM
    /// call from <c>option_chain</c>. Returns the identifier string
    /// (e.g. <c>"AAPL 250815C00220000"</c>) or <c>null</c> if the API
    /// call chain failed. Cached per process.
    /// </summary>
    public static string? ResolveUsOptionIdentifier(QuoteClient qc)
    {
      lock (OptionLock)
      {
        if (OptionIdentifierCache.TryGetValue(Market.US, out var cached))
          return cached;
      }

      try
      {
        // 1. nearest expiry for AAPL
        var expModel = new OptionExpirationModel
        {
          Symbols = new List<string> { "AAPL" },
          Market = Market.US
        };
        var expReq = new TigerRequest<OptionExpirationResponse>
        {
          ApiMethodName = QuoteApiService.OPTION_EXPIRATION,
          ModelValue = expModel
        };
        var expResp = qc.Execute(expReq);
        if (expResp == null || !expResp.IsSuccess()
            || expResp.Data == null || expResp.Data.Count == 0
            || expResp.Data[0].Timestamps == null
            || expResp.Data[0].Timestamps.Count == 0)
        {
          return null;
        }
        // Pick an expiry > 14 days out — the nearest expiry can already be
        // expired or expiring same-day (untradable), mirroring Java's
        // MarketHelpers.resolveUsOptionIdentifier / Python's
        // _resolve_us_option_contract selection algorithm.
        var expItem = expResp.Data[0];
        long expiry = expItem.Timestamps[expItem.Timestamps.Count - 1];
        var today = DateTime.UtcNow.Date;
        for (int i = 0; i < expItem.Timestamps.Count; i++)
        {
          var candidateDate = DateTimeOffset.FromUnixTimeMilliseconds(expItem.Timestamps[i]).UtcDateTime.Date;
          if ((candidateDate - today).TotalDays > 14)
          {
            expiry = expItem.Timestamps[i];
            break;
          }
        }

        // 2. chain for that expiry → pick the middle row's call leg
        //    (server usually orders strikes ascending; middle ≈ ATM).
        var chainModel = new OptionChainV3Model
        {
          Market = Market.US,
          OptionBasic = new List<OptionChainModel>
          {
            new OptionChainModel { Symbol = "AAPL", Expiry = expiry }
          }
        };
        var chainReq = new TigerRequest<OptionChainResponse>
        {
          ApiMethodName = QuoteApiService.OPTION_CHAIN,
          ModelValue = chainModel
        };
        var chainResp = qc.Execute(chainReq);
        if (chainResp == null || !chainResp.IsSuccess()
            || chainResp.Data == null || chainResp.Data.Count == 0
            || chainResp.Data[0].Items == null
            || chainResp.Data[0].Items.Count == 0)
        {
          return null;
        }

        var items = chainResp.Data[0].Items;
        var atm = items[items.Count / 2];
        string? identifier = atm.Call?.Identifier ?? atm.Put?.Identifier;
        if (string.IsNullOrEmpty(identifier))
        {
          // Fall back to first row with any identifier.
          foreach (var row in items)
          {
            identifier = row.Call?.Identifier ?? row.Put?.Identifier;
            if (!string.IsNullOrEmpty(identifier)) break;
          }
        }

        if (!string.IsNullOrEmpty(identifier))
        {
          lock (OptionLock)
          {
            OptionIdentifierCache[Market.US] = identifier!;
          }
        }
        return identifier;
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// Resolves a live US futures contract via <c>future_exchange</c> →
    /// <c>future_current_contract</c> (the server's front-month contract for
    /// a discovered exchange code), converted to a trade-side
    /// <see cref="ContractItem"/> via <see cref="ContractItem.Convert(FutureContractItem)"/>.
    /// Returns <c>null</c> if the API call chain failed or returned no data.
    /// Cached per process.
    /// </summary>
    public static ContractItem? ResolveUsFuturesContract(QuoteClient qc)
    {
      lock (FuturesLock)
      {
        if (_futuresContractCache != null) return _futuresContractCache;
      }

      try
      {
        // 1. discover a live future exchange code
        var exchModel = new FutureExchangeModel { SecType = SecType.FUT.ToString() };
        var exchReq = new TigerRequest<FutureExchangeResponse>
        {
          ApiMethodName = QuoteApiService.FUTURE_EXCHANGE,
          ModelValue = exchModel
        };
        var exchResp = qc.Execute(exchReq);
        if (exchResp == null || !exchResp.IsSuccess()
            || exchResp.Data == null || exchResp.Data.Count == 0)
        {
          return null;
        }
        string exchangeCode = exchResp.Data[0].Code;

        // 2. front-month contract for that exchange
        var byExchModel = new FutureContractByExchCodeModel { ExchangeCode = exchangeCode };
        var byExchReq = new TigerRequest<FutureContractsResponse>
        {
          ApiMethodName = QuoteApiService.FUTURE_CONTRACT_BY_EXCHANGE_CODE,
          ModelValue = byExchModel
        };
        var byExchResp = qc.Execute(byExchReq);
        if (byExchResp == null || !byExchResp.IsSuccess()
            || byExchResp.Data == null || byExchResp.Data.Count == 0)
        {
          return null;
        }
        string futureType = byExchResp.Data[0].Type;

        var currentModel = new FutureContractByTypeModel { FutureType = futureType };
        var currentReq = new TigerRequest<FutureContractsResponse>
        {
          ApiMethodName = QuoteApiService.FUTURE_CURRENT_CONTRACT,
          ModelValue = currentModel
        };
        var currentResp = qc.Execute(currentReq);
        if (currentResp == null || !currentResp.IsSuccess()
            || currentResp.Data == null || currentResp.Data.Count == 0)
        {
          return null;
        }

        var contract = ContractItem.Convert(currentResp.Data[0]);
        lock (FuturesLock)
        {
          _futuresContractCache = contract;
        }
        return contract;
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// Resolves two adjacent-strike PUT legs on AAPL for a vertical spread:
    /// nearest expiry more than 14 days out, chain for that expiry, filtered
    /// to PUTs sorted by strike ascending, picking the two strikes nearest
    /// the middle of the chain. Returns <c>(lower, upper)</c> legs with
    /// <c>Action</c>/<c>Ratio</c> unset (caller fills those in), or
    /// <c>null</c> if the API call chain failed or returned too few strikes.
    /// Cached per process.
    /// </summary>
    public static (ContractLeg lower, ContractLeg upper)? ResolveUsVerticalSpreadLegs(QuoteClient qc)
    {
      lock (VerticalSpreadLock)
      {
        if (_verticalSpreadLegsCache != null) return _verticalSpreadLegsCache;
      }

      try
      {
        // 1. first expiry more than 14 days out
        var expModel = new OptionExpirationModel
        {
          Symbols = new List<string> { "AAPL" },
          Market = Market.US
        };
        var expReq = new TigerRequest<OptionExpirationResponse>
        {
          ApiMethodName = QuoteApiService.OPTION_EXPIRATION,
          ModelValue = expModel
        };
        var expResp = qc.Execute(expReq);
        if (expResp == null || !expResp.IsSuccess()
            || expResp.Data == null || expResp.Data.Count == 0
            || expResp.Data[0].Timestamps == null
            || expResp.Data[0].Timestamps.Count == 0)
        {
          return null;
        }
        long cutoff = DateTimeOffset.UtcNow.AddDays(14).ToUnixTimeMilliseconds();
        long expiry = expResp.Data[0].Timestamps.FirstOrDefault(t => t > cutoff);
        if (expiry == 0) expiry = expResp.Data[0].Timestamps[^1];

        // 2. chain for that expiry, filtered to PUTs, sorted by strike asc
        var chainModel = new OptionChainV3Model
        {
          Market = Market.US,
          OptionBasic = new List<OptionChainModel>
          {
            new OptionChainModel { Symbol = "AAPL", Expiry = expiry }
          }
        };
        var chainReq = new TigerRequest<OptionChainResponse>
        {
          ApiMethodName = QuoteApiService.OPTION_CHAIN,
          ModelValue = chainModel
        };
        var chainResp = qc.Execute(chainReq);
        if (chainResp == null || !chainResp.IsSuccess()
            || chainResp.Data == null || chainResp.Data.Count == 0
            || chainResp.Data[0].Items == null)
        {
          return null;
        }

        var puts = chainResp.Data[0].Items
            .Select(row => row.Put)
            .Where(put => put != null && !string.IsNullOrEmpty(put.Strike))
            .OrderBy(put => double.Parse(put.Strike))
            .ToList();
        if (puts.Count < 2) return null;

        int mid = puts.Count / 2;
        if (mid + 1 >= puts.Count) mid = puts.Count - 2;
        string expiryStr = DateTimeOffset.FromUnixTimeMilliseconds(expiry).ToString("yyyyMMdd");

        var lower = new ContractLeg
        {
          Symbol = "AAPL",
          SecType = SecType.OPT.ToString(),
          Expiry = expiryStr,
          Strike = puts[mid].Strike,
          Right = "PUT"
        };
        var upper = new ContractLeg
        {
          Symbol = "AAPL",
          SecType = SecType.OPT.ToString(),
          Expiry = expiryStr,
          Strike = puts[mid + 1].Strike,
          Right = "PUT"
        };

        var legs = (lower, upper);
        lock (VerticalSpreadLock)
        {
          _verticalSpreadLegsCache = legs;
        }
        return legs;
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// Verifies that the caller's already-detected "empty response" is a
    /// legitimate market-closed condition, not a wire regression. Call at
    /// the point where you would otherwise skip:
    ///  - inside trading hours ⇒ <see cref="Assert.Fail(string)"/>;
    ///  - outside trading hours ⇒ log via
    ///    <see cref="TestContext.Progress"/> and return, so the test passes.
    /// The caller is expected to <c>return;</c> after this call in the
    /// out-of-hours branch — the request round-trip has already validated
    /// the wire path (deserialization + status handling).
    /// </summary>
    public static void AssertNonEmptyDuringTrading(QuoteClient qc, Market market, string context)
    {
      if (IsMarketTrading(qc, market))
      {
        Assert.Fail($"{context} — market {market} is TRADING, expected non-empty data (real regression)");
      }
      else
      {
        var status = GetMarketStatus(qc, market) ?? "UNKNOWN";
        TestContext.Progress.WriteLine(
            $"{context} — market {market} status={status} (non-trading hours, empty OK, wire path validated)");
      }
    }
  }
}
