using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Weex.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BusinessType>))]
    public enum BusinessType
    {
        /// <summary>
        /// ["<c>deposit</c>"] Deposit
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// ["<c>withdraw</c>"] Withdraw
        /// </summary>
        [Map("withdraw")]
        Withdraw,
        /// <summary>
        /// ["<c>trade_out</c>"] Trade out
        /// </summary>
        [Map("trade_out")]
        TradeOut,
        /// <summary>
        /// ["<c>trade_in</c>"] Trade in
        /// </summary>
        [Map("trade_in")]
        TradeIn,
        /// <summary>
        /// ["<c>transfer_in</c>"] Transfer in
        /// </summary>
        [Map("transfer_in")]
        TransferIn,
        /// <summary>
        /// ["<c>transfer_out</c>"] Transfer out
        /// </summary>
        [Map("transfer_out")]
        TransferOut,
    }

}
