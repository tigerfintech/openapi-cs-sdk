# openapi-cs-sdk

[![NuGet](https://img.shields.io/nuget/v/TigerBrokers.OpenAPI.svg)](https://www.nuget.org/packages/TigerBrokers.OpenAPI/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/TigerBrokers.OpenAPI.svg)](https://www.nuget.org/packages/TigerBrokers.OpenAPI/)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)

[English](#english) | [中文](#中文)

---

## English

Tiger Open Platform C# SDK — provides API services for individual developers and institutional clients. Investors can fully utilize Tiger's trading, market data, and account services to build their own investment applications.

### Features

- **Trading**: Create, modify, and cancel orders; check order status
- **Account**: Query balances, positions, assets, and transaction records
- **Market Data**: Real-time and historical quotes for stocks, options, and futures
- **Push Service**: Real-time WebSocket notifications for orders, positions, and market changes

#### Supported Instruments

| Category | Details |
|----------|---------|
| Trading  | US/HK/A stocks, US options, HK warrants, HK bull/bear certificates, Forex |
| Market Data | US stocks, HK stocks, A shares |
| Order Types | Market, Limit, Stop, Stop-Limit, Trailing Stop, Algorithmic |

### Requirements

- .NET 6.0, 7.0, 8.0, 9.0, or 10.0
- C# 10.0 or later, depending on the selected .NET SDK

### Installation

```bash
dotnet add package TigerBrokers.OpenAPI
```

Or via NuGet Package Manager:

```
Install-Package TigerBrokers.OpenAPI
```

### Quick Start

#### 1. Configuration

```csharp
using TigerOpenAPI.Common;
using TigerOpenAPI.Config;

var config = new TigerConfig
{
    TigerId  = "your_tiger_id",
    DefaultAccount = "your_account_id",
    PrivateKey = "your_rsa_private_key",    // RSA private key
    Language = Language.en_US
};
```

#### 2. Query Quotes

```csharp
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;

var quoteClient = new QuoteClient(config);

// Real-time quote
var model = new QuoteRealTimeQuoteModel(new List<string> { "AAPL", "TSLA" });
var request = new TigerRequest<QuoteRealTimeQuoteResponse>
{
    ApiMethodName = QuoteApiService.REAL_TIME_QUOTE,
    ModelValue    = model
};
var response = quoteClient.Execute(request);
```

#### 3. Place an Order

```csharp
using TigerOpenAPI.Trade;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

var tradeClient = new TradeClient(config);

// Place a limit buy order
var model = new PlaceOrderModel("AAPL", OrderType.LMT, ActionType.BUY, 100, limitPrice: 150.0m);
var request = new TigerRequest<PlaceOrderResponse>
{
    ApiMethodName = TradeApiService.PLACE_ORDER,
    ModelValue    = model
};
var response = tradeClient.Execute(request);
```

#### 4. Subscribe to Push (WebSocket)

```csharp
using TigerOpenAPI.Push;

var pushClient = PushClientFactory.CreateSocketClient(config);
pushClient.OrderAssetChange += (sender, e) => Console.WriteLine($"Order update: {e.Data}");
pushClient.Connect();
pushClient.SubscribeOrder();
```

### API Reference

| Class | Description |
|-------|-------------|
| `QuoteClient` | Market data — quotes, K-lines, order book, fundamentals |
| `TradeClient` | Trading — orders, positions, assets, transfers |
| `PushClient`  | WebSocket push — orders, positions, market ticker |

Full documentation: [https://docs-en.itigerup.com/docs/](https://docs-en.itigerup.com/docs/)

### Links

- **Developer Portal**: [https://developer.itigerup.com/](https://developer.itigerup.com/)
- **Documentation**: [https://docs-en.itigerup.com/docs/](https://docs-en.itigerup.com/docs/)
- **GitHub**: [https://github.com/tigerfintech/openapi-cs-sdk](https://github.com/tigerfintech/openapi-cs-sdk)
- **NuGet**: [https://www.nuget.org/packages/TigerBrokers.OpenAPI](https://www.nuget.org/packages/TigerBrokers.OpenAPI)

### Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature`
3. Commit your changes: `git commit -m 'feat: add your feature'`
4. Push the branch: `git push origin feature/your-feature`
5. Open a Pull Request

### License

This project is licensed under the [MIT License](LICENSE).

---

## 中文

老虎开放平台 C# SDK — 为个人开发者和机构客户提供接口服务，投资者可以充分利用老虎的交易服务、行情服务、账户服务等实现自己的投资应用程序。

### 功能特性

- **交易管理**：创建、修改、取消订单；查询订单状态
- **账户查询**：查询余额、持仓、资产及资金流水
- **行情服务**：实时行情与历史数据（股票、期权、期货）
- **推送服务**：WebSocket 实时推送订单变动、持仓变动、行情变动

#### 支持品种

| 类别 | 详情 |
|------|------|
| 交易支持 | 美港股／A股、美股期权、港股窝轮、港股牛熊证、外汇 |
| 行情支持 | 美股、港股、A股 |
| 订单类型 | 市价单、限价单、止损单、止损限价单、跟踪止损单、算法订单 |

### 环境要求

- .NET 6.0、7.0、8.0、9.0 或 10.0
- C# 10.0 或更高版本，取决于所使用的 .NET SDK

### 安装

```bash
dotnet add package TigerBrokers.OpenAPI
```

或通过 NuGet 包管理器：

```
Install-Package TigerBrokers.OpenAPI
```

### 快速开始

#### 1. 配置

```csharp
using TigerOpenAPI.Common;
using TigerOpenAPI.Config;

var config = new TigerConfig
{
    TigerId    = "your_tiger_id",
    DefaultAccount = "your_account_id",
    PrivateKey = "your_rsa_private_key",    // RSA 私钥
    Language   = Language.zh_CN
};
```

#### 2. 查询行情

```csharp
using TigerOpenAPI.Quote;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Quote.Response;

var quoteClient = new QuoteClient(config);

// 实时行情
var model = new QuoteRealTimeQuoteModel(new List<string> { "AAPL", "TSLA" });
var request = new TigerRequest<QuoteRealTimeQuoteResponse>
{
    ApiMethodName = QuoteApiService.REAL_TIME_QUOTE,
    ModelValue    = model
};
var response = quoteClient.Execute(request);
```

### 逐笔成交 `cond` 字段说明

`trade_tick` 接口和 push 层返回的 `cond` 字段已由 SDK 转换为可读字符串，含义如下：

**美股（US）**

| 值 | 含义 |
|----|------|
| `US_REGULAR_SALE` | 常规交易（Regular Sale） |
| `US_BUNCHED_TRADE` | 批量交易（Bunched Trade） |
| `US_CASH_TRADE` | 现金交易（Cash Trade） |
| `US_INTERMARKET_SWEEP` | 跨市场交易（Intermarket Sweep） |
| `US_BUNCHED_SOLD_TRADE` | 批量卖出（Bunched Sold Trade） |
| `US_PRICE_VARIATION_TRADE` | 离价交易（Price Variation Trade） |
| `US_ODD_LOT_TRADE` | 碎股交易（Odd Lot Trade） |
| `US_RULE_127_OR_155_TRADE` | 纽交所第 127/155 条交易 |
| `US_SOLD_LAST` | 延迟交易（Sold Last） |
| `US_MARKET_CENTER_CLOSE_PRICE` | 中央收市价（Market Center Close Price） |
| `US_NEXT_DAY_TRADE` | 隔日交易（Next Day Trade） |
| `US_MARKET_CENTER_OPENING_TRADE` | 中央开盘价交易（Market Center Opening Trade） |
| `US_PRIOR_REFERENCE_PRICE` | 前参考价（Prior Reference Price） |
| `US_MARKET_CENTER_OPEN_PRICE` | 中央开盘价（Market Center Open Price） |
| `US_SELLER` | 卖方（Seller） |
| `US_FORM_T` | 盘前盘后交易（Form T） |
| `US_EXTENDED_TRADING_HOURS` | 延长交易时段（Extended Trading Hours） |
| `US_CONTINGENT_TRADE` | 合单交易（Contingent Trade） |
| `US_AVERAGE_PRICE_TRADE` | 均价交易（Average Price Trade） |
| `US_CROSS_TRADE` | 跨市场交易（Cross Trade） |
| `US_SOLD_OUT_OF_SEQUENCE` | 场外售出（Sold Out of Sequence） |
| `US_DERIVATIVELY_PRICED` | 衍生工具定价（Derivatively Priced） |
| `US_QUALIFIED_CONTINGENT_TRADE` | 合单交易（Qualified Contingent Trade） |

**港股（HK）**

| 值 | 含义 |
|----|------|
| `HK_AUTOMATCH_NORMAL` | 自动对盘（Automatch Normal） |
| `HK_ODD_LOT_TRADE` | 碎股交易（Odd Lot Trade） |
| `HK_AUCTION_TRADE` | 竞价交易（Auction Trade） |
| `HK_OVERSEAS_TRADE` | 场外交易（Overseas Trade） |
| `HK_LATE_TRADE_OFF_EXCHG` | 开市前成交（Late Trade Off Exchange） |
| `HK_NON_DIRECT_OFF_EXCHG_TRADE` | 非自动对盘（Non-Direct Off Exchange Trade） |
| `HK_DIRECT_OFF_EXCHG_TRADE` | 同券商自动对盘（Direct Off Exchange Trade） |
| `HK_AUTOMATIC_INTERNALIZED` | 同券商非自动对盘（Automatic Internalized） |

#### 3. 下单

```csharp
using TigerOpenAPI.Trade;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;

var tradeClient = new TradeClient(config);

// 限价买入
var model = new PlaceOrderModel("AAPL", OrderType.LMT, ActionType.BUY, 100, limitPrice: 150.0m);
var request = new TigerRequest<PlaceOrderResponse>
{
    ApiMethodName = TradeApiService.PLACE_ORDER,
    ModelValue    = model
};
var response = tradeClient.Execute(request);
```

#### 4. 订阅推送（WebSocket）

```csharp
using TigerOpenAPI.Push;

var pushClient = PushClientFactory.CreateSocketClient(config);
pushClient.OrderAssetChange += (sender, e) => Console.WriteLine($"订单更新: {e.Data}");
pushClient.Connect();
pushClient.SubscribeOrder();
```

### API 参考

| 类名 | 说明 |
|------|------|
| `QuoteClient` | 行情 — 实时报价、K 线、深度、基本面 |
| `TradeClient` | 交易 — 下单、持仓、资产、资金划转 |
| `PushClient`  | 推送 — 订单变动、持仓变动、行情推送 |

完整文档：[https://docs.itigerup.com/docs/](https://docs.itigerup.com/docs/)

### 相关链接

- **开发者平台**：[https://developer.itigerup.com/](https://developer.itigerup.com/)
- **开发文档**：[https://docs.itigerup.com/docs/](https://docs.itigerup.com/docs/)
- **GitHub**：[https://github.com/tigerfintech/openapi-cs-sdk](https://github.com/tigerfintech/openapi-cs-sdk)
- **NuGet**：[https://www.nuget.org/packages/TigerBrokers.OpenAPI](https://www.nuget.org/packages/TigerBrokers.OpenAPI)

### 参与贡献

1. Fork 本仓库
2. 创建特性分支：`git checkout -b feature/your-feature`
3. 提交改动：`git commit -m 'feat: add your feature'`
4. 推送分支：`git push origin feature/your-feature`
5. 发起 Pull Request

### 开源协议

本项目使用 [MIT License](LICENSE) 协议。
