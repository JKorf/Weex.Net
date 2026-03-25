using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Internal
{
    internal record WeexPong
    {
        [JsonPropertyName("method")]
        public string Method => "PONG";
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
