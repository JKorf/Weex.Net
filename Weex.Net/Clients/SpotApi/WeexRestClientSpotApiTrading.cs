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
        public async Task<WebCallResult<WeexOrderResult>> PlaceOrderAsync(string symbol, OrderSide side, OrderType orderType, decimal quantity, decimal? price = null, TimeInForce? timeInForce = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", orderType);
            parameters.AddString("quantity", quantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptional("newClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/order", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<WeexCancelResult>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("origClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v3/order", WeexExchange.RateLimiter.WeexRestUid, 1, true);
            var result = await _baseClient.SendAsync<WeexCancelResult>(request, parameters, new ParameterCollection(), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Symbol Orders

        /// <inheritdoc />
        public async Task<WebCallResult<WeexCancelResult[]>> CancelAllSymbolOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v3/openOrders", WeexExchange.RateLimiter.WeexRestUid, 1, true);
            var result = await _baseClient.SendAsync<WeexCancelResult[]>(request, parameters, new ParameterCollection(), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult<WeexCancelResult[]>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderIds", orderIds?.ToArray());
            parameters.AddOptional("origClientOrderIds", clientOrderIds?.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/api/v3/order/batch", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexCancelResultWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<WeexCancelResult[]>(result.Data?.Orders);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<WeexOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("origClientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/order", WeexExchange.RateLimiter.WeexRestUid, 2, true);
            var result = await _baseClient.SendAsync<WeexOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<WeexOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/openOrders", WeexExchange.RateLimiter.WeexRestUid, 3, true);
            var result = await _baseClient.SendAsync<WeexOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<WebCallResult<WeexOrder[]>> GetOrderHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/allOrders", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<WeexUserTrade[]>> GetUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/myTrades", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
