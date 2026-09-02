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
        /// [V1] Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        public IWeexRestClientSpotApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IWeexRestClientSpotSharedApi SharedApi { get; }
    }
}
