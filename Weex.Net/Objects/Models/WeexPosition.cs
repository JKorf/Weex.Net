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
    /// Position info
    /// </summary>
    public record WeexPosition
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
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
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide Side { get; set; }
        /// <summary>
        /// ["<c>marginType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public MarginType MarginType { get; set; }
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
        /// ["<c>size</c>"] Position size
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
        /// ["<c>marginSize</c>"] Margin amount (margin asset)
        /// </summary>
        [JsonPropertyName("marginSize")]
        public decimal MarginQuantity { get; set; }
        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>isAutoAppendIsolatedMargin</c>"] Whether the auto-adding of funds for the isolated margin is enabled (only for isolated mode)
        /// </summary>
        [JsonPropertyName("isAutoAppendIsolatedMargin")]
        public bool IsAutoAppendIsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>cumOpenSize</c>"] Accumulated opened positions
        /// </summary>
        [JsonPropertyName("cumOpenSize")]
        public decimal CumOpenQuantity { get; set; }
        /// <summary>
        /// ["<c>cumOpenValue</c>"] Accumulated value of opened positions
        /// </summary>
        [JsonPropertyName("cumOpenValue")]
        public decimal CumOpenValue { get; set; }
        /// <summary>
        /// ["<c>cumOpenFee</c>"] Accumulated fees paid for opened positions
        /// </summary>
        [JsonPropertyName("cumOpenFee")]
        public decimal CumOpenFee { get; set; }
        /// <summary>
        /// ["<c>cumCloseSize</c>"] Accumulated closed positions
        /// </summary>
        [JsonPropertyName("cumCloseSize")]
        public decimal CumCloseQuantity { get; set; }
        /// <summary>
        /// ["<c>cumCloseValue</c>"] Accumulated value of closed positions
        /// </summary>
        [JsonPropertyName("cumCloseValue")]
        public decimal CumCloseValue { get; set; }
        /// <summary>
        /// ["<c>cumCloseFee</c>"] Accumulated fees paid for closing positions
        /// </summary>
        [JsonPropertyName("cumCloseFee")]
        public decimal CumCloseFee { get; set; }
        /// <summary>
        /// ["<c>cumFundingFee</c>"] Accumulated settled funding fee
        /// </summary>
        [JsonPropertyName("cumFundingFee")]
        public decimal CumFundingFee { get; set; }
        /// <summary>
        /// ["<c>cumLiquidateFee</c>"] Accumulated liquidation fees
        /// </summary>
        [JsonPropertyName("cumLiquidateFee")]
        public decimal CumLiquidateFee { get; set; }
        /// <summary>
        /// ["<c>createdMatchSequenceId</c>"] Created match sequence id
        /// </summary>
        [JsonPropertyName("createdMatchSequenceId")]
        public long? CreatedMatchSequenceId { get; set; }
        /// <summary>
        /// ["<c>updatedMatchSequenceId</c>"] Matching engine sequence ID at creation
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
        /// <summary>
        /// ["<c>unrealizePnl</c>"] Unrealize pnl
        /// </summary>
        [JsonPropertyName("unrealizePnl")]
        public decimal UnrealizePnl { get; set; }
        /// <summary>
        /// ["<c>liquidatePrice</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidatePrice")]
        public decimal LiquidationPrice { get; set; }
    }


}
