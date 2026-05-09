// 01-spot-quickstart.cs
//
// Demonstrates: client setup, public spot market data, authenticated balances,
// limit order placement, order status check.
//
// Setup:
//   dotnet new console -n SpotQuickstart && cd SpotQuickstart
//   dotnet add package Weex.Net
//   Copy this file content into Program.cs
//   Substitute API_KEY / API_SECRET / API_PASSPHRASE below
//   dotnet run

using Weex.Net;
using Weex.Net.Clients;
using Weex.Net.Enums;

// ---- 1. PUBLIC CLIENT (no credentials needed for market data) ----
// Reuse this client across the application; do not create one per request.
var publicClient = new WeexRestClient();

var ticker = await publicClient.SpotApi.ExchangeData.GetTickersAsync(new[] { "ETHUSDT" });
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

var ethTicker = ticker.Data.Single();
Console.WriteLine($"ETH/USDT last price: {ethTicker.LastPrice}");
Console.WriteLine($"24h volume: {ethTicker.Volume} ETH");

// ---- 2. AUTHENTICATED CLIENT (for account / trading) ----
var tradingClient = new WeexRestClient(options =>
{
    options.ApiCredentials = new WeexCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

var account = await tradingClient.SpotApi.Account.GetAccountInfoAsync();
if (!account.Success)
{
    Console.WriteLine($"Failed to get account: {account.Error}");
    return;
}

foreach (var balance in account.Data.Balances.Where(b => b.Free + b.Locked > 0))
{
    Console.WriteLine($"{balance.Asset}: {balance.Free} free, {balance.Locked} locked");
}

// ---- 3. PLACE A LIMIT BUY ORDER ----
// Limit, Buy, 0.1 ETH at a price 5% below current; likely will not fill immediately.
// Let Weex.Net generate the client order id unless you need a specific external id.
var safePrice = Math.Round(ethTicker.LastPrice * 0.95m, 2);

var order = await tradingClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.1m,
    price: safePrice,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
{
    Console.WriteLine($"Failed to place order: {order.Error}");
    return;
}

Console.WriteLine($"Placed order {order.Data.OrderId} at {safePrice}");

// ---- 4. CHECK ORDER STATUS ----
var status = await tradingClient.SpotApi.Trading.GetOrderAsync(orderId: order.Data.OrderId);
if (status.Success)
{
    Console.WriteLine($"Order status: {status.Data.Status}, filled: {status.Data.QuantityFilled}");
}

// ---- 5. CANCEL THE ORDER (cleanup for this example) ----
var cancel = await tradingClient.SpotApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
if (cancel.Success)
{
    Console.WriteLine($"Cancelled order {order.Data.OrderId}");
}

// Common variations:
//   Market order:        orderType: OrderType.Market, omit price and timeInForce
//   Query open orders:   tradingClient.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT")
//   Cancel by client id: tradingClient.SpotApi.Trading.CancelOrderAsync(clientOrderId: id)
//   Price endpoint:      publicClient.SpotApi.ExchangeData.GetPricesAsync(new[] { "ETHUSDT" })
