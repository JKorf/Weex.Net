using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Sockets
{
    internal record WeexSocketResponse
    {
        [JsonPropertyName("result")]
        public bool Result { get; set; }
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
    }
}
