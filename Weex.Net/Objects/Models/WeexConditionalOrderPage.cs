using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Conditional order page
    /// </summary>
    public record WeexConditionalOrderPage
    {
        /// <summary>
        /// ["<c>orders</c>"] Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public WeexFuturesConditionalOrder[] Orders { get; set; } = [];
        /// <summary>
        /// ["<c>hasMore</c>"] Has more
        /// </summary>
        [JsonPropertyName("hasMore")]
        public bool HasMore { get; set; }
    }
}
