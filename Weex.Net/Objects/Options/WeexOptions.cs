using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace Weex.Net.Objects.Options
{
    /// <summary>
    /// Weex options
    /// </summary>
    public class WeexOptions : LibraryOptions<WeexRestOptions, WeexSocketOptions, WeexCredentials, WeexEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
    }
}
