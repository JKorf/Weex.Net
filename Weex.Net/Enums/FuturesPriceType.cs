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
    /// Futures price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesPriceType>))]
    public enum FuturesPriceType
    {
        /// <summary>
        /// ["<c>UNKNOWN_PRICE_TYPE</c>"] Unknown/not set
        /// </summary>
        [Map("UNKNOWN_PRICE_TYPE")]
        Unknown,
        /// <summary>
        /// ["<c>CONTRACT_PRICE</c>"] Last trade price
        /// </summary>
        [Map("CONTRACT_PRICE")]
        ContractPrice,
        /// <summary>
        /// ["<c>MARK_PRICE</c>"] Mark price
        /// </summary>
        [Map("MARK_PRICE")]
        MarkPrice,
    }

}
