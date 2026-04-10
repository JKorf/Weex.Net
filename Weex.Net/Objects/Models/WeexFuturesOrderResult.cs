using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Weex.Net.Converters;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Order result
    /// </summary>
    public record WeexFuturesOrderResult
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long? OrderId { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string? ClientOrderId { get; set; }
        [JsonInclude, JsonPropertyName("origClientOrderId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        internal string? ClientOrderIdInt { set => ClientOrderId = value; }
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>errorCode</c>"] Error code
        /// </summary>
        [JsonPropertyName("errorCode")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// ["<c>errorMessage</c>"] Error message
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }
    }


}
