using System;
using System.Text.Json.Serialization;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Account info
    /// </summary>
    public record WeexAccountInfo
    {
        /// <summary>
        /// ["<c>makerCommission</c>"] Maker commission in bps
        /// </summary>
        [JsonPropertyName("makerCommission")]
        public decimal MakerCommission { get; set; }
        /// <summary>
        /// ["<c>takerCommission</c>"] Taker commission in bps
        /// </summary>
        [JsonPropertyName("takerCommission")]
        public decimal TakerCommission { get; set; }
        /// <summary>
        /// ["<c>commissionRates</c>"] Commission rates
        /// </summary>
        [JsonPropertyName("commissionRates")]
        public WeexAccountCommission CommissionRates { get; set; } = null!;
        /// <summary>
        /// ["<c>canTrade</c>"] Can trade
        /// </summary>
        [JsonPropertyName("canTrade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// ["<c>canWithdraw</c>"] Can withdraw
        /// </summary>
        [JsonPropertyName("canWithdraw")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// ["<c>canDeposit</c>"] Can deposit
        /// </summary>
        [JsonPropertyName("canDeposit")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// ["<c>permissions</c>"] Permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public string[] Permissions { get; set; } = [];
        /// <summary>
        /// ["<c>balances</c>"] Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public WeexBalance[] Balances { get; set; } = [];
        /// <summary>
        /// ["<c>uid</c>"] Uid
        /// </summary>
        [JsonPropertyName("uid")]
        public long Uid { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Commission rates
    /// </summary>
    public record WeexAccountCommission
    {
        /// <summary>
        /// ["<c>maker</c>"] Maker percentage
        /// </summary>
        [JsonPropertyName("maker")]
        public decimal Maker { get; set; }
        /// <summary>
        /// ["<c>taker</c>"] Taker percentage
        /// </summary>
        [JsonPropertyName("taker")]
        public decimal Taker { get; set; }
    }

    /// <summary>
    /// Balance info
    /// </summary>
    public record WeexBalance
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>free</c>"] Free
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Free { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
    }


}
