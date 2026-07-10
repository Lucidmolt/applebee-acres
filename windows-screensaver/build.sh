#!/bin/bash
# Build ApplebeeAcres.scr for Windows (x64) from macOS.
# Rerun after editing ../applebee-acres.html, then re-stage the .scr on the deploy share.
set -euo pipefail
cd "$(dirname "$0")"

export DOTNET_CLI_TELEMETRY_OPTOUT=1
DOTNET="$HOME/.dotnet/dotnet"
[ -x "$DOTNET" ] || DOTNET=dotnet

rm -rf publish
"$DOTNET" publish AcresSaver.csproj -c Release -o publish
cp publish/ApplebeeAcres.exe ApplebeeAcres.scr

echo
ls -lh ApplebeeAcres.scr
echo "Done: windows-screensaver/ApplebeeAcres.scr"
