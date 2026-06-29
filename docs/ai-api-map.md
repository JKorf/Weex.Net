# Weex.Net AI API Quick Map

Use this file to route common user intents to the correct Weex.Net client member. If a method name or parameter is not listed here, inspect `Weex.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new WeexRestClient()` |
| WebSocket streams | `new WeexSocketClient()` |
| API key authentication | `options.ApiCredentials = new WeexCredentials("key", "secret", "passphrase")` |
| Live environment | `WeexEnvironment.Live` |
| Custom environment | `WeexEnvironment.CreateCustom(...)` |
| Dependency injection | `services.AddWeex(options => { ... })` |

## Spot REST

| User intent | Weex.Net member |
|---|---|
| Get server time | `client.SpotApi.ExchangeData.GetServerTimeAsync()` |
| Get assets and network info | `client.SpotApi.ExchangeData.GetAssetsAsync()` |
| Get spot exchange info | `client.SpotApi.ExchangeData.GetExchangeInfoAsync()` |
| Get spot exchange info for symbols | `client.SpotApi.ExchangeData.GetExchangeInfoAsync(new[] { "ETHUSDT" })` |
| Get spot prices | `client.SpotApi.ExchangeData.GetPricesAsync(new[] { "ETHUSDT" })` |
| Get all spot prices | `client.SpotApi.ExchangeData.GetPricesAsync()` |
| Get spot 24h ticker | `client.SpotApi.ExchangeData.GetTickersAsync(new[] { "ETHUSDT" })` |
| Get all spot tickers | `client.SpotApi.ExchangeData.GetTickersAsync()` |
| Get recent trades | `client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT")` |
| Get klines/candles | `client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", KlineInterval.OneMinute)` |
| Get order book | `client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT")` |
| Get book ticker | `client.SpotApi.ExchangeData.GetBookTickersAsync(new[] { "ETHUSDT" })` |
| Get trading symbols | `client.SpotApi.Account.GetTradingSymbolsAsync()` |
| Get account info and balances | `client.SpotApi.Account.GetAccountInfoAsync()` |
| Get account bills | `client.SpotApi.Account.GetAccountBillsAsync(...)` |
| Get funding bills | `client.SpotApi.Account.GetFundingBillsAsync(...)` |
| Get transfer history | `client.SpotApi.Account.GetTransferHistoryAsync(...)` |
| Place spot order | `client.SpotApi.Trading.PlaceOrderAsync(...)` |
| Cancel spot order | `client.SpotApi.Trading.CancelOrderAsync(orderId: orderId)` |
| Cancel spot order by client id | `client.SpotApi.Trading.CancelOrderAsync(clientOrderId: clientOrderId)` |
| Cancel all spot orders for symbol | `client.SpotApi.Trading.CancelAllSymbolOrdersAsync("ETHUSDT")` |
| Cancel multiple spot orders | `client.SpotApi.Trading.CancelOrdersAsync(orderIds: orderIds)` |
| Query spot order | `client.SpotApi.Trading.GetOrderAsync(orderId: orderId)` |
| Query spot order by client id | `client.SpotApi.Trading.GetOrderAsync(clientOrderId: clientOrderId)` |
| Get open spot orders | `client.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT")` |
| Get spot order history | `client.SpotApi.Trading.GetOrderHistoryAsync("ETHUSDT")` |
| Get spot user trades | `client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT")` |

## Futures REST

