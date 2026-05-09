// 02-futures.cs
//
// Demonstrates: Futures setup, leverage, market order, position retrieval,
// close positions.
//
// Setup: dotnet add package Weex.Net
// Substitute API_KEY / API_SECRET / API_PASSPHRASE. The API key must allow futures trading.

using Weex.Net;
using Weex.Net.Clients;
using Weex.Net.Enums;

var client = new WeexRestClient(options =>
{
    options.ApiCredentials = new WeexCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
});

const string symbol = "ETHUSDT";

// ---- 1. SET LEVERAGE ----
// Weex exposes SetLeverageAsync on FuturesApi.Account. For isolated mode, set long and short leverage.
var leverage = await client.FuturesApi.Account.SetLeverageAsync(
    symbol: symbol,
    marginMode: MarginType.Isolated,
    isolatedLongLeverage: 5,
    isolatedShortLeverage: 5);

if (!leverage.Success)
{
    Console.WriteLine($"Failed to set leverage: {leverage.Error}");
    return;
}

Console.WriteLine($"Leverage configured for {symbol}");

// ---- 2. PLACE MARKET ORDER (open long position) ----
// Regular futures orders use OrderType, not FuturesOrderType. FuturesOrderType is for conditional orders.
var openOrder = await client.FuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    positionSide: PositionSide.Long,
    orderType: OrderType.Market,
    quantity: 0.01m);

if (!openOrder.Success)
{
    Console.WriteLine($"Failed to open position: {openOrder.Error}");
    return;
}

Console.WriteLine($"Opened position via order {openOrder.Data.OrderId}");

// ---- 3. GET CURRENT POSITION ----
var positions = await client.FuturesApi.Trading.GetPositionAsync(symbol);
if (!positions.Success)
{
    Console.WriteLine($"Failed to get positions: {positions.Error}");
    return;
}

var position = positions.Data.FirstOrDefault(p => p.Quantity != 0);
if (position == null)
{
    Console.WriteLine("No open position found (order may not have filled yet).");
    return;
}

Console.WriteLine($"Position: {position.Quantity} {symbol} on {position.Side}");
Console.WriteLine($"Unrealized PnL: {position.UnrealizePnl}");
Console.WriteLine($"Liquidation price: {position.LiquidationPrice}");

// ---- 4. CLOSE POSITIONS ----
// Weex exposes ClosePositionsAsync for closing open futures positions by symbol.
var close = await client.FuturesApi.Trading.ClosePositionsAsync(symbol);
if (close.Success)
{
    Console.WriteLine($"Close position requests: {close.Data.Length}");
}

// Common variations:
//   Limit order:          orderType: OrderType.Limit, add price + timeInForce
//   Conditional order:    PlaceConditionalOrderAsync(..., FuturesOrderType.StopMarket, triggerPrice: ...)
//   Margin mode:          client.FuturesApi.Account.SetMarginModeAsync(symbol, MarginType.Isolated)
//   Open orders:          client.FuturesApi.Trading.GetOpenOrdersAsync(symbol)
//   Funding rate:         client.FuturesApi.ExchangeData.GetFundingRateAsync(symbol)
