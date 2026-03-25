using CryptoExchange.Net.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Objects.Models;

namespace Weex.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Weex Spot trading endpoints, placing and managing orders.
    /// </summary>
    public interface IWeexRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/orderApi/PlaceOrder" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/order<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force, required for limit orders</param>
        /// <param name="clientOrderId">["<c>newClientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexOrderResult>> PlaceOrderAsync(string symbol, OrderSide side, OrderType orderType, decimal quantity, decimal? price = null, TimeInForce? timeInForce = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an active order
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/orderApi/CancelOrder" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/order<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Id of the order to cancel, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Client order id of the order to cancel. Either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexCancelResult>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all active orders for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/orderApi/Cancel-Symbol-Orders" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/openOrders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexCancelResult[]>> CancelAllSymbolOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/orderApi/BulkCancel" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/order/batch<br />
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>orderIds</c>"] Order ids to cancel</param>
        /// <param name="clientOrderIds">["<c>origClientOrderIds</c>"] Client order ids to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexCancelResult[]>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default);

        /// <summary>
        /// Get an order by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/orderApi/OrderDetails" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/order<br />
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexOrder>> GetOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/orderApi/UnfinishedOrders" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/openOrders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/orderApi/HistoryOrders" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/allOrders<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexOrder[]>> GetOrderHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://www.weex.com/api-doc/spot/orderApi/TransactionDetails" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/myTrades<br />
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<WeexUserTrade[]>> GetUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    }
}
