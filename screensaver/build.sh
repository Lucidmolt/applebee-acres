#!/bin/bash
# Build + install the Applebee Acres screensaver.
# Rerun this after editing ../applebee-acres.html to refresh the saver.
set -euo pipefail
cd "$(dirname "$0")"

NAME=ApplebeeAcres
SAVER="$NAME.saver"

rm -rf build "$SAVER"
mkdir -p build "$SAVER/Contents/MacOS" "$SAVER/Contents/Resources"

for arch in arm64 x86_64; do
  swiftc -O -swift-version 5 -parse-as-library -module-name "$NAME" \
    -target "$arch-apple-macos12.0" \
    -emit-library -o "build/$NAME-$arch" \
    AcresSaver.swift \
    -framework ScreenSaver -framework WebKit -framework AppKit
done
lipo -create "build/$NAME-arm64" "build/$NAME-x86_64" -output "$SAVER/Contents/MacOS/$NAME"

cp Info.plist "$SAVER/Contents/Info.plist"
cp ../applebee-acres.html "$SAVER/Contents/Resources/applebee-acres.html"
codesign --force --deep --sign - "$SAVER"

mkdir -p "$HOME/Library/Screen Savers"
rm -rf "${HOME}/Library/Screen Savers/$SAVER"
cp -R "$SAVER" "$HOME/Library/Screen Savers/"

echo "Built $SAVER ($(lipo -archs "$SAVER/Contents/MacOS/$NAME"))"
echo "Installed to ~/Library/Screen Savers/$SAVER"
