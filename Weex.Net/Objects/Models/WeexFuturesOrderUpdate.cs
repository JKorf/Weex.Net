using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    public record WeexFuturesOrderUpdate : WeexSocketEvent
    {
        /// <summary>
        /// Update version
        /// </summary>
        [JsonPropertyName("v")]
        public long Version { get; set; }
        /// <summary>
        /// Source event
        /// </summary>
        [JsonPropertyName("msgEvent")]
        public string EventType { get; set; } = string.Empty;
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("d")]
        public WeexFuturesOrderUpdateItem[] Orders { get; set; } = [];
    }

    /// <summary>
    /// Order
    /// </summary>
    public record WeexFuturesOrderUpdateItem
    {
        /// <summary>
        /// ["<c>id</c>"] Order id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginType MarginMode { get; set; }
        /// <summary>
        /// ["<c>separatedMode</c>"] Separated mode
        /// </summary>
        [JsonPropertyName("separatedMode")]
        public PositionCombineType SeparatedMode { get; set; }
        /// <summary>
        /// ["<c>separatedOpenOrderId</c>"] Separated open order id
        /// </summary>
        [JsonPropertyName("separatedOpenOrderId")]
        public long? SeparatedOpenOrderId { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>orderSide</c>"] Order side
        /// </summary>
        [JsonPropertyName("orderSide")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesOrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>triggerPrice</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("triggerPrice")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>triggerPriceType</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("triggerPriceType")]
        public FuturesPriceType TriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>orderSource</c>"] Order source
        /// </summary>
        [JsonPropertyName("orderSource")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>openTpslParentOrderId</c>"] Open tpsl parent order id
        /// </summary>
        [JsonPropertyName("openTpslParentOrderId")]
        public long? OpenTpslParentOrderId { get; set; }
        /// <summary>
        /// ["<c>positionTpsl</c>"] Position take profit/stop loss
        /// </summary>
        [JsonPropertyName("positionTpsl")]
        public bool PositionTpsl { get; set; }
        /// <summary>
        /// ["<c>setOpenTp</c>"] Set open take profit
        /// </summary>
        [JsonPropertyName("setOpenTp")]
        public bool SetOpenTp { get; set; }
        /// <summary>
        /// ["<c>setOpenSl</c>"] Set open stop loss
        /// </summary>
        [JsonPropertyName("setOpenSl")]
        public bool SetOpenSl { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>makerFeeRate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>feeDiscount</c>"] Fee discount
        /// </summary>
        [JsonPropertyName("feeDiscount")]
        public decimal FeeDiscount { get; set; }
        /// <summary>
        /// ["<c>liquidateFeeRate</c>"] Liquidate fee rate
        /// </summary>
        [JsonPropertyName("liquidateFeeRate")]
        public decimal LiquidateFeeRate { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>triggerTime</c>"] Trigger time
        /// </summary>
        [JsonPropertyName("triggerTime")]
        public DateTime? TriggerTime { get; set; }
        /// <summary>
        /// ["<c>triggerPriceTime</c>"] Trigger price time
        /// </summary>
        [JsonPropertyName("triggerPriceTime")]
        public DateTime? TriggerPriceTime { get; set; }
        /// <summary>
        /// ["<c>triggerPriceValue</c>"] Trigger price value
        /// </summary>
        [JsonPropertyName("triggerPriceValue")]
        public decimal TriggerPriceValue { get; set; }
        /// <summary>
        /// ["<c>cancelReason</c>"] Cancel reason
        /// </summary>
        [JsonPropertyName("cancelReason")]
        public string CancelReason { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>latestFillPrice</c>"] Latest fill price
        /// </summary>
        [JsonPropertyName("latestFillPrice")]
        public decimal LatestFillPrice { get; set; }
        /// <summary>
        /// ["<c>maxFillPrice</c>"] Max fill price
        /// </summary>
        [JsonPropertyName("maxFillPrice")]
        public decimal MaxFillPrice { get; set; }
        /// <summary>
        /// ["<c>minFillPrice</c>"] Min fill price
        /// </summary>
        [JsonPropertyName("minFillPrice")]
        public decimal MinFillPrice { get; set; }
        /// <summary>
        /// ["<c>cumFillSize</c>"] Total filled quantity
        /// </summary>
        [JsonPropertyName("cumFillSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>cumFillValue</c>"] Total filled value
        /// </summary>
        [JsonPropertyName("cumFillValue")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// ["<c>cumFillFee</c>"] Total fill fee
        /// </summary>
        [JsonPropertyName("cumFillFee")]
        public decimal TotalFillFee { get; set; }
        /// <summary>
        /// ["<c>cumLiquidateFee</c>"] Total liquidation fee
        /// </summary>
        [JsonPropertyName("cumLiquidateFee")]
        public decimal TotalLiquidationFee { get; set; }
        /// <summary>
        /// ["<c>cumRealizePnl</c>"] Realize pnl
        /// </summary>
        [JsonPropertyName("cumRealizePnl")]
        public decimal RealizePnl { get; set; }
        /// <summary>
        /// ["<c>createdTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updatedTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime? UpdateTime { get; set; }
    }


}
