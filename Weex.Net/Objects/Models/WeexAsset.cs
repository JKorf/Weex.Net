using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Objects.Models
{
    /// <summary>
    /// Asset info
    /// </summary>
    public record WeexAsset
    {
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositAllEnable</c>"] Deposit all enabled
        /// </summary>
        [JsonPropertyName("depositAllEnable")]
        public bool DepositAllEnabled { get; set; }
        /// <summary>
        /// ["<c>withdrawAllEnable</c>"] Withdraw all enabled
        /// </summary>
        [JsonPropertyName("withdrawAllEnable")]
        public bool WithdrawAllEnabled { get; set; }
        /// <summary>
        /// ["<c>networkList</c>"] Networks
        /// </summary>
        [JsonPropertyName("networkList")]
        public WeexAssetNetwork[] Networks { get; set; } = [];
    }

    /// <summary>
    /// Network info
    /// </summary>
    public record WeexAssetNetwork
    {
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawIntegerMultiple</c>"] Withdraw quantity should be multiple of this
        /// </summary>
        [JsonPropertyName("withdrawIntegerMultiple")]
        public decimal WithdrawIntegerMultiple { get; set; }
        /// <summary>
        /// ["<c>isDefault</c>"] Is default
        /// </summary>
        [JsonPropertyName("isDefault")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// ["<c>depositEnable</c>"] Deposit enabled
        /// </summary>
        [JsonPropertyName("depositEnable")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// ["<c>withdrawEnable</c>"] Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdrawEnable")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>depositDesc</c>"] Deposit description
        /// </summary>
        [JsonPropertyName("depositDesc")]
        public string? DepositDescription { get; set; }
        /// <summary>
        /// ["<c>withdrawDesc</c>"] Withdraw description
        /// </summary>
        [JsonPropertyName("withdrawDesc")]
        public string? WithdrawDescription { get; set; }
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawFee</c>"] Withdraw fee
        /// </summary>
        [JsonPropertyName("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// ["<c>withdrawMin</c>"] Withdraw min
        /// </summary>
        [JsonPropertyName("withdrawMin")]
        public decimal WithdrawMin { get; set; }
        /// <summary>
        /// ["<c>withdrawInternalMin</c>"] Withdraw internal minimum
        /// </summary>
        [JsonPropertyName("withdrawInternalMin")]
        public decimal? WithdrawInternalMin { get; set; }
        /// <summary>
        /// ["<c>depositDust</c>"] Minimum deposit amount that will be credited.
        /// </summary>
        [JsonPropertyName("depositDust")]
        public decimal DepositDust { get; set; }
        /// <summary>
        /// ["<c>minConfirm</c>"] Min number of confirmations
        /// </summary>
        [JsonPropertyName("minConfirm")]
        public int MinConfirmations { get; set; }
        /// <summary>
        /// ["<c>withdrawTag</c>"] Requires withdraw tag
        /// </summary>
        [JsonPropertyName("withdrawTag")]
        public bool WithdrawTag { get; set; }
        /// <summary>
        /// ["<c>contractAddressUrl</c>"] Contract address url
        /// </summary>
        [JsonPropertyName("contractAddressUrl")]
        public string ContractAddressUrl { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractAddress</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
    }


}
