using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weex.Net.Enums;
using Weex.Net.Interfaces.Clients.FuturesApi;
using Weex.Net.Objects.Models;

namespace Weex.Net.Clients.FuturesApi
{
    internal partial class WeexRestClientFuturesSharedApi
    {
        #region Place Futures Trigger Order

        async Task<ICallResult<SharedId>> IPlaceFuturesTriggerOrder.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
            => await PlaceFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = [
                RequestParameterRule<PlaceFuturesTriggerOrderRequest>.Required(x => x.PositionSide,"The position side", SharedPositionSide.Long)
                ]
        };
        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var (type, side) = GetTriggerOrderParameters(request.PriceDirection, request.OrderPrice, request.OrderDirection);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceConditionalOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                side,
                positionSide: request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                type,
                request.Quantity.QuantityInBaseAsset ?? 0,
                triggerPrice: request.TriggerPrice,
                price: request.OrderPrice,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.OrderId!.ToString()!));
        }

        #endregion
        #region Get Futures Trigger Order

        async Task<ICallResult<SharedFuturesTriggerOrder>> IGetFuturesTriggerOrder.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var id))
                throw new ArgumentException($"Invalid order id");

            var orders = await _api.Trading.GetOpenConditionalOrdersAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(orders);

            var order = orders.Data.SingleOrDefault(x => x.AlgoId == id);
            if (order == null)
            {
                var closedOrders = await _api.Trading.GetOpenConditionalOrdersAsync(
                    request.Symbol!.GetSymbol(FormatSymbol),
                    ct: ct).ConfigureAwait(false);
                if (!closedOrders.Success)
                    return HttpResult.Fail<SharedFuturesTriggerOrder>(closedOrders);

                order = closedOrders.Data.SingleOrDefault(x => x.AlgoId == id);
            }

            if (order == null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Order not found"));

            WeexFuturesOrder? actualOrder = null;
            if (order.ActualOrderId > 0)
            {
                var actualOrderResult = await _api.Trading.GetOrderAsync(order.ActualOrderId.Value).ConfigureAwait(false);
                actualOrder = actualOrderResult.Data;
            }

            var (orderType, orderDirection) = ParseTriggerDirections(order.OrderType, order.Side);
            // Return
            return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Symbol),
                order.Symbol,
                order.AlgoId.ToString(),
                orderType,
                orderDirection,
                ParseTriggerStatus(order),
                order.TriggerPrice,
                order.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                order.CreateTime
                )
            {
                OrderPrice = order.Price == 0 ? null : order.Price,
                OrderQuantity = new SharedOrderQuantity(order.Quantity, contractQuantity: order.Quantity),
                QuantityFilled = new SharedOrderQuantity(actualOrder?.QuantityFilled, actualOrder?.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.TimeInForce),
                UpdateTime = order.UpdateTime,
                PositionSide = order.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                PlacedOrderId = order.ActualOrderId.ToString(),
                ClientOrderId = order.ClientAlgoId
            });
        }

        #endregion

        private SharedTriggerOrderStatus ParseTriggerStatus(WeexFuturesConditionalOrder data)
        {
            if (data.Status == OrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (data.Status == OrderStatus.Canceled
                || data.Status == OrderStatus.Expired)
            {
                return SharedTriggerOrderStatus.CanceledOrRejected;
            }

            if (data.Status == OrderStatus.Untriggered)
            {
                return SharedTriggerOrderStatus.Active;
            }

            if (data.Status == OrderStatus.New
                || data.Status == OrderStatus.Pending)
            {
                return SharedTriggerOrderStatus.Triggered;
            }

            return SharedTriggerOrderStatus.Unknown;
        }
        #region Cancel Futures Trigger Order

        async Task<ICallResult<SharedId>> ICancelFuturesTriggerOrder.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelConditionalOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.OrderId!.ToString()!));
        }

        #endregion

        private (FuturesOrderType, OrderSide) GetTriggerOrderParameters(SharedTriggerPriceDirection orderType, decimal? orderPrice, SharedTriggerOrderDirection direction)
        {
            if (orderType == SharedTriggerPriceDirection.PriceBelow)
            {
                if (direction == SharedTriggerOrderDirection.Enter)
                    // PriceBelow + Enter = TakeProfit Buy order
                    return (orderPrice == null ? FuturesOrderType.TakeProfitMarket : FuturesOrderType.TakeProfit, OrderSide.Buy);
                else
                    // PriceBelow + Exit = StopLoss Sell order
                    return (orderPrice == null ? FuturesOrderType.StopMarket : FuturesOrderType.Stop, OrderSide.Sell);
            }

            if (direction == SharedTriggerOrderDirection.Enter)
                // PriceAbove + Enter = StopLoss Buy order
                return (orderPrice == null ? FuturesOrderType.StopMarket : FuturesOrderType.Stop, OrderSide.Buy);
            else
                // PriceAbove + Exit = TakeProfit Sell order
                return (orderPrice == null ? FuturesOrderType.TakeProfitMarket : FuturesOrderType.TakeProfit, OrderSide.Sell);
        }

        private (SharedOrderType, SharedTriggerOrderDirection) ParseTriggerDirections(FuturesOrderType orderType, OrderSide side)
        {
            if (orderType == FuturesOrderType.TakeProfit || orderType == FuturesOrderType.TakeProfitMarket)
            {
                if (side == OrderSide.Buy)
                {
                    // TakeProfit + Buy = PriceBelow Enter
                    return (
                        orderType == FuturesOrderType.TakeProfitMarket ? SharedOrderType.Market : SharedOrderType.Limit,
                        SharedTriggerOrderDirection.Enter);
                }
                else
                {
                    // TakeProfit + Sell = PriceAbove Exit
                    return (
                        orderType == FuturesOrderType.TakeProfitMarket ? SharedOrderType.Market : SharedOrderType.Limit,
                        SharedTriggerOrderDirection.Exit);
                }
            }

            if (side == OrderSide.Buy)
            {
                // StopLoss + Buy = PriceAbove Enter
                return (
                    orderType == FuturesOrderType.StopMarket ? SharedOrderType.Market : SharedOrderType.Limit,
                    SharedTriggerOrderDirection.Enter);
            }
            else
            {
                // StopLoss + Sell = PriceBelow Exit
                return (
                    orderType == FuturesOrderType.StopMarket ? SharedOrderType.Market : SharedOrderType.Limit,
                    SharedTriggerOrderDirection.Exit);
            }
        }
    }
}
