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
    /// Bill page
    /// </summary>
    public record WeexFuturesBillPage
    {
        /// <summary>
        /// ["<c>hasNextPage</c>"] Has next page
        /// </summary>
        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage { get; set; }
        /// <summary>
        /// ["<c>items</c>"] Items
        /// </summary>
        [JsonPropertyName("items")]
        public WeexFuturesBill[] Items { get; set; } = [];
    }

    /// <summary>
    /// Bill
    /// </summary>
    public record WeexFuturesBill
    {
        /// <summary>
        /// ["<c>billId</c>"] Bill id
        /// </summary>
        [JsonPropertyName("billId")]
        public long BillId { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>income</c>"] Income
        /// </summary>
        [JsonPropertyName("income")]
        public decimal Income { get; set; }
        /// <summary>
        /// ["<c>incomeType</c>"] Income type
        /// </summary>
        [JsonPropertyName("incomeType")]
        public IncomeType IncomeType { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>fillFee</c>"] Transaction fee
        /// </summary>
        [JsonPropertyName("fillFee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>transferReason</c>"] Transfer reason
        /// </summary>
        [JsonPropertyName("transferReason")]
        public string TransferReason { get; set; } = string.Empty;
    }


}
