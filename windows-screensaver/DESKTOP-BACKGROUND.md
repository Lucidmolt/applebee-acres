# Applebee Acres — animated desktop wallpaper (Windows)

Run the exact same living farm as an **animated desktop background**, not just a screensaver,
using **Lively Wallpaper** (free, open-source). It points at the same HTML the screensaver uses,
so it syncs to the real clock, date, season, sun/moon, and live Liberal, KS weather — the desktop
and the idle screensaver show the same farm at the same time of day.

- **Wallpaper (this doc):** shows while someone is working, as the desktop background.
- **Screensaver (`install.bat` / GPO):** takes over when the machine goes idle.

Both read the same source, so they match automatically. See the bottom section for the fine print
on how "the same" they are.

---

## Fastest: the `install-wallpaper.bat` script

On the USB kit there's now an **`install-wallpaper.bat`** — double-click it and it does the whole
thing: installs Lively (via winget) and applies the farm. If Lively's command line isn't reachable
on that PC (common on the Microsoft Store build), it finishes by opening Lively with the farm URL
already on your clipboard — click the URL box, **Ctrl+V**, Enter. Re-run it anytime to refresh the
farm to the latest build. The sections below are the manual equivalent + the detail behind it.

## 1. Install Lively Wallpaper

Pick either:

- **Microsoft Store** (easiest, auto-updates): search **"Lively Wallpaper"** → Get. Free.
- **GitHub** (offline installer / for locked-down machines): https://github.com/rocksdanister/lively
  → Releases → run the installer.

Lively renders HTML/web wallpapers, supports multiple monitors, and has a command-line utility
(`Livelycu.exe`) for scripting a fleet — which is why we use it.

## 2. Add the farm (per machine, quickest)

1. Open **Lively Wallpaper**.
2. At the top of the library there's an **"Enter URL or drag & drop"** bar (the `+` tile).
   Paste this and press Enter:

   ```
   https://lucidmolt.github.io/applebee-acres/?saver=1
   ```

3. Lively creates a web wallpaper and applies it to the desktop. Done.

**Why this URL:**
- The **GitHub Pages** address is served over **https**, so the Open-Meteo weather fetch works and
  you get **live** Liberal, KS weather (the phone artifact can't — its feed is CSP-blocked).
- **`?saver=1`** hides the on-screen key-hint help so it's a clean backdrop (the clock/weather HUD
  stays). It also arms the network-down effect — see §5.

**Offline / no-internet machines:** point Lively at a **local copy** of the HTML instead:

```
C:\ProgramData\ApplebeeAcres\applebee-acres.html?saver=1
```

(Copy `applebee-acres.html` there first, or reuse the screensaver's unpacked copy under
`%LOCALAPPDATA%\ApplebeeAcres\`.) Weather falls back to a realistic simulation when offline; the
clock/season are still real. Use the https URL wherever there's internet — it's the better look.

## 3. Multiple monitors

In Lively's **Settings → Wallpaper → "Wallpaper arrangement"**:

- **"Span across screens"** → one wide farm stretched across all monitors (matches how the
  screensaver spans). The world is aspect-adaptive, so a wider span shows **more prairie**, not a
  stretched image.
- **"Same wallpaper on each"** or **per-monitor** → a full farm on every display.

Match whatever the store already runs on the screensaver so the two look consistent.

## 4. Hide desktop icons (optional, for a clean display look)

Right-click the desktop → **View → uncheck "Show desktop icons."** (Re-check to bring them back.)
Good for a lobby/showroom machine that's mostly a display; skip it on a machine people actually
work on.

## 5. Heads-up: the "Keating catches fire" effect is ON

Because we use **`?saver=1`**, the wallpaper behaves exactly like the store screensaver: if the
machine's **internet goes fully down**, the Keating dealership building visibly **catches fire**
(smoke plume, flames, a `⚠ offline` HUD tag), and it **clears the moment the connection is back**.
Nothing is damaged — it's a self-contained gag that only triggers on a real, sustained outage
(~1 min of all-hosts-unreachable), never on a single blocked feed.

This is intentional — Austin chose to keep it the same as the screensaver (2026-07-11). If a
specific machine (e.g. a manager's work desktop) shouldn't ever do this, tell me and I'll add a
`?wallpaper` mode that hides the hint **without** arming the fire — it's a one-line change, just not
built yet.

## 6. Fleet automation for the ~30 store machines (later)

Lively ships a command-line utility, **`Livelycu.exe`** (the "Lively Command Utility"), in its
install directory. It applies wallpapers **by file/folder path** and targets monitors — there is
**no documented `--url` flag** in current builds, so the reliable pattern is *"mint the wallpaper
once, then push the folder."*

**One-time setup (on any one machine):**
1. Add the URL via the GUI (§2). Lively saves it as a small library folder here:
   ```
   %LOCALAPPDATA%\Lively Wallpaper\Library\wallpapers\<some-name>\
   ```
   (Just a `LivelyInfo.json` pointing at the URL + a thumbnail — a few KB, fully portable.)
2. Copy that folder; it's what you distribute.

**Per machine (scriptable, e.g. via GPO logon script or your MDM):**
```bat
:: install Lively (Store/MSI) first, then drop the wallpaper folder into the machine's Library, then:
"%ProgramFiles%\WindowsApps\...\Livelycu.exe" setwp --file "C:\ProgramData\AcresWallpaper" --monitor -1
```
- `--monitor -1` applies across **all** screens; a specific index (0, 1, …) targets one.
- Find the exact `Livelycu.exe` path once (Store installs live under `WindowsApps`; the GitHub
  installer uses a normal Program Files path) and hard-code it in the script.

**Other useful commands:**
```bat
Livelycu.exe --play false          :: pause all wallpapers (e.g. to save GPU during a demo)
Livelycu.exe --play true           :: resume
Livelycu.exe closewp --monitor -1  :: remove all wallpapers
```

**Distribution:** reuse the same approach as the screensaver's `GROUP-POLICY-DEPLOY.md` — stage
Lively + the wallpaper folder on a share, push via GPP file copy, and run the `setwp` line once at
logon. **Test the whole chain on one machine before the fleet** (confirm the `Livelycu.exe` path and
that `setwp --file` on a URL-backed folder applies cleanly).

---

## How "the same" the wallpaper and screensaver are

Both are the **same HTML** reading the same real clock/date/live weather, so **time-of-day, season,
sun/moon position, and weather already match** between the desktop wallpaper and the idle
screensaver — no configuration needed.

The **only** difference: each running instance keeps its **own random schedule of rare "strange
sky" sights** (comet, aurora, meteor shower, eclipse, blood moon). So on a given night the wallpaper
might show a comet while the screensaver wouldn't, or a different store's machine shows something
yours didn't — that per-machine surprise is by design (Austin chose to leave it as-is, 2026-07-11).
Everything you actually see minute-to-minute is identical.

Frame-perfect animation sync between the two (the wallpaper and the screensaver being on the exact
same frame) isn't practical — they're separate processes/render loops — and isn't needed for a
display. If Austin ever wants the rare sights to match too, that's a small change behind the
existing seed system (bake one shared seed); just ask.
