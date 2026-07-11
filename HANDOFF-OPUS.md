# Applebee Acres — handoff guide (written by Fable, 2026-07-10)

## ✅ Clipping audit DONE (2026-07-10) — remaining low-priority items
The 3-agent audit (40 findings) was applied; mower removed (resolved ~6 findings). Smoke + visual OK.
LEFT UNDONE (all rare events — do if Austin reports them or wants full polish; each is a clean
draw-order move unless noted):
- Impact crater draws after field machines → move crater draw BEFORE the field-cell loop (asteroid gag only).
- Crop duster (18% summer days) draws before hills/town → move after the hill/town block (or raise flight line above y92).
- Fireworks (Jul4/NYE) draw before hills → move fw draw after hill/town block.
- Chickens field-strolling in front of row-0 (y>134) get crops drawn over them → split chicken draw around the r=0 field iteration.
- Row 3: sprayer-vs-fire now gated; cruise-vs-anything still unguarded (sim-mode only, very rare).

## ✅ Vehicle-collision cleanup DONE (2026-07-10, Opus 4.8) — 24 audited findings → ~10 fixes, all shipped
See project memory "VEHICLE-COLLISION CLEANUP" for the full list. Resolved from the list above:
svcDrop-into-docked-semi (gated !partsT), VFW door-walk-into-slot-2-truck (walkers now a foreground pass),
VFW sleeper eastbound same-lane (sleepers depart pre-dawn before the crew sweep). Plus: VFW arrival-order slots
(no drive-through), bike/truck speed equalized (no overtake-clip), empCar spot spacing, visitor own-side approach,
dealer dock gates, grain-cart right-pass-only, fire-truck to foreground. Verified via `scratchpad/collision-check.js`
(a `__A` state-hook harness — reusable) + smoke + browser. Skipped 2 live-mode-impossible 1px sim-only grazes.

## (historical) IN FLIGHT — clipping audit (now resolved above)
A 3-agent clipping/collision audit workflow is running (task w3pzm4uvq, run wf_2f243e25-990).
Results: the task notification, or read
`~/.claude/projects/-Users-lucid-Creative/ce61226c-cbbc-47b2-ae43-7a7396c77cb5/subagents/workflows/wf_2f243e25-990/journal.jsonl`
(one JSON line per agent with full findings: domains people/vehicles/layers, each finding has
line + summary + geometry + severity + fix sketch). TO DO: verify each finding against the actual
code (agents cite line numbers), fix the confirmed ones, then the full ship loop below.
ONE FINDING ALREADY CONFIRMED by Fable: Boone's nap spot (dogPlan, enter:BARNX+38, tx:BARNX+40)
sits INSIDE the parked Hustler's footprint (mower parked at HX-8 spans ≈71-80; nap at 72-74) —
the dog naps "inside" the mower. Fix: move nap to the barn-door apron, e.g. enter/tx BARNX+12..14
(clear of bales 36-42, tractor 52-69, door traffic is mornings only). Austin's bar for this pass:
"no characters ever clip/collide/get absorbed unless purposeful — professional."

Read the project memory (`project-applebee-acres`) first — it has the full history. This file is
the working checklist for whoever (Opus) touches `applebee-acres.html` next.

## Ship loop (never skip a step)
1. Edit `applebee-acres.html`.
2. `cd ~/Creative/tests && node smoke.js` → must end `OK`, exit 0. Use `set -o pipefail` in chains.
3. `./screensaver/build.sh` (macOS) and `./windows-screensaver/build.sh` (Windows).
4. `cp windows-screensaver/ApplebeeAcres.scr windows-screensaver/usb-kit/` then rebuild
   `ApplebeeAcres-USB.zip` (`zip -r -X ApplebeeAcres-USB.zip usb-kit`).
