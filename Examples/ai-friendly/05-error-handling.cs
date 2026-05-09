// 05-error-handling.cs
//
// Demonstrates: WebCallResult patterns, retry logic, common Weex.Net validation.
//
// Setup: dotnet add package Weex.Net

using CryptoExchange.Net.Objects;
using Weex.Net;
using Weex.Net.Clients;
using Weex.Net.Enums;

var client = new WeexRestClient(options =>
{
    options.ApiCredentials = new WeexCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

// ---- 1. THE BASIC PATTERN ----
// REST methods return WebCallResult<T>; socket subscriptions return CallResult<T>.
// .Success is true/false. .Data is only valid when .Success is true.
// .Error contains Code, Message, ErrorType, and IsTransient.

var result = await client.SpotApi.ExchangeData.GetTickersAsync(new[] { "ETHUSDT" });

if (result.Success)
{
    Console.WriteLine($"Price: {result.Data.Single().LastPrice}");
}
else
{
    Console.WriteLine($"Code:      {result.Error?.Code}");
    Console.WriteLine($"Message:   {result.Error?.Message}");
    Console.WriteLine($"Type:      {result.Error?.ErrorType}");
    Console.WriteLine($"Transient: {result.Error?.IsTransient}");
}

// ---- 2. SIMPLE RETRY WITH BACKOFF ----
// Retry only transient errors such as rate limits, network blips, or server overload.

async Task<WebCallResult<T>> WithRetry<T>(
    Func<Task<WebCallResult<T>>> call,
    int maxAttempts = 3)
{
    WebCallResult<T> last = default!;
    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success) return last;
        if (last.Error?.IsTransient != true) return last;

        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }

    return last;
}

var ticker = await WithRetry(
    () => client.SpotApi.ExchangeData.GetTickersAsync(new[] { "ETHUSDT" }));

if (!ticker.Success)
{
    Console.WriteLine($"Ticker still failed: {ticker.Error}");
}

// ---- 3. ORDER PARAMETER VALIDATION FROM EXCHANGE INFO ----
// Weex spot exchange info exposes TickSize and StepSize. Use those to align price/quantity
// before placing an order, avoiding avoidable exchange-side validation errors.

var exchangeInfo = await client.SpotApi.ExchangeData.GetExchangeInfoAsync(new[] { "ETHUSDT" });
if (!exchangeInfo.Success || exchangeInfo.Data.Symbols.Length == 0)
{
    Console.WriteLine("Cannot fetch symbol info; aborting order.");
    return;
}

var symbolInfo = exchangeInfo.Data.Symbols.First();
var rawQuantity = 0.12345678m;
var rawPrice = 2000.123456m;

decimal FloorToStep(decimal value, decimal step)
{
    if (step <= 0) return value;
    return Math.Floor(value / step) * step;
}

var validQuantity = FloorToStep(rawQuantity, symbolInfo.StepSize);
var validPrice = FloorToStep(rawPrice, symbolInfo.TickSize);

var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: validQuantity,
    price: validPrice,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
{
    var category = order.Error?.IsTransient == true
        ? "Transient - should retry with backoff"
        : "Permanent - surface to caller";

    Console.WriteLine($"{category}: {order.Error?.Code} {order.Error?.Message}");
}

// ---- 4. COMMON WEEX.NET ERROR SCENARIOS ----
//
// Authentication failures:
//   Use WeexCredentials("key", "secret", "passphrase"). The passphrase is required.
//
// Invalid symbol or unavailable product:
//   Fetch ExchangeData.GetExchangeInfoAsync(...) or Account.GetTradingSymbolsAsync()
//   before constructing orders for user-provided symbols.
//
// Order precision / minimum size errors:
//   Use WeexSymbol.TickSize, StepSize, MinTradeQuantity, and MaxTradeQuantity from
//   SpotApi.ExchangeData.GetExchangeInfoAsync(...).
//
// Futures order type confusion:
//   Regular futures PlaceOrderAsync uses OrderType. Conditional futures orders use FuturesOrderType.
//
// Testnet assumptions:
//   The current source exposes WeexEnvironment.Live and custom environments only.

// ---- 5. EXCEPTIONS VS ERROR RESULTS ----
// Exchange/API errors are returned through WebCallResult.Error, not thrown.
// Exceptions are typically configuration, cancellation, disposal, or programmer errors.

// Common variations:
//   With CancellationToken:    pass ct: cancellationToken
//   With timeout per request:  options.RequestTimeout = TimeSpan.FromSeconds(10)
//   Socket subscriptions:      check subscription.Success before using subscription.Data
