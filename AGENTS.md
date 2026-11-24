# Working on Weex.Net

The [Weex.Net skill](.claude/skills/weex-net/SKILL.md) owns consumer guidance. [llms.txt](llms.txt) is its discovery index. This file describes repository development.

## Layout and implementation

- [Public client interfaces](Weex.Net/Interfaces/Clients) and [client implementations](Weex.Net/Clients): spot/futures REST, sockets, V1 compatibility, and Shared API V2 adapters.
- [Models and options](Weex.Net/Objects), [enums](Weex.Net/Enums), and [converters](Weex.Net/Converters): wire models, configuration, enum mappings, and JSON source-generation metadata.
- [DI extensions](Weex.Net/ExtensionMethods), [local order books](Weex.Net/SymbolOrderBooks), [tests](Weex.Net.UnitTests), and [examples](Examples/README.md).

Follow neighboring endpoints for request construction, signing, validation, result handling, cancellation, nullable annotations, and XML documentation. Use `ConfigureAwait(false)` on library awaits as required by the analyzer. Update public interfaces with implementations, wire models with serialization metadata, and Shared API adapters with accurate capability options. Add or adjust relevant request/response fixtures when changing endpoint behavior. Preserve native distinctions between spot, regular futures orders, conditional orders, and TP/SL workflows.

Maintain compatibility across the target frameworks in the [project](Weex.Net/Weex.Net.csproj). Do not change unrelated package versions, release settings, or generated output.

## Build and validation

Use the SDK configured by [CI](.github/workflows/dotnet.yml). Debug normally builds only `netstandard2.0` and `net11.0`; the test project targets `net9.0`, so include all frameworks for solution/test builds:

```powershell
dotnet restore Weex.Net.sln -p:BuildAllFrameworks=true
dotnet build Weex.Net.sln --no-restore -p:BuildAllFrameworks=true
dotnet test Weex.Net.UnitTests/Weex.Net.UnitTests.csproj --no-build -p:BuildAllFrameworks=true --filter "FullyQualifiedName~AiExampleCompileTests"
```

The documentation test compiles standalone examples as console programs without running their exchange operations. Run relevant fixture tests for implementation changes. Live REST/socket integration tests have their own `Run` gates; shared REST integration tests use `ManualRun` or `INTEGRATION=1`. Do not enable them for documentation validation.

Validate all distributable frameworks and package contents:

```powershell
dotnet build Weex.Net/Weex.Net.csproj -c Release -p:BuildAllFrameworks=true
dotnet pack Weex.Net/Weex.Net.csproj -c Release --no-build
dotnet build Examples/Examples.sln
```

Compile changed skill snippets with the local project. Building example projects does not execute their requests; running trading examples can change live account state.

## Documentation ownership

Keep API routing in `.claude/skills/weex-net/references/api-surfaces.md`, focused consumer snippets in `references/usage.md`, and distinct execution considerations in `references/safety.md`. `SKILL.md` routes to those references, `CLAUDE.md` imports this file, and `llms.txt` is a short link index.

Pack `.claude/skills/**` under `skills/` without appending `%(RecursiveDir)`. Inspect the NuGet archive for exactly five skill files under `skills/weex-net/`, matching source bytes. Validate relative links and heading anchors. Label repository-source/example links inside the skill as checkout-only so the packaged skill remains usable independently.
