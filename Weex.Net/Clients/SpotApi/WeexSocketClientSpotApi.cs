using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using Weex.Net.Clients.MessageHandlers;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Objects.Models;
using Weex.Net.Objects.Options;
using Weex.Net.Objects.Sockets.Subscriptions;

namespace Weex.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the Weex Spot websocket Api
    /// </summary>
    internal partial class WeexSocketClientSpotApi : SocketApiClient<WeexEnvironment, WeexAuthenticationProvider, WeexCredentials>, IWeexSocketClientSpotApi
    {
        #region fields
        private readonly WeexSocketClient _baseClient;

        //protected override ErrorMapping ErrorMapping => WeexErrors.Errors;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal WeexSocketClientSpotApi(WeexSocketClient baseClient, ILogger logger, WeexSocketOptions options) :
            base(logger, options.Environment.SocketClientSpotAddress!, options, options.SpotOptions)
        {
            _baseClient = baseClient;

            RateLimiter = WeexExchange.RateLimiter.WeexSocket;

            AddSystemSubscription(new WeexConnectedSubscription(logger));
            AddSystemSubscription(new WeexPingSubscription(logger));
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(WeexExchange._serializerContext);
        /// <inheritdoc />
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new WeexSocketSpotMessageHandler();

        /// <inheritdoc />
        protected override WeexAuthenticationProvider CreateAuthenticationProvider(WeexCredentials credentials)
            => new WeexAuthenticationProvider(credentials);

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<WeexTickerUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToTickerUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WeexTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, WeexSocketEvent<WeexTickerUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.EventTime);

                onMessage(
                    new DataEvent<WeexTickerUpdate>(WeexExchange.Metadata.Id, data.Data.First(), receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });

            var subscription = new WeexSubscription<WeexSocketEvent<WeexTickerUpdate[]>>(_logger, symbols.Select(x => $"{x}@ticker").ToArray(), ["ticker"], symbols.ToArray(), internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("/v3/ws/public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<WeexBookTickerUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToBookTickerUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WeexBookTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, WeexBookTickerUpdate>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.EventTime);

                onMessage(
                    new DataEvent<WeexBookTickerUpdate>(WeexExchange.Metadata.Id, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });

            var subscription = new WeexSubscription<WeexBookTickerUpdate>(_logger, symbols.Select(x => $"{x}@bookTicker").ToArray(), ["bookTicker"], symbols.ToArray(), internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("/v3/ws/public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<WeexOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync([symbol], depth, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<WeexOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, WeexOrderBookUpdate>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.EventTime);

                onMessage(
                    new DataEvent<WeexOrderBookUpdate>(WeexExchange.Metadata.Id, data, receiveTime, originalData)
                        .WithUpdateType(data.UpdateType.Equals("SNAPSHOT", StringComparison.OrdinalIgnoreCase) ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });

            var subscription = new WeexSubscription<WeexOrderBookUpdate>(_logger, symbols.Select(x => $"{x}@depth" + depth).ToArray(), ["depth", "depthSnapshot"], symbols.ToArray(), internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("/v3/ws/public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<WeexKlineUpdate[]>> onMessage, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<WeexKlineUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, WeexSocketEvent<WeexKlineUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.EventTime);

                onMessage(
                    new DataEvent<WeexKlineUpdate[]>(WeexExchange.Metadata.Id, data.Data, receiveTime, originalData)
                        .WithUpdateType(data.Event.Equals("klineSnapshot", StringComparison.OrdinalIgnoreCase) ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });

            var intervalString = EnumConverter.GetString(interval);
            var subscription = new WeexSubscription<WeexSocketEvent<WeexKlineUpdate[]>>(
                _logger,
                symbols.Select(x => $"{x}@kline_{intervalString}_LAST_PRICE").ToArray(),
                ["kline", "klineSnapshot"], 
                symbols.Select(x => $"{x}{intervalString}").ToArray(), 
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("/v3/ws/public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<WeexTradeUpdate[]>> onMessage, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<WeexTradeUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, WeexSocketEvent<WeexTradeUpdate[]>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.EventTime);

                onMessage(
                    new DataEvent<WeexTradeUpdate[]>(WeexExchange.Metadata.Id, data.Data, receiveTime, originalData)
                        .WithUpdateType(data.Event is "tradeSnapshot" ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });

            var subscription = new WeexSubscription<WeexSocketEvent<WeexTradeUpdate[]>>(_logger, symbols.Select(x => $"{x}@trade").ToArray(), ["trade", "tradeSnapshot"], symbols.ToArray(), internalHandler, false);
            return await SubscribeAsync(BaseAddress.AppendPath("/v3/ws/public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<WeexAccountUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, WeexAccountUpdate>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.EventTime);

                onMessage(
                    new DataEvent<WeexAccountUpdate>(WeexExchange.Metadata.Id, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });

            var subscription = new WeexSubscription<WeexAccountUpdate>(_logger, ["account"], ["account"], null, internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("/v3/ws/private"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<WeexOrderUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, WeexOrderUpdate>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.EventTime);

                onMessage(
                    new DataEvent<WeexOrderUpdate>(WeexExchange.Metadata.Id, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });

            var subscription = new WeexSubscription<WeexOrderUpdate>(_logger, ["orders"], ["orders"], null, internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("/v3/ws/private"), subscription, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<WeexUserTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, WeexUserTradeUpdate>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.EventTime);

                onMessage(
                    new DataEvent<WeexUserTradeUpdate>(WeexExchange.Metadata.Id, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });

            var subscription = new WeexSubscription<WeexUserTradeUpdate>(_logger, ["fill"], ["fill"], null, internalHandler, true);
            return await SubscribeAsync(BaseAddress.AppendPath("/v3/ws/private"), subscription, ct).ConfigureAwait(false);
        }

        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        protected override WebSocketParameters GetWebSocketParameters(string address)
        {
            var result = base.GetWebSocketParameters(address);
            result.Headers = new Dictionary<string, string>
            {
                { "User-Agent", "CryptoExchange.Net/" + _baseClient.CryptoExchangeLibVersion }
            };

            if (address.EndsWith("/private"))
            {
                // Apply authentication
                AuthenticationProvider!.ApplyWebsocketAuthentication(this, result.Headers);
            }
             
            return result;
        }

        /// <inheritdoc />
        public IWeexSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => WeexExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
