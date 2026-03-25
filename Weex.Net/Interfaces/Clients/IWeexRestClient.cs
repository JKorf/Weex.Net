using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Interfaces.Clients.SpotApi;

namespace Weex.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Weex Rest API. 
    /// </summary>
    public interface IWeexRestClient : IRestClient<WeexCredentials>
    {        
        /// <summary>
        /// Futures API endpoints
        /// </summary>
        /// <see cref="IWeexRestClientFuturesApi"/>
        public IWeexRestClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IWeexRestClientSpotApi"/>
        public IWeexRestClientSpotApi SpotApi { get; }
    }
}
