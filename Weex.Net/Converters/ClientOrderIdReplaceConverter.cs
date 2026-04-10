using CryptoExchange.Net;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Weex.Net.Converters
{
    internal class ClientOrderIdReplaceConverter : ReplaceConverter
    {
        public ClientOrderIdReplaceConverter(): base(
            $"{WeexExchange._clientReference}{LibraryHelpers.ClientOrderIdSeparator}->")
        {
        }
    }
}
