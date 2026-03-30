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
    /// Position combine type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionCombineType>))]
    public enum PositionCombineType
    {
        /// <summary>
        /// ["<c>COMBINED</c>"] Combined mode
        /// </summary>
        [Map("COMBINED")]
        Combined,
        /// <summary>
        /// ["<c>SEPARATED</c>"] Separated mode
        /// </summary>
        [Map("SEPARATED")]
        Separated,
    }

}
