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
    /// Margin adjust type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginAdjustType>))]
    public enum MarginAdjustType
    {
        /// <summary>
        /// ["<c>1</c>"] Increase margin
        /// </summary>
        [Map("1")]
        Increase,
        /// <summary>
        /// ["<c>2</c>"] Decrease margin
        /// </summary>
        [Map("2")]
        Decrease,
    }

}
