using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Websocket event
    /// </summary>
    public record WeexSocketEvent
    {
        /// <summary>
        /// Event name
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// Event time
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
    }

    /// <summary>
    /// Socket event
    /// </summary>
    public record WeexSocketEvent<T> : WeexSocketEvent
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("d")]
        public T Data { get; set; } = default!;
    }
}
