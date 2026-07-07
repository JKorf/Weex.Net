using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Rate limit
    /// </summary>
    public record WeexRateLimit
    {
        /// <summary>
        /// ["<c>interval</c>"] Interval
        /// </summary>
        [JsonPropertyName("interval")]
        public string Interval { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>intervalNum</c>"] Interval number
        /// </summary>
        [JsonPropertyName("intervalNum")]
        public int IntervalNumber { get; set; }
        /// <summary>
        /// ["<c>limit</c>"] Limit
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
        /// <summary>
        /// ["<c>rateLimitType</c>"] Rate limit type
        /// </summary>
        [JsonPropertyName("rateLimitType")]
        public string RateLimitType { get; set; } = string.Empty;
    }
}
