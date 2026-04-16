using CryptoExchange.Net.Objects;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Objects.Internal;
using Weex.Net.Objects.Models;

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

        #region Endpoints
        #region Get Balances

        /// <inheritdoc />
        public async Task<WebCallResult<WeexFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/account/balance", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexFuturesBalance[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trading Fees

        /// <inheritdoc />
        public async Task<WebCallResult<WeexTradingFee>> GetTradingFeesAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/account/commissionRate", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexTradingFee>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Config

        /// <inheritdoc />
        public async Task<WebCallResult<WeexAccountConfig>> GetAccountConfigAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/account/accountConfig", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexAccountConfig>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Config

        /// <inheritdoc />
        public async Task<WebCallResult<WeexSymbolConfig[]>> GetSymbolConfigAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/capi/v3/account/symbolConfig", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexSymbolConfig[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Bills

        /// <inheritdoc />
        public async Task<WebCallResult<WeexFuturesBillPage>> GetAccountBillsAsync(string? asset = null, string? symbol = null, IncomeType? incomeType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("asset", asset);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("incomeType", incomeType);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/capi/v3/account/income", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexFuturesBillPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Margin Mode

        /// <inheritdoc />
        public async Task<WebCallResult> SetMarginModeAsync(string symbol, MarginType marginType, PositionCombineType? combineType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("marginType", marginType);
            parameters.AddOptionalEnum("separatedType", combineType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/capi/v3/account/marginType", WeexExchange.RateLimiter.WeexRestUid, 1, true);
            var result = await _baseClient.SendAsync<WeexResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.AsDataless();
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<WeexLeverageResult>> SetLeverageAsync(string symbol, MarginType? marginMode = null, decimal? crossLeverage = null, decimal? isolatedLongLeverage = null, decimal? isolatedShortLeverage = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalEnum("marginType", marginMode);
            parameters.AddOptionalString("crossLeverage", crossLeverage);
            parameters.AddOptionalString("isolatedLongLeverage", isolatedLongLeverage);
            parameters.AddOptionalString("isolatedShortLeverage", isolatedShortLeverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/capi/v3/account/leverage", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexLeverageResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Adjust Isolated Margin

        /// <inheritdoc />
        public async Task<WebCallResult> AdjustIsolatedMarginAsync(long positionId, decimal quantity, MarginAdjustType adjustType, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("isolatedPositionId", positionId);
            parameters.AddString("amount", quantity);
            parameters.AddEnum("type", adjustType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/capi/v3/account/positionMargin", WeexExchange.RateLimiter.WeexRestUid, 30, true);
            var result = await _baseClient.SendAsync<WeexResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.AsDataless();
        }

        #endregion

        #region Set Auto Append Margin

        /// <inheritdoc />
        public async Task<WebCallResult> SetAutoAppendMarginAsync(long positionId, bool autoAppendMargin, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("positionId", positionId);
            parameters.Add("autoAppendMargin", autoAppendMargin);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/capi/v3/account/modifyAutoAppendMargin", WeexExchange.RateLimiter.WeexRestUid, 30, true);
            var result = await _baseClient.SendAsync<WeexResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.AsDataless();
        }

        #endregion
        #endregion
    }
}
