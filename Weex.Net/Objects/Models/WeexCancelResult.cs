using System.Text.Json.Serialization;
using Weex.Net.Converters;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    internal record WeexCancelResultWrapper
    {
        [JsonPropertyName("orderList")]
        public WeexCancelResult[] Orders { get; set; } = [];
    }

    /// <summary>
    /// Cancel result
    /// </summary>
    public record WeexCancelResult
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long? OrderId { get; set; }
        /// <summary>
        /// ["<c>origClientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("origClientOrderId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("errorMsg")]
        public string? ErrorMessage { get; set; }
    }
}
