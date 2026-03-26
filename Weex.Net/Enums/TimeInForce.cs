using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Weex.Net.Enums
{
    /// <summary>
    /// Time in force
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TimeInForce>))]
    public enum TimeInForce
    {
        /// <summary>
        /// ["<c>GTC</c>"] Good until canceled
        /// </summary>
        [Map("GTC", "GOOD_TIL_CANCEL")]
        GoodTillCanceled,
        /// <summary>
        /// ["<c>IOC</c>"] Immediately fill at least partially or cancel
        /// </summary>
        [Map("IOC")]
        ImmediateOrCancel,
        /// <summary>
        /// ["<c>FOK</c>"] Immediately fill entirely or cancel
        /// </summary>
        [Map("FOK")]
        FillOrKill,
        /// <summary>
        /// ["<c>POST_ONLY</c>"] Post only
        /// </summary>
        [Map("POST_ONLY")]
        PostOnly
    }

}
