using System;
using System.Text.Json.Serialization;
using Weex.Net.Converters;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Order result
    /// </summary>
    public record WeexOrderResult
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>transactTime</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("transactTime")]
        public DateTime Timestamp { get; set; }
    }


}
