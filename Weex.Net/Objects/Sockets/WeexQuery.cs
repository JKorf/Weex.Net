using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System;
using Weex.Net.Objects.Models;

namespace Weex.Net.Objects.Sockets
{
    internal class WeexQuery : Query<WeexSocketResponse>
    {
        public WeexQuery(WeexSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<WeexSocketResponse>(request.Id.ToString(), HandleMessage);
        }

        public CallResult<WeexSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, WeexSocketResponse message)
        {
            if (!message.Result)
                return new CallResult<WeexSocketResponse>(new ServerError(ErrorInfo.Unknown with { Message = message.Message }));

            return new CallResult<WeexSocketResponse>(message, originalData, null);
        }
    }
}
