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
    /// Leverage result
    /// </summary>
    public record WeexLeverageResult
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>crossLeverage</c>"] Cross leverage
        /// </summary>
        [JsonPropertyName("crossLeverage")]
        public decimal CrossLeverage { get; set; }
        /// <summary>
        /// ["<c>isolatedLongLeverage</c>"] Isolated long leverage
        /// </summary>
        [JsonPropertyName("isolatedLongLeverage")]
        public decimal IsolatedLongLeverage { get; set; }
        /// <summary>
        /// ["<c>isolatedShortLeverage</c>"] Isolated short leverage
        /// </summary>
        [JsonPropertyName("isolatedShortLeverage")]
        public decimal IsolatedShortLeverage { get; set; }
    }


}
