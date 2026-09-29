using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Weex.Net.Enums
{
    /// <summary>
    /// Underlying type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnderlyingType>))]
    public enum UnderlyingType
    {
        /// <summary>
        /// ["<c>COIN</c>"] Crypto
        /// </summary>
        [Map("COIN")]
        Crypto,
        /// <summary>
        /// ["<c>Metals</c>"] Metals
        /// </summary>
        [Map("Metals")]
        Metals,
        /// <summary>
        /// ["<c>Forex</c>"] Forex
        /// </summary>
        [Map("Forex")]
        Forex,
        /// <summary>
        /// ["<c>Stocks</c>"] Stocks
        /// </summary>
        [Map("Stocks")]
        Stocks,
        /// <summary>
        /// ["<c>Indices</c>"] Indices
        /// </summary>
        [Map("Indices")]
        Indices,
        /// <summary>
        /// ["<c>Commodities</c>"] Commodities
        /// </summary>
        [Map("Commodities")]
        Commodities,
        /// <summary>
        /// ["<c>Pre-IPO</c>"] Pre-IPO
        /// </summary>
        [Map("Pre-IPO")]
        PreIpo,
    }

}
