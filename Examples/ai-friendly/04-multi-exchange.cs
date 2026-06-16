// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same pattern works across exchanges that implement the shared interfaces.
//
// Setup:
//   dotnet add package Weex.Net
//   dotnet add package Binance.Net    // optional, for another exchange
//   dotnet add package JK.OKX.Net     // optional, for another exchange

using CryptoExchange.Net.SharedApis;
using Weex.Net.Clients;

// ---- THE PATTERN ----
// Each exchange client exposes a SharedClient property on supported API surfaces.
// Use SharedClient.Discover() when you need runtime capability metadata.
ISpotTickerRestClient weexShared = new WeexRestClient().SpotApi.SharedClient;

// To add other exchanges, install their packages and assign their SharedClient:
//   ISpotTickerRestClient binanceShared = new BinanceRestClient().SpotApi.SharedClient;
//   ISpotTickerRestClient okxShared     = new OKXRestClient().UnifiedApi.SharedClient;

var ethusdt = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");

await PrintTicker(weexShared, ethusdt);

async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// ---- AVAILABLE WEEX SHARED CLIENTS ----
// REST:
//   Spot: ISpotTickerRestClient, ISpotSymbolRestClient, ISpotOrderRestClient,
//         ISpotOrderClientIdRestClient, IBalanceRestClient, IAssetsRestClient,
//         IFeeRestClient, IOrderBookRestClient, IRecentTradeRestClient,
//         IKlineRestClient, IDepositRestClient, IWithdrawalRestClient,
//         IBookTickerRestClient
//   Futures: IFuturesTickerRestClient, IFuturesSymbolRestClient,
//            IFuturesOrderRestClient, IFuturesTriggerOrderRestClient,
//            IBalanceRestClient, IFundingRateRestClient, ILeverageRestClient,
//            IOpenInterestRestClient, IOrderBookRestClient, IRecentTradeRestClient,
//            IKlineRestClient, IIndexPriceKlineRestClient, IMarkPriceKlineRestClient
// WebSocket:
//   ITickerSocketClient, IBookTickerSocketClient, IKlineSocketClient,
//   ITradeSocketClient, IBalanceSocketClient, IUserTradeSocketClient,
//   ISpotOrderSocketClient, IFuturesOrderSocketClient, IPositionSocketClient

// ---- WEBSOCKET EXAMPLE ----
// Shared socket subscriptions return WebSocketResult<UpdateSubscription>.
var weexSocket = new WeexSocketClient();
ITickerSocketClient weexTickerSocket = weexSocket.SpotApi.SharedClient;

var sub = await weexTickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(ethusdt),
    update => Console.WriteLine($"[{weexTickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();

await weexSocket.UnsubscribeAsync(sub.Data);

// Common variations:
//   Multi-exchange ticker scan: loop over List<ISpotTickerRestClient>
//   Cross-exchange orderbook:  IOrderBookSocketClient on each exchange
//   Unified futures orders:    IFuturesOrderRestClient
//   Symbol normalization:      SharedSymbol handles common exchange formatting differences
