using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Objects.Models;

namespace Weex.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Weex Futures streams
    /// </summary>
    public interface IWeexSocketClientFuturesApi : ISocketApiClient<WeexCredentials>, IDisposable
    {
        /// <summary>
        /// Subscribe to price ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/public/Tickers-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: ticker)
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<WeexFuturesTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to price ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/public/Tickers-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/public (topic: ticker)
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WeexFuturesTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/public/Candlesticks-Channel" /><br />
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
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/public/Candlesticks-Channel" /><br />
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
        /// Subscribe to order book updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/public/Depth-Channel" /><br />
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
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/public/Depth-Channel" /><br />
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
        /// Subscribe to trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/public/Trades-Channel" /><br />
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
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/public/Trades-Channel" /><br />
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
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/private/Account-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/private (topic: account)
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<WeexFuturesAccountUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/private/Positions-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/private (topic: positions)
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<WeexPositionUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/private/Order-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/private (topic: orders)
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<WeexFuturesOrderUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Websocket/private/Fill-Channel" /><br />
        /// Endpoint:<br />
        /// /v3/ws/private (topic: fill)
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<WeexFuturesUserTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IWeexSocketClientFuturesApiShared SharedClient { get; }
    }
}
