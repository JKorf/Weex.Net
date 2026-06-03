using Weex.Net;
using Weex.Net.Clients;
using Weex.Net.Enums;

const string spotSymbol = "ETHUSDT";
const string futuresSymbol = "ETHUSDT";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var apiSecret = "SECRET";
var apiPassphrase = "PASSPHRASE";

Console.WriteLine("Weex.Net V3 order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new WeexRestClient(options =>
{
    options.ApiCredentials = new WeexCredentials(apiKey, apiSecret, apiPassphrase);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesLimitOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(WeexRestClient client)
{
    Console.WriteLine($"Placing spot V3 limit buy order for {spotSymbol}...");

    var tickers = await client.SpotApi.ExchangeData.GetTickersAsync([spotSymbol]);
    if (!tickers.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {tickers.Error}");
        return;
    }

    var ticker = tickers.Data.SingleOrDefault();
    if (ticker == null)
    {
        Console.WriteLine($"Spot ticker for {spotSymbol} was not returned.");
        return;
    }

    var safePrice = Math.Round(ticker.LastPrice * 0.95m, 2);
    var order = await client.SpotApi.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        side: OrderSide.Buy,
        orderType: OrderType.Limit,
        quantity: 0.1m,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCanceled);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.OrderId} at {safePrice}");

    var orderStatus = await client.SpotApi.Trading.GetOrderAsync(orderId: order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.Status}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.SpotApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.OrderId}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesLimitOrderExampleAsync(WeexRestClient client)
{
    Console.WriteLine($"Placing futures V3 limit sell order for {futuresSymbol}...");

    var tickers = await client.FuturesApi.ExchangeData.GetTickersAsync(futuresSymbol);
    if (!tickers.Success)
    {
        Console.WriteLine($"Failed to get futures ticker: {tickers.Error}");
        return;
    }

    var ticker = tickers.Data.SingleOrDefault();
    if (ticker == null)
    {
        Console.WriteLine($"Futures ticker for {futuresSymbol} was not returned.");
        return;
    }

    var safePrice = Math.Round(ticker.LastPrice * 1.05m, 2);
    var order = await client.FuturesApi.Trading.PlaceOrderAsync(
        symbol: futuresSymbol,
        side: OrderSide.Sell,
        positionSide: PositionSide.Short,
        orderType: OrderType.Limit,
        quantity: 0.01m,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCanceled);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    if (order.Data.OrderId == null)
    {
        Console.WriteLine("Futures order placement did not return an order id.");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data.OrderId} at {safePrice}");

    var orderStatus = await client.FuturesApi.Trading.GetOrderAsync(order.Data.OrderId.Value);
    if (orderStatus.Success)
        Console.WriteLine($"Futures order status: {orderStatus.Data.Status}, executed: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");

    var cancel = await client.FuturesApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data.OrderId}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
