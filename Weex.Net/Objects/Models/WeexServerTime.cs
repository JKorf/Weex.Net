using System;
using System.Text.Json.Serialization;

namespace Weex.Net.Objects.Models
{
    internal record WeexServerTime
    {
        [JsonPropertyName("serverTime")]
        public DateTime Timestamp { get; set; }
    }
}
