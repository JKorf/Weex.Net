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
        public async Task<HttpResult<string[]>> GetTradingSymbolsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/apiTradingSymbols", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<string[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<HttpResult<WeexAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/account/", WeexExchange.RateLimiter.WeexRestUid, 5, true, forcePathEndWithSlash: true);
            var result = await _baseClient.SendAsync<WeexAccountInfo>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Bills

        /// <inheritdoc />
        public async Task<HttpResult<WeexBill[]>> GetAccountBillsAsync(long? assetId = null, BusinessType? businessType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("coinId", assetId);
            parameters.Add("bizType", businessType);
            parameters.Add("after", startTime);
            parameters.Add("before", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/account/bills", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexBill[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Bills

        /// <inheritdoc />
        public async Task<HttpResult<WeexPage<WeexFundingBill>>> GetFundingBillsAsync(long? assetId = null, BusinessType? businessType = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("coinId", assetId);
            parameters.Add("bizType", businessType);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("pageIndex", page);
            parameters.Add("pageSize", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/account/fundingBills", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexPage<WeexFundingBill>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<HttpResult<WeexTransfer[]>> GetTransferHistoryAsync(long? assetId = null, AccountType? fromType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("coinId", assetId);
            parameters.Add("fromType", fromType);
            parameters.Add("after", startTime);
            parameters.Add("before", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/account/transferRecords", WeexExchange.RateLimiter.WeexRestUid, 3, true);
            var result = await _baseClient.SendAsync<WeexTransfer[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
