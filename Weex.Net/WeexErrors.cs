using CryptoExchange.Net.Objects.Errors;

namespace Weex.Net
{
    internal static class WeexErrors
    {
        public static ErrorMapping RestErrors { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.SystemError, true, "System error", "-1054"),

                new ErrorInfo(ErrorType.InvalidTimestamp, false, "Invalid request timestamp", "-1044", "-1046"),

                new ErrorInfo(ErrorType.Unauthorized, false, "Unauthorized, check credentials", "-1047", "-1049"),

                new ErrorInfo(ErrorType.Unauthorized, false, "Unauthorized", "-1050", "-1051", "-1057", "-1190"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "-1052", "-1053"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Unauthorized IP address", "-1056"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Trading pair not allowed", "-1058"),

                new ErrorInfo(ErrorType.MissingParameter, false, "Missing parameter", "-1141"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", "-1115", "-1116", "-1128", "-1140", "-1142", "-1170", "-1171", "-1180"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Decimal precision incorrect", "-1160"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "-1121", "-2007"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Invalid order", "-2200"),
            ]
            );
    }
}
