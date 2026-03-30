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
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderType>))]
    public enum FuturesOrderType
    {
        /// <summary>
        /// ["<c>LIMIT</c>"] Limit
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// ["<c>MARKET</c>"] Market
        /// </summary>
        [Map("MARKET")]
        Market,
        /// <summary>
        /// ["<c>STOP</c>"] Stop
        /// </summary>
        [Map("STOP")]
        Stop,
        /// <summary>
        /// ["<c>TAKE_PROFIT</c>"] Take profit
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfit,
        /// <summary>
        /// ["<c>STOP_MARKET</c>"] Stop market
        /// </summary>
        [Map("STOP_MARKET")]
        StopMarket,
        /// <summary>
        /// ["<c>TAKE_PROFIT_MARKET</c>"] Take profit market
        /// </summary>
        [Map("TAKE_PROFIT_MARKET")]
        TakeProfitMarket,
    }

}
