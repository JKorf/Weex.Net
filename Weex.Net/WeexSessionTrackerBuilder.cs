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
    public class WeexSessionTrackerBuilder<TSession>
        : SessionBuilder<TSession, WeexSessionTrackerBuilder<TSession>>
        where TSession : Session
    {
        private readonly IWeexOrderBookFactory _orderBookFactory;
        internal IWeexTrackerFactory Trackers { get; }

        public WeexSessionTrackerBuilder(
            IWeexUserClientProvider clientProvider,
            IWeexOrderBookFactory orderBookFactory,
            IWeexTrackerFactory trackerFactory,
            ILogger<WeexSessionTrackerBuilder<TSession>> logger) : base(clientProvider, logger)
        {
            _orderBookFactory = orderBookFactory ?? throw new ArgumentNullException(nameof(orderBookFactory));
            Trackers = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
        }

        public WeexSessionTrackerBuilder<TSession> AddSymbolTracking(
            SharedSymbol symbol,
            Action<WeexSymbolTrackingBuilder<TSession>> configure,
            WeexEnvironment? environment = null)
        {
            RegisterSymbol(new WeexSymbolTrackingBuilder<TSession>(symbol, _orderBookFactory, environment), configure);
            return this;
        }

        public WeexSessionTrackerBuilder<TSession> AddUser(
            string userIdentifier,
            Action<UserTrackingBuilder<TSession, WeexEnvironment>> configure,
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