5. Redeploy artifact: strip the 7 wrapper lines (doctype/html/head//head/body//body//html) with sed,
   publish to https://claude.ai/code/artifact/9cec4d2d-4717-4a1d-88be-0fb40e6ec2d1 (same URL).
6. Update the project memory.

## House rules (violations WILL be visible on a 24/7 display)
- **Timing**: every timed event = windowed (`tod>X&&tod<X+w`) + expired at end of onNewDay + fed
  through the `spread()` pass (which also catches just-missed events — keep it that way).
- **No `Math.random()` in draw paths** — `rt`, `tod`, or position hashes (`(x*13)%3`) only.
- **Placement**: folk first-ever placement falls back to tx/mid(pa,pb) via `f.placed` — never remove;
  `sit` engages only on arrival (|tx−x|≤1.6). New scene-entities: null them in onNewDay,
  synthSeason, AND jumpSeason.
- **Depth/draw order**: things lower on screen (larger y) draw later. Lot inventory tractors draw
  AFTER folk (walkers pass behind). Parked Hustler draws BEFORE traffic.
- **16:9 horizon slots** (W=384, the store standard — always check collisions at LX=1):
  town core ~109-141 · open ~209-246 · fair ~258-283 · open ~303-330. Everything else is occupied.
- **Austin's taste calls (do not undo)**: no hillside text art; no breath puffs ("smoking");
  no manual content feeds; rainbows die at `dl<0.35`; tractor parks at `BARNX+27`;
  mower/picnic/snowman live on the lawn (`HX-14..HX+30`); saver hosts hide #hint, keep clock HUD.

## Current state
All family/dealership wave features shipped, reviewed (3-lens workflow), placement-fixed, and
deployed. Smoke green. Windows `.scr` still untested on real hardware — Austin runs
`usb-kit/install.bat` on his PC, then GPO per `GROUP-POLICY-DEPLOY.md`.

## Done since this guide was written (Fable, same day)
- ✅ Soak rerun post-wave: SOAK OK, heap flat, 2.8MB growth over 12 sim-years — GPO gate green.
- ✅ kid2 solo beat: dandelion moment (`k2At` roll, 18% of spring/summer days, tod ~0.66-0.71;
  she crouches on the lawn at HX-6, white seed puffs drift downwind via the smoke system).
  Full window+expiry+spread pattern — use it as the template for future micro-beats.
- ✅ Semis-over-walkers checked: already depth-correct (y126 semis occlude y124 walkers). No change.

## RESOLVED — macOS grey screen, the REAL fix (2026-07-10, proven by probe)
WebKit occlusion tracking suspends pages in saver-level windows: page commits fine but rAF never
fires and the layer tree detaches → grey. Fix (KEEP FOREVER): in AcresSaver.swift, immediately
after creating the WKWebView, call private selector `_setWindowOcclusionDetectionEnabled:` = false
(unsafeBitCast pattern, in the source). Proven: probe's hud went from empty to live clock+weather.
If a future macOS drops that selector (the `responds(to:)` guard just skips it), the symptom
returns as grey — the fallback is Apple adding a public API, or kiosk-browser shipping.
Diagnostic pattern that solved it (reuse for any "webview shows nothing"): standalone swiftc probe
with WKNavigationDelegate → log file + delayed evaluateJavaScript state dump (readyState, bodyBG,
element presence, a JS-loop-driven value like hud text to detect suspension).

## RESOLVED — macOS "black screen", first layer (2026-07-10)
Root cause: NO saver module was configured — Austin had only *previewed* in System Settings, never
selected, and `com.apple.screensaver` ByHost had no moduleDict, so `open -a ScreenSaverEngine` ran
an empty engine (renders black). Our saver never launched in the failing tests (logs were silent).
Fixed by `defaults -currentHost write com.apple.screensaver moduleDict -dict moduleName -string
"ApplebeeAcres" path -string "$HOME/Library/Screen Savers/ApplebeeAcres.saver" type -int 0` +
`killall cfprefsd`. Verified live: legacyScreenSaver hosting the bundle with a fresh WebKit.GPU
process bound. NOTE: Austin should still click-select Applebee Acres in System Settings so the
macOS-26 wallpaper-agent idle path uses it too. The same-day Swift hardening (webview attached in
`viewDidMoveToWindow`, `loadFileURL` instead of remote-baseURL `loadHTMLString`, hints hidden on
`file:`) is kept — it's the robust pattern regardless. Settings preview thumbnails may still show
black for WebView savers; that's cosmetic, ignore it.

## Nice-to-haves (unprioritized; nothing is broken)
- Town buildings with dx < −15 hide behind the farmhouse at 16:9 until wide screens; if Austin ever
  says the town looks sparse, bias later TGROW buildings east instead of moving the anchor again.
- macOS Sonoma+ legacyScreenSaver can cache old .saver bundles — if Austin's Mac shows a stale
  build, `killall legacyScreenSaver` or reboot.
- Austin still needs to test `usb-kit/install.bat` on one real Windows PC before GPO rollout.
