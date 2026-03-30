using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weex.Net.Enums
{
    /// <summary>
    /// Income type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IncomeType>))]
    public enum IncomeType
    {
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// Withdrawal
        /// </summary>
        [Map("withdraw")]
        Withdraw,
        /// <summary>
        /// Transfer between different accounts (in
        /// </summary>
        [Map("transfer_in")]
        TransferIn,
        /// <summary>
        /// Transfer between different accounts (out
        /// </summary>
        [Map("transfer_out")]
        TransferOut,
        /// <summary>
        /// Collateral transferred within the same account due to opening/closing positions, manual/auto addition
        /// </summary>
        [Map("margin_move_in")]
        MarginMoveIn,
        /// <summary>
        /// Collateral transferred out within the same account due to opening/closing positions, manual/auto addition
        /// </summary>
        [Map("margin_move_out")]
        MarginMoveOut,
        /// <summary>
        /// Collateral change from opening long positions (buying decreases collateral
        /// </summary>
        [Map("position_open_long")]
        PositionOpenLong,
        /// <summary>
        /// Collateral change from opening short positions (selling increases collateral
        /// </summary>
        [Map("position_open_short")]
        PositionOpenShort,
        /// <summary>
        /// Collateral change from closing long positions (selling increases collateral
        /// </summary>
        [Map("position_close_long")]
        PositionCloseLong,
        /// <summary>
        /// Collateral change from closing short positions (buying decreases collateral
        /// </summary>
        [Map("position_close_short")]
        PositionCloseShort,
        /// <summary>
        /// Collateral change from position funding fee settlement
        /// </summary>
        [Map("position_funding")]
        PositionFunding,
        /// <summary>
        /// Order fill fee income (specific to fee account
        /// </summary>
        [Map("order_fill_fee_income")]
        OrderFillFeeIncome,
        /// <summary>
        /// Order liquidation fee income (specific to fee account
        /// </summary>
        [Map("order_liquidate_fee_income")]
        OrderLiquidateFeeIncome,
        /// <summary>
        /// Start liquidation
        /// </summary>
        [Map("start_liquidate")]
        StartLiquidate,
        /// <summary>
        /// Finish liquidation
        /// </summary>
        [Map("finish_liquidate")]
        FinishLiquidate,
        /// <summary>
        /// Compensation for liquidation loss
        /// </summary>
        [Map("order_fix_margin_amount")]
        OrderFixMarginAmount,
        /// <summary>
        /// Copy trading payment, pre-deducted from followers after position closing if profitable
        /// </summary>
        [Map("tracking_follow_pay")]
        TrackingFollowPay,
        /// <summary>
        /// Pre-received commission, commission system account receives pre-deducted amount from followers
        /// </summary>
        [Map("tracking_system_pre_receive")]
        TrackingSystemPreReceive,
        /// <summary>
        /// Copy trading commission refund
        /// </summary>
        [Map("tracking_follow_back")]
        TrackingFollowBack,
        /// <summary>
        /// Lead trader income
        /// </summary>
        [Map("tracking_trader_income")]
        TrackingTraderIncome,
        /// <summary>
        /// Profit sharing (shared by lead trader with others
        /// </summary>
        [Map("tracking_third_party_share")]
        TrackingThirdPartyShare,
    }

}
