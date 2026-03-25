using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Interfaces.Clients.SpotApi;

namespace Weex.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Weex websocket API
    /// </summary>
    public interface IWeexSocketClient : ISocketClient<WeexCredentials>
    {        
        /// <summary>
        /// Futures API endpoints
        /// </summary>
        /// <see cref="IWeexSocketClientFuturesApi"/>
        public IWeexSocketClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IWeexSocketClientSpotApi"/>
        public IWeexSocketClientSpotApi SpotApi { get; }
    }
}
