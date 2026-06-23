using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Weex.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatus>))]
    public enum SymbolStatus
    {
        /// <summary>
        /// ["<c>TRADING</c>"] Trading
        /// </summary>
        [Map("TRADING")]
        Trading,
        /// <summary>
        /// ["<c>BREAK</c>"] Paused
        /// </summary>
        [Map("BREAK")]
        Break,
        /// <summary>
        /// ["<c>HALT</c>"] Halted
        /// </summary>
        [Map("HALT")]
        Halt
    }

}
