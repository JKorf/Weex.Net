using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Kline update
    /// </summary>
    public record WeexKlineUpdate
    {
        /// <summary>
        /// ["<c>t</c>"] Open timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>T</c>"] Close timestamp
        /// </summary>
        [JsonPropertyName("T")]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>i</c>"] Interval
        /// </summary>
        public KlineInterval Interval => EnumConverter.ParseString<KlineInterval>(IntervalString)!.Value;
        [JsonPropertyName("i")]
        internal string IntervalString { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>o</c>"] Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] HighPrice
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] LowPrice
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>n</c>"] Number of trades
        /// </summary>
        [JsonPropertyName("n")]
        public int Trades { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Quote volume
        /// </summary>
        [JsonPropertyName("q")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>V</c>"] Taker buy volume in base asset
        /// </summary>
        [JsonPropertyName("V")]
        public decimal TakerBuyVolume { get; set; }
        /// <summary>
        /// ["<c>Q</c>"] Taker buy volume in quote asset
        /// </summary>
        [JsonPropertyName("Q")]
        public decimal TakerBuyQuoteVolume { get; set; }
    }


}
