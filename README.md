# Voltage.FarseerPhysics

Rigid-body physics for [Voltage Engine](https://github.com/VoltageEngine) — a Box2D-lineage Farseer port
exposed through the `FS*` component family: `FSRigidBody`, `FSCollisionBox` / `Circle` / `Polygon`, joints,
and `FSWorld`.

Distributed as a Voltage **plugin**. It is not bundled with the editor; install it per project.

## Install

Editor ▸ **Plugin Manager** ▸ **Browse Plugins** ▸ *Farseer Physics* ▸ **Install**.

Or add it to your project's `plugins.json` by hand:

```json
{
  "Id": "voltage.farseer",
  "Source": { "Zip": "https://github.com/VoltageEngine/Voltage.FarseerPhysics/releases/download/v1.0.0/voltage.farseer-1.0.0.zip" }
}
```

A `Git` source works too, but prefer the release zip: this repository is source-only, and a Git source is
resolved by shallow clone without building, so it would require the built DLLs to be committed.

## Building

The plugin binds to a built engine rather than a project reference. Point it at one, in priority order:

```bash
dotnet build -p:VoltageEnginePath=/path/to/Voltage.Engine/bin/Release/net8.0
export VOLTAGE_ENGINE_PATH=/path/to/Voltage.Engine/bin/Release/net8.0   # persistent alternative
```

With no override it looks for a sibling `../VoltageEngine/Voltage.Engine/bin/$(Configuration)/net8.0`
checkout, which is the usual setup when working on both.

Configurations mirror the engine's, and this matters: `Editor-*` defines `EDITOR`, which changes the
engine's public surface. Build a plugin configuration against the matching engine configuration.

## Releasing

```bash
dotnet msbuild Voltage.FarseerPhysics.csproj -t:PackagePlugin
```

Stages `plugin.json` + `lib/` + `editor-lib/`. Zip those three and attach the archive to a tagged release;
`.github/workflows/release.yml` does this automatically when you push a `v*` tag.

Bump `Version` in `plugin.json` to match the tag — the registry pins by content hash, but the manifest
version is what the editor displays and what dependency ranges resolve against.
