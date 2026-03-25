using CryptoExchange.Net.Objects.Options;

namespace Weex.Net.Objects.Options
{
    /// <summary>
    /// Options for the WeexSocketClient
    /// </summary>
    public class WeexSocketOptions : SocketExchangeOptions<WeexEnvironment, WeexCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static WeexSocketOptions Default { get; set; } = new WeexSocketOptions()
        {
            Environment = WeexEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };


        /// <summary>
        /// ctor
        /// </summary>
        public WeexSocketOptions()
        {
            Default?.Set(this);
        }


        
         /// <summary>
        /// Futures API options
        /// </summary>
        public SocketApiOptions FuturesOptions { get; private set; } = new SocketApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();


        internal WeexSocketOptions Set(WeexSocketOptions targetOptions)
        {
            targetOptions = base.Set<WeexSocketOptions>(targetOptions);
            
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);

            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);

            return targetOptions;
        }
    }
}
