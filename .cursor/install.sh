#!/usr/bin/env bash
# Idempotent Cloud Agent setup for the curling-simulator-2 Blazor WebAssembly app.
# The project is Blazor WASM 3.2.1 (netstandard2.1) and is built with the
# .NET Core 3.1 SDK, matching .github/workflows/main.yml.
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
DOTNET_DIR="$HOME/.dotnet"

# 1. Native dependencies .NET Core 3.1 needs on Ubuntu 24.04.
#    - libssl1.1 (OpenSSL 1.1) is not in noble; pull it from the security pool.
#    - libicu is required for globalization support.
if ! dpkg -s libssl1.1 >/dev/null 2>&1; then
  ssl_deb="$(mktemp --suffix=.deb)"
  curl -fsSL -o "$ssl_deb" \
    http://security.ubuntu.com/ubuntu/pool/main/o/openssl/libssl1.1_1.1.1f-1ubuntu2.24_amd64.deb
  sudo dpkg -i "$ssl_deb"
  rm -f "$ssl_deb"
fi

if ! dpkg -s libicu74 >/dev/null 2>&1; then
  sudo apt-get update -qq
  sudo apt-get install -y -qq libicu74
fi

# 2. .NET Core SDK 3.1 (installed once under $HOME/.dotnet).
if [ ! -x "$DOTNET_DIR/dotnet" ]; then
  curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
  chmod +x /tmp/dotnet-install.sh
  /tmp/dotnet-install.sh --channel 3.1 --install-dir "$DOTNET_DIR"
fi

# 3. Wrapper on PATH so `dotnet` works in any shell. .NET Core 3.1 does not
#    recognise Ubuntu 24.04's ICU 74, so run in invariant-globalization mode.
sudo tee /usr/local/bin/dotnet >/dev/null <<'WRAPPER'
#!/usr/bin/env bash
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_NOLOGO=1
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
exec "$HOME/.dotnet/dotnet" "$@"
WRAPPER
sudo chmod +x /usr/local/bin/dotnet

# 4. Restore + build so the workspace is ready to run.
cd "$REPO_ROOT"
dotnet build curling-simulator-2.csproj -c Debug
