using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #region Get Positions

        /// <inheritdoc />
        public async Task<HttpResult<WeexPosition[]>> GetPositionsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/account/position/allPosition", WeexExchange.RateLimiter.WeexRestUid, 15, true);
            var result = await _baseClient.SendAsync<WeexPosition[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position

        /// <inheritdoc />
        public async Task<HttpResult<WeexPosition[]>> GetPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/account/position/singlePosition", WeexExchange.RateLimiter.WeexRestUid, 3, true);
            var result = await _baseClient.SendAsync<WeexPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrderResult>> PlaceOrderAsync(string symbol, OrderSide side, PositionSide positionSide, OrderType orderType, decimal quantity, decimal? price = null, TimeInForce? timeInForce = null, string? clientOrderId = null, decimal? takeProfitTriggerPrice = null, decimal? stopLossTriggerPrice = null, FuturesPriceType? takeProfitWorkingType = null, FuturesPriceType? stopLossWorkingType = null, CancellationToken ct = default)
        {
            var clientOrderIdUpdated = LibraryHelpers.ApplyBrokerId(
                clientOrderId,
                _baseClient.ClientOptions.BrokerId ?? WeexExchange._clientReference,
                36,
                _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            parameters.Add("positionSide", positionSide);
            parameters.Add("type", orderType);
            parameters.Add("quantity", quantity);
            parameters.Add("price", price);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("newClientOrderId", clientOrderIdUpdated ?? ExchangeHelpers.RandomString(20));
            parameters.Add("tpTriggerPrice", takeProfitTriggerPrice);
            parameters.Add("slTriggerPrice", stopLossTriggerPrice);
            parameters.Add("TpWorkingType", takeProfitWorkingType);
            parameters.Add("SlWorkingType", stopLossWorkingType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/capi/v3/order", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrderResult>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
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
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/capi/v3/order", WeexExchange.RateLimiter.WeexRestUid, 3, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrderResult>(request, parameters, new Parameters(WeexExchange._parameterSerializationSettings), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrderResult[]>> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/capi/v3/allOpenOrders", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrderResult[]>(request, parameters, new Parameters(WeexExchange._parameterSerializationSettings), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrderResult[]>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default)
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
            parameters.AddRaw("orderIdList", orderIds?.ToArray());
            parameters.AddRaw("origClientOrderIdList", clientOrderIds?.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/capi/v3/batchOrders", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            return await _baseClient.SendAsync<WeexFuturesOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Close Positions

        /// <inheritdoc />
        public async Task<HttpResult<WeexPositionResult[]>> ClosePositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/capi/v3/closePositions", WeexExchange.RateLimiter.WeexRestUid, 50, true);
            var result = await _baseClient.SendAsync<WeexPositionResult[]>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (result.Data.All(x => !x.Success))
                return HttpResult.Fail<WeexPositionResult[]>(result, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, false, "All orders failed")));

            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/order", WeexExchange.RateLimiter.WeexRestUid, 3, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrder[]>> GetOpenOrdersAsync(string? symbol = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", fromId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/openOrders", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrder[]>> GetOrderHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/order/history", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesUserTrade[]>> GetUserTradesAsync(string? symbol = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/userTrades", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexFuturesUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Conditional Order

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrderResult>> PlaceConditionalOrderAsync(string symbol, OrderSide side, PositionSide positionSide, FuturesOrderType type, decimal quantity, decimal triggerPrice, decimal? price = null, string? clientOrderId = null, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, FuturesPriceType? takeProfitWorkingType = null, FuturesPriceType? stopLossWorkingType = null, CancellationToken ct = default)
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
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            parameters.Add("positionSide", positionSide);
            parameters.Add("type", type);
            parameters.Add("quantity", quantity);
            parameters.Add("triggerPrice", triggerPrice);
            parameters.Add("price", price);
            parameters.Add("clientAlgoId", clientOrderId);
            parameters.Add("presetTakeProfitPrice", takeProfitPrice);
            parameters.Add("presetStopLossPrice", stopLossPrice);
            parameters.Add("TpWorkingType", takeProfitWorkingType);
            parameters.Add("SlWorkingType", stopLossWorkingType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/capi/v3/algoOrder", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Conditional Order

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrderResult>> CancelConditionalOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/capi/v3/algoOrder", WeexExchange.RateLimiter.WeexRestUid, 3, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrderResult>(request, parameters, new Parameters(WeexExchange._parameterSerializationSettings), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Conditional Orders

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrderResult[]>> CancelAllConditionalOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/capi/v3/algoOpenOrders", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrderResult[]>(request, parameters, new Parameters(WeexExchange._parameterSerializationSettings), ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesConditionalOrder[]>> GetOpenConditionalOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/openAlgoOrders", WeexExchange.RateLimiter.WeexRestUid, 3, true);
            var result = await _baseClient.SendAsync<WeexFuturesConditionalOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Conditional Order History

        /// <inheritdoc />
        public async Task<HttpResult<WeexConditionalOrderPage>> GetConditionalOrderHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/capi/v3/allAlgoOrders", WeexExchange.RateLimiter.WeexRestUid, 10, true);
            var result = await _baseClient.SendAsync<WeexConditionalOrderPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Tp Sl Order

        /// <inheritdoc />
        public async Task<HttpResult<WeexFuturesOrderResult[]>> PlaceTpSlOrderAsync(string symbol, PlanType planType, decimal triggerPrice, decimal quantity, PositionSide positionSide, decimal? executePrice = null, string? clientOrderId = null, FuturesPriceType? triggerPriceType = null, CancellationToken ct = default)
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
            parameters.Add("symbol", symbol);
            parameters.Add("planType", planType);
            parameters.Add("triggerPrice", triggerPrice);
            parameters.Add("quantity", quantity);
            parameters.Add("positionSide", positionSide);
            parameters.Add("executePrice", executePrice);
            parameters.Add("clientAlgoId", clientOrderId);
            parameters.Add("triggerPriceType", triggerPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/capi/v3/placeTpSlOrder", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexFuturesOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Tp Sl Order

        /// <inheritdoc />
        public async Task<HttpResult> EditTpSlOrderAsync(long orderId, decimal triggerPrice, decimal? executePrice = null, FuturesPriceType? triggerPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(WeexExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("triggerPrice", triggerPrice);
            parameters.Add("executePrice", executePrice);
            parameters.Add("triggerPriceType", triggerPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/capi/v3/modifyTpSlOrder", WeexExchange.RateLimiter.WeexRestUid, 5, true);
            var result = await _baseClient.SendAsync<WeexResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            return HttpResult.Ok(result);
        }

        #endregion

    }
}
