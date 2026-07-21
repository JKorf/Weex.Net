# Copilot Instructions for Weex.Net

This repository is **Weex.Net**, a strongly typed C#/.NET client library for the Weex cryptocurrency exchange API. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes Weex.Net, follow these conventions:

## Use Weex.Net, Not Raw HTTP

Never generate `HttpClient` calls to Weex API endpoints. Always use `WeexRestClient` or `WeexSocketClient` so signing, rate limiting, models, and error handling stay correct.

## Client Setup

```csharp
using Weex.Net;
using Weex.Net.Clients;

var restClient = new WeexRestClient(options =>
{
    options.ApiCredentials = new WeexCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});
```

For public market data only, credentials are not required: `new WeexRestClient()`.

## Result Handling

REST methods return `WebCallResult<T>` and socket subscriptions return `CallResult<UpdateSubscription>`. Always check `.Success` before reading `.Data`; errors are on `.Error`.

## API Structure

- `restClient.SpotApi.ExchangeData` - public spot market data
- `restClient.SpotApi.Account` - spot balances, bills, transfers
- `restClient.SpotApi.Trading` - spot orders and user trades
- `restClient.FuturesApi.ExchangeData` - futures market data, funding, open interest
- `restClient.FuturesApi.Account` - futures balances, fees, margin and leverage settings
- `restClient.FuturesApi.Trading` - futures orders, positions, conditional orders, TP/SL
- `socketClient.SpotApi` - spot public/private streams
- `socketClient.FuturesApi` - futures public/private streams

## Order Placement

Use `OrderType` for spot orders and regular futures orders. Use `FuturesOrderType` only for futures conditional orders.

Let the library generate a client order id unless an existing workflow requires a custom one.

## WebSocket Pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

## Cross-Exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces (`ISpotTickerRestClient`, `ISpotOrderRestClient`, `IFuturesOrderRestClient`, etc.) through `.SharedClient`.

For shared symbol discovery, use `ISpotSymbolRestClient` or `IFuturesSymbolRestClient`. Successful symbol queries honor `GetSymbolsRequest` filters, refresh `SpotSymbolCatalog` / `FuturesSymbolCatalog`, and return display-name and asset-class metadata.

## Avoid

- Generic `ApiCredentials`; use `WeexCredentials`
- Binance-style `UsdFuturesApi` or `CoinFuturesApi`; use `FuturesApi`
- Spot `GetTickerAsync`; use `GetTickersAsync(new[] { symbol })`
- Testnet assumptions; the current source exposes live and custom environments
- Synchronous `.Result` / `.Wait()`
- Instantiating clients per request
- Reading `.Data` before checking `.Success`

## Reference

For detailed patterns and pitfalls see `AGENTS.md`, `llms.txt`, and `llms-full.txt` in the repository root. See `docs/ai-api-map.md` for the intent-to-method table and `Examples/ai-friendly/` for compilable examples.
