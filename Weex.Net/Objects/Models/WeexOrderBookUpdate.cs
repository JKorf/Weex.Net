using System.Text.Json.Serialization;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    public record WeexOrderBookUpdate : WeexSocketEvent
    {
        /// <summary>
        /// Last update id
        /// </summary>
        [JsonPropertyName("u")]
        public long LastUpdateId { get; set; }
        /// <summary>
        /// First update id
        /// </summary>
        [JsonPropertyName("U")]
        public long FirstUpdateId { get; set; }
        /// <summary>
        /// Depth level
        /// </summary>
        [JsonPropertyName("l")]
        public int Depth { get; set; }
        /// <summary>
        /// Update type
        /// </summary>
        [JsonPropertyName("d")]
        public string UpdateType { get; set; } = string.Empty;
        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("a")]
        public WeexOrderBookEntry[] Asks { get; set; } = [];
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("b")]
        public WeexOrderBookEntry[] Bids { get; set; } = [];
    }
}
