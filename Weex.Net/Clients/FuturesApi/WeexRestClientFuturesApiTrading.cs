using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Weex.Net.Interfaces.Clients.FuturesApi;

namespace Weex.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class WeexRestClientFuturesApiTrading : IWeexRestClientFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly WeexRestClientFuturesApi _baseClient;
        private readonly ILogger _logger;

        internal WeexRestClientFuturesApiTrading(ILogger logger, WeexRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }
    }
}
