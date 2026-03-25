using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Weex.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Weex Futures API endpoints
    /// </summary>
    public interface IWeexRestClientFuturesApi : IRestApiClient<WeexCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IWeexRestClientFuturesApiAccount" />
        public IWeexRestClientFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IWeexRestClientFuturesApiExchangeData" />
        public IWeexRestClientFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IWeexRestClientFuturesApiTrading" />
        public IWeexRestClientFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IWeexRestClientFuturesApiShared SharedClient { get; }
    }
}
