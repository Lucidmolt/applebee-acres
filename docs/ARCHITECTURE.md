# Applebee Acres — Architecture & Operations

Reference for how the farm is built, how its events fire, how it ships, and the
gotchas that cost time. Keep this current when behaviour changes — it exists so we
don't re-derive the same facts every session.

---

## 1. Repository layout

| Path | What it is |
|---|---|
| `applebee-acres.html` | **The whole app** — one self-contained HTML canvas + vanilla-JS file. Source of truth. |
| `index.html` | **Byte-identical copy** of `applebee-acres.html`. This is the GitHub Pages source (the live web version). |
| `screensaver/` | macOS `.saver` — Swift + WKWebView. `build.sh`, `AcresSaver.swift`, `Info.plist`. |
| `windows-screensaver/` | Windows `.scr` — .NET 8 WinForms + WebView2. `build.sh`, `Program.cs`, `AcresSaver.csproj`, plus the `usb-kit/` install kit and GPO deploy guide. |
| `tests/` | `smoke.js` (headless full-year + every-event test) and `soak.js` (12-year heap/soak). |
| `evolve/` | Autonomous evolution loop (launchd, every 5h). Runtime logs + safety `.bak` (all gitignored). |
| `docs/` | This file. |

### The two-HTML-file rule ⚠️
`applebee-acres.html` and `index.html` **must stay byte-identical.** They share the
same committed blob. Edit `applebee-acres.html`, then sync:

```sh
cp applebee-acres.html index.html
```

- `applebee-acres.html` is what the screensaver build scripts embed.
- `index.html` is what GitHub Pages serves.

If they drift, the web version and the screensavers show different farms. Always
`cp` and commit both together.

---

## 2. Runtime model

