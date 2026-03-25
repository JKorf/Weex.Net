using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Objects.Models;

namespace Weex.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class WeexRestClientFuturesApiExchangeData : IWeexRestClientFuturesApiExchangeData
    {
        private readonly WeexRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal WeexRestClientFuturesApiExchangeData(ILogger logger, WeexRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "XXX", WeexExchange.RateLimiter.Weex, 1, false);
            var result = await _baseClient.SendAsync<WeexModel>(request, null, ct).ConfigureAwait(false);
            throw new NotImplementedException();
        }

        #endregion
    }
}
