# Operational considerations

## Account and environment

Private REST and socket calls require the key, secret, and passphrase for the intended account and permissions. Store credentials outside source and logs. Multi-user client providers must select the intended user's credentials rather than sharing account state accidentally. The supplied live environment is production; custom URLs are not evidence of sandbox support.

## Order execution and positions

Spot and regular futures orders use `OrderType`; conditional futures orders use `FuturesOrderType`. Price/quantity increments, min/max values, available balance, and notional constraints must come from the relevant market. A rounded price or a price below a recent ticker is not inherently safe: a limit order can execute immediately.

Futures leverage, margin mode, isolated-margin adjustments, auto-append margin, TP/SL, and close-position methods alter live exposure or account configuration. Confirm position side, current configuration, quantity units, reduce-only behavior, and trigger price reference before constructing a request. A successful leverage change followed by a failed order is a partial workflow, not a transaction with rollback. `ClosePositionsAsync` can affect multiple positions; shared full-position closing is not a partial close.

Cancellation races with fills. A cancellation acknowledgement does not prove that no execution occurred. Cancel-all operations affect orders beyond a single submitted example; reconcile orders, fills, and positions explicitly.

## Partial and ambiguous outcomes

A successful outer result is not proof the operation or every batch item succeeded. `WeexFuturesOrderResult` has its own `Success`, `ErrorCode`, and `ErrorMessage`; inspect these even for a single order. `WeexPositionResult` exposes `Success` and `ErrorMessage`, and spot `WeexCancelResult` exposes `Status` and `ErrorMessage`. Inspect per-item status/error fields for batch cancel, TP/SL, and close-position results where supplied. Preserve successful items when reporting partial failures.

Timeouts, disconnects, or cancellation after submission can leave an account-changing request accepted remotely. Query order/account state and use supported client order identifiers for correlation before repeating it. An identifier is not a universal idempotency guarantee. `Error.IsTransient` can inform bounded public-read retries; it does not make mutation retries safe.

## Histories and streams

Account bills, funding bills, transfer history, deposit history, and withdrawal history are reads; they do not authorize or implement moving funds. Do not invent transfer initiation, withdrawal destinations, or deposit-address methods.

Private streams contain account data. Avoid logging it indiscriminately, keep callbacks short, and handle interruption/reconnection without assuming cached positions or balances remain current. Keep REST reconciliation available when a missed update matters. Stop subscriptions before disposing their owning client.
