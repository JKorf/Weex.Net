namespace Weex.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class WeexApiAddresses
    {
        /// <summary>
        /// The address used by the WeexRestClient for the Spot API
        /// </summary>
        public string RestClientSpotAddress { get; set; } = "";
        /// <summary>
        /// The address used by the WeexSocketClient for the websocket Spot API
        /// </summary>
        public string SocketClientSpotAddress { get; set; } = "";
        /// <summary>
        /// The address used by the WeexRestClient for the Futures API
        /// </summary>
        public string RestClientFuturesAddress { get; set; } = "";
        /// <summary>
        /// The address used by the WeexSocketClient for the websocket Futures API
        /// </summary>
        public string SocketClientFuturesAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Weex API
        /// </summary>
        public static WeexApiAddresses Default = new WeexApiAddresses
        {
            RestClientSpotAddress = "https://api-spot.weex.com",
            SocketClientSpotAddress = "wss://ws-spot.weex.com",
            RestClientFuturesAddress = "https://api-contract.weex.com",
            SocketClientFuturesAddress = "wss://ws-contract.weex.com"
        };
    }
}
