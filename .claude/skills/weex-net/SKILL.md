---
name: weex-net
description: Build C#/.NET Weex spot and futures integrations with Weex.Net, including market data, authenticated accounts, orders, conditional orders, TP/SL, WebSocket streams, and Shared API V2 capabilities.
---

# Weex.Net

```shell
dotnet add package Weex.Net
```

Use `WeexRestClient` and `WeexSocketClient` from `Weex.Net.Clients`. REST roots are `SpotApi` and `FuturesApi`, each with `ExchangeData`, `Account`, and `Trading`; socket roots are `SpotApi` and `FuturesApi`. Native symbols commonly use compact `ETHUSDT` formatting. Spot ticker reads use `GetTickersAsync`, not a native singular `GetTickerAsync`.

Public market data needs no credentials. Private calls use `WeexCredentials(key, secret, passphrase)` from `Weex.Net` on the underlying client options. Built-in environments expose `WeexEnvironment.Live`; custom endpoints do not imply a supported testnet.

Check `Success` before using `Data`. REST returns `HttpResult<T>`/`HttpResult`, subscriptions return `WebSocketResult<UpdateSubscription>`, and some shared helpers return `ExchangeCallResult<T>`. Keep clients alive for their workload, retain successful subscriptions, and unsubscribe before disposing the owning socket client. Respect DI ownership for container-managed clients.

Use `OrderType` for spot and regular futures orders; conditional futures placement uses `FuturesOrderType`. Account-changing calls require explicit workflow decisions about quantity, position side, margin mode, and execution. Fund-history APIs do not imply fund-movement support.

For portable code, use narrow Shared API V2 capabilities through `.SharedApi`; `.SharedClient` is V1 compatibility. `IWeexSharedApiClient` exposes `SpotRest`, `FuturesRest`, `SpotSocket`, and `FuturesSocket`. Dynamic lookups can return `null`; inspect capability options and trading modes. Native and shared models are separate. No other skill is required.

## References

- [API surfaces](references/api-surfaces.md): native method routing, actual V2 capabilities, dynamic selection, symbol catalogs, quantities, and factories.
- [Usage](references/usage.md): public setup, credentials, account reads, defined trading functions, subscriptions, Shared API, and DI.
- [Operational considerations](references/safety.md): order outcomes, partial results, cancellation, position changes, and retries.

Checkout-only: [repository development](../../../AGENTS.md) and [complete examples](../../../Examples/README.md). The references above remain usable in the packaged skill.
