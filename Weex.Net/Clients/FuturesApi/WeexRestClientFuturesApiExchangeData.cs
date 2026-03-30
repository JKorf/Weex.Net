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
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/time", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexServerTime>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.Timestamp ?? default);
        }

        #endregion

        #region Get Exchange Info

        /// <inheritdoc />
        public async Task<WebCallResult<WeexFuturesExchangeInfo>> GetExchangeInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/exchangeInfo", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexFuturesExchangeInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<WeexOrderBook>> GetOrderBookAsync(string symbol, int? depth = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("limit", depth);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/depth", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<WeexFuturesTicker[]>> GetTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/ticker/24hr", WeexExchange.RateLimiter.WeexRestIp, 40, false);
            var result = await _baseClient.SendAsync<WeexFuturesTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<WeexFuturesBookTicker[]>> GetBookTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/ticker/bookTicker", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexFuturesBookTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trades

        /// <inheritdoc />
        public async Task<WebCallResult<WeexTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/trades", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<WeexTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<WeexKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/klines", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Price Klines

        /// <inheritdoc />
        public async Task<WebCallResult<WeexKline[]>> GetIndexPriceKlinesAsync(string symbol, FuturesKlineInterval interval, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/indexPriceKlines", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Mark Price Klines

        /// <inheritdoc />
        public async Task<WebCallResult<WeexKline[]>> GetMarkPriceKlinesAsync(string symbol, FuturesKlineInterval interval, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/markPriceKlines", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Kline History

        /// <inheritdoc />
        public async Task<WebCallResult<WeexKline[]>> GetKlineHistoryAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, PriceType? priceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalEnum("priceType", priceType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/historyKlines", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<WeexKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Price

        /// <inheritdoc />
        public async Task<WebCallResult<WeexFuturesPrice>> GetPriceAsync(string symbol, PriceType? priceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalEnum("priceType", priceType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/symbolPrice", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexFuturesPrice>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Interest

        /// <inheritdoc />
        public async Task<WebCallResult<WeexOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/openInterest", WeexExchange.RateLimiter.WeexRestIp, 2, false);
            var result = await _baseClient.SendAsync<WeexOpenInterest>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<WebCallResult<WeexFundingInfo[]>> GetFundingRateAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/premiumIndex", WeexExchange.RateLimiter.WeexRestIp, 1, false);
            var result = await _baseClient.SendAsync<WeexFundingInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<WeexFundingHistory[]>> GetFundingRateHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/fundingRate", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<WeexFundingHistory[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trading Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<string[]>> GetTradingSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/market/apiTradingSymbols", WeexExchange.RateLimiter.WeexRestIp, 5, false);
            var result = await _baseClient.SendAsync<string[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
