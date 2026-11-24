# Standalone examples

Each `.cs` file is an independent top-level console program. Copy one into `Program.cs` at a time.

| File | Purpose |
| --- | --- |
| [01-spot-quickstart.cs](01-spot-quickstart.cs) | Public spot data and authenticated spot orders |
| [02-futures.cs](02-futures.cs) | Leverage, futures orders, positions, and closing |
| [03-websocket.cs](03-websocket.cs) | Public streams and optional private order stream with cleanup |
| [04-multi-exchange.cs](04-multi-exchange.cs) | Shared API V2 market data and subscriptions |
| [05-error-handling.cs](05-error-handling.cs) | Result handling and read retries |

```shell
dotnet new console -n MyWeexApp
cd MyWeexApp
dotnet add package Weex.Net
# Copy ONE example into Program.cs
dotnet build
```

Compilation executes no exchange operations. Running the spot and futures trading examples with valid credentials changes live account state; they are not sandbox programs. The socket example optionally reads `WEEX_API_KEY`, `WEEX_API_SECRET`, and `WEEX_API_PASSPHRASE` for private updates. Examples using placeholders require explicit credential configuration before private calls can succeed.

See the [consumer skill](../../.claude/skills/weex-net/SKILL.md), [operational considerations](../../.claude/skills/weex-net/references/safety.md), and [complete projects](../README.md).
