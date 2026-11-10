using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using System.Threading;
using System.Threading.Tasks;

namespace Weex.Net.Interfaces
{
    /// <summary>
    /// Tracker factory
    /// </summary>
    public interface IWeexTrackerFactory 
        : ITrackerFactory,
          ISpotUserDataTrackerFactory<WeexEnvironment>,
          IFuturesUserDataTrackerFactory<WeexEnvironment>
    {
        /// <summary>
        /// Create a new Spot user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="environment">Environment</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, WeexCredentials credentials, SpotUserDataTrackerConfig? config = null, WeexEnvironment? environment = null);
        /// <summary>
        /// Create a new spot user data tracker
        /// </summary>
        /// <param name="config">Configuration</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null);

        /// <summary>
        /// Create a new Futures user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="environment">Environment</param>
        IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, WeexCredentials credentials, FuturesUserDataTrackerConfig? config = null, WeexEnvironment? environment = null);
        /// <summary>
        /// Create a new Futures user data tracker
        /// </summary>
        /// <param name="config">Configuration</param>
        IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null);
        Task<CallResult<IUserFuturesDataTracker>> TryCreateUserFuturesDataTrackerAsync(string userIdentifier, FuturesUserDataTrackerConfig? config = null, WeexEnvironment? environment = null, CancellationToken ct = default);
    }
}
