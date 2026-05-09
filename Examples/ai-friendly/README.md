# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable** - drop into a console project with `dotnet add package Weex.Net` and it builds.
- **Self-contained** - single file, no external setup, no shared helpers.
- **Heavily commented** - explains why each important step exists.
- **Idiomatic** - follows the current Weex.Net 1.x API shape.

## Files

| File | What it shows |
|---|---|
| `01-spot-quickstart.cs` | Client setup, spot ticker, account balances, limit order, order status |
| `02-futures.cs` | Futures leverage, market order, positions, close positions |
| `03-websocket.cs` | Spot and futures public streams, private account/order streams, proper teardown |
| `04-multi-exchange.cs` | `CryptoExchange.Net.SharedApis` pattern for exchange-agnostic code |
| `05-error-handling.cs` | `WebCallResult` patterns, retry, validation with exchange info |

## Running

```bash
dotnet new console -n MyWeexApp
cd MyWeexApp
dotnet add package Weex.Net
# Copy the example .cs file content into Program.cs
# Replace API_KEY / API_SECRET / API_PASSPHRASE placeholders where needed
dotnet run
```
