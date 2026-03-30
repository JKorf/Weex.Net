using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Funding info
    /// </summary>
    public record WeexFundingInfo
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>indexPrice</c>"] Index price
        /// </summary>
        [JsonPropertyName("indexPrice")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>lastFundingRate</c>"] Last funding rate
        /// </summary>
        [JsonPropertyName("lastFundingRate")]
        public decimal LastFundingRate { get; set; }
        /// <summary>
        /// ["<c>forecastFundingRate</c>"] Forecast funding rate
        /// </summary>
        [JsonPropertyName("forecastFundingRate")]
        public decimal ForecastFundingRate { get; set; }
        /// <summary>
        /// ["<c>interestRate</c>"] Interest rate
        /// </summary>
        [JsonPropertyName("interestRate")]
        public decimal InterestRate { get; set; }
        /// <summary>
        /// ["<c>nextFundingTime</c>"] Next funding time
        /// </summary>
        [JsonPropertyName("nextFundingTime")]
        public DateTime? NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Data timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>collectCycle</c>"] Interval in minutes
        /// </summary>
        [JsonPropertyName("collectCycle")]
        public int FundingInterval { get; set; }
    }


}
