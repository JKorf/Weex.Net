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
    /// Plan type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PlanType>))]
    public enum PlanType
    {
        /// <summary>
        /// ["<c>TAKE_PROFIT</c>"] Take profit
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfit,
        /// <summary>
        /// ["<c>STOP_LOSS</c>"] Stop loss
        /// </summary>
        [Map("STOP_LOSS")]
        StopLoss,
    }

}
