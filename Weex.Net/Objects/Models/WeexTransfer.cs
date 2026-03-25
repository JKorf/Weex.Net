using System;
using System.Text.Json.Serialization;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Transfer info
    /// </summary>
    public record WeexTransfer
    {
        /// <summary>
        /// ["<c>coinName</c>"] Asset name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public TransferStatus Status { get; set; }
        /// <summary>
        /// ["<c>toType</c>"] To account type
        /// </summary>
        [JsonPropertyName("toType")]
        public AccountType? ToType { get; set; }
        /// <summary>
        /// ["<c>toSymbol</c>"] To symbol
        /// </summary>
        [JsonPropertyName("toSymbol")]
        public string? ToSymbol { get; set; }
        /// <summary>
        /// ["<c>fromType</c>"] From account type
        /// </summary>
        [JsonPropertyName("fromType")]
        public AccountType? FromType { get; set; }
        /// <summary>
        /// ["<c>fromSymbol</c>"] From symbol
        /// </summary>
        [JsonPropertyName("fromSymbol")]
        public string? FromSymbol { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>tradeTime</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("tradeTime")]
        public DateTime Timestamp { get; set; }
    }
}
