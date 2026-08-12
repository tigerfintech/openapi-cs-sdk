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

- .NET 10.0
- C# 13.0

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
    Account  = "your_account_id",
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
- **NuGet**: [https://www.nuget.org/packages/tiger-openapi](https://www.nuget.org/packages/tiger-openapi)

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

- .NET 10.0
- C# 13.0

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
    Account    = "your_account_id",
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
- **NuGet**：[https://www.nuget.org/packages/tiger-openapi](https://www.nuget.org/packages/tiger-openapi)

### 参与贡献

1. Fork 本仓库
2. 创建特性分支：`git checkout -b feature/your-feature`
3. 提交改动：`git commit -m 'feat: add your feature'`
4. 推送分支：`git push origin feature/your-feature`
5. 发起 Pull Request

### 开源协议

本项目使用 [MIT License](LICENSE) 协议。
