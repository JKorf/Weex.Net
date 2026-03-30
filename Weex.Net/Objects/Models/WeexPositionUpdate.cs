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
    /// Position update
    /// </summary>
    public record WeexPositionUpdate : WeexSocketEvent
    {
        /// <summary>
        /// Update version
        /// </summary>
        [JsonPropertyName("v")]
        public long Version { get; set; }
        /// <summary>
        /// Source event
        /// </summary>
        [JsonPropertyName("msgEvent")]
        public string EventType { get; set; } = string.Empty;
        /// <summary>
        /// Positions
        /// </summary>
        [JsonPropertyName("d")]
        public WeexPositionUpdateItem[] Positions { get; set; } = [];
    }

    /// <summary>
    /// Position 
    /// </summary>
    public record WeexPositionUpdateItem
    {
        /// <summary>
        /// ["<c>id</c>"] Position id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Position side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide Side { get; set; }
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginType MarginMode { get; set; }
        /// <summary>
        /// ["<c>separatedMode</c>"] Separated mode
        /// </summary>
        [JsonPropertyName("separatedMode")]
        public PositionCombineType SeparatedMode { get; set; }
        /// <summary>
        /// ["<c>separatedOpenOrderId</c>"] Separated open order id
        /// </summary>
        [JsonPropertyName("separatedOpenOrderId")]
        public long? SeparatedOpenOrderId { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>openValue</c>"] Open value
        /// </summary>
        [JsonPropertyName("openValue")]
        public decimal OpenValue { get; set; }
        /// <summary>
        /// ["<c>openFee</c>"] Open fee
        /// </summary>
        [JsonPropertyName("openFee")]
        public decimal OpenFee { get; set; }
        /// <summary>
        /// ["<c>fundingFee</c>"] Funding fee
        /// </summary>
        [JsonPropertyName("fundingFee")]
        public decimal FundingFee { get; set; }
        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>autoAppendIsolatedMargin</c>"] Auto append isolated margin
        /// </summary>
        [JsonPropertyName("autoAppendIsolatedMargin")]
        public bool AutoAppendIsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>cumOpenSize</c>"] Totalopen quantity
        /// </summary>
        [JsonPropertyName("cumOpenSize")]
        public decimal TotalOpenQuantity { get; set; }
        /// <summary>
        /// ["<c>cumOpenValue</c>"] Totalopen value
        /// </summary>
        [JsonPropertyName("cumOpenValue")]
        public decimal TotalOpenValue { get; set; }
        /// <summary>
        /// ["<c>cumOpenFee</c>"] Total open fee
        /// </summary>
        [JsonPropertyName("cumOpenFee")]
        public decimal TotalOpenFee { get; set; }
        /// <summary>
        /// ["<c>cumCloseSize</c>"] Total close quantity
        /// </summary>
        [JsonPropertyName("cumCloseSize")]
        public decimal TotalCloseQuantity { get; set; }
        /// <summary>
        /// ["<c>cumCloseValue</c>"] Total close value
        /// </summary>
        [JsonPropertyName("cumCloseValue")]
        public decimal TotalCloseValue { get; set; }
        /// <summary>
        /// ["<c>cumCloseFee</c>"] Total close fee
        /// </summary>
        [JsonPropertyName("cumCloseFee")]
        public decimal TotalCloseFee { get; set; }
        /// <summary>
        /// ["<c>cumFundingFee</c>"] Total funding fee
        /// </summary>
        [JsonPropertyName("cumFundingFee")]
        public decimal TotalFundingFee { get; set; }
        /// <summary>
        /// ["<c>cumLiquidateFee</c>"] Total liquidate fee
        /// </summary>
        [JsonPropertyName("cumLiquidateFee")]
        public decimal TotalLiquidateFee { get; set; }
        /// <summary>
        /// ["<c>createdMatchSequenceId</c>"] Created match sequence id
        /// </summary>
        [JsonPropertyName("createdMatchSequenceId")]
        public long? CreatedMatchSequenceId { get; set; }
        /// <summary>
        /// ["<c>updatedMatchSequenceId</c>"] Updated match sequence id
        /// </summary>
        [JsonPropertyName("updatedMatchSequenceId")]
        public long? UpdatedMatchSequenceId { get; set; }
        /// <summary>
        /// ["<c>createdTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updatedTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime? UpdateTime { get; set; }
    }
}
