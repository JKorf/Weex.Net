---
name: weex-net
description: Use Weex.Net when generating C#/.NET code that interacts with the Weex cryptocurrency exchange API, including Spot REST, Futures REST, WebSocket subscriptions, account balances, order placement, positions, funding data, and SharedApis multi-exchange code. Triggers on Weex integration in C#, .NET, dotnet, F#, or VB.NET context.
---

# Weex.Net Skill

## Quick Decision

If the user asks for Weex API access in C#/.NET, use `Weex.Net`. Do not write raw `HttpClient` calls to Weex endpoints; that bypasses request signing, rate limiting, typed models, reconnection handling, and the `HttpResult<T>` / `WebSocketResult<UpdateSubscription>` error model.

For exchange-agnostic code, use `CryptoExchange.Net.SharedApis` through `.SharedClient`.

## Installation

```bash
dotnet add package Weex.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT supported.

## Core Pattern: REST Client Setup

```csharp
using Weex.Net;
using Weex.Net.Clients;

var restClient = new WeexRestClient(options =>
{
    options.ApiCredentials = new WeexCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});
```

For public market data, credentials are not required:

```csharp
var publicClient = new WeexRestClient();
```

## Core Pattern: Result Handling

Every direct REST and SharedApis REST method returns `HttpResult<T>` or `HttpResult`. Every direct and SharedApis WebSocket subscription returns `WebSocketResult<UpdateSubscription>`. Always check `.Success` before reading `.Data`.

```csharp
var ticker = await restClient.SpotApi.ExchangeData.GetTickersAsync(new[] { "ETHUSDT" });
if (!ticker.Success)
{
    Console.WriteLine($"Error: {ticker.Error}");
    return;
}

var lastPrice = ticker.Data.Single().LastPrice;
```

## Core Pattern: API Surface

```csharp
restClient.SpotApi.ExchangeData      // public spot market data
restClient.SpotApi.Account           // spot balances, bills, transfers
restClient.SpotApi.Trading           // spot orders and user trades

restClient.FuturesApi.ExchangeData   // futures market data, klines, funding, open interest
restClient.FuturesApi.Account        // futures balances, fees, configs, leverage, margin
restClient.FuturesApi.Trading        // futures orders, positions, conditional orders, TP/SL

socketClient.SpotApi                 // spot public and private streams
socketClient.FuturesApi              // futures public and private streams
```

## Core Pattern: Placing a Spot Order

Use `OrderType` for spot order placement. The library can generate a client order id; pass `clientOrderId` only when an existing workflow requires your own id.

```csharp
using Weex.Net.Enums;

var order = await restClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.1m,
    price: 2000m,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success) { Console.WriteLine(order.Error); return; }
Console.WriteLine(order.Data.OrderId);
```

## Core Pattern: Placing a Futures Order

Futures regular orders also use `OrderType`. Conditional futures orders use `FuturesOrderType`.

```csharp
await restClient.FuturesApi.Account.SetLeverageAsync(
    symbol: "ETHUSDT",
    marginMode: MarginType.Isolated,
    isolatedLongLeverage: 5,
    isolatedShortLeverage: 5);

var order = await restClient.FuturesApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    positionSide: PositionSide.Long,
    orderType: OrderType.Market,
    quantity: 0.01m);
```

## Core Pattern: WebSocket Subscriptions

Use `WeexSocketClient`. Store each `UpdateSubscription` and unsubscribe when shutting down.

```csharp
var socketClient = new WeexSocketClient();

var sub = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!sub.Success) { Console.WriteLine(sub.Error); return; }

await socketClient.UnsubscribeAsync(sub.Data);
```

Authenticated streams use the same socket client with credentials:

```csharp
var authSocket = new WeexSocketClient(options =>
{
    options.ApiCredentials = new WeexCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

await authSocket.SpotApi.SubscribeToOrderUpdatesAsync(update =>
{
    foreach (var order in update.Data.Orders)
        Console.WriteLine($"{order.Symbol} {order.Status}");
});
```

## Multi-Exchange via CryptoExchange.Net.SharedApis

```csharp
using CryptoExchange.Net.SharedApis;
using Weex.Net.Clients;

var shared = new WeexRestClient().SpotApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");

var ticker = await shared.GetSpotTickerAsync(new GetTickerRequest(symbol));
if (!ticker.Success) { Console.WriteLine(ticker.Error); return; }
Console.WriteLine(ticker.Data.LastPrice);
```

Weex shared REST interfaces include spot ticker, spot symbols, spot orders, balances, assets, fees, klines, order books, recent trades, deposits, withdrawals, futures ticker, futures symbols, futures orders, funding rates, leverage, and open interest. Socket shared interfaces include ticker, book ticker, klines, trades, balances, orders, user trades, futures positions.

Use `SharedClient.Discover()` on any shared client root when code needs runtime metadata about supported shared interfaces and endpoint options.

## Dependency Injection

```csharp
using Weex.Net;

services.AddWeex(options =>
{
    options.ApiCredentials = new WeexCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

// Inject IWeexRestClient and IWeexSocketClient.
```

## Common Pitfalls - Avoid

- Do not use raw `HttpClient` for Weex endpoints; use `WeexRestClient` or `WeexSocketClient`.
- Do not use generic `ApiCredentials`; use `WeexCredentials("key", "secret", "passphrase")`.
- Do not invent `UsdFuturesApi` or `CoinFuturesApi`; Weex exposes a single `FuturesApi`.
- Do not call `GetTickerAsync` on spot; use `GetTickersAsync(new[] { "ETHUSDT" })`.
- Do not use `FuturesOrderType` for regular futures orders; `PlaceOrderAsync` takes `OrderType`.
- Do not skip `.Success` checks before reading `.Data`.
- Do not use `.Result` or `.Wait()`; use `await`.
- Do not create clients per request; reuse clients or use DI.
- Do not forget to unsubscribe WebSocket subscriptions on shutdown.
- Do not assume testnet support; the current source exposes `WeexEnvironment.Live` and custom environments only.

## Environments

```csharp
var live = new WeexRestClient(o => o.Environment = WeexEnvironment.Live);

var custom = new WeexRestClient(o => o.Environment = WeexEnvironment.CreateCustom(
    "custom",
    spotRestAddress: "https://spot.example",
    spotSocketStreamAddress: "wss://spot-ws.example",
    futuresRestAddress: "https://futures.example",
    futuresSocketStreamAddress: "wss://futures-ws.example"));
```

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/Weex.Net/
- Examples: `Examples/ai-friendly/`
- Quick method map: `docs/ai-api-map.md`
- LLM index: `llms.txt`
- Full LLM context: `llms-full.txt`
- Source: https://github.com/JKorf/Weex.Net
- NuGet: https://www.nuget.org/packages/Weex.Net
