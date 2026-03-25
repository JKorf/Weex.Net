using System.Text.Json.Serialization;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Book ticker
    /// </summary>
    public record WeexBookTicker
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bidPrice</c>"] Bid price
        /// </summary>
        [JsonPropertyName("bidPrice")]
        public decimal BidPrice { get; set; }
        /// <summary>
        /// ["<c>bidQty</c>"] Bid quantity
        /// </summary>
        [JsonPropertyName("bidQty")]
        public decimal BidQuantity { get; set; }
        /// <summary>
        /// ["<c>askPrice</c>"] Ask price
        /// </summary>
        [JsonPropertyName("askPrice")]
        public decimal AskPrice { get; set; }
        /// <summary>
        /// ["<c>askQty</c>"] Ask quantity
        /// </summary>
        [JsonPropertyName("askQty")]
        public decimal AskQuantity { get; set; }
    }


}
