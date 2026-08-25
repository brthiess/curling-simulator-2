# Rewrite the app in Vue and TypeScript

The public app is a client-only GitHub Pages toy. The UI we want is Vue, not an upgraded Blazor 3.2 project. The simulation engine ports to TypeScript rather than wrapping C# WASM, so the whole app is one stack.

**Considered options:** stay on Blazor and upgrade .NET; Vue UI calling a C# WASM engine. Rejected: the first fights the UI preference; the second is two toolchains for a small engine.
