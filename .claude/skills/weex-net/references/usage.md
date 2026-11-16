# Usage examples

Install `Weex.Net`. Each C# block is independent and contains its imports. Trading blocks define functions without invoking them; compiling never submits an order. Use [API surfaces](api-surfaces.md) to select methods and read [operational considerations](safety.md) before account-changing workflows.

## Public setup and result handling

```csharp
using Weex.Net.Clients;

using var client = new WeexRestClient();
var result = await client.SpotApi.ExchangeData.GetTickersAsync(new[] { "ETHUSDT" });
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
var ticker = result.Data.SingleOrDefault();
if (ticker is not null)
    Console.WriteLine(ticker.LastPrice);
```

## Credentials and account reads

The three credential values are required for private access. Public requests need none.

```csharp
using Weex.Net;
using Weex.Net.Clients;

var key = Environment.GetEnvironmentVariable("WEEX_API_KEY")
    ?? throw new InvalidOperationException("Missing API key");
var secret = Environment.GetEnvironmentVariable("WEEX_API_SECRET")
    ?? throw new InvalidOperationException("Missing API secret");
var passphrase = Environment.GetEnvironmentVariable("WEEX_API_PASSPHRASE")
    ?? throw new InvalidOperationException("Missing API passphrase");
using var client = new WeexRestClient(options =>
    options.ApiCredentials = new WeexCredentials(key, secret, passphrase));
var spot = await client.SpotApi.Account.GetAccountInfoAsync();
if (!spot.Success)
    Console.WriteLine(spot.Error);
else
    Console.WriteLine(spot.Data);
var futures = await client.FuturesApi.Account.GetBalancesAsync();
if (!futures.Success)
    Console.WriteLine(futures.Error);
else
    Console.WriteLine($"Futures balance entries: {futures.Data.Length}");
```

## Spot order function

Pass an authenticated client and validated inputs when explicitly invoking this function. Order status and cancellation are separate operations; an unsuccessful or ambiguous submission needs reconciliation before another attempt.

```csharp
using Weex.Net.Clients;
using Weex.Net.Enums;

static async Task PlaceSpotLimitAsync(
    WeexRestClient client, string symbol, decimal quantity, decimal price, CancellationToken ct)
{
    var result = await client.SpotApi.Trading.PlaceOrderAsync(
        symbol, OrderSide.Buy, OrderType.Limit, quantity,
        price: price, timeInForce: TimeInForce.GoodTillCanceled, ct: ct);
    if (!result.Success)
    {
        Console.WriteLine(result.Error);
        return;
    }
    Console.WriteLine($"Submitted order: {result.Data.OrderId}");
}
```

## Regular and conditional futures functions

These functions accept position side and price inputs instead of changing leverage or margin mode implicitly. Both require an authenticated client and supported account configuration.

```csharp
using Weex.Net.Clients;
using Weex.Net.Enums;

static async Task PlaceFuturesLimitAsync(
    WeexRestClient client, string symbol, PositionSide positionSide,
    decimal quantity, decimal price, CancellationToken ct)
{
    var result = await client.FuturesApi.Trading.PlaceOrderAsync(
        symbol, OrderSide.Buy, positionSide, OrderType.Limit, quantity,
        price: price, timeInForce: TimeInForce.GoodTillCanceled, ct: ct);
    if (!result.Success)
        Console.WriteLine(result.Error);
    else if (!result.Data.Success)
        Console.WriteLine($"{result.Data.ErrorCode}: {result.Data.ErrorMessage}");
    else
        Console.WriteLine(result.Data.OrderId);
}

static async Task PlaceConditionalAsync(
    WeexRestClient client, string symbol, PositionSide positionSide,
    decimal quantity, decimal triggerPrice, CancellationToken ct)
{
    var result = await client.FuturesApi.Trading.PlaceConditionalOrderAsync(
        symbol, OrderSide.Sell, positionSide, FuturesOrderType.StopMarket,
        quantity, triggerPrice, ct: ct);
    if (!result.Success)
        Console.WriteLine(result.Error);
    else if (!result.Data.Success)
        Console.WriteLine($"{result.Data.ErrorCode}: {result.Data.ErrorMessage}");
    else
        Console.WriteLine(result.Data.OrderId);
}
```

## WebSocket subscription lifecycle

Private subscriptions use the same credential configuration as private REST. See the checkout-only complete socket example below for public and private streams together.

```csharp
using Weex.Net.Clients;

using var socket = new WeexSocketClient();
var subscription = await socket.SpotApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT", update => Console.WriteLine(update.Data.LastPrice));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socket.UnsubscribeAsync(subscription.Data);
}
```

## Shared API V2

```csharp
using CryptoExchange.Net.SharedApis;
using Weex.Net.Clients;

using var client = new WeexRestClient();
IGetTickerRest ticker = client.SpotApi.SharedApi;
var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
var result = await ticker.GetTickerAsync(new GetTickerRequest(symbol));
if (!result.Success)
    Console.WriteLine($"{ticker.Exchange}: {result.Error}");
else
    Console.WriteLine(result.Data.LastPrice);
```

## Dependency injection and discovery

Inject a typed aggregate property when the surface is known. Runtime lookup must specify the intended trading mode and handle missing support. With several exchanges registered, common capability injection alone does not identify the intended exchange.

```csharp
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
using Weex.Net;
using Weex.Net.Interfaces.Clients;

var services = new ServiceCollection();
services.AddWeex(options => options.Environment = WeexEnvironment.Live);
using var provider = services.BuildServiceProvider();
var shared = provider.GetRequiredService<IWeexSharedApiClient>();
IGetTickerRest knownSurface = shared.SpotRest;
var match = shared.GetCapability(
    SharedCapabilities.Tickers.GetTicker.Rest, TradingMode.Spot);
if (match is null)
    return;
Console.WriteLine(match.Options);
var result = await match.Capability.GetTickerAsync(
    new GetTickerRequest(new SharedSymbol(TradingMode.Spot, "ETH", "USDT")));
if (!result.Success)
    Console.WriteLine(result.Error);
else
    Console.WriteLine(result.Data.LastPrice);
```

Checkout-only: [standalone programs](../../../../Examples/ai-friendly/README.md), including [public/private socket lifecycle](../../../../Examples/ai-friendly/03-websocket.cs), and [complete projects](../../../../Examples/README.md). Build these without running to validate usage without exchange operations. Some programs place live orders when run with valid credentials.
