# Examples

| Project | Purpose |
| --- | --- |
| [API](Weex.Examples.Api/Weex.Examples.Api.csproj) | Inject clients into a web API |
| [Console](Weex.Examples.Console/Weex.Examples.Console.csproj) | Basic client use |
| [Order book](Weex.Examples.OrderBook/Weex.Examples.OrderBook.csproj) | Maintained local book |
| [Tracker](Weex.Examples.Tracker/Weex.Examples.Tracker.csproj) | Trade tracker |
| [V3 order placement](Weex.Examples.V3OrderPlacement/Weex.Examples.V3OrderPlacement.csproj) | Spot and futures order submissions |
| [Standalone programs](ai-friendly/README.md) | Focused REST, WebSocket, V2, and error examples |

Compile with `dotnet build Examples/Examples.sln` from the repository root. Builds do not execute exchange calls. Running examples can contact live markets or change account state when credentials are supplied.

See the [consumer skill](../.claude/skills/weex-net/SKILL.md) and [repository instructions](../AGENTS.md).
