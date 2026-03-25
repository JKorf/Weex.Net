namespace Weex.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class WeexApiAddresses
    {
        /// <summary>
        /// The address used by the WeexRestClient for the API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the WeexSocketClient for the websocket API
        /// </summary>
        public string SocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Weex API
        /// </summary>
        public static WeexApiAddresses Default = new WeexApiAddresses
        {
            RestClientAddress = "https://api-spot.weex.com",
            SocketClientAddress = "wss://ws-spot.weex.com"
        };
    }
}
