using System;
using System.Text.Json.Serialization;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    public record WeexOrderUpdate : WeexSocketEvent
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
        public AccountUpdateType EventType { get; set; }
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("d")]
        public WeexOrderItemUpdate[] Orders { get; set; } = [];
    }

    /// <summary>
    /// Order update
    /// </summary>
    public record WeexOrderItemUpdate
    {
        /// <summary>
        /// ["<c>id</c>"] Order id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseCoin</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseCoin")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteCoin</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteCoin")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderSide</c>"] Order side
        /// </summary>
        [JsonPropertyName("orderSide")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>value</c>"] Order value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal? Value { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
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
        /// ["<c>orderSource</c>"] Order source
        /// </summary>
        [JsonPropertyName("orderSource")]
        public string OrderSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>openTpslParentOrderId</c>"] Open tpsl parent order id
        /// </summary>
        [JsonPropertyName("openTpslParentOrderId")]
        public string OpenTpslParentOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>setOpenTp</c>"] Set open tp
        /// </summary>
        [JsonPropertyName("setOpenTp")]
        public bool SetOpenTp { get; set; }
        /// <summary>
        /// ["<c>setOpenSl</c>"] Set open sl
        /// </summary>
        [JsonPropertyName("setOpenSl")]
        public bool SetOpenSl { get; set; }
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
        /// ["<c>feeDiscount</c>"] Fee discount ratio
        /// </summary>
        [JsonPropertyName("feeDiscount")]
        public decimal FeeDiscount { get; set; }
        /// <summary>
        /// ["<c>takerFeeDiscount</c>"] Taker fee discount ratio
        /// </summary>
        [JsonPropertyName("takerFeeDiscount")]
        public decimal TakerFeeDiscount { get; set; }
        /// <summary>
        /// ["<c>makerFeeDiscount</c>"] Maker fee discount ratio
        /// </summary>
        [JsonPropertyName("makerFeeDiscount")]
        public decimal MakerFeeDiscount { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
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
        public decimal? TriggerPriceValue { get; set; }
        /// <summary>
        /// ["<c>cancelReason</c>"] Cancel reason
        /// </summary>
        [JsonPropertyName("cancelReason")]
        public string CancelReason { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>latestFillPrice</c>"] Fill price of latest trade
        /// </summary>
        [JsonPropertyName("latestFillPrice")]
        public decimal LastTradeFillPrice { get; set; }
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
        /// ["<c>cumFillSize</c>"] Total quantity filled
        /// </summary>
        [JsonPropertyName("cumFillSize")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>cumFillValue</c>"] Total value filled
        /// </summary>
        [JsonPropertyName("cumFillValue")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// ["<c>cumFillFee</c>"] Total fee
        /// </summary>
        [JsonPropertyName("cumFillFee")]
        public decimal Fee { get; set; }
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
