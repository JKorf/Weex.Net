// Install Weex.Net. Public streams need no credentials; private streams use environment variables.
using Weex.Net;
using Weex.Net.Clients;
using Weex.Net.Enums;

using var publicSocket = new WeexSocketClient();
var key = Environment.GetEnvironmentVariable("WEEX_API_KEY");
var secret = Environment.GetEnvironmentVariable("WEEX_API_SECRET");
var passphrase = Environment.GetEnvironmentVariable("WEEX_API_PASSPHRASE");
using var authSocket = new WeexSocketClient(options =>
{
    if (key is not null && secret is not null && passphrase is not null)
        options.ApiCredentials = new WeexCredentials(key, secret, passphrase);
});
try
{
    var ticker = await publicSocket.SpotApi.SubscribeToTickerUpdatesAsync(
        "ETHUSDT", update => Console.WriteLine($"Spot: {update.Data.LastPrice}"));
    if (!ticker.Success)
    {
        Console.WriteLine(ticker.Error);
        return;
    }
    var klines = await publicSocket.FuturesApi.SubscribeToKlineUpdatesAsync(
        "ETHUSDT", KlineInterval.OneMinute, update =>
        {
            foreach (var kline in update.Data)
                Console.WriteLine($"Futures close: {kline.ClosePrice}");
        });
    if (!klines.Success)
    {
        Console.WriteLine(klines.Error);
        return;
    }
    if (key is not null && secret is not null && passphrase is not null)
    {
        var orders = await authSocket.SpotApi.SubscribeToOrderUpdatesAsync(update =>
        {
            foreach (var order in update.Data.Orders)
                Console.WriteLine($"{order.Symbol}: {order.Status}");
        });
        if (!orders.Success)
        {
            Console.WriteLine(orders.Error);
            return;
        }
    }
    Console.WriteLine("Press Enter to unsubscribe");
    Console.ReadLine();
}
finally
{
    // These clients are owned exclusively by this example, including any partially established setup.
    await Task.WhenAll(publicSocket.UnsubscribeAllAsync(), authSocket.UnsubscribeAllAsync());
}
