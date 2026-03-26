using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Objects.Models;

namespace Weex.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class WeexRestClientSpotApiExchangeData : IWeexRestClientSpotApiExchangeData
    {
        private readonly WeexRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal WeexRestClientSpotApiExchangeData(ILogger logger, WeexRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/time", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexServerTime>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.Timestamp ?? default);
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<WebCallResult<WeexAsset[]>> GetAssetsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/coins", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<WeexAsset[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Exchange Info

        /// <inheritdoc />
        public async Task<WebCallResult<WeexExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string>? symbols = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalCommaSeparated("symbols", symbols);
            parameters.AddOptionalEnum("symbolStatus", symbolStatus);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/exchangeInfo", WeexExchange.RateLimiter.WeexRestIp, 20, false);
            var result = await _baseClient.SendAsync<WeexExchangeInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Prices

        /// <inheritdoc />
        public async Task<WebCallResult<WeexPrice[]>> GetPricesAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalCommaSeparated("symbols", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/market/ticker/price", WeexExchange.RateLimiter.WeexRestIp, 4, false);
            var result = await _baseClient.SendAsync<WeexPrice[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<WeexTicker[]>> GetTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalCommaSeparated("symbols", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/market/ticker/24hr", WeexExchange.RateLimiter.WeexRestIp, 2, false);
            var result = await _baseClient.SendAsync<WeexTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<WebCallResult<WeexTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/market/trades", WeexExchange.RateLimiter.WeexRestIp, 25, false);
            var result = await _baseClient.SendAsync<WeexTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<WeexKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/market/klines", WeexExchange.RateLimiter.WeexRestIp, 2, false);
            var result = await _baseClient.SendAsync<WeexKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<WeexOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/market/depth", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<WeexOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<WeexBookTicker[]>> GetBookTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalCommaSeparated("symbols", symbols);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/market/ticker/bookTicker", WeexExchange.RateLimiter.WeexRestIp, 4, false);
            var result = await _baseClient.SendAsync<WeexBookTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
