using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Interfaces.Clients.SpotApi;

namespace Weex.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Weex
    /// </summary>
    public interface IWeexSharedApiClient
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IWeexRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// Futures REST shared API implementations
        /// </summary>
        IWeexRestClientFuturesSharedApi FuturesRest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IWeexSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// Futures WebSocket shared API implementations
        /// </summary>
        IWeexSocketClientFuturesSharedApi FuturesSocket { get; }
    }
}
