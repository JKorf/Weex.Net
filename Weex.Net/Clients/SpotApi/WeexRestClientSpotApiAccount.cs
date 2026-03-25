using CryptoExchange.Net.Objects;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Objects;
using Weex.Net.Objects.Models;

namespace Weex.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class WeexRestClientSpotApiAccount : IWeexRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly WeexRestClientSpotApi _baseClient;

        internal WeexRestClientSpotApiAccount(WeexRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Trading Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<string[]>> GetTradingSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/apiTradingSymbols", WeexExchange.RateLimiter.Weex, 5, true);
            var result = await _baseClient.SendAsync<string[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<WeexAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/account/", WeexExchange.RateLimiter.Weex, 5, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<WeexAccountInfo>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Bills

        /// <inheritdoc />
        public async Task<WebCallResult<WeexBill[]>> GetAccountBillsAsync(long? assetId = null, BusinessType? businessType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coinId", assetId);
            parameters.AddOptionalEnum("bizType", businessType);
            parameters.AddOptionalMillisecondsString("after", startTime);
            parameters.AddOptionalMillisecondsString("before", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/account/bills", WeexExchange.RateLimiter.Weex, 5, true);
            var result = await _baseClient.SendAsync<WeexBill[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Bills

        /// <inheritdoc />
        public async Task<WebCallResult<WeexPage<WeexFundingBill>>> GetFundingBillsAsync(long? assetId = null, BusinessType? businessType = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coinId", assetId);
            parameters.AddOptionalEnum("bizType", businessType);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("pageIndex", page);
            parameters.AddOptional("pageSize", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/account/fundingBills", WeexExchange.RateLimiter.Weex, 5, true);
            var result = await _baseClient.SendAsync<WeexPage<WeexFundingBill>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<WeexTransfer[]>> GetTransferHistoryAsync(long? assetId = null, AccountType? fromType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("coinId", assetId);
            parameters.AddOptionalEnum("fromType", fromType);
            parameters.AddOptionalMillisecondsString("after", startTime);
            parameters.AddOptionalMillisecondsString("before", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/account/transferRecords", WeexExchange.RateLimiter.Weex, 3, true);
            var result = await _baseClient.SendAsync<WeexTransfer[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
