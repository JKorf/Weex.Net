using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Trading fee
    /// </summary>
    public record WeexTradingFee
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>makerCommissionRate</c>"] Maker commission rate
        /// </summary>
        [JsonPropertyName("makerCommissionRate")]
        public decimal MakerCommissionRate { get; set; }
        /// <summary>
        /// ["<c>takerCommissionRate</c>"] Taker commission rate
        /// </summary>
        [JsonPropertyName("takerCommissionRate")]
        public decimal TakerCommissionRate { get; set; }
    }


}
