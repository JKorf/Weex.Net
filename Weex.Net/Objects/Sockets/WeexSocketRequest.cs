using System.Text.Json.Serialization;

namespace Weex.Net.Objects.Sockets
{
    internal record WeexSocketRequest
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;
        [JsonPropertyName("params")]
        public string[] Parameters { get; set; } = [];
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
