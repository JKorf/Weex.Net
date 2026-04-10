using CryptoExchange.Net.Objects.Options;

namespace Weex.Net.Objects.Options
{
    /// <summary>
    /// Options for the WeexRestClient
    /// </summary>
    public class WeexRestOptions : RestExchangeOptions<WeexEnvironment, WeexCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static WeexRestOptions Default { get; set; } = new WeexRestOptions()
        {
            Environment = WeexEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public WeexRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Whether to allow the client to adjust the clientOrderId parameter send by the user when placing orders to include a client reference. This reference is used by the exchange to allocate a small percentage of the paid trading fees to developer of this library. Defaults to true.<br />
        /// Note that:<br />
        /// * It does not impact the amount of fees a user pays in any way<br />
        /// * It does not impact functionality. The reference is added just before sending the request and removed again during data deserialization<br />
        /// * It does respect client order id field limitations. For example if the user provided client order id parameter is too long to fit the reference it will not be added<br />
        /// * Toggling this option might fail operations using a clientOrderId parameter for pre-existing orders which were placed before the toggle. Operations on orders placed after the toggle will work as expected. It's advised to toggle when there are no open orders
        /// </summary>
        public bool AllowAppendingClientOrderId { get; set; } = true;

        /// <summary>
        /// Broker id
        /// </summary>
        public string? BrokerId { get; set; }

        /// <summary>
        /// Futures API options
        /// </summary>
        public RestApiOptions FuturesOptions { get; private set; } = new RestApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions();


        internal WeexRestOptions Set(WeexRestOptions targetOptions)
        {
            targetOptions = base.Set<WeexRestOptions>(targetOptions);
            targetOptions.AllowAppendingClientOrderId = AllowAppendingClientOrderId;
            targetOptions.BrokerId = BrokerId;
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
