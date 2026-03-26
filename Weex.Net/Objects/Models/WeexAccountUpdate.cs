using System.Text.Json.Serialization;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Account update
    /// </summary>
    public record WeexAccountUpdate : WeexSocketEvent
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
        /// Balances
        /// </summary>
        [JsonPropertyName("d")]
        public WeexAccountBalanceUpdate[] Balances { get; set; } = [];
    }

    /// <summary>
    /// Balance update
    /// </summary>
    public record WeexAccountBalanceUpdate
    {
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>frozen</c>"] Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>equity</c>"] Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
    }
}
