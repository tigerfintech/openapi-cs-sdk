# TigerOpenAPI

[![NuGet](https://img.shields.io/nuget/v/TigerBrokers.OpenAPI.svg)](https://www.nuget.org/packages/TigerBrokers.OpenAPI/)
[![License](https://img.shields.io/github/license/tigerfintech/openapi-cs-sdk.svg)](https://github.com/tigerfintech/openapi-cs-sdk/blob/master/LICENSE)

[English](#english) | [中文](#中文)

---

## English

Tiger Open Platform C# SDK. Provides trading, market data, and account management APIs for individual developers and institutional clients.

**Requirements**: .NET 6.0, 7.0, 8.0, 9.0, or 10.0 · C# 10.0 or later

### Installation

```bash
dotnet add package TigerBrokers.OpenAPI
```

### Quick Start

```csharp
// Configure
var config = new TigerConfig
{
    TigerId    = "your_tiger_id",
    DefaultAccount = "your_account_id",
    PrivateKey = "your_rsa_private_key",
    Language   = Language.en_US
};

// Query real-time quote
var quoteClient = new QuoteClient(config);
var model   = new QuoteRealTimeQuoteModel(new List<string> { "AAPL" });
var request = new TigerRequest<QuoteRealTimeQuoteResponse>
{
    ApiMethodName = QuoteApiService.REAL_TIME_QUOTE,
    ModelValue    = model
};
var response = quoteClient.Execute(request);

// Place a limit order
var tradeClient  = new TradeClient(config);
var orderModel   = new PlaceOrderModel("AAPL", OrderType.LMT, ActionType.BUY, 100, limitPrice: 150.0m);
var orderRequest = new TigerRequest<PlaceOrderResponse>
{
    ApiMethodName = TradeApiService.PLACE_ORDER,
    ModelValue    = orderModel
};
var orderResponse = tradeClient.Execute(orderRequest);
```

### Features

| Category | Details |
|----------|---------|
| Trading  | US/HK/A stocks, US options, HK warrants, HK bull/bear certificates, Forex |
| Market Data | Real-time quotes, K-lines, order book depth, fundamentals |
| Order Types | Market, Limit, Stop, Stop-Limit, Trailing Stop, Algorithmic |
| Push | WebSocket real-time notifications for orders, positions, and market data |

### Links

- **Documentation**: [https://quant.itigerup.com/openapi/zh/csharp/overview/introduction.html](https://quant.itigerup.com/openapi/zh/csharp/overview/introduction.html)
- **Developer Portal**: [https://developer.itigerup.com/](https://developer.itigerup.com/)
- **GitHub**: [https://github.com/tigerfintech/openapi-cs-sdk](https://github.com/tigerfintech/openapi-cs-sdk)

---

## 中文

老虎开放平台 C# SDK，为个人开发者和机构客户提供交易、行情、账户管理等接口服务。

**环境要求**：.NET 6.0、7.0、8.0、9.0 或 10.0 · C# 10.0 或更高版本

### 安装

```bash
dotnet add package TigerBrokers.OpenAPI
```

### 快速开始

```csharp
// 配置
var config = new TigerConfig
{
    TigerId    = "your_tiger_id",
    DefaultAccount = "your_account_id",
    PrivateKey = "your_rsa_private_key",
    Language   = Language.zh_CN
};

// 查询实时行情
var quoteClient = new QuoteClient(config);
var model   = new QuoteRealTimeQuoteModel(new List<string> { "AAPL" });
var request = new TigerRequest<QuoteRealTimeQuoteResponse>
{
    ApiMethodName = QuoteApiService.REAL_TIME_QUOTE,
    ModelValue    = model
};
var response = quoteClient.Execute(request);

// 下限价买单
var tradeClient  = new TradeClient(config);
var orderModel   = new PlaceOrderModel("AAPL", OrderType.LMT, ActionType.BUY, 100, limitPrice: 150.0m);
var orderRequest = new TigerRequest<PlaceOrderResponse>
{
    ApiMethodName = TradeApiService.PLACE_ORDER,
    ModelValue    = orderModel
};
var orderResponse = tradeClient.Execute(orderRequest);
```

### 功能特性

| 类别 | 详情 |
|------|------|
| 交易支持 | 美港股／A股、美股期权、港股窝轮、港股牛熊证、外汇 |
| 行情服务 | 实时报价、K 线、深度行情、基本面数据 |
| 订单类型 | 市价单、限价单、止损单、止损限价单、跟踪止损单、算法订单 |
| 推送服务 | WebSocket 实时推送订单、持仓、行情变动 |

### 相关链接

- **开发文档**：[https://quant.itigerup.com/openapi/zh/csharp/overview/introduction.html](https://quant.itigerup.com/openapi/zh/csharp/overview/introduction.html)
- **开发者平台**：[https://developer.itigerup.com/](https://developer.itigerup.com/)
- **GitHub**：[https://github.com/tigerfintech/openapi-cs-sdk](https://github.com/tigerfintech/openapi-cs-sdk)
