using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
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
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Objects.Models;
using Weex.Net.Objects.Options;
using Weex.Net.Objects.Sockets.Subscriptions;

namespace Weex.Net.Clients.FuturesApi
{
    /// <summary>
    /// Client providing access to the Weex Futures websocket Api
    /// </summary>
    internal partial class WeexSocketClientFuturesApi : SocketApiClient<WeexEnvironment, WeexAuthenticationProvider, WeexCredentials>, IWeexSocketClientFuturesApi
    {
        #region fields
        protected override ErrorMapping ErrorMapping => WeexErrors.Errors;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal WeexSocketClientFuturesApi(ILogger logger, WeexSocketOptions options) :
            base(logger, options.Environment.SocketClientAddress!, options, options.FuturesOptions)
        {
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToXXXUpdatesAsync(Action<DataEvent<WeexModel>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, WeexModel>((receiveTime, originalData, data) =>
            {
                //UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<WeexModel>(WeexExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        //.WithStreamId(data.Stream)
                        //.WithSymbol(data.Symbol)
                        //.WithDataTimestamp(data.EventTime, GetTimeOffset())
                    );
            });
            throw new Exception();
            //var subscription = new WeexSubscription<WeexModel>(_logger, new [] { "XXX" }, internalHandler, false);
            //return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public IWeexSocketClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => WeexExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
