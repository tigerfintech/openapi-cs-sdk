# Changelog

## 1.2.10 (2026-11-18)

### New

- Contract responses expose supported combinations of order types, time in force, and trading sessions through order capabilities.

## 1.2.9 (2026-11-11)

### New

- US option snapshots and option chains provide GTH/CURB extended-session quotes and independent cumulative volume and turnover.
- Protobuf option callbacks provide current-session prices, volume, and turnover with `PreMarket` / `AfterHours` tags and independent cumulative totals.
- Option minute-bar requests can include or exclude extended-session data; omitted options retain the server default.
- `NewsClient.GetStockNewsAsync` queries stock news with page-number pagination.
- `NewsClient.GetMarketNewsAsync` queries market highlights with cursor pagination.

### Changed

- Protobuf option callbacks use flat quote fields; HTTP snapshots and option chains retain their extended-session objects.

## 1.2.8 (2026-10-10)

### Fixed

- Concurrent HTTP requests now retain the correct authorization token without mixing, duplicating, or dropping credentials.
- Requests with an empty token and domain-discovery requests no longer reuse credentials from earlier requests.

## 1.2.7 (2026-10-09)

### New

- Asset and position queries support `AssetQuoteType` values `ETH`, `RTH`, and `OVERNIGHT`.
- Asset and position subscriptions support quote-basis selection through a new `ISubscribeAsyncApi.Subscribe` overload. Existing interface implementations remain compatible.

### Fixed

- Explicit `AssetQuoteType.ETH` values are now sent instead of being omitted as defaults.
- Repeated HTTP requests no longer accumulate authorization tokens.

### Breaking

- `PositionsModel.AssetQuoteType` is now nullable. Replace direct reads into `AssetQuoteType` with `.Value` when present, `.GetValueOrDefault()`, or a fallback such as `?? AssetQuoteType.RTH`. Assignments remain compatible.

## 1.2.6 (2026-08-27)

### New

- Real-time stock and cryptocurrency quotes provide turnover through `RealTimeQuoteItem.Amount`.
- `QuoteSymbolModel.SecType` enables real-time cryptocurrency quote requests.

## 1.2.5 (2026-08-26)

### Fixed

- Default service addresses now use public endpoints instead of internal addresses.

## 1.2.4 (2026-08-19)

### New

- Cryptocurrency K-line and current-timeline queries support `SecType.CC`.
- K-line and timeline responses provide precise volume.
- Option quotes provide mark prices and midpoint prices.
- Trade-tick push messages provide readable trade-condition descriptions.

## 1.2.3 (2026-07-23)

### New

- Corporate-action queries support symbol changes, delistings, and IPOs.
- `CorporateActionType` includes `SYMBOL_CHANGE`, `DELISTING`, and `IPO`.

### Fixed

- Overnight quote trading status is parsed correctly.

## 1.2.2 (2026-06-24)

### New

- `PlaceOrderModel.BuildIcebergOrder` creates iceberg orders.

## 1.2.1 (2026-06-08)

### New

- Option-exercise APIs support submission, cancellation, position-change checks, exercise records, and eligible-position queries.

## 1.2.0 (2026-06-08)

### Fixed

- Removed duplicate constants.

### Breaking

- The package name is now `TigerBrokers.OpenAPI`. Update existing `using` statements and NuGet references to `TigerBrokers.OpenAPI` v1.2.0.
