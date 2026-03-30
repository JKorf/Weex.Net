using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Weex.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceType>))]
    public enum PriceType
    {
        /// <summary>
        /// ["<c>LAST</c>"] Last trade price
        /// </summary>
        [Map("LAST")]
        Last,
        /// <summary>
        /// ["<c>INDEX</c>"] Index price
        /// </summary>
        [Map("INDEX")]
        Index,
        /// <summary>
        /// ["<c>MARK</c>"] Mark price
        /// </summary>
        [Map("MARK")]
        Mark,
    }

}
