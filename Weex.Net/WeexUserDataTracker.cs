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
            IWeexRestClient restClient,
            IWeexSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config = null) : base(
                logger,
                restClient.SpotApi.SharedClient,
                null,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
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
            IWeexRestClient restClient,
            IWeexSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config = null) :
            base(logger,
                restClient.FuturesApi.SharedClient,
                null,
                restClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                restClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {

        }
    }
}
