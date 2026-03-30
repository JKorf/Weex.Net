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
    /// User trade update
    /// </summary>
    public record WeexFuturesUserTradeUpdate : WeexSocketEvent
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
        /// Trades
        /// </summary>
        [JsonPropertyName("d")]
        public WeexFuturesUserTradeUpdateItem[] Trades { get; set; } = [];
    }

    /// <summary>
    /// Trade
    /// </summary>
    public record WeexFuturesUserTradeUpdateItem
    {
        /// <summary>
        /// ["<c>id</c>"] Trade id
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
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
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
        /// ["<c>fillSize</c>"] Fill quantity
        /// </summary>
        [JsonPropertyName("fillSize")]
        public decimal FillQuantity { get; set; }
        /// <summary>
        /// ["<c>fillValue</c>"] Fill value
        /// </summary>
        [JsonPropertyName("fillValue")]
        public decimal FillValue { get; set; }
        /// <summary>
        /// ["<c>fillFee</c>"] Fill fee
        /// </summary>
        [JsonPropertyName("fillFee")]
        public decimal FillFee { get; set; }
        /// <summary>
        /// ["<c>liquidateFee</c>"] Liquidate fee
        /// </summary>
        [JsonPropertyName("liquidateFee")]
        public decimal LiquidateFee { get; set; }
        /// <summary>
        /// ["<c>realizePnl</c>"] Realize profit and loss
        /// </summary>
        [JsonPropertyName("realizePnl")]
        public decimal RealizePnl { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public TradeRole Direction { get; set; }
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
