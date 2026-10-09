## Unreleased

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
