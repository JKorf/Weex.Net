using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Weex.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountUpdateType>))]
    public enum AccountUpdateType
    {
        /// <summary>
        /// Deposit update
        /// </summary>
        [Map("DepositUpdate")]
        DepositUpdate,
        /// <summary>
        /// Withdraw update
        /// </summary>
        [Map("WithdrawUpdate")]
        WithdrawUpdate,
        /// <summary>
        /// Order update
        /// </summary>
        [Map("OrderUpdate")]
        OrderUpdate
    }
}
