using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Position result
    /// </summary>
    public record WeexPositionResult
    {
        /// <summary>
        /// ["<c>positionId</c>"] Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public long PositionId { get; set; }
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>successOrderId</c>"] Success order id
        /// </summary>
        [JsonPropertyName("successOrderId")]
        public long? SuccessOrderId { get; set; }
        /// <summary>
        /// ["<c>errorMessage</c>"] Error message
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }
    }


}