| User intent | Weex.Net member |
|---|---|
| Get futures server time | `client.FuturesApi.ExchangeData.GetServerTimeAsync()` |
| Get futures exchange info | `client.FuturesApi.ExchangeData.GetExchangeInfoAsync()` |
| Get futures exchange info for symbol | `client.FuturesApi.ExchangeData.GetExchangeInfoAsync("ETHUSDT")` |
| Get futures order book | `client.FuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT")` |
| Get futures 24h ticker | `client.FuturesApi.ExchangeData.GetTickersAsync("ETHUSDT")` |
| Get all futures 24h tickers | `client.FuturesApi.ExchangeData.GetTickersAsync()` |
| Get futures book ticker | `client.FuturesApi.ExchangeData.GetBookTickersAsync("ETHUSDT")` |
| Get futures recent trades | `client.FuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSDT")` |
| Get futures klines | `client.FuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", FuturesKlineInterval.OneMinute)` |
| Get index price klines | `client.FuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", FuturesKlineInterval.OneMinute)` |
| Get mark price klines | `client.FuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", FuturesKlineInterval.OneMinute)` |
| Get kline history | `client.FuturesApi.ExchangeData.GetKlineHistoryAsync("ETHUSDT", KlineInterval.OneMinute)` |
| Get index/mark price | `client.FuturesApi.ExchangeData.GetPriceAsync("ETHUSDT")` |
| Get open interest | `client.FuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSDT")` |
| Get current funding rate | `client.FuturesApi.ExchangeData.GetFundingRateAsync("ETHUSDT")` |
| Get funding rate history | `client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("ETHUSDT")` |
| Get futures trading symbols | `client.FuturesApi.ExchangeData.GetTradingSymbolsAsync()` |
| Get futures balances | `client.FuturesApi.Account.GetBalancesAsync()` |
| Get futures fees | `client.FuturesApi.Account.GetTradingFeesAsync("ETHUSDT")` |
| Get account config | `client.FuturesApi.Account.GetAccountConfigAsync()` |
| Get symbol config | `client.FuturesApi.Account.GetSymbolConfigAsync("ETHUSDT")` |
| Get futures account bills | `client.FuturesApi.Account.GetAccountBillsAsync(...)` |
| Set margin mode | `client.FuturesApi.Account.SetMarginModeAsync("ETHUSDT", MarginType.Isolated)` |
| Set leverage | `client.FuturesApi.Account.SetLeverageAsync("ETHUSDT", marginMode: MarginType.Isolated, isolatedLongLeverage: 5, isolatedShortLeverage: 5)` |
| Adjust isolated margin | `client.FuturesApi.Account.AdjustIsolatedMarginAsync(positionId, quantity, MarginAdjustType.Increase)` |
| Set auto append margin | `client.FuturesApi.Account.SetAutoAppendMarginAsync(positionId, true)` |
| Get all positions | `client.FuturesApi.Trading.GetPositionsAsync()` |
| Get symbol positions | `client.FuturesApi.Trading.GetPositionAsync("ETHUSDT")` |
| Place futures order | `client.FuturesApi.Trading.PlaceOrderAsync(...)` |
| Cancel futures order | `client.FuturesApi.Trading.CancelOrderAsync(orderId: orderId)` |
| Cancel futures order by client id | `client.FuturesApi.Trading.CancelOrderAsync(clientOrderId: clientOrderId)` |
| Cancel all futures orders | `client.FuturesApi.Trading.CancelAllOrdersAsync("ETHUSDT")` |
| Cancel multiple futures orders | `client.FuturesApi.Trading.CancelOrdersAsync(orderIds: orderIds)` |
| Close positions | `client.FuturesApi.Trading.ClosePositionsAsync("ETHUSDT")` |
| Query futures order | `client.FuturesApi.Trading.GetOrderAsync(orderId)` |
| Get open futures orders | `client.FuturesApi.Trading.GetOpenOrdersAsync("ETHUSDT")` |
| Get futures order history | `client.FuturesApi.Trading.GetOrderHistoryAsync("ETHUSDT")` |
| Get futures user trades | `client.FuturesApi.Trading.GetUserTradesAsync("ETHUSDT")` |
| Place conditional order | `client.FuturesApi.Trading.PlaceConditionalOrderAsync(...)` |
| Cancel conditional order | `client.FuturesApi.Trading.CancelConditionalOrderAsync(orderId)` |
| Cancel all conditional orders | `client.FuturesApi.Trading.CancelAllConditionalOrdersAsync("ETHUSDT")` |
| Get open conditional orders | `client.FuturesApi.Trading.GetOpenConditionalOrdersAsync("ETHUSDT")` |
| Get conditional order history | `client.FuturesApi.Trading.GetConditionalOrderHistoryAsync("ETHUSDT")` |
| Place take-profit/stop-loss order | `client.FuturesApi.Trading.PlaceTpSlOrderAsync(...)` |
| Edit take-profit/stop-loss order | `client.FuturesApi.Trading.EditTpSlOrderAsync(orderId, triggerPrice)` |

