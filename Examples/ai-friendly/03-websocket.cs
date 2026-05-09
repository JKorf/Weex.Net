// 03-websocket.cs
//
// Demonstrates: WebSocket subscriptions for spot/futures public streams and
// private user streams, with proper teardown.
//
// Setup: dotnet add package Weex.Net

using Weex.Net;
using Weex.Net.Clients;
using Weex.Net.Enums;

// ---- 1. PUBLIC SOCKET CLIENT ----
var publicSocket = new WeexSocketClient();

var tickerSub = await publicSocket.SpotApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT",
    update =>
    {
        Console.WriteLine($"Spot ETH: {update.Data.LastPrice} (24h vol {update.Data.Volume})");
    });

if (!tickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe spot ticker: {tickerSub.Error}");
    return;
}

var klineSub = await publicSocket.FuturesApi.SubscribeToKlineUpdatesAsync(
    "ETHUSDT",
    KlineInterval.OneMinute,
    update =>
    {
        foreach (var kline in update.Data)
        {
            Console.WriteLine($"Futures {kline.Symbol} 1m: O={kline.OpenPrice} H={kline.HighPrice} L={kline.LowPrice} C={kline.ClosePrice}");
        }
    });

if (!klineSub.Success)
{
    Console.WriteLine($"Failed to subscribe futures klines: {klineSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    return;
}

// ---- 2. AUTHENTICATED SOCKET CLIENT ----
var authSocket = new WeexSocketClient(options =>
{
    options.ApiCredentials = new WeexCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

var accountSub = await authSocket.SpotApi.SubscribeToAccountUpdatesAsync(update =>
{
    foreach (var balance in update.Data.Balances)
        Console.WriteLine($"Balance update {balance.Asset}: available={balance.Available}, frozen={balance.Frozen}");
});

if (!accountSub.Success)
{
    Console.WriteLine($"Failed to subscribe account updates: {accountSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    return;
}

var orderSub = await authSocket.FuturesApi.SubscribeToOrderUpdatesAsync(update =>
{
    foreach (var order in update.Data.Orders)
        Console.WriteLine($"Futures order {order.Id} {order.Symbol}: {order.Status}");
});

if (!orderSub.Success)
{
    Console.WriteLine($"Failed to subscribe order updates: {orderSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    await authSocket.UnsubscribeAsync(accountSub.Data);
    return;
}

Console.WriteLine("All subscriptions active. Press Enter to teardown...");
Console.ReadLine();

// ---- 3. TEARDOWN ----
await publicSocket.UnsubscribeAsync(tickerSub.Data);
await publicSocket.UnsubscribeAsync(klineSub.Data);
await authSocket.UnsubscribeAsync(accountSub.Data);
await authSocket.UnsubscribeAsync(orderSub.Data);

Console.WriteLine("Clean shutdown complete.");

// Common variations:
//   Multiple tickers:      SubscribeToTickerUpdatesAsync(new[] { "ETHUSDT", "BTCUSDT" }, handler)
//   Order book stream:     SubscribeToOrderBookUpdatesAsync(symbol, 15, handler)
//   Spot order updates:    authSocket.SpotApi.SubscribeToOrderUpdatesAsync(handler)
//   Futures positions:     authSocket.FuturesApi.SubscribeToPositionUpdatesAsync(handler)
