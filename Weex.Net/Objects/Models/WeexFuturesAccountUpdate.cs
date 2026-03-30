using System;
using System.Text.Json.Serialization;
using Weex.Net.Enums;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Account update
    /// </summary>
    public record WeexFuturesAccountUpdate : WeexSocketEvent
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
        /// Balances
        /// </summary>
        [JsonPropertyName("d")]
        public WeexFuturesAccountBalanceUpdate[] Balances { get; set; } = [];
    }

    /// <summary>
    /// Balance update
    /// </summary>
    public record WeexFuturesAccountBalanceUpdate
    {
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginType MarginMode { get; set; }
        /// <summary>
        /// ["<c>crossSymbol</c>"] Cross symbol
        /// </summary>
        [JsonPropertyName("crossSymbol")]
        public string? CrossSymbol { get; set; }
        /// <summary>
        /// ["<c>isolatedPositionId</c>"] Isolated position id
        /// </summary>
        [JsonPropertyName("isolatedPositionId")]
        public string IsolatedPositionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>pendingDepositAmount</c>"] Pending deposit quantity
        /// </summary>
        [JsonPropertyName("pendingDepositAmount")]
        public decimal PendingDepositQuantity { get; set; }
        /// <summary>
        /// ["<c>pendingWithdrawAmount</c>"] Pending withdraw quantity
        /// </summary>
        [JsonPropertyName("pendingWithdrawAmount")]
        public decimal PendingWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>pendingTransferInAmount</c>"] Pending transfer in quantity
        /// </summary>
        [JsonPropertyName("pendingTransferInAmount")]
        public decimal PendingTransferInQuantity { get; set; }
        /// <summary>
        /// ["<c>pendingTransferOutAmount</c>"] Pending transfer out quantity
        /// </summary>
        [JsonPropertyName("pendingTransferOutAmount")]
        public decimal PendingTransferOutQuantity { get; set; }
        /// <summary>
        /// ["<c>liquidating</c>"] Liquidating
        /// </summary>
        [JsonPropertyName("liquidating")]
        public bool Liquidating { get; set; }
        /// <summary>
        /// ["<c>legacyAmount</c>"] Legacy quantity
        /// </summary>
        [JsonPropertyName("legacyAmount")]
        public decimal LegacyQuantity { get; set; }
        /// <summary>
        /// ["<c>cumDepositAmount</c>"] Total deposit quantity
        /// </summary>
        [JsonPropertyName("cumDepositAmount")]
        public decimal TotalDeposits { get; set; }
        /// <summary>
        /// ["<c>cumWithdrawAmount</c>"] Total withdrawal quantity
        /// </summary>
        [JsonPropertyName("cumWithdrawAmount")]
        public decimal TotalWithdrawals { get; set; }
        /// <summary>
        /// ["<c>cumTransferInAmount</c>"] Total transfer in quantity
        /// </summary>
        [JsonPropertyName("cumTransferInAmount")]
        public decimal TotalTransferIn { get; set; }
        /// <summary>
        /// ["<c>cumTransferOutAmount</c>"] Total transfer out quantity
        /// </summary>
        [JsonPropertyName("cumTransferOutAmount")]
        public decimal TotalTransferOut { get; set; }
        /// <summary>
        /// ["<c>cumMarginMoveInAmount</c>"] Total margin move in quantity
        /// </summary>
        [JsonPropertyName("cumMarginMoveInAmount")]
        public decimal TotalMarginMoveIn { get; set; }
        /// <summary>
        /// ["<c>cumMarginMoveOutAmount</c>"] Total margin move out quantity
        /// </summary>
        [JsonPropertyName("cumMarginMoveOutAmount")]
        public decimal TotalMarginMoveOut { get; set; }
        /// <summary>
        /// ["<c>cumPositionOpenLongAmount</c>"] Total position open long quantity
        /// </summary>
        [JsonPropertyName("cumPositionOpenLongAmount")]
        public decimal TotalOpenLong { get; set; }
        /// <summary>
        /// ["<c>cumPositionOpenShortAmount</c>"] Total position open short quantity
        /// </summary>
        [JsonPropertyName("cumPositionOpenShortAmount")]
        public decimal TotalOpenShort { get; set; }
        /// <summary>
        /// ["<c>cumPositionCloseLongAmount</c>"] Total position close long quantity
        /// </summary>
        [JsonPropertyName("cumPositionCloseLongAmount")]
        public decimal TotalCloseLong { get; set; }
        /// <summary>
        /// ["<c>cumPositionCloseShortAmount</c>"] Total position close short quantity
        /// </summary>
        [JsonPropertyName("cumPositionCloseShortAmount")]
        public decimal TotalCloseShort { get; set; }
        /// <summary>
        /// ["<c>cumPositionFillFeeAmount</c>"] Total position fill fee quantity
        /// </summary>
        [JsonPropertyName("cumPositionFillFeeAmount")]
        public decimal TotalFillFee { get; set; }
        /// <summary>
        /// ["<c>cumPositionLiquidateFeeAmount</c>"] Total position liquidation fee quantity
        /// </summary>
        [JsonPropertyName("cumPositionLiquidateFeeAmount")]
        public decimal TotalLiquidationFee { get; set; }
        /// <summary>
        /// ["<c>cumPositionFundingAmount</c>"] Total position funding fee quantity
        /// </summary>
        [JsonPropertyName("cumPositionFundingAmount")]
        public decimal TotalFundingFee { get; set; }
        /// <summary>
        /// ["<c>cumOrderFillFeeIncomeAmount</c>"] Total order fill fee income quantity
        /// </summary>
        [JsonPropertyName("cumOrderFillFeeIncomeAmount")]
        public decimal TotalOrderFillFeeIncome { get; set; }
        /// <summary>
        /// ["<c>cumOrderLiquidateFeeIncomeAmount</c>"] Total order liquidation fee income quantity
        /// </summary>
        [JsonPropertyName("cumOrderLiquidateFeeIncomeAmount")]
        public decimal TotalOrderLiquidationFeeIncome { get; set; }
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
