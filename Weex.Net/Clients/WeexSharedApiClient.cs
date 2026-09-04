using Weex.Net.Interfaces.Clients;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Interfaces.Clients.SpotApi;

namespace Weex.Net.Clients
{
    /// <inheritdoc />
    public class WeexSharedApiClient : IWeexSharedApiClient
    {
        /// <inheritdoc />
        public IWeexRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IWeexRestClientFuturesSharedApi FuturesRest { get; }
        /// <inheritdoc />
        public IWeexSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IWeexSocketClientFuturesSharedApi FuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public WeexSharedApiClient(
            IWeexRestClient restClient,
            IWeexSocketClient socketClient)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.FuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
        }
    }
}
