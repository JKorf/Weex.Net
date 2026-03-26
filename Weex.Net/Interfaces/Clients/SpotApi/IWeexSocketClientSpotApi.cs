using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Objects.Models;

namespace Weex.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Weex Spot streams
    /// </summary>
    public interface IWeexSocketClientSpotApi : ISocketApiClient<WeexCredentials>, IDisposable
    {
        /// <summary>
        /// Subscribe to price ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/Tickers-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: ticker)
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<WeexTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to price ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/Tickers-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: ticker)
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WeexTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to best book ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/BookTicker-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: bookTicker)
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<WeexBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to best book ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/BookTicker-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: bookTicker)
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WeexBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/Depth-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: depth{depth})
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="depth">Depth of the book, 15 or 200</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<WeexOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/Depth-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: depth{depth})
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="depth">Depth of the book, 15 or 200</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<WeexOrderBookUpdate>> onMessage, CancellationToken ct = default);


        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/Candlesticks-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: kline_{interval}_LAST_PRICE)
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<WeexKlineUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/Candlesticks-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: kline_{interval}_LAST_PRICE)
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<WeexKlineUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/Trades-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: trade)
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<WeexTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/public/Trades-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: trade)
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WeexTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user account updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/private/Account-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/private (topic: account)
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<WeexAccountUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/private/Order-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/private (topic: orders)
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<WeexOrderUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/Websocket/private/Fill-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/private (topic: fill)
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<WeexUserTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IWeexSocketClientSpotApiShared SharedClient { get; }
    }
}
