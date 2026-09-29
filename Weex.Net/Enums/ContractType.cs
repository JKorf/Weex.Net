using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Weex.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractType>))]
    public enum ContractType
    {
        /// <summary>
        /// ["<c>PERPETUAL</c>"] Perpetual
        /// </summary>
        [Map("PERPETUAL")]
        Perpetual,
        /// <summary>
        /// ["<c>TRADIFI_PERPETUAL</c>"] TradFi perpetual
        /// </summary>
        [Map("TRADIFI_PERPETUAL")]
        TradFiPerpetual,
    }

}
