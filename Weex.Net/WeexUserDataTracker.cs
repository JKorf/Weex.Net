using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weex.Net.Interfaces.Clients;

namespace Weex.Net
{
    /// <inheritdoc />
    public class WeexUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public WeexUserSpotDataTracker(
            ILogger<WeexUserSpotDataTracker> logger,
            IWeexSharedApiClient client,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config = null) : base(
                logger,
                client,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
        {

        }
    }

    /// <inheritdoc />
    public class WeexUserFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc />
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public WeexUserFuturesDataTracker(
            ILogger<WeexUserFuturesDataTracker> logger,
            IWeexSharedApiClient client,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config = null) :
            base(logger,
                client,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {

        }
    }
}
