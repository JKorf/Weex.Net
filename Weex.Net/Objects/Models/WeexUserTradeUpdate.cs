using System;
using System.Text.Json.Serialization;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    public record WeexUserTradeUpdate : WeexSocketEvent
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
        /// Trades
        /// </summary>
        [JsonPropertyName("d")]
        public WeexUserTradeItemUpdate[] Trades { get; set; } = [];
    }

    /// <summary>
    /// Trade
    /// </summary>
    public record WeexUserTradeItemUpdate
    {
        /// <summary>
        /// ["<c>id</c>"] Trade id
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
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderSide</c>"] Order side
        /// </summary>
        [JsonPropertyName("orderSide")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>fillSize</c>"] Filled quantity
        /// </summary>
        [JsonPropertyName("fillSize")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>fillValue</c>"] Filled value
        /// </summary>
        [JsonPropertyName("fillValue")]
        public decimal Value { get; set; }
        /// <summary>
        /// ["<c>fillFee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fillFee")]
        public decimal Fee { get; set; }
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
