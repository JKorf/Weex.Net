using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Enums
{
    /// <summary>
    /// Algo type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AlgoType>))]
    public enum AlgoType
    {
        /// <summary>
        /// ["<c>CONDITIONAL</c>"] Conditional
        /// </summary>
        [Map("CONDITIONAL")]
        Conditional,
    }

}
