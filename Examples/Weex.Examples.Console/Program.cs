
using Weex.Net.Clients;

// REST
var restClient = new WeexRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickersAsync(["ETHUSDT"]);
Console.WriteLine($"Rest client ticker price for ETHUSDT: {ticker.Data.Single().LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new WeexSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.LastPrice}");
});

Console.ReadLine();