- The **entire script is one sealed IIFE**: `(()=>{ 'use strict'; … })()`. Nothing is
  exposed on `window` except `window.__acresExit` (the host exit hook). You **cannot**
  inspect or drive internals (`kaiju`, `tod`, `startCowUfo`, …) from outside — see
  [Gotchas](#8-gotchas--hard-won-lessons).
- One `requestAnimationFrame` loop (`frame()` → `update(dt)` → `draw()`) drives
  everything, including the HUD clock (written inside `draw()`).
- **Live mode** (`live=true`, the default): `tod` (time-of-day, 0–1) comes from the real
  system clock via `sync()`; weather is real Liberal KS conditions (Open-Meteo). A new
  day (`onNewDay()`) rolls at real midnight.
- **Sim mode** (`live=false`): time is accelerated (`SPD` multipliers); weather/seasons
  are simulated. Entered by the speed/season keys; `L` returns to live.
- Offline / `file://` / blocked-fetch → weather quietly self-simulates.

---

## 3. The random-event system 🎲

This is the part everyone asks about ("why do I never see X?"). There are **two
independent trigger mechanisms**, and confusing them is the usual source of surprise.

### 3a. Daily-roll events — `onNewDay()`
Once per day, each event rolls a probability for whether it will happen and picks a
time-of-day window. It then fires later that day in `update()` **only if** its window
is current *and* the gating conditions hold. If the window passes unmet, it's cancelled.

Representative rolls (see `onNewDay()`):

| Event | Daily chance | Window | Extra gates when it fires |
|---|---|---|---|
| **Kaiju** (prairie lizard) | **10%** | ~2–5pm | daylight `dl>0.35`, `weather<0.55`, no plane/jet/balloon/hawk/cowUfo |
| **Cow abduction** (`cowUfo`) | 2% | ~10am–2pm | `dl>0.5`, `weather<0.55`, `snowCover<0.6`, a hill cow grazing, no plane/jet/balloon/hawk/kaiju |
| Hawk | 30% | midday | clear sky + weather gate |
| Deer / rabbit / pheasant | 30–40% | dawn/dusk | season-gated |
| Cattle graze | 35% | daytime | late-summer/winter stubble |
| Sale, train, crop-duster, family scenes, … | various | various | see `onNewDay()` |

Because a daily-roll event is **one coin flip per real day** in a **narrow window**, a
rare one (2–10%) is effectively never caught by a human unless they happen to be
watching that afternoon. That is by design, not a bug.

### 3b. Exit-roll events — `acresExit()`
`acresExit()` is the hook the screensaver host calls when the user dismisses the saver
(exposed as `window.__acresExit`). It rolls a small "send you out with a spectacle"
chance **every dismissal**. Because a saver is dismissed many times a day, the same 2%
gets rolled dozens of times daily — so these *feel* frequent even though the per-roll
odds are tiny.

Current `acresExit()` order:
1. **Cow abduction** — ~5% of exits, **daytime only**. A saucer swoops in (close-start
   "near" swoop), beams a hill cow, and is gone; exit is held ~5s. Falls through if it's
   night or no cow is grazing.
2. **Asteroid impact** — ~2% of exits, any time. Goes out with a bang (~3–4s), then exits.
3. Otherwise → immediate exit.

They're mutually exclusive: cow **or** bang, never both.

> **The asymmetry that started all this:** the asteroid *only* exists on the exit roll,
> while kaiju and the abduction were *only* daily rolls. Same ~2% number, but the exit
> roll is exercised dozens of times a day and the daily roll once — so the asteroid was
> visible and the others never were. Fix (2026-07-22): kaiju → 10%/day, and the
> abduction also rides the exit roll.

### 3c. Firing conditions cheat-sheet
Most spawn gates check some combination of: `dl` (daylight 0–1), `weather` (0 clear →
1 storm), `snowCover`, season, and "no conflicting sprite already on screen." A
scheduled event that never meets its gate inside its window is silently dropped.

---

## 4. Demo keys (trigger any event on demand)

Open `applebee-acres.html` (or the live URL) **in a normal browser** and press these.
The HUD hint is hidden on saver hosts; keys work when viewing the page directly, not
while it's the active OS screensaver (any key dismisses a live screensaver).

| Key | Event | Key | Event |
|---|---|---|---|
| `G` | Kaiju / prairie lizard | `u` | Bale-lifting UFO (night) |
| `U` | Cow abduction (`cowUfo`) | `b` | Hot-air balloon |
| `q` | Asteroid impact | `H` | Hawk |
| `f` | Crop fire | `P` | Pheasant |
| `k` | Keating network-down fire | `C` | Cattle herd |
| `e` | Deer | `n` | Train |
| `s` | Lot sale | `m` | Snowman |
| `r` | Rainbow | `c` | Crop duster |
| `a` | Aurora | `v` | Meteor shower |
| `o` | Comet | `y` | Eclipse |
| `w` | Fireworks | `7`/`9` | Duck hunt (interactive / ambient) |
| `1`–`4` | Jump season | `L` | Back to live |
| `-`/`=` | Speed down/up | `h` | Hide HUD |
| `space` | Pause | click | Plant a sunflower |

(Not exhaustive — see the `keydown` handler for the full set.)

---

## 5. Build & deploy

Four ship targets, all built from `applebee-acres.html`.

### Web (GitHub Pages) — the "live" link
- **Live:** https://lucidmolt.github.io/applebee-acres/ — serves `index.html` from repo root.
- Deploy = push `main`. Pages rebuilds in ~1–3 min.
- ⚠️ **Push identity:** the remote is **`Lucidmolt/applebee-acres`**, but this machine's
  git/`gh` defaults to **CorruptFun** (the brain-vault identity) → `403 denied`. Both
  accounts are in `gh` (keyring). Push as Lucidmolt for the single push, then restore
  CorruptFun (the vault sync depends on it being active):
  ```sh
  gh auth switch --user Lucidmolt
  git -c credential.helper= -c credential.helper='!gh auth git-credential' push origin main
  gh auth switch --user CorruptFun
  ```
  Never `gh auth logout` CorruptFun.

### macOS `.saver`
```sh
screensaver/build.sh      # swiftc arm64+x86_64 → lipo → codesign → install to ~/Library/Screen Savers
```
Embeds `../applebee-acres.html` into the bundle's Resources. `Info.plist` carries the
version (`CFBundleShortVersionString` / `CFBundleVersion`) — bump on a release.

### Windows `.scr`
```sh
windows-screensaver/build.sh   # dotnet 8 publish → ApplebeeAcres.scr (self-contained, ~63 MB)
```
The HTML is embedded at build time as a .NET `EmbeddedResource` (`AcresSaver.csproj`),
extracted at runtime to `%LOCALAPPDATA%\ApplebeeAcres\` and served via WebView2 from
`https://acres.local/...` (`Program.cs`). Because it's a compressed resource, you
**can't `grep` the farm HTML out of the `.scr`** — trust the build timestamp.

Two Windows distribution channels (both **manual** off this machine — no automation):
- **GPO share:** copy `windows-screensaver/ApplebeeAcres.scr` to `\\SERVER\deploy$`,
  GPP copies to `C:\Windows\ApplebeeAcres.scr`. See `GROUP-POLICY-DEPLOY.md`.
- **USB kit:** `windows-screensaver/usb-kit/` (the `.scr` + install `.bat`s), zipped to
  `ApplebeeAcres-USB.zip`. **Re-stage after every farm change:**
  ```sh
  cd windows-screensaver
  cp ApplebeeAcres.scr usb-kit/ApplebeeAcres.scr
  rm -f ApplebeeAcres-USB.zip && zip -r ApplebeeAcres-USB.zip usb-kit/
  ```
  (These are gitignored build artifacts — never on `main`. They do **not** auto-refresh
  when you rebuild the root `.scr`.)

### Release checklist
1. Edit `applebee-acres.html`; `cp applebee-acres.html index.html`.
2. `cd tests && node smoke.js` → must print `OK`.
3. `screensaver/build.sh` and `windows-screensaver/build.sh`.
4. Re-stage the Windows USB kit (above) if the USB channel is used.
5. Bump `screensaver/Info.plist` version.
6. Commit `applebee-acres.html`, `index.html`, `Info.plist` together; push `main` (as Lucidmolt).
7. Verify the live URL serves the new build.

---

## 6. Testing

- **Smoke** (`tests/smoke.js`, ~seconds): loads `applebee-acres.html`, runs live + sim,
  fires every event class (prints `cow abduction ran`, `kaiju ran`, `asteroid ran`, …).
  Must exit `0` / print `OK`. This is the gate the evolve loop uses.
- **Soak** (`tests/soak.js`): 12-year run, heap/leak check.

---

## 7. The evolve loop

`evolve/evolve.sh`, launchd `com.applebeeacres.evolve`, every 5h. Makes **one**
budget-capped, smoke-gated change to the farm then stops; rolls back if smoke goes red.
It does **not** `git commit`/`push` itself — changes accumulate for a human/session to
review and ship. (Seen failing auth — "Not logged in · run /login" — when the headless
`claude` has no valid session; it then makes no change.)

---

## 8. Gotchas / hard-won lessons

- **Internals are IIFE-sealed.** `(()=>{…})()` means `kaiju`, `tod`, `startCowUfo`, etc.
  are **not** on `window`. External `javascript_tool` / injected `<script>` / dispatched
  `KeyboardEvent` cannot read or reliably drive them. To inspect/drive live, add a
  temporary hook *inside* the IIFE (before `})();`) that closes over the vars — e.g.
  `window.__acres = { state:()=>({tod,kaiju,…}), showKaiju:()=>{…} }` — on a throwaway
  copy, then delete it.
- **Preview-pane rAF throttle.** In an embedded/preview browser the `requestAnimationFrame`
  loop only ticks when the pane composites (e.g. on screenshot), not continuously. A
  spawned object with a rise/anim (kaiju `up`, cow `abdY`) won't visibly progress between
  headless JS calls even though the code is fine. Force the visible state directly and
  screenshot, or test in a real foreground browser. **This is an environment artifact,
  not a farm bug.**
- **Two-file byte-sync** (§1) — the #1 way to ship a half-update.
- **Push identity** (§5) — Lucidmolt vs CorruptFun 403.
- **No copyrighted characters on the public display.** The "Godzilla" is a *generic
  prairie lizard* on purpose (it's a business display). Keep it generic.
- **Calm & self-contained is a standing rule.** No live AI / external data feeds beyond
  the weather API; deterministic; single file, no deps.
