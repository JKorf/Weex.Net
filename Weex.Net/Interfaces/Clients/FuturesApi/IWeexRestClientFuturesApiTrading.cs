using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Objects.Internal;
using Weex.Net.Objects.Models;

namespace Weex.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Weex Futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IWeexRestClientFuturesApiTrading
    {
        /// <summary>
        /// Get positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/GetAllPositions" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/account/position/allPosition<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexPosition[]>> GetPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get positions for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Account_API/GetSinglePosition" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/account/position/singlePosition<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexPosition[]>> GetPositionAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/PlaceOrder" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/order<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Order quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="clientOrderId">["<c>newClientOrderId</c>"] Client order id</param>
        /// <param name="takeProfitTriggerPrice">["<c>tpTriggerPrice</c>"] Take profit trigger price</param>
        /// <param name="stopLossTriggerPrice">["<c>slTriggerPrice</c>"] Stop loss trigger price</param>
        /// <param name="takeProfitWorkingType">["<c>TpWorkingType</c>"] Take profit trigger price type</param>
        /// <param name="stopLossWorkingType">["<c>SlWorkingType</c>"] Stop loss trigger price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrderResult>> PlaceOrderAsync(string symbol, OrderSide side, PositionSide positionSide, OrderType orderType, decimal quantity, decimal? price = null, TimeInForce? timeInForce = null, string? clientOrderId = null, decimal? takeProfitTriggerPrice = null, decimal? stopLossTriggerPrice = null, FuturesPriceType? takeProfitWorkingType = null, FuturesPriceType? stopLossWorkingType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/CancelOrder" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/order<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrderResult>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all active orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/CancelAllOrders" /><br />
        /// Endpoint:<br />
        /// DELETE /capi/v3/allOpenOrders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrderResult[]>> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/CancelOrdersBatch" /><br />
        /// Endpoint:<br />
        /// DELETE /capi/v3/batchOrders<br />
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>orderIds</c>"] Order ids to cancel</param>
        /// <param name="clientOrderIds">["<c>origClientOrderIds</c>"] Client order ids to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrderResult[]>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default);

        /// <summary>
        /// Close all positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/ClosePositions" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/closePositions<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexPositionResult[]>> ClosePositionsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get order info by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/GetSingleOrderInfo" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/order<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] </param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/GetCurrentOrderStatus" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/order/openOrders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">["<c>orderId</c>"] Return results after this id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrder[]>> GetOpenOrdersAsync(string? symbol = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/GetOrderHistory" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/order/history<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrder[]>> GetOrderHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/GetTradeDetails" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/userTrades<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesUserTrade[]>> GetUserTradesAsync(string? symbol = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new conditional order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/PlacePendingOrder" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/algoOrder<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="type">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity</param>
        /// <param name="triggerPrice">["<c>triggerPrice</c>"] Trigger price</param>
        /// <param name="price">["<c>price</c>"] Execution price</param>
        /// <param name="clientOrderId">["<c>clientAlgoId</c>"] Client order id</param>
        /// <param name="takeProfitPrice">["<c>presetTakeProfitPrice</c>"] Take profit price</param>
        /// <param name="stopLossPrice">["<c>presetStopLossPrice</c>"] Stop loss price</param>
        /// <param name="takeProfitWorkingType">["<c>TpWorkingType</c>"] Take profit trigger price type</param>
        /// <param name="stopLossWorkingType">["<c>SlWorkingType</c>"] Stop loss trigger price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrderResult>> PlaceConditionalOrderAsync(string symbol, OrderSide side, PositionSide positionSide, FuturesOrderType type, decimal quantity, decimal triggerPrice, decimal? price = null, string? clientOrderId = null, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, FuturesPriceType? takeProfitWorkingType = null, FuturesPriceType? stopLossWorkingType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an open conditional order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/CancelPendingOrder" /><br />
        /// Endpoint:<br />
        /// DELETE /capi/v3/algoOrder<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrderResult>> CancelConditionalOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/CancelAllPendingOrders" /><br />
        /// Endpoint:<br />
        /// DELETE /capi/v3/algoOpenOrders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrderResult[]>> CancelAllConditionalOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get open conditional orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/GetCurrentPendingOrders" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/openAlgoOrders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesConditionalOrder[]>> GetOpenConditionalOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Get conditional order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/GetHistoricalPendingOrders" /><br />
        /// Endpoint:<br />
        /// GET /capi/v3/allAlgoOrders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexConditionalOrderPage>> GetConditionalOrderHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new take profit/stop loss order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/PlaceTpSlOrder" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/placeTpSlOrder<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="planType">["<c>planType</c>"] Plan type</param>
        /// <param name="triggerPrice">["<c>triggerPrice</c>"] Trigger price</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="executePrice">["<c>executePrice</c>"] Execute order limit price</param>
        /// <param name="clientOrderId">["<c>clientAlgoId</c>"] Client order id</param>
        /// <param name="triggerPriceType">["<c>triggerPriceType</c>"] Trigger price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexFuturesOrderResult[]>> PlaceTpSlOrderAsync(string symbol, PlanType planType, decimal triggerPrice, decimal quantity, PositionSide positionSide, decimal? executePrice = null, string? clientOrderId = null, FuturesPriceType? triggerPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Edit an existing take profit/stop loss order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/contract/Transaction_API/ModifyTpSlOrder" /><br />
        /// Endpoint:<br />
        /// POST /capi/v3/modifyTpSlOrder<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id</param>
        /// <param name="triggerPrice">["<c>triggerPrice</c>"] Trigger price</param>
        /// <param name="executePrice">["<c>executePrice</c>"] Executing order limit price</param>
        /// <param name="triggerPriceType">["<c>triggerPriceType</c>"] Trigger price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditTpSlOrderAsync(long orderId, decimal triggerPrice, decimal? executePrice = null, FuturesPriceType? triggerPriceType = null, CancellationToken ct = default);

    }
}
