# API surfaces

## Client roots

`WeexRestClient` / `IWeexRestClient` expose `SpotApi` and `FuturesApi`, each with `ExchangeData`, `Account`, `Trading`, and `SharedApi`. `WeexSocketClient` / `IWeexSocketClient` expose spot and futures subscriptions plus each surface's `SharedApi`. There are no `UsdFuturesApi` or `CoinFuturesApi` roots.

The built-in environment is `WeexEnvironment.Live`. `WeexEnvironment.CreateCustom` accepts separate spot/futures REST and socket addresses; it does not supply a built-in sandbox. Private access uses `WeexCredentials(key, secret, passphrase)`. Native symbols commonly use `ETHUSDT`; obtain supported markets and filters from exchange information.

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


## Shared API V2

`IWeexSharedApiClient` implements `ISharedApiClientBase`. Its `SpotRest`, `FuturesRest`, `SpotSocket`, and `FuturesSocket` properties expose these actual contracts:

| Surface type | Supported capabilities |
| --- | --- |
| `IWeexRestClientSpotSharedApi` | `IGetAllAssetsRest`, `IGetAssetRest`, `IGetBalancesRest`, `IGetBookTickerRest`, `IGetDepositHistoryRest`, `IGetFeesRest`, `IGetKlinesRest`, `IGetOrderBookRest`, `IGetRecentTradesRest`, `IGetWithdrawalHistoryRest`, `IGetSpotSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IPlaceSpotOrderRest`, `ICancelSpotOrderRest`, `IGetSpotOrderRest`, `IGetOpenSpotOrdersRest`, `IGetClosedSpotOrdersRest`, `IGetSpotOrderTradesRest`, `IGetSpotUserTradeHistoryRest`, `IGetSpotOrderByClientOrderIdRest`, `ICancelSpotOrderByClientOrderIdRest`, `IGetTransferHistoryRest` |
| `IWeexRestClientFuturesSharedApi` | `IGetBalancesRest`, `IGetBookTickerRest`, `IGetFeesRest`, `IGetKlinesRest`, `IGetOrderBookRest`, `IGetRecentTradesRest`, `IGetFundingRateHistoryRest`, `IGetFuturesSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetIndexPriceKlinesRest`, `IGetMarkPriceKlinesRest`, `IGetLeverageRest`, `ISetLeverageRest`, `IGetOpenInterestRest`, `IPlaceFuturesOrderRest`, `IGetFuturesOrderRest`, `IGetOpenFuturesOrdersRest`, `IGetClosedFuturesOrdersRest`, `IGetFuturesOrderTradesRest`, `IGetFuturesUserTradeHistoryRest`, `ICancelFuturesOrderRest`, `IGetPositionsRest`, `ICloseFullPositionRest`, `IPlaceFuturesTriggerOrderRest`, `IGetFuturesTriggerOrderRest`, `ICancelFuturesTriggerOrderRest` |
| `IWeexSocketClientSpotSharedApi` | `ISubscribeBalancesSocket`, `ISubscribeBookTickerSocket`, `ISubscribeKlinesSocket`, `ISubscribeTickerSocket`, `ISubscribeTradesSocket`, `ISubscribeUserTradesSocket`, `ISubscribeSpotOrdersSocket` |
| `IWeexSocketClientFuturesSharedApi` | `ISubscribeBalancesSocket`, `ISubscribeKlinesSocket`, `ISubscribeTickerSocket`, `ISubscribeTradesSocket`, `ISubscribeUserTradesSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribePositionsSocket` |

Use a typed surface or narrow interface when the operation is known. For runtime selection, use `GetCapability(SharedCapabilities.Tickers.GetTicker.Rest, TradingMode.Spot)` or `GetCapabilities(...)`. Use `TradingMode.PerpetualLinear` for the linear futures surface. A nullable `SharedCapabilityResolution<T>` contains `Capability` and `Options`; handle missing support. `Discover()` supplies summary metadata, and `GetCapabilities` returns all matching surfaces. `PreferredTransport` affects generic selection; a `.Rest` descriptor explicitly requires REST.

Inspect supported trading modes, request/exchange parameter rules, limits, and notes. Do not infer optional-parameter support from the shared request model. Use native APIs for exact Weex conditional-order, TP/SL, margin, or account semantics not expressible in a shared capability. V1 `.SharedClient` facades remain for compatibility.

## Symbols, catalogs, and quantities

Use `SharedSymbol(TradingMode.Spot, "ETH", "USDT")` for portable spot symbols, or `TradingMode.PerpetualLinear` for futures. Successful `GetSpotSymbolsAsync`/`GetFuturesSymbolsAsync` calls apply `GetSymbolsRequest` filters and refresh `SpotSymbolCatalog`/`FuturesSymbolCatalog` for that API/environment. Symbol metadata includes display names, asset classifications, fees, price limits, and futures maximum leverage where available. Do not turn missing metadata into assumed support.

Shared order requests use `SharedQuantity.Base`, `.Quote`, or `.Contracts` only where the selected capability supports those units. Response `SharedOrderQuantity` values expose nullable `QuantityInBaseAsset`, `QuantityInQuoteAsset`, and `QuantityInContracts`; absent units are not zero. Ticker volume uses `Volumes` and book ticker quantities use `BestBidQuantities`/`BestAskQuantities`. Inspect `SharedOrderBook.QuantityType` before interpreting depth quantities.

Spot deposit/withdrawal history comes from account bills. The current native APIs expose history rather than transfer/withdrawal initiation, and shared deposit-address retrieval is unavailable. Do not infer wallet operations from asset/network metadata.

## Services and factories

`services.AddWeex(...)` registers native clients, supported Shared API capabilities, the exchange aggregate, `IWeexOrderBookFactory`, `IWeexTrackerFactory`, and `IWeexUserClientProvider`. Use the exchange aggregate for explicit selection when multiple exchanges are registered. Factories supply maintained local books and trackers; check startup results and stop/dispose owned instances on shutdown. Multi-user clients require deliberate account/credential selection.

Checkout-only: [public contracts](../../../../Weex.Net/Interfaces/Clients), [Shared API adapters](../../../../Weex.Net/Clients), and [DI registration](../../../../Weex.Net/ExtensionMethods/ServiceCollectionExtensions.cs).
