# Applebee Acres - live desktop wallpaper setup (via Lively Wallpaper)
# Puts the SAME farm the screensaver shows onto the desktop as an animated background.
# Safe to re-run: it refreshes the farm to the latest build and re-applies.
#
# What it does, in order:
#   1. Installs Lively Wallpaper (winget) if it isn't already present.
#   2. Builds a small local Lively web wallpaper under %LOCALAPPDATA%\ApplebeeAcres\Wallpaper
#      (a copy of the farm + an index.html that frames it in saver mode = hints hidden).
#   3. Best-effort: applies it automatically through Lively's command utility.
#   4. Falls back to: open Lively + farm URL on the clipboard + one paste to finish.
#
# NOTE: the ?saver=1 mode hides the key-hint AND arms the "Keating catches fire when the
# internet's down" effect, matching the store screensaver (Austin's call). To run a machine
# without the fire, ask for a ?wallpaper (no-fire) build - it's a one-line source change.

$ErrorActionPreference = 'Continue'
$Url   = 'https://lucidmolt.github.io/applebee-acres/?saver=1'   # live page, saver mode
$Pages = 'https://lucidmolt.github.io/applebee-acres/'           # raw HTML to cache locally
$Pkg   = 'rocksdanister.LivelyWallpaper'
$WpDir = Join-Path $env:LOCALAPPDATA 'ApplebeeAcres\Wallpaper'

function Write-NoBom([string]$Path, [string]$Text) {
  [System.IO.File]::WriteAllText($Path, $Text, (New-Object System.Text.UTF8Encoding($false)))
}

Write-Host ''
Write-Host 'Applebee Acres - live desktop wallpaper (Lively) setup' -ForegroundColor Green
Write-Host ''

# --- 1. Ensure Lively Wallpaper is installed ---------------------------------
# NOTE: --accept-source-agreements is REQUIRED here. On a machine where winget has
# never run, a bare `winget list` stops to ask for source-terms acceptance — and with
# the output piped, that prompt is INVISIBLE and the script hangs forever (hit on
# Austin's PC, 2026-07-13).
Write-Host 'Checking for Lively Wallpaper... (winget can take a minute on first run)'
$installed = $false
try { if (winget list --id $Pkg -e --accept-source-agreements 2>$null | Select-String -SimpleMatch $Pkg) { $installed = $true } } catch {}
if ($installed) {
  Write-Host 'Lively Wallpaper already installed.'
} else {
  Write-Host 'Installing Lively Wallpaper (winget)...'
  winget install -e --id $Pkg --accept-package-agreements --accept-source-agreements --silent
}

# --- 2. Build the local wallpaper --------------------------------------------
New-Item -ItemType Directory -Force -Path $WpDir | Out-Null

# Cache the current farm locally so the wallpaper keeps running through later
# network blips - the same self-contained approach the screensaver uses. (If this
# machine can reach winget it has internet now, so the fetch normally succeeds.)
$farm = Join-Path $WpDir 'applebee-acres.html'
try {
  Invoke-WebRequest -Uri $Pages -OutFile $farm -UseBasicParsing -TimeoutSec 25
  Write-Host 'Cached the latest farm locally.'
} catch {
  Write-Host 'Could not download the farm (offline?) - the wallpaper will load it live instead.' -ForegroundColor Yellow
}

# index.html redirects to the farm with ?saver=1 so it loads TOP-LEVEL (fills the
# screen) with hints hidden. Point at the local copy when present (offline-tolerant),
# otherwise the live page.
$src = if (Test-Path $farm) { 'applebee-acres.html?saver=1' } else { $Url }
$index = @"
<!doctype html><meta charset="utf-8"><title>Applebee Acres</title>
<style>html,body{margin:0;height:100%;background:#0a0c16}</style>
<script>location.replace("$src")</script>
<noscript><meta http-equiv="refresh" content="0; url=$src"></noscript>
"@
Write-NoBom (Join-Path $WpDir 'index.html') $index

# LivelyInfo.json - Type 1 is a local web wallpaper (schema verified against
# rocksdanister's own Lively wallpapers: Type 1 + FileName index.html + IsAbsolutePath false).
$info = [ordered]@{
  AppVersion = '2.0.0.0'
  Title      = 'Applebee Acres'
  Desc       = 'Living pixel-art farm for Keating Tractor'
  Author     = 'Keating Tractor'
  License    = ''
  Contact    = ''
  Type       = 1
  FileName   = 'index.html'
  Arguments  = ''
  IsAbsolutePath = $false
}
Write-NoBom (Join-Path $WpDir 'LivelyInfo.json') ($info | ConvertTo-Json)
Write-Host "Wallpaper ready: $WpDir"

# --- 3. Best-effort auto-apply via Lively's command utility ------------------
# On the Microsoft Store / MSIX build the CLI often isn't reachable from a script;
# that's fine - step 4 always gives a reliable one-paste finish.
$applied = $false
$exes = @()
$pkg = Get-AppxPackage $Pkg -ErrorAction SilentlyContinue
if ($pkg) {
  $exes += (Join-Path $pkg.InstallLocation 'Livelycu.exe')
  $exes += (Join-Path $pkg.InstallLocation 'Lively.exe')
}
foreach ($n in 'Livelycu.exe','Lively.exe') {
  $c = Get-Command $n -ErrorAction SilentlyContinue
  if ($c) { $exes += $c.Source }
}
foreach ($exe in ($exes | Select-Object -Unique)) {
  if ((-not $applied) -and (Test-Path $exe)) {
    try {
      & $exe setwp --file $WpDir 2>$null
      if ($LASTEXITCODE -eq 0) { $applied = $true }
    } catch {}
  }
}

# --- 4. Open Lively + clipboard fallback + instructions ----------------------
try { Set-Clipboard -Value $Url } catch {}
if ($pkg) { try { Start-Process ("shell:appsFolder\{0}!App" -f $pkg.PackageFamilyName) } catch {} }

Write-Host ''
if ($applied) {
  Write-Host 'Done - the farm is now your desktop background.' -ForegroundColor Green
  Write-Host '(Multiple monitors: in Lively > Settings, choose "span across screens" to match the screensaver.)'
} else {
  Write-Host 'Almost there - one step to finish:' -ForegroundColor Cyan
  Write-Host '  Lively should be opening. The farm URL is on your clipboard, so in Lively'
  Write-Host '  click the "Enter URL" box at the top, press Ctrl+V, then Enter.'
  Write-Host ''
  Write-Host "  (Offline/alternative: in Lively use the '+' > browse to this folder:"
  Write-Host "     $WpDir )"
}
Write-Host ''
