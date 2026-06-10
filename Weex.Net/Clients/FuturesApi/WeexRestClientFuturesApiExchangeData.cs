using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Weex.Net.Enums;
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
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/time", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexServerTime>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.Timestamp);
        }

        #endregion

        #region Get Exchange Info

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesExchangeInfo>> GetExchangeInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/exchangeInfo", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexFuturesExchangeInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<WeexOrderBook>> GetOrderBookAsync(string symbol, int? depth = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", depth);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/depth", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesTicker[]>> GetTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/ticker/24hr", WeexExchange.RateLimiter.WeexRestIp, 40, false);
            var result = await _baseClient.SendAsync<WeexFuturesTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Ticker

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesBookTicker[]>> GetBookTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/ticker/bookTicker", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexFuturesBookTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trades

        /// <inheritdoc />
        public async Task<HttpResult<WeexTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/trades", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<WeexTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<WeexKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/klines", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Price Klines

        /// <inheritdoc />
        public async Task<HttpResult<WeexKline[]>> GetIndexPriceKlinesAsync(string symbol, FuturesKlineInterval interval, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/indexPriceKlines", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Mark Price Klines

        /// <inheritdoc />
        public async Task<HttpResult<WeexKline[]>> GetMarkPriceKlinesAsync(string symbol, FuturesKlineInterval interval, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/markPriceKlines", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Kline History

        /// <inheritdoc />
        public async Task<HttpResult<WeexKline[]>> GetKlineHistoryAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, PriceType? priceType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("interval", interval);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.Add("priceType", priceType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/historyKlines", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<WeexKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Price

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesPrice>> GetPriceAsync(string symbol, PriceType? priceType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("priceType", priceType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/symbolPrice", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexFuturesPrice>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<WeexOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/openInterest", WeexExchange.RateLimiter.WeexRestIp, 2, false);
            var result = await _baseClient.SendAsync<WeexOpenInterest>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<HttpResult<WeexFundingInfo[]>> GetFundingRateAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/premiumIndex", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexFundingInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<HttpResult<WeexFundingHistory[]>> GetFundingRateHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/fundingRate", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<WeexFundingHistory[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trading Symbols

        /// <inheritdoc />
        public async Task<HttpResult<string[]>> GetTradingSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/market/apiTradingSymbols", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<string[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
