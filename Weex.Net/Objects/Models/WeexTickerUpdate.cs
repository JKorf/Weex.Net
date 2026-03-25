using System;
using System.Text.Json.Serialization;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Ticker update
    /// </summary>
    public record WeexTickerUpdate
    {
        /// <summary>
        /// ["<c>p</c>"] Price change
        /// </summary>
        [JsonPropertyName("p")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// ["<c>P</c>"] Price change percentage
        /// </summary>
        [JsonPropertyName("P")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// ["<c>w</c>"] Volume weighted average price
        /// </summary>
        [JsonPropertyName("w")]
        public decimal Vwap { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Open price 24h ago
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] High price in last 24h
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Low price in last 24h
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Volume in base asset
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Volume in quote asset
        /// </summary>
        [JsonPropertyName("q")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>O</c>"] Stats start time
        /// </summary>
        [JsonPropertyName("O")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>C</c>"] Stats end time
        /// </summary>
        [JsonPropertyName("C")]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// ["<c>n</c>"] Number of trades
        /// </summary>
        [JsonPropertyName("n")]
        public int Trades { get; set; }
    }


}
