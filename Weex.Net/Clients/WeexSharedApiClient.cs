using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;
using Weex.Net.Interfaces.Clients;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Objects.Options;

namespace Weex.Net.Clients
{
    /// <inheritdoc />
    public class WeexSharedApiClient : SharedApiClientBase, IWeexSharedApiClient
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
            IWeexSocketClient socketClient,
            IOptions<WeexOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                  restClient.SpotApi.SharedApi,
                  restClient.FuturesApi.SharedApi,
                  socketClient.SpotApi.SharedApi,
                  socketClient.FuturesApi.SharedApi
                  )
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.FuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
        }
    }
}
