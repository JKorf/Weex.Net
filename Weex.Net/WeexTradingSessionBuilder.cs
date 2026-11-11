using CryptoExchange.Net.Sessions;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Sessions;
using CryptoExchange.Net.Trackers.Sessions.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Weex.Net.Interfaces;
using Weex.Net.Interfaces.Clients;

namespace Weex.Net
{
    /// <inheritdoc />
    public class WeexTradingSessionBuilder<TSession>
        : TradingSessionBuilder<TSession, WeexTradingSessionBuilder<TSession>>
        where TSession : TradingSession
    {
        private readonly IWeexOrderBookFactory _orderBookFactory;
        internal IWeexTrackerFactory Trackers { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="clientProvider"></param>
        /// <param name="orderBookFactory"></param>
        /// <param name="trackerFactory"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WeexTradingSessionBuilder(
            IWeexUserClientProvider clientProvider,
            IWeexOrderBookFactory orderBookFactory,
            IWeexTrackerFactory trackerFactory,
            ILogger<WeexTradingSessionBuilder<TSession>> logger) : base(clientProvider, logger)
        {
            _orderBookFactory = orderBookFactory ?? throw new ArgumentNullException(nameof(orderBookFactory));
            Trackers = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
        }

        /// <summary>
        /// Add symbol tracking
        /// </summary>
        /// <param name="symbol">The symbol to track</param>
        /// <param name="configure">Symbol tracking configuration</param>
        /// <param name="environment">Trading environment</param>
        public WeexTradingSessionBuilder<TSession> AddSymbolTracking(
            SharedSymbol symbol,
            Action<WeexTradingSessionSymbolBuilder<TSession>> configure,
            WeexEnvironment? environment = null)
        {
            RegisterSymbol(new WeexTradingSessionSymbolBuilder<TSession>(symbol, _orderBookFactory, environment), configure);
            return this;
        }

        /// <summary>
        /// Add a session user
        /// </summary>
        /// <param name="userIdentifier">User identifier, should be unique</param>
        /// <param name="configure">User configuration</param>
        /// <param name="environment">Trading environment</param>
        public WeexTradingSessionBuilder<TSession> AddUser(
            string userIdentifier,
            Action<TradingSessionUserBuilder<TSession, WeexEnvironment>> configure,
            WeexEnvironment? environment = null)
        {
            RegisterUser(
                userIdentifier,
                configure,
                Trackers,
                environment);

            return this;
        }
    }
}
