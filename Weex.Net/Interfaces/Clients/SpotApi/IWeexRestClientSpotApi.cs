using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Weex.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Weex Spot API endpoints
    /// </summary>
    public interface IWeexRestClientSpotApi : IRestApiClient<WeexCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IWeexRestClientSpotApiAccount" />
        public IWeexRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IWeexRestClientSpotApiExchangeData" />
        public IWeexRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IWeexRestClientSpotApiTrading" />
        public IWeexRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IWeexRestClientSpotApiShared SharedClient { get; }
    }
}
