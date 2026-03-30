using System;
using System.Text.Json.Serialization;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Funding bill
    /// </summary>
    public record WeexFundingBill
    {
        /// <summary>
        /// ["<c>billId</c>"] Bill id
        /// </summary>
        [JsonPropertyName("billId")]
        public string BillId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coinId</c>"] Asset id
        /// </summary>
        [JsonPropertyName("coinId")]
        public long AssetId { get; set; }
        /// <summary>
        /// ["<c>coinName</c>"] Asset name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bizType</c>"] Business type
        /// </summary>
        [JsonPropertyName("bizType")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>deltaAmount</c>"] Delta quantity
        /// </summary>
        [JsonPropertyName("deltaAmount")]
        public decimal DeltaQuantity { get; set; }
        /// <summary>
        /// ["<c>afterAmount</c>"] After quantity
        /// </summary>
        [JsonPropertyName("afterAmount")]
        public decimal AfterQuantity { get; set; }
        /// <summary>
        /// ["<c>fees</c>"] Fees
        /// </summary>
        [JsonPropertyName("fees")]
        public decimal Fees { get; set; }
        /// <summary>
        /// ["<c>cTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("cTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>fillSize</c>"] Fill quantity
        /// </summary>
        [JsonPropertyName("fillSize")]
        public decimal? FillQuantity { get; set; }
        /// <summary>
        /// ["<c>fillValue</c>"] Fill value
        /// </summary>
        [JsonPropertyName("fillValue")]
        public decimal? FillValue { get; set; }
    }


}
