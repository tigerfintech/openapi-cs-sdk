# CLAUDE.md - TigerOpen C# SDK

## 项目概述

老虎证券 OpenAPI 官方 C# SDK，为 .NET 开发者提供交易、行情、账户接口服务。

## 技术栈

- .NET Framework 6.0
- C# 8
- WebSocket（实时推送）
- HTTP REST API

## 目录结构

```
openapi-cs-sdk/
├── TigerOpenAPI/             # SDK 核心库
├── Sample/                   # 示例项目
├── openapi-cs-sdk.sln        # Visual Studio 解决方案
└── README.md
```

## 主要功能

| 功能 | 说明 |
|------|------|
| 交易管理 | 创建、修改、取消订单，查询订单状态 |
| 账户信息 | 余额查询、持仓管理 |
| 行情查询 | 股票、期权价格和信息 |
| 实时推送 | 订单变动、持仓变动、行情变动 |

## 支持的交易类型

- **交易**：股票（美港股/A股）、美股期权、港股窝轮、港股牛熊证、外汇
- **行情**：美股、港股、A股
- **订单**：市价单、限价单、止损单、止损限价单、跟踪止损单、算法订单

## 安装

```bash
dotnet add package tiger-openapi
```

## 开发环境

推荐使用 Visual Studio 作为 IDE

## 构建

```bash
dotnet build
```

## 文档

- API 文档（中文）：https://docs.itigerup.com/docs/
- API 文档（英文）：https://docs-en.itigerup.com/docs/
- 开发者平台：https://developer.itigerup.com/
