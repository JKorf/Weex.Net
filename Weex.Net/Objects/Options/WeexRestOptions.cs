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
            
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);

            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);

            return targetOptions;
        }
    }
}
