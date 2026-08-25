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
