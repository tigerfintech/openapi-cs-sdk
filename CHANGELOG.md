## 1.2.4 (2026-08-19)
### New
- `OptionRealTimeQuote` 新增 `MarkPrice`、`PreMarkPrice`、`MarkTimestamp`、`MidPrice`、`PreMidPrice`、`MidTimestamp` 字段

## 1.2.3 (2026-07-23)
### New
- `CorporateActionType` 新增：`SYMBOL_CHANGE`、`DELISTING`、`IPO`
- 新增 `CorporateSymbolChangeItem`、`CorporateDelistingItem`、`CorporateIpoItem` 及对应 Response 包装类

### Change
- 修正 `QuoteOvernightItem` 的服务端字段映射：移除不存在的 `Overnight`，新增映射 `tradingStatus` 的 `TradingStatus`

## 1.2.2 (2026-06-24)
### New
- **冰山单支持**：新增 `PlaceOrderModel.BuildIcebergOrder()` 两个重载（基础参数 / 完整参数），支持 `DisplaySize`、`MinDisplaySize`、`CheckIntervals`、`PriceType`（`LIMIT_PRICE` / `OPPONENT_PRICE`）、`StartTime`、`EndTime` 字段。
- **单元测试**：`IcebergUnitTest`（25 项断言），覆盖基础构造、完整参数、零值省略及常量值，运行命令：`dotnet run --project Sample -- iceberg-unit`。

## 1.2.1 (2026-06-08)
### New
- 新增期权提前行权接口：`SubmitOptionExerciseAsync` 提交行权/放弃申请、`CancelOptionExerciseAsync` 撤销申请、`CheckOptionExerciseAsync` 行权检验（预估持仓变化）、`GetOptionExerciseRecordsAsync` 分页查询行权记录、`GetOptionExercisePositionsAsync` 查询可行权持仓

## 1.2.0 (2026-06-08)
### Change
- 修复重复常量，包名更新为 `TigerBrokers.OpenAPI` v1.2.0
