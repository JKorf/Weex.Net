using CryptoExchange.Net.Objects;
using Weex.Net.Objects;

namespace Weex.Net
{
    /// <summary>
    /// Weex environments
    /// </summary>
    public class WeexEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Rest Spot API address
        /// </summary>
        public string RestClientSpotAddress { get; }

        /// <summary>
        /// Socket Spot API address
        /// </summary>
        public string SocketClientSpotAddress { get; }
        /// <summary>
        /// Rest Futures API address
        /// </summary>
        public string RestClientFuturesAddress { get; }

        /// <summary>
        /// Socket Futures API address
        /// </summary>
        public string SocketClientFuturesAddress { get; }

        internal WeexEnvironment(
            string name,
            string restSpotAddress,
            string streamSpotAddress,
            string restFuturesAddress,
            string streamFuturesAddress) :
            base(name)
        {
            RestClientSpotAddress = restSpotAddress;
            SocketClientSpotAddress = streamSpotAddress;
            RestClientFuturesAddress = restFuturesAddress;
            SocketClientFuturesAddress = streamFuturesAddress;
        }

        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public WeexEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the Weex environment by name
        /// </summary>
        public static WeexEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static WeexEnvironment Live { get; }
            = new WeexEnvironment(TradeEnvironmentNames.Live,
                                     WeexApiAddresses.Default.RestClientSpotAddress,
                                     WeexApiAddresses.Default.SocketClientSpotAddress,
                                     WeexApiAddresses.Default.RestClientFuturesAddress,
                                     WeexApiAddresses.Default.SocketClientFuturesAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static WeexEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketStreamAddress,
                        string futuresRestAddress,
                        string futuresSocketStreamAddress)
            => new WeexEnvironment(name, spotRestAddress, spotSocketStreamAddress, futuresRestAddress, futuresSocketStreamAddress);
    }
}
