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
    /// Conditional order
    /// </summary>
    public record WeexFuturesConditionalOrder
    {
        /// <summary>
        /// ["<c>algoId</c>"] Algo id
        /// </summary>
        [JsonPropertyName("algoId")]
        public long AlgoId { get; set; }
        /// <summary>
        /// ["<c>clientAlgoId</c>"] Client algo id
        /// </summary>
        [JsonPropertyName("clientAlgoId")]
        public string? ClientAlgoId { get; set; }
        /// <summary>
        /// ["<c>algoType</c>"] Algo type
        /// </summary>
        [JsonPropertyName("algoType")]
        public AlgoType AlgoType { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public FuturesOrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>algoStatus</c>"] Algo status
        /// </summary>
        [JsonPropertyName("algoStatus")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>actualOrderId</c>"] Id of the triggered active order
        /// </summary>
        [JsonPropertyName("actualOrderId")]
        public long? ActualOrderId { get; set; }
        /// <summary>
        /// ["<c>actualPrice</c>"] Execution price if triggered
        /// </summary>
        [JsonPropertyName("actualPrice")]
        public decimal? ActualPrice { get; set; }
        /// <summary>
        /// ["<c>triggerPrice</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("triggerPrice")]
        public decimal TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Execution price configured for the triggered order.
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>tpTriggerPrice</c>"] Linked take-profit trigger price (if configured).
        /// </summary>
        [JsonPropertyName("tpTriggerPrice")]
        public decimal? TakeProfitTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>tpPrice</c>"] Linked take-profit execution price (if configured).
        /// </summary>
        [JsonPropertyName("tpPrice")]
        public decimal? TakeProfitPrice { get; set; }
        /// <summary>
        /// ["<c>slTriggerPrice</c>"] Linked stop-loss trigger price (if configured).
        /// </summary>
        [JsonPropertyName("slTriggerPrice")]
        public decimal? StopLossTriggerPrice { get; set; }
        /// <summary>
        /// ["<c>slPrice</c>"] Linked stop-loss execution price (if configured).
        /// </summary>
        [JsonPropertyName("slPrice")]
        public decimal? StopLossPrice { get; set; }
        /// <summary>
        /// ["<c>tpOrderType</c>"] Take profit order type
        /// </summary>
        [JsonPropertyName("tpOrderType")]
        public FuturesPriceType? TakeProfitPriceType { get; set; }
        /// <summary>
        /// ["<c>workingType</c>"] Working type
        /// </summary>
        [JsonPropertyName("workingType")]
        public FuturesPriceType WorkingType { get; set; }
        /// <summary>
        /// ["<c>closePosition</c>"] Close position if triggered
        /// </summary>
        [JsonPropertyName("closePosition")]
        public bool ClosePosition { get; set; }
        /// <summary>
        /// ["<c>reduceOnly</c>"] Reduce only
        /// </summary>
        [JsonPropertyName("reduceOnly")]
        public bool ReduceOnly { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>triggerTime</c>"] Trigger time
        /// </summary>
        [JsonPropertyName("triggerTime")]
        public DateTime? TriggerTime { get; set; }
    }


}