## Spot WebSocket

| User intent | Weex.Net member |
|---|---|
| Subscribe spot ticker updates | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe many spot ticker updates | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe spot book ticker | `socketClient.SpotApi.SubscribeToBookTickerUpdatesAsync(symbol, handler)` |
| Subscribe spot order book | `socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(symbol, 15, handler)` |
| Subscribe spot klines | `socketClient.SpotApi.SubscribeToKlineUpdatesAsync(symbol, KlineInterval.OneMinute, handler)` |
| Subscribe spot trades | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe spot account updates | `socketClient.SpotApi.SubscribeToAccountUpdatesAsync(handler)` |
| Subscribe spot order updates | `socketClient.SpotApi.SubscribeToOrderUpdatesAsync(handler)` |
| Subscribe spot user trade updates | `socketClient.SpotApi.SubscribeToUserTradeUpdatesAsync(handler)` |

## Futures WebSocket

| User intent | Weex.Net member |
|---|---|
| Subscribe futures ticker updates | `socketClient.FuturesApi.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe many futures ticker updates | `socketClient.FuturesApi.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe futures klines | `socketClient.FuturesApi.SubscribeToKlineUpdatesAsync(symbol, KlineInterval.OneMinute, handler)` |
| Subscribe futures order book | `socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync(symbol, 15, handler)` |
| Subscribe futures trades | `socketClient.FuturesApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe futures account updates | `socketClient.FuturesApi.SubscribeToAccountUpdatesAsync(handler)` |
| Subscribe futures position updates | `socketClient.FuturesApi.SubscribeToPositionUpdatesAsync(handler)` |
| Subscribe futures order updates | `socketClient.FuturesApi.SubscribeToOrderUpdatesAsync(handler)` |
| Subscribe futures user trade updates | `socketClient.FuturesApi.SubscribeToUserTradeUpdatesAsync(handler)` |

## SharedApis

| User intent | Weex.Net member or interface |
|---|---|
| Shared spot REST client | `new WeexRestClient().SpotApi.SharedClient` |
| Shared futures REST client | `new WeexRestClient().FuturesApi.SharedClient` |
| Shared spot socket client | `new WeexSocketClient().SpotApi.SharedClient` |
| Shared futures socket client | `new WeexSocketClient().FuturesApi.SharedClient` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared funding REST | `IFundingRateRestClient.GetFundingRateAsync(...)` |
| Shared leverage REST | `ILeverageRestClient.SetLeverageAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared order socket | `ISpotOrderSocketClient` / `IFuturesOrderSocketClient` |
| Shared position socket | `IPositionSocketClient` |
| Discover shared capabilities | `client.SpotApi.SharedClient.Discover()` or the equivalent futures/socket SharedClient root |

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | Direct and shared REST methods return `HttpResult<T>` / `HttpResult` |
| Socket subscription success check | Direct and shared subscriptions return `WebSocketResult<UpdateSubscription>` |
| Generic success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Read result data | Read `result.Data` only after `result.Success` |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| `WeexClient` | `WeexRestClient` |
| `ApiCredentials` | `WeexCredentials("key", "secret", "passphrase")` |
| `UsdFuturesApi` / `CoinFuturesApi` | `FuturesApi` |
| `SpotApi.ExchangeData.GetTickerAsync(...)` | `SpotApi.ExchangeData.GetTickersAsync(new[] { symbol })` |
| `FuturesOrderType` for regular futures orders | `OrderType` |
| `SpotApi.Trading.CancelOrderAsync(symbol, orderId)` | `SpotApi.Trading.CancelOrderAsync(orderId: orderId)` |
| `.Data` without `.Success` check | Check `.Success` first |
| `ITickerSocketClient.UnsubscribeAsync(...)` | Keep the concrete socket client and call `socketClient.UnsubscribeAsync(subscription.Data)` |
| Testnet environment | `WeexEnvironment.Live` or `WeexEnvironment.CreateCustom(...)` |
