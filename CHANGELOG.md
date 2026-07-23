## 1.2.3 (2026-07-23)
### New
- `CorporateActionType` 枚举新增：`SYMBOL_CHANGE`、`DELISTING`、`IPO`
- 新增响应模型类：`CorporateSymbolChangeItem`（含 `OldSymbol`/`NewSymbol`）、`CorporateDelistingItem`（含 `AnnouncedDate`/`Reason`）、`CorporateIpoItem`（含 `ListingDate`/`ListingPrice`/`PriceRange`/`SharesOutstanding`/`SharesFloat`/`OfferAmount`/`Currency`/`MinPurchaseQuantity`/`LeverageRatio`/`IpoName`）
- 新增响应包装类：`CorporateSymbolChangeResponse`、`CorporateDelistingResponse`、`CorporateIpoResponse`（`Dictionary<string, List<T>>` 结构与服务端 grouped-map 格式对应）
- `DocTest.cs` 新增三个测试方法；配置路径改为读 `TIGER_CONFIG_PATH` 环境变量（fallback `~/.tigeropen`）

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
