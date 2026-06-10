using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.SpotApi;
using Weex.Net.Objects.Models;

namespace Weex.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class WeexRestClientSpotApiTrading : IWeexRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly WeexRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal WeexRestClientSpotApiTrading(ILogger logger, WeexRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<WeexOrderResult>> PlaceOrderAsync(string symbol, OrderSide side, OrderType orderType, decimal quantity, decimal? price = null, TimeInForce? timeInForce = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var clientOrderIdUpdated = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                _baseClient.ClientOptions.BrokerId ?? WeexExchange._clientReference,
                36,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            parameters.Add("type", orderType);
            parameters.Add("quantity", quantity);
            parameters.Add("price", price);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("newClientOrderId", clientOrderIdUpdated);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/order", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<WeexCancelResult>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            if (clientOrderId != null)
            {
                clientOrderId = LibraryHelpers.ApplyBrokerId(
                    clientOrderId,
                    _baseClient.ClientOptions.BrokerId ?? WeexExchange._clientReference,
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v3/order", WeexExchange.RateLimiter.WeexRestUid, 1, true);
            var result = await _baseClient.SendAsync<WeexCancelResult>(request, parameters, new Parameters(WeexExchange._parameterSerializationSettings), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Symbol Orders

        /// <inheritdoc />
        public async Task<HttpResult<WeexCancelResult[]>> CancelAllSymbolOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v3/openOrders", WeexExchange.RateLimiter.WeexRestUid, 1, true);
            var result = await _baseClient.SendAsync<WeexCancelResult[]>(request, parameters, new Parameters(WeexExchange._parameterSerializationSettings), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult<WeexCancelResult[]>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default)
        {
            if (clientOrderIds?.Count() > 0)
            {
                clientOrderIds = clientOrderIds.Select(clientOrderId => 
                     LibraryHelpers.ApplyBrokerId(
                        clientOrderId,
                        _baseClient.ClientOptions.BrokerId ?? WeexExchange._clientReference,
                        36,
                        _baseClient.ClientOptions.AllowAppendingClientOrderId)).ToArray();
            }

            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.AddRaw("orderIds", orderIds?.ToArray());
            parameters.AddRaw("origClientOrderIds", clientOrderIds?.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/api/v3/order/batch", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexCancelResultWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<WeexCancelResult[]>(result);

            return HttpResult.Ok(result, result.Data.Orders);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<WeexOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            if (clientOrderId != null)
            {
                clientOrderId = LibraryHelpers.ApplyBrokerId(
                    clientOrderId,
                    _baseClient.ClientOptions.BrokerId ?? WeexExchange._clientReference,
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/order", WeexExchange.RateLimiter.WeexRestUid, 2, true);
            var result = await _baseClient.SendAsync<WeexOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<WeexOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/openOrders", WeexExchange.RateLimiter.WeexRestUid, 3, true);
            var result = await _baseClient.SendAsync<WeexOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<HttpResult<WeexOrder[]>> GetOrderHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/allOrders", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<WeexUserTrade[]>> GetUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/myTrades", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
