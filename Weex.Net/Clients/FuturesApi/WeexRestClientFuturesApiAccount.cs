using CryptoExchange.Net.Objects;
using Weex.Net.Interfaces.Clients.FuturesApi;

namespace Weex.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class WeexRestClientFuturesApiAccount : IWeexRestClientFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly WeexRestClientFuturesApi _baseClient;

        internal WeexRestClientFuturesApiAccount(WeexRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }
    }
}
