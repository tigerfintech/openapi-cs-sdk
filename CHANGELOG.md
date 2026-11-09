## Unreleased

## 1.2.9 (2026-11-09)
### New
- 新增资讯查询接口（`news_stock_list` / `news_highlight_list`）：`NewsClient` 支持个股新闻列表（`page_count`/`page_size` 分页，单标的）与市场要闻列表（`last_id` 游标翻页）；请求统一路由到主网关（不走行情域名分流）；`lang` 可选（`zh_CN` / `zh_TW` / `en_US`）

## 1.2.8 (2026-10-10)

### Fixed
- 修复并发调用 `HttpUtil.HttpPostAsync` 时 `Authorization` 头错乱的问题。`HttpRequestHeaders` 非线程安全，并发写共享默认请求头会导致部分请求携带他人的 token、携带多个 token，甚至完全丢失 `Authorization` 变成匿名请求。改为按请求携带后天然并发安全。
- `token` 传空时不再残留上一次请求的 token；`HttpUtil.HttpGet`（`NetworkUtil` 用于域名发现）也不再被动携带上一次 POST 残留的凭据。

## 1.2.7 (2026-10-09)
### New
- 资产与持仓查询新增 `AssetQuoteType` 字段（`ETH` / `RTH` / `OVERNIGHT`），支持按夜盘口径查询：`PrimeAssetsModel`、`GlobalAssetsModel`、`AggregateAssetModel`、`PositionsModel`。
- `ISubscribeAsyncApi` 新增重载 `Subscribe(Subject, string?, AssetQuoteType?)`，推送订阅可指定资产行情口径，仅对 `Asset` / `Position` 主题生效；该重载带默认实现（转调 `Subscribe(Subject, string)` 并忽略口径），自行实现该接口（含测试 Mock）的调用方无需改动即可继续编译。

### Fixed
- 显式设置 `AssetQuoteType.ETH` 现在会被正常发送。此前该字段为不可空枚举，`ETH` 的序数值为 0，会被序列化配置 `DefaultValueHandling.Ignore` 当作默认值丢弃，导致请求里没有 `asset_quote_type`。
- 修复 `HttpUtil` 中 `Authorization` 请求头在多次调用时重复累积的问题。`HttpClient` 为静态共享实例，`TryAddWithoutValidation` 每次都追加而非替换，导致第二个及后续请求携带多个 token 值，服务端解析失败报 `user token error`。现已改为每次请求先 `Remove` 再添加。

### Breaking
- `PositionsModel.AssetQuoteType` 由 `AssetQuoteType` 改为可空 `AssetQuoteType?`。读取侧的写法需要调整：`AssetQuoteType t = model.AssetQuoteType;` 不再能通过编译，请改用 `model.AssetQuoteType.Value`（确定非空时）或 `model.AssetQuoteType.GetValueOrDefault()` / `?? AssetQuoteType.RTH`（需要兜底时）。赋值侧写法不变。

## 1.2.6 (2026-08-27)
### New
- `RealTimeQuoteItem` 新增 `Amount` 字段，支持股票和数字货币实时行情成交额。
- `QuoteSymbolModel` 新增 `SecType` 字段，支持通过 `QUOTE_REAL_TIME` 查询数字货币实时行情。

## 1.2.5 (2026-08-26)
### Fixed
- 移除公开 SDK 中的内部测试域名默认值，TEST 环境默认回退到公开 sandbox 域名。

## 1.2.4 (2026-08-19)
### New
- 新增公开数字货币 K 线和当前分时行情支持（`SecType.CC`）
- K 线和分时行情补充精确成交量字段
- 期权实时行情补充标记价格和中间价相关字段
- Push 推送的逐笔成交消息补充成交条件说明字段，原始代码已转换为可读字符串

## 1.2.3 (2026-07-23)
### New
- `CorporateActionType` 新增：`SYMBOL_CHANGE`、`DELISTING`、`IPO`
- 新增公司行为查询能力，支持股票代码变更、摘牌、IPO 事件的结构化查询

### Change
- 修复 `QuoteOvernightItem` 交易状态字段解析错误的问题

## 1.2.2 (2026-06-24)
### New
- `PlaceOrderModel.BuildIcebergOrder()` — 冰山单构造

## 1.2.1 (2026-06-08)
### New
- 新增期权提前行权接口，支持提交/撤销行权申请、行权前检验持仓变化、查询行权记录和可行权持仓

## 1.2.0 (2026-06-08)
### Change
- 修复重复常量

### Breaking
- 包名更新为 `TigerBrokers.OpenAPI`；请将所有 `using` 引用及 NuGet 包引用从旧包名更新为 `TigerBrokers.OpenAPI` v1.2.0
