using System;
using System.Text.Json.Serialization;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Trade update
    /// </summary>
    public record WeexTradeUpdate
    {
        /// <summary>
        /// ["<c>T</c>"] Trade time
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Trade id
        /// </summary>
        [JsonPropertyName("t")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>p</c>"] Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Value
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Value { get; set; }
        /// <summary>
        /// ["<c>m</c>"] BuyerIsMaker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
    }
}
