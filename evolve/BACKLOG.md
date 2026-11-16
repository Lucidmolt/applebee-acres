# Applebee Acres — Evolve Backlog & Changelog

This file is read by the autonomous **evolve** run (launchd, every 5h). It is the memory between
runs: the operating rules, the idea backlog to draw from, and the log of what's already been done.

## Operating rules (the evolve agent MUST follow these)
- **One change per run.** Small, self-contained, in-aesthetic. Prefer subtle polish over loud features.
  When in doubt, do less, better. This is a calm dealership screensaver — subtlety is the whole point.
- **Read first:** the project memory (`project-applebee-acres.md`) for full history + Austin's taste
  rulings + code conventions, and this file's Changelog so you never repeat or undo a prior run.
- **Conventions (from memory):** timing = window+expiry+spread (never bare `tod>X`); NO `Math.random()`
  in draw code (use `rt`/`tod`/position hashes); shade with `S()`/`P()`; give movers shadows; respect
  draw order (no clipping); reset every new global in BOTH `synthSeason()` and `jumpSeason()`;
  dealership events gated on Sundays. Honor every "do not re-add" note in memory.
- **Quality gate:** `cd tests && node smoke.js` MUST print `OK`. If red, undo your edit and abort the
  run with no ship. Never leave the source red; never ship red.
- **Ship (only if green):** rebuild both savers, refresh the usb-kit + zip. Do NOT redeploy the public
  artifact (needs an interactive session) and NEVER touch the store machines, GPO, or anything outside
  `~/Creative/applebee-acres` + the memory dir. No destructive commands.
- **Log every run:** append a dated line to the Changelog below, and add a short note to the memory.

## Idea backlog (pick the best fit for the current season/state; add your own)
Seasonal ambient (match the current season):
- Autumn: a V-formation of migrating geese crossing high, honking south (silhouettes, position-hashed).
- Winter: kids build a snowman together over a few minutes; a wreath on the farmhouse/dealership door in Dec.
- Spring: barn swallows darting around the barn eaves at dusk; a robin on the fence.
- Summer: fireflies blinking in the yard after dark; a garden sprinkler ticking on the lawn some mornings.

Aliveness / character (keep rare and subtle):

Polish / realism:
- A touch more shading/detail on an existing sprite that reads flat.
- Smoother transition somewhere that currently pops.
- VFW "small unmarked plaque" (line ~2483) is flat — deliberately left alone twice
  now since it's an intentional easter egg per its own comment; only revisit with a
  very light touch that doesn't imply new markings.
## Changelog
(most recent first — the evolve agent appends here)
- 2026-08-21 — Followed up the explicit runner-up note left by 2026-08-20 #4:
  `drawTruckSm` (applebee-acres.html:2502-2504) had the identical flat-tire
  gap that `drawCar` was just fixed for — two flat `#22242c` tire discs
  (`arc(-3,-2,2.1,...)`/`arc(4,-2,2.1,...)`) with no hub/rim color layering,
  unlike `drawBus` (2-ring), `drawTractorAt`/`drawCombine`/`drawSprayer`
  (3-ring gold hub), and `drawTruck`/`drawSemi`/`drawPickup`/`drawCar`
  (3-ring chrome hub). Verified directly against source (both drawTruckSm
  and the freshly-shipped drawCar fix at 2313-2315) before shipping — no
  agent delegation needed, the prior run had already pinpointed the exact
  lines. Added `g.fillStyle=S('#4a4e58');` plus two hub `arc()` calls at the
  same wheel centers, radius 0.7 (drawTruckSm's tire radius 2.1 is closest to
  drawBus's 2, so reused drawBus's exact hub radius rather than drawCar's
  0.65, which was scaled for a smaller 1.8 tire). Zero new state, no
  Math.random, no timing change. Exercisable now (drawTruckSm renders
  regularly — dealership/staff pickups use it). Smoke OK, both builds green
  (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warning), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (same overdue-review-
  session backlog spanning 07-22 onward).
- 2026-08-20 #4 — With every closed decorative family still closed (see
  earlier entries for the full list), an Explore agent found a fresh,
  previously-unaddressed micro-detail: `drawCar`'s wheels (~line 2310-2312)
  are two flat `#22242c` tire-black discs with no hub/rim color layering,
  while every other wheeled vehicle in the file — `drawBus` (~line 3674-3679,
  tire+hub 2-ring), `drawTractorAt`/`drawCombine`/`drawSprayer` (tire+rim+
  gold hub, 3-ring), `drawTruck`/`drawSemi`/`drawPickup` (tire+rim+chrome
  hub, 3-ring) — marks the wheel center with a second fillStyle. Not the
  same as the closed two-tone/sun-catch family (that's body-panel roof/
  undercarriage shading) or rotating-mechanism (static color layering, not
  motion) — a distinct, never-addressed wheel-hub detail. Verified directly
  against source (drawCar 2307-2326, drawBus 3674-3679) before shipping, not
  just the agent's report. `drawCar` is one of the most-drawn sprites in the
  file (staff cars, famCar, sale customers, visitors, empCars) and already
  carries every other decorative layer (roof highlight, glass glint,
  headlight/glow-beam/taillight) — this was the one remaining flat spot.
  Added, matching the bus's simpler 2-ring treatment (drawCar's tire radius
  1.8 is close to the bus's 2, same visual weight, hub radius scaled to the
  bus's ~0.35 ratio): `g.fillStyle=S('#4a4e58');
  g.beginPath();g.arc(-3,-1.6,0.65,0,TAU);g.fill();
  g.beginPath();g.arc(3.2,-1.6,0.65,0,TAU);g.fill();` right after the two
  tire-black arcs, before the body `P()` call. Zero new state, no
  Math.random, no timing change. Exercisable now (any car, any time of day).
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean aside
  from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (same overdue-review-session backlog). Runner-up noted for next time:
  `drawTruckSm` (~line 2499-2501) has the identical flat-tire gap — same
  fix, not done this run to keep to one change.
- 2026-08-20 #3 — With every closed decorative family still closed, an
  Explore agent found the freight train's own locomotive (inline `if(train)
  {...}` block, applebee-acres.html:3170-3173) was missing the roof-
  highlight/undercarriage-shade two-tone treatment that its own box cars —
  drawn three lines above it in the exact same block (3162-3169) — already
  carry: `shA(mixA(carC,SNOW,0.22),0.5)` on the top row,
  `shA(mixA(carC,NIGHT,0.3),0.72)` on the bottom row. The engine's body fill
  (`HOR-6,12,5`) went straight to a flat `shA(C('#3a4048'),0.6)` fill plus a
  mid-body yellow paint stripe at `HOR-3`, with no highlight/shade rows at
  all — a real asymmetry sitting three lines from its own sibling
  implementation, in the same inline block prior sweeps already touched
  twice (windmill snow-cap, caboose taillight) without ever checking the
  engine itself. Verified directly against source before shipping. Added
  the roof highlight at `HOR-6` (top row) and undercarriage shade at
  `HOR-2` (bottom row) — neither collides with the existing stripe at
  `HOR-3` — using the cars' identical `shA(mixA(...))` idiom, engine's own
  `#3a4048` body color. Zero new state, no Math.random, no timing change.
  Exercisable now via the `n` demo key or natural train spawns. Smoke OK,
  both builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (same
  overdue-review-session backlog).
- 2026-08-20 #2 — With the leg-scissor-gait family already marked closed
  (drawWalker, drawHunter, drawFolk, hillCows, protest picketers), an
  Explore agent found the dealership `sale` event's walking customer
  (inline `if(sale.ph===1){...}` block, applebee-acres.html:3547-3555) was
  never checked — a near-verbatim hand-copy of `drawWalker`'s body plan
  (same leg/torso/head/cap rect layout and colors) that walks several
  seconds toward the parked sale pickup (`px3` genuinely animates via
  `lerp(sale.x-6,DX+58,...)`) but had two fully static leg rects, unlike
  drawWalker (line 2329), drawHunter, drawFolk, and the protest picketers,
  which all scissor their legs with `Math.sin(x*0.9)*0.4` keyed to ground
  covered. Same class of miss as the recurring "inline block hand-copies a
  drawX() sibling but skips the sweep" pattern (bus/fire truck/train/
  windmill/protest picketers/deer eye-pixel). Verified both sides directly
  before shipping (line 3551 vs. drawWalker 2329-2330). Added
  `const lsw3=Math.sin(px3*0.9)*0.4;` and offset both leg fillRects by
  `+lsw3`/`-lsw3`, using the block's own existing `px3`. Zero new state, no
  Math.random, no timing change. Exercisable now — `sale` has no season
  gate, only a daytime/non-Sunday-random spawn check. Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-20 — With the eye-pixels family already marked closed, an Explore
  agent re-checked it directly and found it only covers `drawCow` (line
  2178/2183, dark `#1a1712` eye dropped on both the grazing and alert head
  poses) — the inline `if(deer){...}` block (~line 3492), the closest
  structural sibling (same graze/alert head-drop idiom, comparable render
  scale, barrel 6x2 vs cow's 10*s x 4*s), draws its head block (lines
  3500-3503) and falls straight to the tail-flick color with no eye pixel
  anywhere in the block — confirmed by direct read, not just the agent's
  report. (Also checked rabbit, ~line 3507: no eye either, but its 3x2 body
  is genuinely sub-legible, left alone — same legibility-floor class as
  fence-birds/grille-slats.) Added one line matching drawCow's exact idiom
  and color: `g.fillStyle=S('#1a1712',0.55);g.fillRect(dx3+4,hy2,0.6,0.6);`
  right after the head fillRects, before the tail-flick line — `dx3+4,hy2`
  sits inside the head block in both grazing and alert poses (same
  coordinates, unconditional). Zero new state, no Math.random, no timing
  change (rides existing `dx3`/`hy2`, already in scope). Exercisable now —
  deer are an active daytime/dusk spawn in August. Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session remains genuinely
  overdue).
- 2026-08-19 #4 — With every closed decorative family still closed (shadow/
  glint/two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-
  grading/rotating-mechanism/leg-scissor-gait (drawCow/hillCows/drawWalker/
  drawHunter)/exhaust-puff/wx-easing/light-bloom-easing/eye-pixels/ground-
  wear/fireflies/idle-rest-breathing/dry-ground-vehicle-dust/chore-details/
  tail-flick/moth-swarm/jack-o'-lantern-flicker/chassis-bounce/discrete-
  state-triggered-appendage/seasonal-snow-accretion/hen-pheasant-head-jab/
  drawCar headlight-glow-beam/bale snow-cap/bus+fire-truck night-lights/
  drawSemi roof-highlight/Gus tail-flick/jet night beacon/human-figure cap
  band/train caboose taillight/windmill snow-cap/Boone tail-flick), an
  Explore agent found the picket-line protesters (inline `if(protest)for
  (const p of protest.arr){...}` block, ~line 3707) are a near-verbatim
  hand-copy of `drawWalker`'s body plan (same leg color, same shirt/skin/cap
  rect offsets) but never got the leg-scissor gait `drawWalker` (line 2329)
  has carried for weeks — the protesters' legs were two static planted
  rects even though `p.x` genuinely animates every frame while protest is
  active (update loop, line 1462, eases `p.x` toward a swaying `p.tx+
  Math.sin(rt*0.4+p.ph)*3` target, or off-screen when leaving). Same class
  of miss as prior inline-block gaps (bus/fire-truck/train/windmill) — an
  inline `if(x){...}` block hand-rolled as a near-duplicate of a named
  drawX() sibling, invisible to sweeps that only walk the drawX() cast.
  Verified directly against both sides (protest block 3707-3718, drawWalker
  2327-2334, spawn/update logic 1456-1465, 2410-2419) before shipping. Added
  the identical idiom: `const lsw7=Math.sin(px7*0.9)*0.4;` then offset both
  leg fillRects by `+lsw7`/`-lsw7`, using the block's own existing `px7`
  (=`p.x`) position hash — matches drawWalker's exact comment ("leg scissor
  keyed to ground covered, freezes when wx stops"). Zero new state, no
  Math.random, no timing change (rides existing `p.x`/`rt`, no new global to
  reset in synth/jumpSeason). Exercisable live today: protest is a saga
  event on specific epDay values (`SAGA.protests=[80,152,290]`) or can be
  force-triggered instantly with the `0` demo key. Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session remains genuinely
  overdue).
- 2026-08-19 #3 — With every closed decorative family still closed (shadow/
  glint/two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-
  grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-
  bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-
  ground-vehicle-dust/chore-details/tail-flick (cow/hillCows/deer/rabbit/
  Gus)/moth-swarm/jack-o'-lantern-flicker/chassis-bounce/discrete-state-
  triggered-appendage/seasonal-snow-accretion/hen-pheasant-head-jab/drawCar
  headlight-glow-beam/bale snow-cap/bus+fire-truck night-lights/drawSemi
  roof-highlight/Gus tail-flick/jet night beacon/human-figure cap band/train
  caboose taillight/windmill snow-cap), an Explore agent found the tail-flick
  family had never reached `drawDog` (Boone), a structurally matching
  sibling never checked before — his tail rect (line 434) only animated on
  the discrete `greetT` trigger (a rapid bounce when greeting a family
  member); at all other times, including ordinary trotting (his default
  state most of the day), the tail sat fully static, unlike drawCow/
  hillCows/deer/rabbit/Gus, which all continuously flick via a per-frame
  `Math.sin(rt*k+posX*0.4)*amp` term regardless of state. (Worth noting:
  Boone's separate napping branch, lines ~407-411, already has its own idle
  animation — an occasional dream tail-thump — so only the trotting branch's
  tail was actually static.) Verified directly against source (rabbit
  3515-3516, deer 3503-3504, cat 3660-3661, dog 434 + the `greetT` fields at
  545/1768/1820-1823) before shipping. Added the standard baseline flick,
  layered under the existing greet-bounce so both coexist (greet only
  offsets the y-origin, flick only grows the rect's height):
  `const tfD=Math.sin(rt*3.4+x*0.4)*0.35;` then appended `+tfD` to the tail
  fillRect's height argument. Zero new state, no Math.random, no timing
  change (rides existing `rt`/`x`, already in scope). Exercisable right now
  — Boone trots normally most of the day when not napping/greeting. Smoke
  OK, both builds green (mac .saver + Windows .scr, dotnet clean aside from
  the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-19 #2 — With every closed decorative family still closed (shadow/
  glint/two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-
  grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-
  bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-
  ground-vehicle-dust/chore-details/tail-flick/moth-swarm/jack-o'-lantern-
  flicker/chassis-bounce/discrete-state-triggered-appendage/seasonal-snow-
  accretion/hen-pheasant-head-jab/drawCar headlight-glow-beam/bale snow-cap/
  bus+fire-truck night-lights/drawSemi roof-highlight/Gus tail-flick/jet
  night beacon/human-figure cap band/train caboose taillight), an Explore
  agent found the windmill (inline block, ~line 3317) was the one standalone
  yard structure never swept into the seasonal-snow-accretion family — its
  own shadow comment (line 3318) already flags it as "the one standalone
  ground structure the prop-shadow audit missed (mailbox/sign got theirs,
  this didn't)," i.e. the file already treats it as a sibling of mailbox/
  sign/silo, but that earlier catch-up pass only added the shadow, not the
  matching snow cap every other member of that family carries
  (drawMailbox line 2005, drawSign line 2429, silo line 3307, barn ridge,
  farmhouse ridge). Verified directly against source before shipping.
  Added a 1px snow cap one row above the gearbox housing's top edge
  (`WMX-1,HOR-27,2,2` → cap at `HOR-28`, same width, same "one row above"
  idiom as mailbox/sign rather than the silo's overlapping-top-row variant):
  `if(snowCover>0.25){g.fillStyle=css(SNOW);g.fillRect(WMX-1,HOR-28,2,1);}`
  placed right after the tail-vane line, before the owl block. Zero new
  state (reuses the existing `snowCover` global, already reset in both
  synthSeason() and jumpSeason()), no Math.random, no timing change. Not
  visible today (Aug, no snow) — code-verified against the mailbox/sign
  precedent's exact geometry, same class of verification as the mailbox/
  sign/farmhouse-ridge/bale snow-cap ships. Smoke OK, both builds green
  (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-19 — With every closed decorative family still closed (shadow/glint/
  two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-grading/
  rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-bloom-
  easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-ground-
  vehicle-dust/chore-details/tail-flick/moth-swarm/jack-o'-lantern-flicker/
  chassis-bounce/discrete-state-triggered-appendage/seasonal-snow-accretion/
  hen-pheasant-head-jab/drawCar headlight-glow-beam/bale snow-cap/bus+fire-
  truck night-lights/drawSemi roof-highlight/Gus tail-flick/jet night beacon/
  human-figure cap band), an Explore agent found the freight train (inline
  `if(train){...}` block, ~line 3160) had only two-thirds of the night-
  lighting triplet: a headlight + soft glow-beam at line 3173-3174, but no
  rear (caboose) taillight, unlike every other night-lit vehicle in the file
  that carries the full triplet (drawTruck, drawTruckSm, drawCar, drawPickup,
  drawSemi, bus — bus being the closest structural sibling, also an inline
  block). Confirmed directly against source before shipping. Added the red
  taillight at the trailing edge of the last railcar, reusing the existing
  per-car position formula from the render loop (`train.x-train.dir*
  (14+i*10)` at i=train.n-1) plus the same edge-outset convention drawTruck's
  taillight uses (0.6px beyond the car body): `g.fillStyle='rgba(255,90,80,
  0.7)';g.fillRect(train.x-train.dir*(18.6+(train.n-1)*10),HOR-4,0.6,0.8);`
  right before the block's closing brace, color/size copied verbatim from
  the drawTruck/bus taillight idiom. Zero new state — `train.x`/`train.dir`/
  `train.n` already exist and are already nulled together in both
  synthSeason() and jumpSeason() (lines 748/786); nothing new to reset. No
  Math.random in draw code (rides the existing `dl<0.3` gate). Exercisable
  live today: press the `n` demo key to force-spawn a train, then wait for
  or sim-jump to night (dl<0.3) — genuinely testable now, not a seasonal-
  only fix. Smoke OK, both builds green (mac .saver + Windows .scr, dotnet
  clean aside from the pre-existing WindowsBase version-conflict warnings),
  kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy
  session remains genuinely overdue).
- 2026-08-18 #5 — With every closed decorative family still closed (shadow/
  glint/two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-
  grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-
  bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-
  ground-vehicle-dust/chore-details/tail-flick/moth-swarm/jack-o'-lantern-
  flicker/chassis-bounce/discrete-state-triggered-appendage/seasonal-snow-
  accretion/hen-pheasant-head-jab/drawCar headlight-glow-beam/bale snow-cap/
  bus+fire-truck night-lights/drawSemi roof-highlight/Gus tail-flick/jet
  night beacon), an Explore agent found a fresh family: every human figure
  in the file that has a head also gets a small cap band rendered above it
  — `drawWalker` (line 2332), `drawTractorAt`'s seated rider (1934),
  `drawCombine`'s rider (comment: "same treatment as drawTractorAt"), and
  `drawMower`'s riding=true rider all draw head-then-cap — but `drawBike`'s
  moving-rider block (~line 2485-2486) drew torso + head only, no cap at
  all, despite being the one other human figure in the file with a visible
  head. Confirmed directly against source before shipping. Added one line
  matching `drawWalker`'s simpler single-rect cap idiom (bike's head is
  already the plain/no-brim style): `g.fillStyle=S('#8a2f2a',0.55);
  g.fillRect(x-0.9+wbx,y-8.8,2,1);` right after the head fillRect, reusing
  the same red '#8a2f2a' cap color `drawTractorAt`'s red-rider option
  already uses (no new global/param — drawBike takes no cap-color arg).
  Zero new state, no Math.random, no timing change. Exercisable right now:
  Wade's own bike (`bikeRide`, line 3624, rider always shown while riding)
  and VFW-lot patron bikes (line 3776, shown while arriving/leaving).
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings),
  kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy
  session remains genuinely overdue).
- 2026-08-18 #4 — With every closed decorative family still closed (shadow/
  glint/two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-
  grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-
  bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-
  ground-vehicle-dust/chore-details/tail-flick/moth-swarm/jack-o'-lantern-
  flicker/chassis-bounce/discrete-state-triggered-appendage/seasonal-snow-
  accretion/hen-pheasant-head-jab/drawCar headlight-glow-beam/bale snow-cap/
  bus+fire-truck night-lights/drawSemi roof-highlight/Gus tail-flick), an
  Explore agent pivoted to the sky-traffic sprites (jet/plane/hawk/balloon)
  outside the drawX()-named cast every prior sweep enumerated, and found
  `jet` (inline `if(jet){...}` block, ~line 2978, "jet + contrail") is the
  one background aircraft with no night marker, while its closest structural
  sibling `plane` (~line 3002) already blinks a red anti-collision beacon:
  `if(dl<0.35&&((rt*2|0)%2)){g.fillStyle='#ff5a5a';g.fillRect(px8-3.6,
  py8-1.4,0.7,0.7);}`. Confirmed directly against source before shipping
  (not just the agent's report) — this is a distinct idiom from the closed
  ground-vehicle night-lighting triplet (headlight/beam/taillight), used
  nowhere else but `plane`. Genuinely exercisable: `jet`'s guaranteed-
  liveliness spawner in `nudge()` (line ~256) has no `dl` gate, so a jet can
  cross a fully dark sky with no blink today. Added the same beacon idiom to
  `jet`, repositioned using `jet`'s own fields (nose at `jet.x,jet.y`,
  travel direction `jet.dir`) rather than copying `plane`'s literal offsets:
  `if(dl<0.35&&((rt*2|0)%2)){g.fillStyle='#ff5a5a';g.fillRect((jet.x-jet.dir*3)|0,
  (jet.y-0.5)|0,0.7,0.7);}` right before the block's closing brace. Zero new
  state, nothing to reset in synth/jumpSeason (rides existing `dl`/`rt`/
  `jet` fields), no Math.random, no timing change. Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session remains genuinely
  overdue).
- 2026-08-18 #3 — With every closed decorative family still closed (shadow/
  glint/two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-
  grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-
  bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-
  ground-vehicle-dust/chore-details/tail-flick (cow+hillCows)/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce/discrete-state-triggered-appendage/
  seasonal-snow-accretion/hen-pheasant-head-jab/drawCar headlight-glow-beam/
  bale snow-cap/bus+fire-truck night-lights/drawSemi roof-highlight), an
  Explore agent found the tail-flick family — already closed on drawCow and
  hillCows — never reached Gus, the barn cat perched on the fence rail
  (inline block, comment "Gus on the fence rail," ~line 3645). His tail rect
  (line 3656, `g.fillRect(cx6+(cat.dir>0?-1.5:4.2),cy6-3.4,0.7,1)`) sat fully
  static while the deer (line 3499-3500), rabbit (3511-3512), cow (2185),
  and hillCows (3128-3129) precedents all extend the tail rect's height by a
  `Math.sin(rt*3+<pos>*0.4)*0.4` term. Verified directly against source
  (not just the agent's report) before shipping — same "closed family misses
  a sibling implementation" class as the hillcow/window gaps. Applied the
  exact convention: `const tfc=Math.sin(rt*3+cx6*0.4)*0.4;` then
  `g.fillRect(...,0.7,1+tfc)`, matching the height-based (not offset-based)
  idiom every other tail-flick site uses — corrected the Explore agent's own
  proposed diff, which had suggested an x-offset instead. Zero new state, no
  Math.random, no timing change (rides `rt`, already in scope). Exercisable
  right now — Gus spawns and perches during normal daylight/dusk hours today.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean aside
  from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-18 #2 — With every closed decorative family still closed (shadow/
  glint/two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-
  grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-
  bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-
  ground-vehicle-dust/chore-details/tail-flick/moth-swarm/jack-o'-lantern-
  flicker/chassis-bounce/discrete-state-triggered-appendage/seasonal-snow-
  accretion/hen-pheasant-head-jab/drawCar headlight-glow-beam/bale snow-cap/
  bus+fire-truck night-lights/drawSemi roof-highlight), an Explore agent
  found `hillCows` (~line 3109-3110, the distant background herd) had a
  static two-leg pair that never moved even though the sprite genuinely
  ambles (`c.mv`/`c.d`-driven `c.x` motion in `update()`) — the same class
  of "closed family covers only one of two sibling implementations" miss as
  the earlier hillcow tail-flick fix: `drawCow` (near-field herd, line 2151)
  has had a proper diagonal leg-scissor gait for weeks, but hillCows is a
  separate hand-rolled sprite (its own comment: "the old 3px sprite... below
  the legibility floor," rewritten 2026-07-13) that never got it. Verified
  both sides directly. Added the same position-keyed idiom `drawWalker`/
  `drawHunter` use (`Math.sin(x*k)*amp`, freezes automatically once `x` stops
  moving — no need to gate on the existing `graze` flag separately): `const
  lsw5=Math.sin(x*1.3)*0.4;` then offset the fore/hind leg fillRects by
  `+lsw5`/`-lsw5`. Zero new global state, nothing to reset in synth/
  jumpSeason, no Math.random, no timing change. Exercisable live right now
  — hillCows render whenever `dl>0.4&&snowCover<0.6` (true today, Aug 18,
  Kansas summer), amble phase recurs roughly every 77s of runtime. Smoke OK,
  both builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (same
  overdue-review-session backlog).
- 2026-08-18 — Followed up the explicit runner-up note left by 2026-08-17 #3's
  bus-night-lights run: `drawTruck` (the fire truck, ~line 2598) has the same
  gap the bus had — a daylight glass-glint (`dl>0.5` block, line 2617) whose
  own comment reads "same treatment as drawTruckSm/drawCar", but it never
  paired that glint with the closing `dl<0.3` night headlight+glow-beam+
  taillight block that drawTruckSm/drawCar/drawPickup/drawSemi/drawBike/bus
  all carry — confirmed by direct read, not just the flagged note. Added the
  same three-line idiom, positioned at the cab's front edge (x=10, matching
  `P(4,-11,6,7,...)`'s right edge — the frontmost drawn shape, no separate
  hood ahead of it like drawTruckSm has) and the hose-bed's rear edge
  (x=-10.6, matching `P(-10,-9,14,5,...)`'s left edge, 0.6 outward same as
  drawCar's/bus's edge-inset convention): `if(dl<0.3){g.fillStyle=
  'rgba(255,230,150,0.7)';g.fillRect(10,-5,1,1);g.fillStyle=
  'rgba(255,230,150,0.08)';g.fillRect(11,-6,10,3);g.fillStyle=
  'rgba(255,90,80,0.7)';g.fillRect(-10.6,-5,0.6,0.8);}` placed right before
  `g.restore()`, matching where drawTruckSm/drawCar place theirs. Zero new
  state, nothing to reset in synth/jumpSeason, no Math.random, no timing
  change (rides the existing `dl<0.3` gate). Exercisability is genuinely
  weak — same conclusion 2026-08-17 #3 already reached: `fire` can only
  start while `tod<0.75` and `startFire()`'s own burn/drive-away duration is
  short, so reaching `dl<0.3` (near-dusk/night) would need an unusually late
  ignition — but it's still a real, verified asymmetry against five siblings,
  same class of code-verified-not-live-observed ship as most of this week's
  runs. Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-17 #3 — With every closed decorative family still closed (shadow/
  glint/two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-
  grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-
  bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-
  ground-vehicle-dust/chore-details/tail-flick/moth-swarm/jack-o'-lantern-
  flicker/chassis-bounce/discrete-state-triggered-appendage/seasonal-snow-
  accretion/hen-pheasant-head-jab/drawCar headlight-glow-beam/bale snow-cap),
  an Explore agent found the school bus (inline `if(bus){...}` block, ~line
  3639) has a daylight glass-glint whose own comment reads "same treatment
  as drawTruckSm/drawCar/drawPickup" — but unlike every one of those cited
  siblings (plus drawSemi/drawBike), it never paired that daylight glint
  with the closing night `dl<0.3` headlight+glow-beam+taillight block. Being
  an inline block rather than a `drawX()` function, it had slipped past
  every prior night-lighting sweep. Not a dead path: the bus's live spawn
  window (weekday mornings mid-Aug through late May, `tod>0.318&&tod<0.34`,
  line 1451) actually does overlap `dl<0.3` on real winter mornings in
  Liberal, KS, since December sunrise runs close to or after the window's
  8:10am close. Added the same three-line idiom used by drawTruckSm/drawCar/
  drawPickup, scaled to the bus's wider 21px body and placed at its front-
  right (headlight, matching the bus's rightward-only travel direction,
  `bus.x` only ever increases) / rear-left (taillight) corners, right after
  the existing trim paint and before the `bus.ph===1` stop-arm block:
  `if(dl<0.3){g.fillStyle='rgba(255,230,150,0.7)';g.fillRect(x+10.4,y-4.4,
  1,1);g.fillStyle='rgba(255,230,150,0.08)';g.fillRect(x+11.4,y-5.4,10,3);
  g.fillStyle='rgba(255,90,80,0.7)';g.fillRect(x-10.6,y-4.4,0.6,0.8);}`.
  Runner-up noted: the fire truck (`drawTruck`, ~line 2598) has the same
  glint-without-night-lights gap, but its sole call site is gated
  `tod<0.75` daytime-start, so `dl<0.3` is only reachable if a fire runs
  unusually long into dusk — real but far less reliably exercisable than
  the bus's winter-morning darkness, so left for a future run. Zero new
  state, nothing to reset in synth/jumpSeason, no Math.random, no timing
  change (rides the existing `dl<0.3` gate already used by every sibling).
  Exercisable live on real dark Kansas winter school mornings; not visible
  today (Aug, bright morning) — code-verified against the drawTruckSm/
  drawCar/drawPickup precedent's exact idiom and sizing. Smoke OK, both
  builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-17 #2 — With every closed decorative family still closed (shadow/
  glint/two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-
  grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-
  bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-
  ground-vehicle-dust/chore-details/tail-flick/moth-swarm/jack-o'-lantern-
  flicker/chassis-bounce/discrete-state-triggered-appendage/seasonal-snow-
  accretion/hen-pheasant-head-jab/drawCar headlight-glow-beam), an Explore
  agent found the two round hay bales stacked west of the barn (inline
  `draw()` block, "round bales stacked west of the barn," lines 3254-3257 —
  always-drawn, unconditional on season) never got a snow cap, while their
  structural sibling — the field-scattered round bales drawn per-row during
  harvest (line 3804) — already carry one via `snowCover>0.3`. Both are the
  identical shadow+two-nested-circle tan-body/twine-shadow-core primitive
  for a round hay bale; only the field bales' pass reached the "one row
  above the bale's top edge" snow-cap idiom already established by
  drawMailbox/drawSign/farmhouse-ridge/barn-ridge. Verified the exact
  geometry directly before shipping (agent proposed `HOR-0.4`; recomputed
  from the actual circle call — center `HOR+3`, radius `2.3`, so top edge
  is `HOR+0.7` and one row above is `HOR-0.3` — used the precise value
  instead). Added: `if(snowCover>0.3){g.fillStyle=css(SNOW);
  g.fillRect(BARNX+0.5,HOR-0.3,3,1);g.fillRect(BARNX+4.5,HOR-0.3,3,1);}`
  right after the two bale circles, one rect per bale, each centered over
  its own bale. Zero new state (reuses the existing `snowCover` global,
  already reset in synth/jumpSeason), no Math.random, no timing change —
  these bales render every day regardless of season/event state. Not
  visible today (Aug, no snow) — code-verified against the field-bale
  precedent's exact geometry, same class of verification as the mailbox/
  sign/farmhouse-ridge snow-cap ships. Smoke OK, both builds green (mac
  .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-17 — With every closed decorative family still closed (shadow/glint/
  two-tone/sun-catch/wind-sway/night-lighting/rain-splash/color-grading/
  rotating-mechanism/leg-scissor-gait/exhaust-puff/wx-easing/light-bloom-
  easing/eye-pixels/ground-wear/fireflies/idle-rest-breathing/dry-ground-
  vehicle-dust/chore-details/tail-flick/moth-swarm/jack-o'-lantern-flicker/
  chassis-bounce/discrete-state-triggered-appendage/seasonal-snow-accretion/
  hen-pheasant-head-jab), used a single Explore agent to double-check one
  specific loose thread the 2026-08-15 #3 log had flagged but never
  confirmed: "drawCar lacks the soft headlight-glow beam rect its siblings
  (drawSemi/drawTruckSm/drawPickup/drawBike) all have — real asymmetry, but
  sits inside the already-exhaustively-closed night-lighting family, so left
  alone." That earlier run had assumed the family-level closure covered it
  without actually checking drawCar's own night block. This run verified
  directly against all four sibling call sites: `drawTruckSm` (2493-2496),
  `drawSemi` (2357-2360), `drawPickup` (2773-2776), and `drawBike`
  (2471-2475) all pair their `dl<0.3` headlight dot with a
  `rgba(255,230,150,0.08)` glow-beam rect just ahead of it; `drawCar`'s own
  `dl<0.3` block (line 2306-2307, confirmed by direct read) had only the
  headlight dot and taillight dot, no beam — a genuine miss inside a
  "closed" family, same class of gap as the window-2-vs-window-1 and
  hillcow-vs-cow misses from earlier runs. Added the beam following the
  established offset convention (beam.x = dot.x+dot.width, beam.y = dot.y-1,
  8x3 sizing matching the similarly-scaled drawBike beam rather than the
  larger truck/pickup 10-wide beams): `g.fillRect(6,-4.6,8,3)` right after
  the headlight-dot line, with a "same idiom as..." comment matching the
  file's convention. Zero new state, nothing to reset in synth/jumpSeason,
  no Math.random, no timing change (rides the existing `dl<0.3` gate).
  Exercisable right now: any car (visitor/VFW-lot/drive-by) rendered after
  dusk. Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue). Takeaway: when a run notes "real gap X, but Y
  family already closed so skipping" — that's a flagged-but-unverified
  assumption, not a confirmed closure; worth a direct read next time it
  surfaces rather than trusting the family label.
- 2026-08-16 #4 — Confirmed last run's noted lead first: `jet`'s contrail block
  (~line 2957, a 2x1 dot + fading trail strokes) and `drawHawk` (~line 2138,
  a thin stroke silhouette) are both genuine dead ends — too minimal for any
  analogous highlight, same legibility-floor class as the declined grille-
  slats/fence-birds ideas. Pivoted (one Explore agent, given the full closed-
  family + declined-idea list) and found a fresh gap: `drawHens` (~line 3567)
  has a `stepj` head-jab — "the signature hen head-jab, keyed to ground
  covered," `Math.max(0,Math.sin(c.x*2.8))*0.6` applied to the head/neck x
  offset while walking toward its next stop — but `drawPheasant`'s strutting
  branch (~line 3500-3512), built on the identical `peck`-bool-gated walk-
  then-peck skeleton (`p.x` fractional glide toward `p.tx`, same precondition
  the hen idiom relies on), had no equivalent: its neck/head/face/ring rects
  sat at fixed offsets the whole time it was mid-stride, only bobbing in
  place once actually pecking. Since walking is most of the pheasant's
  screen time, it read as a rigid cutout next to the hen's visible head-bob
  doing the same routine. This is a fresh gap, not a re-tread of the closed
  leg-scissor-gait family (that's a leg idiom for quadrupeds/walkers; this is
  a head idiom, previously only on drawHens). Added the same `stepj` term
  (const `stepj=Math.abs(p.tx-p.x)>1.5?Math.max(0,Math.sin(p.x*2.8))*0.5:0`,
  slightly smaller amplitude — 0.5 vs the hen's 0.6 — matching the pheasant's
  already-smaller head scale) and folded it into the neck/head/face/ring
  x-offsets (`x+D*(1.3+stepj)` etc, 4 touched lines + 1 new). Zero new global
  state (`p.x`/`p.tx` already exist, reset on spawn/despawn), no
  Math.random, no timing change (rides the existing walk-vs-peck condition).
  Exercisable right now — pheasants are an active daytime spawn in August.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean aside
  from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-16 #3 — After two same-day aborts (below) concluded the usual
  vehicle/animal/architecture/people cast was genuinely swept clean of
  sibling-asymmetry gaps, checked a different axis per those runs' own
  suggestion: rare one-shot sky events (balloon/jet/hawk) that neither past
  sweep's enumerated cast list ever named, since they're spawned inline in
  `update()`/drawn inline near the horizon layer rather than through a
  `drawX()` function alongside the other vehicles/animals. Found the
  hot-air balloon's canopy (~line 2947, the `if(balloon)` block, a rare
  calm-morning-only spawn) is a single flat `circle()` fill with no
  highlight at all, while every other rounded tank/drum/chamber shape in
  the file that gets the "closed" two-tone/sun-catch family (drawBaler's
  baling chamber ~line 2267, drawSprayer's tank, drawCart's drum) carries
  an offset lighter-toned highlight arc simulating the sun catching the
  curve. Added the identical idiom verbatim: a second, smaller `circle()`
  in `shA(mixA(C('#3e7d3b'),SNOW,0.22),0.4)` offset up-left of the base
  canopy circle — kept the local `0.4` shade factor already used by every
  other color in this specific block (balloon colors are deliberately
  under-shaded relative to the usual `0.5`/`1` ground-object convention,
  presumably since it's an aerial sprite) rather than importing drawBaler's
  literal `0.5`. One line, zero new state, nothing to reset in synth/
  jumpSeason, no Math.random, no timing change (rides the balloon's
  existing rare spawn gate, `Math.random()<0.000006` in `update()`).
  Exercisable live only on a rare calm clear summer/spring morning — not
  observed live this run, verified by code reading against the drawBaler
  precedent, same class of verification as prior seasonal/rare-event ships.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings),
  kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy
  session remains genuinely overdue). Note for next run: `jet`'s contrail/
  fuselage block (~line 2956) and `drawHawk` were also outside both prior
  sweeps' cast lists — jet is a minimal silhouette (a 2x1 dot + fading
  contrail lines) that likely has no analogous highlight to add, but worth
  a quick look before assuming closed.
- 2026-08-16 #2 — ABORTED, no ship. Gave a single Explore agent the full
  closed-family list (shadow/glint/two-tone/sun-catch/wind-sway/night-
  lighting/rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/
  exhaust-puff/wx-easing/light-bloom-easing/eye-pixels/ground-wear/
  fireflies/idle-rest-breathing/dry-ground-vehicle-dust/chore-details/
  tail-flick/moth-swarm/jack-o'-lantern-flicker/chassis-bounce/discrete-
  state-triggered-appendage/seasonal-snow-accretion) plus the declined-idea
  list (tie-down strap x3, fence birds, fence-post lean, breath puffs,
  crop-art, VFW plaque) and asked for a thorough fresh sweep across every
  vehicle/animal/architecture/people/one-off-gag draw function. It read
  through the full cast (drawCombine/Sprayer/Planter/Baler/Cart/Car/Semi/
  Mower/Bike/TruckSm/Truck/Pickup/TractorAt, drawCow/hillCows/Dog/deer/
  rabbit/pheasant/Duck/Hawk/cat, barn/silo/farmhouse/drawDealership/
  drawVFW/drawMailbox/drawSign/drawTown, drawFolk/Hunter/walkers/bus,
  drawWeld/MannyCoffee/KeatingFire/train/snowman) and confirmed every
  closed family is genuinely closed everywhere it checked. Only one
  candidate asymmetry surfaced: `drawSemi` alone has cab grille-slats
  (line ~2343) that drawTruck/drawTruckSm/drawPickup lack — declined by
  the agent itself as likely below the legibility floor at those smaller
  cabs' scale (drawTruckSm's whole cab is ~7x7px), the same class of risk
  that got fence-birds and the tie-down strap rejected. Rather than force
  a low-confidence pick, aborting with no edit (two runs today have now
  turned up nothing shippable — this file may be genuinely swept clean of
  safe small polish for now; a future run might need to look at a
  different axis entirely, e.g. new one-shot ambient events rather than
  sibling-asymmetry fixes on existing sprites, or revisit the grille-slats
  idea with an actual rendered screenshot to check legibility before
  deciding). Nothing to smoke-test or rebuild.
- 2026-08-16 — ABORTED, no ship. Explore agent found only one concrete
  candidate outside the closed-family list: a tie-down strap on
  `drawSemi`'s flatbed load (line 2363, `drawLoad`), echoing the barn
  hay-bale twine idiom (line 3210). This exact idea was already surfaced
  and explicitly declined twice before (2026-08-13 #3 and #5) as reading
  like decorative texture rather than a fresh animation family. This
  round's proposed fix was also a single fixed-coordinate strap line
  meant to span six structurally different load sprites (tractor/
  combine/sprayer/baler/plow/mower, each drawn via its own function at a
  different scale: 0.45/0.32/0.3/0.6/n-a/n-a) — no way to confirm it
  wouldn't visibly clip through some loads without rendering the canvas,
  which wasn't available. Given a twice-declined idea plus an unverifiable
  diff, shipping would violate the "verify rigorously" rule, so this run
  aborts with no edit made (nothing to smoke-test or rebuild). Note for
  next run: if the flatbed-strap idea resurfaces a third time, it likely
  needs per-load-kind strap placement (a small lookup table keyed on
  `kind`) rather than one fixed line, if it's worth doing at all.

- 2026-08-15 #5 — With every closed decorative family still closed, an Explore
  agent found `drawSemi` (~line 2318) never got the dynamic two-tone
  roof-highlight treatment its siblings all carry: `drawTruck` (cab and box,
  lines 2609/2612), `drawTruckSm` (line 2486), `drawCar` (line 2299),
  `drawSprayer` (line 2239), and `drawCart` (line 2284) all paint their body
  color via `P()` then add `shA(mixA(C(col),SNOW,0.22),0.5)` on the top row
  as a "roof highlight, matches drawX's cab treatment" — but drawSemi's cab
  (`P(-1,-13,9,9,'#e8eaee')`, line 2333) went straight from body paint to a
  static roof-cap/visor shape with no dynamic highlight at all, despite the
  cab being drawn with the same `P()`+`shA(mixA())` idiom for everything
  else nearby (glass glint, night lights). Verified directly against all
  five sibling call sites before shipping — same class of narrow miss as
  the hillcow/mini-crane tail-flick and beacon gaps. Added one line
  matching the idiom exactly: `g.fillStyle=shA(mixA(C('#e8eaee'),SNOW,0.22),
  0.5);g.fillRect(-1,-13,9,1);` on the cab's top row. Deliberately left the
  existing static bottom skirt-trim row and the box-trailer's own trim
  lines untouched (distinct static design elements, not covered by this
  one-line fix, out of scope for a single change). Zero new state, no
  Math.random, no timing change. Exercisable right now — the sale-delivery
  semi and any drive-by hauler render this cab in normal daylight.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings),
  kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy
  session remains genuinely overdue).
- 2026-08-15 #4 — Farmhouse roof snow (line 3443) covered both eave bands but
  never the ridge apex, while its sibling the barn (line 3238) gets snow on
  eave AND ridge cap. Added `g.fillRect(hx+10,HOR-22,4,1)` inside the
  existing `snowCover>0.25` block, at the apex vertex (`hx+12,HOR-22`, roof
  triangle line 3315), one row above the existing ridge sun-catch highlight
  (`hx+11,HOR-21`, line 3316) — same "something happens at the ridge"
  precedent already established for daylight, just missing winter. Zero new
  state, no Math.random, no timing. Not visible today (Aug, no snow) —
  code-verified against the barn precedent, same class as prior seasonal-
  only ships. Smoke OK, both builds green, kit+zip refreshed, index.html
  re-synced. NOT committed (same overdue-review-session backlog).
- 2026-08-15 #3 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce/discrete-state-triggered-appendage/
  seasonal-snow-accretion all exhaustively closed (including this morning's
  hillcow tail-flick run below), used a single Explore agent, given the full
  dead-end list, to find a fresh gap. It found `drawTown()` (~line 2627)
  carries two near-identical construction-crane renderers for the horizon
  town's growth phases: the main in-progress-building crane
  (`last&&frac<0.99`, ~line 2683) and a smaller "outskirts mini-crane" drawn
  once the core town plan is fully built out and expansion continues
  (`extraN<12&&(epDay-doneAt)%45<4`, ~line 2713). Both share the same mast/
  boom visual grammar and brown palette, but only the main crane had a
  blinking red night-warning beacon (`dl<0.25&&((rt*0.8|0)%3)!==0`, line
  2692) — the mini-crane had no beacon at all, despite being the same kind
  of tall nighttime hazard a real construction crane needs lit. Added the
  identical beacon line to the mini-crane, same window/blink cadence/color,
  repositioned to its mast top (`x+3.5,cb-9.4`, 1.4px above its boom line at
  `cb-8`, same offset convention as the main crane's `ct2-1.4` above its own
  boom). Zero new state (`dl`/`rt` already in scope), nothing to reset in
  synth/jumpSeason, no Math.random in draw code, no new timing window (rides
  the mini-crane's existing spawn gate). Not live-visible today: this path
  only fires once `epDay>doneAt` (`doneAt`≈428 days after the town's July 6
  2026 founding epoch, i.e. ~September 2027) and today's `epDay`≈40 — code-
  verified only, same class of unseen-this-season verification as the sign
  snow-cap/jack-o'-lantern precedents, but will render correctly once the
  town's growth phase reaches that point (or immediately under a jumped/sim
  date). Runner-up rejected: `drawCar` lacks the soft headlight-glow beam
  rect its siblings (drawSemi/drawTruckSm/drawPickup/drawBike) all have —
  real asymmetry, but sits inside the already-exhaustively-closed
  night-lighting family, so left alone. Smoke OK, both builds green (mac
  .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session remains genuinely
  overdue).
- 2026-08-15 #2 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce/discrete-state-triggered-appendage/
  seasonal-snow-accretion all exhaustively closed (including this morning's
  sale-trailer wheel-color bugfix below), used a single Explore agent, given
  the full dead-end list, to find a fresh gap. It found that "tail-flick" was
  only closed for the *near* cattle herd: `drawCow` (line 2170, the crop-row
  `cattle.arr` herd) has carried `Math.sin(c.tail*3)` tail-flick for weeks,
  and deer/rabbit both got it in earlier runs — but the distant background
  herd, `hillCows` (a separate, hand-rolled sprite drawn inline at ~line 3085,
  NOT routed through `drawCow`), still drew a fully static "tail nub off the
  rump" (`g.fillRect(x+(c.d>0?-2.5:1.9),y-2.1,0.6,1);`, line 3102) — the exact
  same class of miss as the window-2 moth-swarm and window-2-vs-window-1 gaps
  from earlier runs, where a "closed" family turned out to only cover one of
  two structurally-separate sibling implementations. `hillCows` was reworked
  for legibility on 2026-07-13 (comment at line 3086) but that pass never
  touched tail motion. Added a one-line sine term, same idiom as the deer/
  rabbit precedent (phased off `rt` + the cow's own `x` position hash, no new
  state): `const tf5=Math.sin(rt*3+x*0.4)*0.4;` applied to the tail rect's
  height (`0.6,1+tf5`, anchor fixed at `y-2.1` same as the cow/deer/rabbit
  convention of a fixed top edge with animated height). Zero new global
  state, nothing to reset in synth/jumpSeason, no Math.random (draw-code
  only). Exercisable right now: `hillCows` render whenever `dl>0.4&&
  snowCover<0.6`, true most of a summer day. Runner-ups rejected: drawPlanter
  lacks an undercarriage-shade but its own comment explicitly justifies the
  omission ("too thin for full sandwich"); drawMower wheels don't rotate but
  are plain filled circles with nothing for motion to read against (no
  spokes/tread), likely deliberate. Smoke OK, both builds green (mac .saver +
  Windows .scr, dotnet clean aside from the pre-existing WindowsBase
  version-conflict warnings), kit+zip refreshed, index.html re-synced
  (byte-identical, verified). NOT committed (evolve doesn't commit/push
  itself — uncommitted work continues to span 07-22 onward across many runs;
  a review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-15 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce/discrete-state-triggered-appendage/
  seasonal-snow-accretion all exhaustively closed (including yesterday's bike
  taillight and autumn leaf-litter off-by-one fixes), used a single Explore
  agent, given the full dead-end list, to find a fresh gap. It found
  `drawPickup` (~line 2745-2761): the dealership's sale/delivery trailer
  (drawn trailing the pickup when hauling a sold tractor/mower off the lot,
  `sale.item`) painted its deck, hitch tongue, AND both trailer wheels with
  the single steel-gray `#4a4e58` frame color — while two lines later, in
  the same function, the truck's own wheels get the file's standard
  three-layer tire treatment (`#22242c` tire / `#3d434e` rim / `#9aa2ae`
  hub), a rule every other wheeled sprite in the file (drawTractorAt,
  drawCombine, drawSprayer, drawPlanter, drawBaler, drawCart, drawCar,
  drawTruckSm, drawSemi, drawBike, ...) follows without exception. `#4a4e58`
  is legitimately the file's steel-frame/hitch color elsewhere (sprayer
  boom, cart hitch) but nowhere else leaks onto a wheel — this was the one
  gray-disc wheel pair in the file. Split the fillStyle so the trailer
  wheels render in tire-black alongside the truck's own, folding both wheel
  pairs under one `#22242c` fillStyle set (removed a now-redundant second
  set of the same color rather than leaving a duplicate). 3-line diff, zero
  new state, nothing to reset in synth/jumpSeason, no Math.random, no
  timing change. Exercisable right now: `sale` triggers most non-Sunday
  days (~30% chance, `onNewDay`) and can be force-triggered instantly via
  the `s` demo key; today (Aug 15) is a Saturday. Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-14 #3 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce/discrete-state-triggered-appendage/
  seasonal-snow-accretion all exhaustively closed (including this morning's
  autumn leaf-litter off-by-one fix below), used a single Explore agent,
  given the full dead-end list, to find a fresh gap. It found `drawBike`
  (~line 2451-2475): its `dl<0.3` night-lighting block (line 2470) already
  draws an amber headlight glow (dot + soft cone) at the front, but unlike
  every other night-lit mover with the closed night-lighting treatment —
  `drawCar`, `drawTruckSm`, `drawSemi`, `drawPickup` — it never drew a rear
  taillight, despite the bike being the one moving vehicle in that family
  that already had a headlight to pair one with. Added one line inside the
  existing `if(dl<0.3){...}` block, exact color/alpha/size idiom copied from
  `drawTruckSm`'s taillight (`rgba(255,90,80,0.7)`, `0.6x0.8` rect),
  positioned at the frame's rear (`x-4,y-4`, mirroring the headlight's
  `x+4,y-4` across the bike's own center — front wheel is at `x+2.5`, rear
  wheel at `x-2.5`, so both lights sit just beyond their respective wheel).
  Zero new state, nothing to reset in synth/jumpSeason, no Math.random, no
  new timing (rides the existing `dl<0.3` gate). Exercisable right now:
  any bike (bikeRide event, VFW-lot patron bikes) moving after dusk. Runner-
  up rejected: the school bus has no headlights/taillights at all, but its
  ~7:38-8:10am spawn window is normally already past sunrise, so the
  absence looks intentional (no real night-driving case) rather than a
  gap. Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings),
  kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy
  session remains genuinely overdue).
- 2026-08-14 #2 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce/discrete-state-triggered-appendage/
  seasonal-snow-accretion-on-flat-topped-post-mounted-objects all exhaustively
  closed (including this morning's sign snow-cap run below), used a single
  Explore agent, given the full dead-end list, to find a fresh gap outside
  all decorative-family sweeps. It found a genuine off-by-one bug rather than
  a missing-treatment gap: the autumn leaf-litter spawn (`update()`,
  ~line 1664) picked its source tree via `TREES[1+(Math.random()*3|0)]`,
  which can only ever yield indices 1-3 — `TREES[0]` (`{x:AX(14),big:1}`,
  the barn-yard tree where Boone naps and the rabbit spawns) was silently
  excluded from ever dropping leaves, despite being drawn through the exact
  same `tree(x,big)` function (called explicitly at line 3189) and getting
  the identical autumn-canopy-speckle and spring-blossom treatment inside
  `tree()` itself — a full sibling in every other season-driven way, just
  carved out of the one place deciding which trees drop litter. No comment
  nearby justified the exclusion; read as an off-by-one range slip
  (`1+(rand*3|0)` instead of `rand*4|0`), not a design choice. Fixed with a
  one-token range change: `TREES[Math.random()*4|0]`. This is inside
  `update()`'s spawn logic, not draw code, so `Math.random()` here is the
  established convention (same as every other particle-spawn line in this
  block). Zero new state, nothing to reset in synth/jumpSeason, no new
  timing window (rides the existing `seasonIdx===2` gate). Exercisable live
  once autumn arrives; not visible today (August, Summer) but a pure code-
  level range fix, not a seasonal-only visual needing sim verification.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings),
  kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy
  session remains genuinely overdue).
- 2026-08-14 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce/discrete-state-triggered-appendage
  all exhaustively closed, used a single Explore agent, given the full
  dead-end list, to find a fresh gap. It found a genuinely new (unclosed)
  family: seasonal snow accretion on flat-topped post-mounted objects.
  `drawMailbox` (~line 1982) already carries both a sun-catch highlight AND
  a `snowCover>0.3` snow cap on its box top (line 1990:
  `g.fillStyle=css(SNOW);g.fillRect(x-2,HOR-10,5,1);` — one row above the
  box's own top edge), but `drawSign` (~line 2403, the storyline yard signs
  posted during `sagaSigns`) copies the mailbox's sun-catch line verbatim
  (line 2407) yet stops there — no snow branch at all, despite 15+ other
  call sites in the file applying `snowCover>0.3` consistently (dealership
  roofline, farmhouse door, VFW door, silo cap, church steps, picnic basket
  lid, etc.). Added one line matching the mailbox's exact convention
  (snow cap sits one row above the box's top row, same width as the box):
  `if(snowCover>0.3){g.fillStyle=css(SNOW);g.fillRect(x,y-7,4,1);}` appended
  after the sign panel's existing paint. Zero new state (reuses existing
  `snowCover` global, already reset in synth/jumpSeason), no timing, no
  Math.random. Code-verified only for today (Aug 14, Kansas, no snow) since
  it needs `sagaSigns` + winter to coincide to be seen live, same class of
  verification as the winter stove-smoke/jack-o'-lantern precedents — but
  the sign panel itself is fully exercisable now via `sagaSigns`, and this
  closes a real, unclosed idiom gap rather than re-treading a swept family.
  Runner-up rejected: `drawWalker` (~line 2310) has no arms at all unlike
  `drawFolk`'s counter-swaying arms — weaker since drawWalker reads as a
  deliberately minimal, smaller-scale background sprite, so the omission
  may be an intentional scale simplification rather than a genuine gap.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings),
  kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy
  session remains genuinely overdue).
- 2026-08-13 #5 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce/discrete-state-triggered-appendage
  all exhaustively closed, used a single Explore agent to check a specific
  loose thread from 2026-08-10 #3's log: that run picked `drawWalker`'s
  leg-scissor-gait fix over `drawHunter` (~line 205) as the stronger pick,
  explicitly noting `drawHunter` had the identical rigid-leg gap during his
  walk-in/walk-out phases but left it unfixed. Confirmed still open:
  `drawHunter`'s two leg rects (line 208) were still flat static rects while
  `D.x` moves continuously during `D.ph==='in'` (walking on to his spot) and
  `D.ph==='out'` (walking off after the hunt ends). Applied the same
  ground-covered-phase idiom `drawWalker` already uses
  (`Math.sin(x*0.9)*0.4` scissoring the two leg rects oppositely), but with
  one adaptation `drawWalker` didn't need: `drawHunter` also has a third,
  stationary `D.ph==='ready'` phase (aiming/firing at ducks) where `D.x`
  never changes, so froze the scissor there (`D.ph==='ready'?0:...`) to keep
  his stance planted while shooting rather than let the sine term run on
  stale position. Zero new state, nothing to reset in synth/jumpSeason, no
  Math.random. Exercisable live: the ambient hunt fires some dawns/dusks
  when clear and dry (`weather<0.4&&snowCover<0.5`); smoke.js's synthetic
  "ambient hunt" pass also exercises `D.ph==='in'` directly. Smoke OK, both
  builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-13 #4 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce/discrete-state-triggered-appendage
  all exhaustively closed, used a single Explore agent, given the full
  dead-end list, to find a fresh gap. It found `drawTruckSm` (~line 2489):
  its night lighting block only drew a front headlight dot+glow, with no
  rear taillight — even though the two nearby night-lighting comments on
  `drawCar`/`drawPickup`/`drawSemi` (lines 2355, 2763) explicitly read
  "same idiom as drawTruckSm/drawCar", i.e. the file's own comments assert
  drawTruckSm already has this treatment when it doesn't. All three
  siblings draw a `rgba(255,90,80,0.7)` taillight dot near the truck bed's
  rear corner at night (`dl<0.3`). drawTruckSm is the most frequently used
  vehicle sprite (dealership visitors, VFW-lot trucks, mail carrier), so
  this was a small but often-visible asymmetry. Added one line matching
  the exact color/idiom, positioned at the bed's rear corner
  (`P(-6,-7,13,4,col)` → rear x=-6, offset -0.6 same as drawPickup's
  bed-rear-to-taillight offset): `g.fillStyle='rgba(255,90,80,0.7)';
  g.fillRect(-6.6,-5,0.6,0.8);` inside the existing `dl<0.3` block, and
  added the missing "same idiom as..." comment the sibling functions
  already carry. Zero new state, nothing to reset in synth/jumpSeason, no
  Math.random, no new timing (rides the existing `dl` gate). Runner-up
  (already known, rejected a third time): flatbed-semi hauled loads lack
  a tie-down strap (barn hay-bale twine precedent) — Explore agent
  reconfirmed nothing fresher beats the taillight asymmetry. Smoke OK,
  both builds green (mac .saver + Windows .scr, dotnet clean aside from
  the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work
  continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-13 #3 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker/chassis-bounce all exhaustively closed, used a
  single Explore agent, given the full dead-end list, to find a fresh gap.
  It found the school bus's `bus.ph===1` stopped-and-loading state (`draw()`,
  ~line 3648): the only feedback that the bus has stopped for a pickup was
  one flashing roof-corner pixel — a real school bus's most iconic tell, the
  red stop-sign arm that swings out from the side, was entirely absent, even
  though the bus already carries the file's full glass-glint treatment
  (~line 3642) so it's clearly been passed over rather than left plain on
  purpose. This is a fresh category distinct from every closed family above:
  a discrete state-triggered mechanical appendage that pops out and retracts,
  an idiom the file had used exactly once before (the mailbox flag at
  `drawMailbox`, ~line 1987 — a small red rect that only appears while a
  boolean is true) and never applied to any vehicle with a similarly
  discrete stop state. Added a two-rect hinge-arm+octagon-plate pair reusing
  that pole/flag idiom, animated via a triangular ease already available for
  free from the existing `bus.t` field (no new state): `arm=Math.min(1,
  bus.t/0.4,(3-bus.t)/0.4)` swings the arm out over the first 0.4s, holds
  through the 3s stop, swings back in over the last 0.4s — reusing the same
  `Math.min`-chain triangular-envelope idiom the flicker/breathing passes
  already use elsewhere, just with three terms instead of two. Both rects
  shaded through `S()` like everything else in the block. Zero new global
  state (only `bus.ph`/`bus.t`, already existing), nothing to reset in
  synth/jumpSeason, no Math.random, no new timing window (rides the bus's
  existing spawn gate). Exercisable right now: live weekdays mid-Aug through
  late May during the school-morning window, and today (Aug 13, a Thursday)
  is inside that live gate — verified by reading the `school` date-range
  check at line 1452, not observed live in this session. Runner-up noted for
  next time: flatbed-semi hauled loads (`drawLoad`, ~line 2363) have no
  tie-down strap/binding securing cargo to the deck, despite a precedent for
  exactly this texture (the barn's hay-bale twine, ~line 3203) — judged
  weaker since a strap reads more like decorative texture already brushed by
  the two-tone/shading passes than a genuinely new animation family. Smoke
  OK, both builds green (mac .saver + Windows .scr, dotnet clean aside from
  the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-13 #2 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm/
  jack-o'-lantern-flicker (this morning's run below) all exhaustively closed,
  used a single Explore agent, given the full dead-end list, to find a fresh
  gap. It found `drawSprayer` (~line 2221): the file's two other self-propelled
  field machines, `drawTractorAt` (line 1884, `mov?Math.abs(Math.sin(x*1.9))
  *0.7:0`) and `drawCombine` (line 2175, `Math.abs(Math.sin(x*1.7))*0.5`), both
  bump the chassis vertically as a function of ground position covered —
  simulating rolling over uneven field dirt — but the See & Spray sprayer
  (third of the trio, spawned during `phase==='growing'`, always advancing
  `sprayer.x` every frame it exists per its `update()` at line 1631, so never
  drawn stationary in the field) had a flat `g.translate(x,y)` with no bump
  term at all, gliding perfectly level while its two siblings visibly bounce.
  Added the identical idiom, unconditional like the combine's (no `mov` flag
  needed — sprayer has none and is never drawn at rest in the field; the one
  static use as a for-sale-lot flatbed load via `drawLoad`, line 2366, gets a
  harmless fixed sub-pixel offset from the constant x, exactly like
  `drawCombine` already tolerates there): `Math.abs(Math.sin(x*1.8))*0.6`
  (constants split the difference between the tractor's 1.9/0.7 and combine's
  1.7/0.5). Exercisable right now — August 13 is squarely in the growing
  season, See & Spray passes are a normal daytime occurrence. Runner-up noted
  for next time: the towed implements (`drawPlanter`/`drawBaler`/`drawCart`,
  called at lines 3796-3798) render at the tractor's raw `tract.x,tract.y`
  while `drawTractorAt` applies its own bump on top of those same coordinates
  — so the tractor bobs slightly while whatever it's towing stays rigidly
  level, a subtle hitch/tractor detachment. Weaker pick than the sprayer fix
  since it's a two-part coupled change (the bump would need to be shared/
  exposed rather than staying encapsulated in `drawTractorAt`), not a single-
  function addition. Zero new state, nothing to reset in synth/jumpSeason, no
  Math.random — pure per-frame trig offset of the existing `x` position hash,
  same pattern as the combine's own precedent. Smoke OK, both builds green
  (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward across
  many runs; a review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-13 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details/tail-flick/moth-swarm all
  exhaustively closed, used a single Explore agent, given the full dead-end
  list, to find a fresh gap. It found the October jack-o'-lantern glow
  (`draw()`, ~line 3431, gated `hallo&&dl<0.4` on the actual carve-night —
  Oct 31 live, or `seasonT>0.9` in sim): a flat, constant `rgba(255,150,50,
  0.18)` circle with zero motion, while every other flame/arc/spark light
  source in the file already breathes or stutters on `rt` — the welder arc
  (`drawWeld`, `((t*22|0)%4)!==0`), the KEATING-sign fire's window-blowout
  color alternation and flame-tongue palette cycling (`drawKeatingFire`),
  the kaiju tail-rotor flicker, even the owl's eye-blink. The jack-o'-lantern
  was the one candle-lit prop that never flickered. Added a two-sine flicker
  factor (`fk=0.75+0.25*Math.sin(rt*9+hx)*Math.sin(rt*3.3+hx*0.7)`, phased
  off the farmhouse's own `hx` so it wouldn't sync with any other rt-phased
  light) scaling the glow circle's alpha only — same "modulate alpha, not
  hue" idiom as the simplest flicker cases, kept to a template string since
  the base circle() color was a literal, not a shA/mixA call. Zero new
  state, nothing to reset in synth/jumpSeason, no Math.random — pure
  per-frame trig, same gate as before (`hallo&&dl<0.4`). Runner-up rejected:
  the window-2 AC unit's static condensation stain (~3346-3353) is a
  plausible companion to the closed chore-droplet-arc idiom but it's a damp
  stain, not a light source — weaker match to any single established
  convention, and closer to the already-closed chore-details family. Not
  exercisable live today (August, not Oct 31/sim autumn window) — verified
  by code reading, same as the winter stove-smoke precedent. Smoke OK, both
  builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-12 #4 — Picked up the explicit runner-up note left by 2026-08-12 #3's
  deer-tail run: the rabbit's cotton-tail patch (`draw()`, ~line 3478) was still
  a static 1x1 fillRect, the same gap the deer's rump/tail patch had before that
  run. Checked whether the rabbit could also get the deer's ph2-amplified "alarm
  flag" treatment first — rabbit.ph (0 approach/1 idle-nibble/2 depart) looks
  structurally identical to deer.ph at a glance — but the rabbit's ph1→ph2
  transition fires on `t>9||dl>0.5||fire||weather>0.4`, i.e. routine dawn's-over
  or bad-weather departures just as often as an actual fire-spook, unlike the
  deer's `t>18||fire` (idle-timeout or fire only). Amplifying the flick on
  ph===2 would read as "alarmed" on every ordinary morning exit, not just a real
  spook — so, matching this file's own prior note, shipped only the base sine-
  on-rect-height flick with no ph-based amplification: `tf4=Math.sin(rt*3+
  rx*0.4)*0.35` applied to the cotton-tail fillRect's height (anchor y fixed,
  same idiom as the deer's baseline case), phased off `rt` + the rabbit's own
  `x` position hash. Zero new state, nothing to reset in synth/jumpSeason, no
  Math.random. Smoke OK, both builds green (mac .saver + Windows .scr, dotnet
  clean aside from the pre-existing WindowsBase version-conflict warnings),
  kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues to
  span 07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-12 #3 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details all exhaustively closed
  (including this morning's window-2 moth-swarm run below), used a single
  Explore agent, given the full dead-end list, to find a fresh gap. It found
  the dawn deer's static tail patch (`draw()`, ~line 3466): `drawCow()` (line
  2169) has carried a `Math.sin(c.tail*3)` tail-flick on its tail rect's
  height since early on ("tail flick" comment, phased off each cow's own
  `tail` field), but the deer's analogous light rump/tail patch — the same
  single 1x1 `fillRect` idiom — never moved, despite the deer block using
  `rt`-phased sine for everything else it animates (`walkb` gait bob, `graze`
  head-down dip). Added a one-line sine term to the patch's height
  (`1+tf3`, anchor fixed at the top same as the cow's own y-anchored variant),
  phased off `rt` + a `dx3*0.4` position hash (no new state, `dx3` and
  `deer.ph` already in scope) — and gave it real narrative payoff beyond pure
  idle motion by amplifying and speeding the flick when `deer.ph===2` (the
  existing spook/flee state triggered by `fire` or an 18s graze timeout):
  amplitude 0.7 vs 0.35, frequency 6 vs 3, mimicking a whitetail's classic
  "tail flag" alarm signal. Height stays in [0.3,1.7]px, never negative at
  either amplitude. Zero new global state, nothing to reset in
  synth/jumpSeason, no Math.random. Runner-up noted for next time: the
  rabbit's cotton-tail patch (~line 3477) has the identical static-tail gap,
  a few lines below the deer in the same wildlife block — weaker pick this
  round since it has no analogous alarm-state hook to give the flick meaning.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean aside
  from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-12 #2 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust/chore-details all exhaustively closed
  (including this morning's Earl stock-tank water-arc run below), used a
  single Explore agent, given the full dead-end list, to find a fresh gap. It
  found the farmhouse's two bedroom windows (`draw()`, ~3336-3362): `l1On`/
  `l2On` are built as a fully matched pair (mirrored lit-window logic,
  mirrored `winLit`/`w2Lit` room-glow bloom circles at lines 3344/3345), but
  the moth-swarm block at 3354 — three particles orbiting the lit window on
  warm nights — was keyed only to `l1On||l3am` and only orbits window 1's
  coordinates (`hx+5,HOR-8`); window 2 lights up just as often via `l2On` and
  gets the identical glow treatment but never drew its own moths. Added a
  mirrored block right after the original, same 3-particle orbit math
  reused verbatim, retargeted to `hx+19,HOR-8` (window 2's glow center) and
  gated on `l2On` alone (window 2 has no `l3am`-equivalent 3am case), with a
  `+4.7` phase offset on `mp` so the two clusters don't move in lockstep.
  Minor accepted overlap: window 2 also grows a summer-only window AC unit at
  the same coordinates (`seasonIdx===1`, lines 3346-3353) drawn just before
  this block, so the new moths will occasionally flit in front of it — same
  class of minor overlap window 1's moths already have with its own frame,
  judged negligible at 1px/low-alpha. Zero new global state (reuses `l2On`,
  `seasonIdx`, `seasonT`, `hx`, `rt`), no timing, no Math.random — nothing to
  reset in synth/jumpSeason. Runner-up rejected: a daytime glass-glint streak
  on the VFW post's windows — judged too close to the closed glint family and
  visually competing with the VFW's existing multicolor party-glow treatment.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean aside
  from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-12 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing/dry-ground-vehicle-dust all exhaustively closed, used a single
  Explore agent, given the full dead-end list, to find a fresh gap. It found
  the chore-details block (`draw()`, ~line 3582-3593): Earl (folk[0]) and Dot
  (folk[4]) both get a `chore==='water'` state from `folkPlan()` (line 256:
  Earl "topping the stock tank"; line 287: Dot "watering the flowerbed"), and
  the rendering block right there already has a `dw.chore==='water'` branch
  for Dot — a small droplet-arc animation (4 particles lerping from her hands
  to the flowerbed, `rt`-phased, fading alpha) — but no matching
  `fm.chore==='water'` branch for Earl, so every morning he walks to his
  stock-tank spot and just stands there. Added the identical arc block for
  Earl, verbatim precedent (same lerp/phase/fade math, same
  `rgba(150,200,225,...)` water color) retargeted from `HX+22`/flowerbed to
  `HX+24`/stock-tank (Earl's own `tx` from his chore state) and sourced from
  `fm.x+2,fm.y-4` instead of Dot's. Zero new state, no timing, no
  Math.random — nothing to reset in synth/jumpSeason. Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward across
  many runs; a review/commit/push/redeploy session remains genuinely
  overdue).
- 2026-08-11 #4 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies/idle-rest-
  breathing all exhaustively closed (including this evening's seated-folk
  breathing run below), used a single Explore agent, given the full dead-end
  list, to find a fresh gap. It found the VFW-lot vehicle drive-in/drive-out
  branches (`update()`, ~line 1221-1235, `p.st==='in'`/`'go'`): trucks and
  bikes roll onto/off the post's gravel apron at speed with zero dust, even
  though that surface is explicitly established as loose gravel (`drawVFW`'s
  own "wheel ruts worn into the gravel by the nightly parade of pickups"
  comment) and the tractor's dry-fieldwork dust plume (~line 1639,
  `weather<0.35&&snowCover<0.2`, tan `#c2ab84` particles into the shared
  `smoke` array) is the established precedent for exactly this kind of dry-
  ground vehicle dust. Added a matching probabilistic puff
  (`Math.random()<0.15` per frame while rolling) to both the `'in'` and
  `'go'` branches, gated `p.kind!=='bike'` (bikes don't kick up real dust)
  and the same `weather`/`snowCover` dryness check, positioned just behind
  each truck's direction of travel at gravel-lot ground height (`y:127`,
  matching `vfwY()`'s baseline). Reuses the existing `smoke` array and its
  generic draw-side loop, so no new draw code, no new global state (nothing
  to reset in synth/jumpSeason), and `Math.random()` stays confined to
  `update()` exactly like the tractor/train/cattle dust precedents — never in
  draw code. Exercisable right now: August, dry weather common, VFW lot
  active most evenings. Smoke OK, both builds green (mac .saver + Windows
  .scr, dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced (byte-identical,
  verified). NOT committed (evolve doesn't commit/push itself — uncommitted
  work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-11 #3 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies all exhaustively
  closed (including this morning's Dot-skirt-sway run below), used a single
  Explore agent, given the full dead-end list, to find a fresh gap outside all
  of them. It found a new family entirely — idle-rest animation, distinct from
  walking-gait: `drawFolk()`'s `f.sit` branch (~line 353, used for Earl & Dot on
  the porch bench, Manny at lunch on the bay step, and the kid blowing
  dandelions) is a completely rigid rect stack with zero motion while seated,
  but its direct sibling `drawDog()`'s nap pose (~line 397) already has a slow
  visible-breathing sine (`br=Math.sin(rt*0.85)*0.3` rising/falling the back
  rect) — the resting dog breathes right next to statue-still resting humans in
  the same scenes. Added the identical idiom to the torso/head rects of the sit
  branch: `br=Math.sin(rt*0.85+x*0.4)*0.15` (phased off each figure's own `x` so
  multiple seated folks don't breathe in lockstep, scaled smaller than the dog's
  0.3 since a human torso reads bigger than a small dog's back at this pixel
  scale), applied as `y-6-br,3,3+br` etc. across the shirt/collar/head rects and
  `drawHat`'s y — the whole upper body rises and falls together as one unit
  rather than each rect breathing independently. Zero new state, no timing, no
  Math.random — pure per-frame trig offset of existing globals (`rt`), nothing
  to reset in synth/jumpSeason. Exercisable right now (Dot/Earl sit on the
  porch, Manny sits at lunch, the dandelion kid sits — all summer-common poses).
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean aside
  from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-11 #2 — With shadow/glint/two-tone/sun-catch/wind-sway/night-lighting/
  rain-splash/color-grading/rotating-mechanism/leg-scissor-gait/exhaust-puff/
  wx-easing/light-bloom-easing/eye-pixels/ground-wear/fireflies all exhaustively
  closed (including this morning's farmhouse roof-ridge run below), used a
  single Explore agent, given the full dead-end list, to find a fresh gap. It
  found `drawFolk()` (~line 364-366): Dot's skirt rect (`g.fillRect(x-1.5,
  legTop,4,LH)`) was the one piece of loose cloth on a person in the whole
  scene that never reacted to `wind` — every other cloth surface already does
  (VFW flag, dealership pennants, and the laundry line itself, which Dot
  herself hangs). A fresh category, distinct from the closed wind-sway-on-
  foliage and leg-scissor-gait families: cloth-on-a-mover, not a plant or a
  limb. Added a small horizontal sway term matching the flowerbed's minimal
  single-sine idiom (`Math.sin(rt*2+i2*1.7)*wind*0.015`) scaled slightly larger
  since a skirt hem swings more than a flower stem: `skSw=Math.sin(rt*1.7+
  x*0.3)*Math.min(0.6,wind*0.02)`, phased off Dot's own `x` so it doesn't
  lockstep with other wind-reactive elements, applied only to the skirt
  fillRect's x-offset. Zero new state, no timing, no Math.random — pure
  per-frame trig offset of existing globals (`rt`/`wind`), nothing to reset in
  synth/jumpSeason. Exercisable year-round, not gated to a season window.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean aside
  from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-11 — Picked up the explicit next-candidate note left by 2026-08-10 #4's
  barn ridge-cap run: the farmhouse roof itself (`draw()`, ~line 3301-3302, the
  peaked triangle `g.beginPath();g.moveTo(hx-2,HOR-14);g.lineTo(hx+12,HOR-22);
  g.lineTo(hx+26,HOR-14);g.closePath();g.fill()`) was still flat `S('#5b6070')`
  with no ridge sun-catch, even though `drawTown()`'s own miniature skyline
  replica of this exact house got its ridge highlight back on 2026-08-09 #3, and
  the barn right next to it got its ridge-cap highlight the day before this run.
  Since the roof is a path-fill triangle (not a rect like the barn's cap), placed
  a small 2×0.5px highlight strip 1px below the apex point rather than at the
  apex itself (width 0 there) — computed the triangle's width at that height by
  hand (~3.5px at y=HOR-21) to confirm a 2px-wide strip centered on the apex
  x (`hx+11`) stays safely inside both roof slopes with margin either side. Same
  `shA(mixA(C('#5b6070'),SNOW,0.22),0.5)` idiom used everywhere else in this
  block (steps/bench/flue/window frames/door), inserted right after the roof
  fill and before the door draw. This was the last open item in the "full-scale
  roofline sun-catch" thread the barn run opened — no candidates left there.
  Zero new state, no timing, no Math.random — pure static structural detail,
  nothing to reset in synth/jumpSeason. Smoke OK, both builds green (mac .saver
  + Windows .scr, dotnet clean aside from the pre-existing WindowsBase
  version-conflict warnings), kit+zip refreshed, index.html re-synced
  (byte-identical, verified). NOT committed (evolve doesn't commit/push itself —
  uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-10 #5 — With shadow/glint/two-tone/sun-catch/wind-sway/eye-pixels/ground-
  wear/fireflies/night-lighting/rain-splash/blite-sun-catch/bare-rgba-color-grading/
  rotating-mechanism/leg-scissor-gait/full-scale-roofline-sun-catch (this evening's
  barn ridge-cap run) all exhaustively closed, used a single Explore agent, given
  the full dead-end list, to find a fresh gap. It found `drawTown()`'s winter
  "somebody's stove is going" chimney puff (~line 2640, the miniature skyline house
  block): every other smoke source in the file — farmhouse chimney, tractor/combine/
  duster/train/parts-semi/hauler exhaust — is a real particle pushed into the shared
  `smoke` array that rises and leans downwind with `wind`, but this one draws two
  literal fixed-offset `fillRect` daubs at a constant `globalAlpha=0.22`, sitting
  static every frame it's gated on — a decal, not smoke. Rejected the Explore
  agent's literal suggestion of routing it through the shared `smoke` array: that
  array's particles grow to a 3px-radius circle by end of life, sized for full-scale
  farm sprites, and would visually dwarf this miniature skyline house (only a few
  px tall) — mismatched scale, not a fix. Instead added a cheap procedural drift:
  `sw=wind*0.025+Math.sin(rt*0.6+i)*0.12` nudges both daubs sideways (the higher,
  more-risen daub leans 1.8x further, mimicking a puff that's had more time to
  drift) plus a small independent vertical bob, phased by the building's own loop
  index `i` so different houses' wisps don't move in lockstep. Reuses only existing
  globals (`wind`, `rt`, `i`), no Math.random in draw code, zero new state, nothing
  to reset in synth/jumpSeason — same "augment in place" scope as the file's
  smallest polish passes. Winter-only (`seasonIdx===3`), so not exercisable in the
  current summer state but verified by reading the code path and confirmed correct
  by inspection; smoke.js's synthetic winter-HUD pass in the test suite exercises
  the surrounding draw() code without asserting on this specific pixel. Runner-up
  rejected: `drawSign`'s roadside post — a rigid board, wind-sway would look
  physically wrong, and it's already fully two-tone treated. Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html re-synced
  (byte-identical, verified). NOT committed (evolve doesn't commit/push itself —
  uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-10 #4 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing/sun-catch/wind-sway/eye-pixels/ground-wear/fireflies/night-lighting/
  rain-splash/blite-sun-catch/bare-rgba-color-grading/rotating-mechanism/leg-
  scissor-gait (this morning's drawWalker run) all exhaustively closed, used a
  single Explore agent, given the full dead-end list, to find a fresh gap. It
  found that the closed "roofline sun-catch" thread only ever covered the
  miniature drawTown() building-kind switch (house/water tower/church/shop/
  elevator skyline replicas) — the real, full-scale farmhouse and barn
  structures live outside that switch (drawn inline in `draw()`) and were
  never swept, even though the drawTown skyline's own miniature replica of
  this exact farmhouse already got its ridge highlight back on 2026-08-09 #3.
  Picked the barn (~line 3190, `P(bx+4,HOR-28,22,1,'#7a3434'); // ridge cap`)
  over the farmhouse roof triangle as the cleaner one-line fix — a flat rect
  vs. a path-fill triangle — and reused the idiom already established twice
  over in this exact function (the barn door's top/bottom two-tone, two lines
  below) rather than reaching for `blite` (which is local to drawTown's own
  scope and not reusable here). Added one line right after the ridge-cap
  fill: `shA(mixA(C('#7a3434'),SNOW,0.22),0.5)` on a 0.5px-tall strip spanning
  the cap. Farmhouse roof triangle noted as the next candidate (needs a small
  rect near the apex rather than a strip, since it's a path fill not a rect).
  Zero new state, no timing, no Math.random — pure static structural detail,
  nothing to reset in synth/jumpSeason. Smoke OK, both builds green (mac
  .saver + Windows .scr, dotnet clean aside from the pre-existing WindowsBase
  version-conflict warnings), kit+zip refreshed, index.html re-synced
  (byte-identical, verified). NOT committed (evolve doesn't commit/push
  itself — uncommitted work continues to span 07-22 onward across many runs;
  a review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-10 #3 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing/sun-catch/wind-sway/eye-pixels/ground-wear/fireflies/night-lighting/
  rain-splash/blite-sun-catch/bare-rgba-color-grading/rotating-mechanism (this
  morning's bike spoke run) all exhaustively closed, used a single Explore
  agent, given the full dead-end list plus every taste ruling, to find a fresh
  gap outside all of them. It found `drawWalker` (~line 2302, the generic
  pedestrian sprite used for dealership staff walking to/from parked cars,
  customers walking in/out of the showroom and on lot tours, and the service-
  drop-off customer): its two leg rects were pure flat static rectangles with
  zero phase animation, even though every caller moves `wx` continuously over
  several seconds (`wx+=Math.sign(target-wx)*7*vdt`) — the figure visibly
  glided across the lot instead of walking. `drawFolk` (line 342) has a full
  leg-scissor gait for the farm family, but `drawWalker` carries no step-timer
  state to reuse that idiom directly, so instead applied the cheaper
  ground-covered-phase idiom the file already uses for deer/rabbit gait
  (`Math.sin(deer.x*1.15)`/`Math.sin(rabbit.x*1.4)`): a small scissor offset
  `lsw=Math.sin(wx*0.9)*0.4` applied oppositely to the two leg rects. Since
  it's phased off the walker's own `wx` rather than a new persistent angle, it
  freezes automatically the instant `wx` stops changing (e.g. the service-drop
  customer standing at the dock) — same "freezes naturally" property as the
  bike spoke fix. Zero new global state, nothing to reset in synth/jumpSeason,
  no Math.random, no new timing window. Runner-up rejected: `drawHunter`
  (~line 205) has the identical rigid-leg gap during his walk-in/walk-out
  phases, but `drawWalker` was the stronger pick — three separate call sites,
  more frequently and visibly on screen (dealership lot traffic) than the
  duck-hunt event. Smoke OK, both builds green (mac .saver + Windows .scr,
  dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced (byte-identical,
  verified). NOT committed (evolve doesn't commit/push itself — uncommitted
  work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-10 #2 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing/sun-catch/wind-sway/eye-pixels/ground-wear/fireflies/night-lighting/
  rain-splash/blite-sun-catch/bare-rgba-color-grading all exhaustively closed
  (including this morning's grain-pour fix below), used a single Explore agent,
  given the full dead-end list, to find a fresh gap. It found `drawBike`
  (~line 2441-2458): the file has an established "rotating mechanism" idiom for
  things that actually spin while running — the tractor's rear wheel swings a
  spoke line off `wheelAng` (only advancing while moving, freezing the instant
  it stops) and the combine header reel rotates via `combine.reel` — but
  `drawBike`'s two wheels were pure flat-filled arcs with zero rotation cue,
  even though it already carries the same `moving` boolean semantics both call
  sites (`bikeRide`, VFW patron bikes) already pass correctly. Added a spinning
  spoke line per wheel, gated on the existing `moving` param, phased off the
  bike's own `x` (`wa2=x*0.6`) rather than a new persistent angle — so it
  freezes automatically the instant the bike stops moving (x stops changing),
  same visual effect as `wheelAng` freezing, with no new global to reset in
  synth/jumpSeason. Inserted right after the two wheel arc().fill() calls,
  before the frame fillRect. Zero Math.random, no new timing window,
  season-agnostic. Runner-up rejected: cloud-cast shadow patches drifting
  across the field (clouds already track x/y/s/v but nothing reacts to their
  position) — harder to keep small, risks growing past a few lines and treads
  close to the closed weather-easing/lighting families. Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward across
  many runs; a review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-10 — Picked up the explicit next-candidate note left by 2026-08-09 #5's
  puddle-grading run: the combine's grain-pour arc (`draw()`, ~line 3778, the
  stream of kernels flying from the combine's spout into the grain cart during
  `unld.ph==='pour'`) painted its 9 particle pixels as a bare literal
  `rgba(232,200,60,0.9)` while every other ground/particle surface in the scene
  is color-graded through the `mixA(...,GOLD,ghK)`/`mixA(...,NIGHT,(1-dl)*0.6)`
  chain (same family as the puddle fix, `S()`/`shA()` elsewhere). Since combine
  harvesting happens at any hour including full night, an undimmed golden-yellow
  stream flying between two correctly-darkened machines looked artificially lit.
  Fixed the exact same way as the puddle: computed the graded color inline
  (`shA`/`css` don't expose alpha control, needed here for the fixed `0.9` alpha)
  from `#e8c83c` (hex of the original 232,200,60 literal, pure like-for-like, no
  new tone) through `mixA` for golden-hour tint then night-mix, building the rgba
  string from the rounded channels, keeping alpha at `0.9` unchanged. Zero new
  state (per-frame recompute of existing `dl`/`ghK`), nothing to reset in
  synth/jumpSeason, no Math.random, no new timing window (same `unld.ph==='pour'`
  gate as before). Grepped every other raw `rgba(` fillStyle literal in `draw()`
  afterward: the rest are legitimately un-graded — emissive light sources
  (headlights, window glow, beacons, stars/sparks) that shouldn't dim with
  night-mix since they're the light, and shadow fills (`rgba(10,12,20,...)`)
  which are already dark by design — so this closes the "reflected/ambient
  surface color painted as a flat literal" thread specifically, not all rgba
  literals. Smoke OK, both builds green (mac
  .saver + Windows .scr, dotnet clean aside from the pre-existing WindowsBase
  version-conflict warnings), kit+zip refreshed, index.html re-synced
  (byte-identical, verified). NOT committed (evolve doesn't commit/push itself —
  uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-09 #5 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing/sun-catch/wind-sway/eye-pixels/ground-wear/fireflies/night-lighting/
  rain-splash/blite-sun-catch all exhaustively closed, used a single Explore
  agent, given the full dead-end list, to find a fresh gap. It found the
  puddle overlay itself (`draw()`, comment "rain leaves its mark for a while",
  ~line 3638): `puddleT>0.05` painted four ground patches as a bare literal
  `rgba(150,180,215,${0.28*puddleT})` while every other surface in the scene —
  soil, verge, buildings, even the water-tower band — is colored through
  `S()`/`shA()`, which mixes in `NIGHT` by `(1-dl)` and the golden-hour tint by
  `ghK`. Since puddles linger ~400s after any rain (day or night, `puddleT`
  decay per line ~1514) and the color never got graded, a night rain left four
  flat, undimmed pale-blue rectangles glowing on the ground looking
  artificially lit next to correctly-darkened buildings — a legible "missed
  the color-grading pass" gap, distinct from the closed shadow/glint/two-tone/
  night-lighting families (a ground surface never tinted at all, not a
  missing highlight/shadow pair or emissive light). Since `shA`/`css` don't
  expose alpha control (needed here for `puddleT`-scaled transparency),
  computed the graded color inline using `shA`'s own formula
  (`mixA(ghK>0.01?mixA(C('#96b4d7'),GOLD,ghK):C('#96b4d7'),NIGHT,(1-dl)*0.6)`)
  and built the rgba string from its rounded channels, keeping `0.28*puddleT`
  as the alpha exactly as before. `#96b4d7` is the same color as the original
  literal (150,180,215) converted to hex — pure like-for-like, no new visual
  tone introduced. Zero new state (pure per-frame recompute of existing
  globals `dl`/`ghK`), nothing to reset in synth/jumpSeason, no Math.random,
  no new timing window (same `puddleT` gate as before). Runner-up rejected:
  the combine's grain-pour arc (~line 3778) has the same bare-literal-color
  issue and is visible during night harvests, but it's a narrower, less
  consistently exercisable window than the puddle case. Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward across
  many runs; a review/commit/push/redeploy session remains genuinely
  overdue).
- 2026-08-09 #4 — Closed the one explicitly-flagged open item left in this file
  (noted by run #3 this morning): `drawTown()`'s `elev` (grain elevator) branch
  never used `blite` on its headhouse cap (~line 2641), unlike the water tower/
  church/shop/house siblings in the same building-kind switch, all of which
  already got a `blite` sun-catch accent on their topmost feature — the run #3
  note called this a weaker case than the house roof ridge (cap reads less
  obviously as a "sunlit edge") but it was still the last open asymmetry in the
  switch. Added the identical top-half-row highlight the house ridge/shop
  parapet use: `g.fillStyle=blite;g.fillRect(x+1,y-1,B.w-2,0.5);g.fillStyle=bcl;`
  right after the existing headhouse-cap fill, restoring `bcl` before the
  function moves on (matches every sibling branch's own restore-after-blite
  pattern). Season-agnostic, zero new state, no timing, no Math.random —
  nothing to reset in synth/jumpSeason. This closes the `blite` sun-catch
  sweep across every building kind in the switch — no more open items in this
  family. Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-09 #3 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing/sun-catch/wind-sway/eye-pixels/ground-wear/fireflies/night-lighting/
  rain-splash all exhaustively closed (per this file's history), used a single
  Explore agent, given the full dead-end list, to find a fresh gap. It found
  `drawTown()`'s per-building-kind switch (~line 2608-2654): the water tower,
  church, and shop building kinds all get a `blite` sun-catch accent on their
  topmost feature (`blite=shA(mixA(bA,C('#e8e6da'),0.3),0.5)`, defined once at
  the top of the function) — tank band, steeple, parapet cap respectively —
  but the `house` kind, almost certainly the most numerous building in the
  skyline, draws its peaked roof entirely in the flat wall tone (`bcl`) with
  no light-catch at all. The roof ridge is the most naturally lit surface on
  the silhouette, the same "peak catches last light" idea as the church
  steeple's own inline comment. Added one line right after the roof-triangle
  fill: `g.fillStyle=blite;g.fillRect(x+B.w/2-1,y-2,2,0.5);g.fillStyle=bcl;`
  — a small highlight centered on the ridge apex, then restoring `bcl` before
  the existing chimney/stove-smoke logic below it (which expects `bcl` as the
  active fillStyle). Season-agnostic (runs year-round), zero new state, no
  timing, no Math.random — pure static structural detail, nothing to reset in
  synth/jumpSeason. Runner-up rejected: the `elev` (grain elevator) branch
  also never touches `blite` on its headhouse cap, but elevators are rarer in
  the skyline and the cap reads less obviously as a "sunlit edge" than a roof
  ridge — noted here as the next candidate if this thread reopens. Smoke OK,
  both builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-09 #2 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing/sun-catch/wind-sway/eye-pixels/ground-wear/fireflies/night-lighting/
  rain-splash all exhaustively closed (per this file's history, including this
  morning's rain-splash run), used a single Explore agent, given the full
  dead-end list, to find a fresh gap. It found Dot's flowerbed (`draw()`
  farmhouse block, ~line 3347-3353): three single-pixel blooms drawn as a
  static `for` loop, the one foliage detail left unswayed while everything
  else touching wind nearby already moves with it — the lawn grass blades a
  few lines later (`sway=Math.sin(rt*2+gx2)*wind*0.015`), the laundry line
  hanging just above, crop rows, dealership pennants, and the VFW flag. The
  "wind-sway on foliage" thread (2026-08-08) only ever covered mature trees
  and grove saplings, leaving this small bed — sitting between two other
  wind-reactive elements — as the static outlier. Added the identical sway
  formula (phased per bloom via `i2*1.7` so the three don't move in lockstep)
  applied only to the bloom pixel's x-offset, leaving the stem pixel fixed —
  same "anchor fixed, tip free" idiom the laundry hems and grass blades use.
  Zero new state, no timing, no Math.random — pure per-frame trig offset of
  existing globals (`rt`/`wind`), nothing to reset in synth/jumpSeason.
  Runner-ups rejected: the hot-air balloon and fair tents are both flat-filled
  with no two-tone shading, but they read as intentionally minimal "distant
  sky prop"/structure-class objects rather than a foliage/motion sibling with
  a nearby animated counterexample — weaker asymmetry case than the flowerbed.
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean aside
  from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-09 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-easing/
  sun-catch/wind-sway/eye-pixels/ground-wear/fireflies/night-lighting all
  exhaustively closed (per this file's extensive history), used a single Explore
  agent, given the full dead-end list, to hunt outside the sprite-detail families
  entirely — this time landing on a weather particle rather than a static sprite.
  It found the rain `drops` particle system (spawn ~line 1653, motion/removal
  ~line 1668, draw ~line 3853): every drop is a silent 1x3px streak that gets
  filtered out of the array the instant `p.y>=p.gy` with zero terminal detail —
  the one moving weather/impact effect in the file with no payoff at the moment
  it lands, unlike the sprinkler droplet (catches sun at arc peak) or the
  lingering puddle overlay (rain "leaves its mark for a while"). Added a
  `splashes` particle array using the exact `debris`-style life/max fade idiom
  already established elsewhere in the file: on drop removal (`p.y>=p.gy`) push
  `{x:p.x,y:p.gy,life:0.15,max:0.15}`; splashes filtered down each frame same as
  `debris`; drawn as a tiny expanding-and-fading stroked ring
  (`rgba(200,222,245,...)`, radius `(1-life/max)*1.6`, alpha `0.5*(life/max)`)
  right after the rain streak draw, reusing the existing `TAU` constant. Declared
  `splashes=[]` alongside `drops` (line 529) and reset it in both `synthSeason()`
  and `jumpSeason()` right next to the existing `drops=[]` reset, matching that
  convention exactly. Zero Math.random in draw code (spawn condition is purely
  positional, `p.y>=p.gy`, not randomized); no new timing window needed since
  life/max is a self-expiring particle lifespan, same class as `debris`/`smoke`,
  not a daily-roll/exit-roll event. Smoke OK, both builds green (mac .saver +
  Windows .scr, dotnet clean aside from the pre-existing WindowsBase
  version-conflict warnings), kit+zip refreshed, index.html re-synced
  (byte-identical, verified). NOT committed (evolve doesn't commit/push itself —
  uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-08 #5 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing/sun-catch/wind-sway/eye-pixels/ground-wear/fireflies/night-lighting all
  exhaustively closed (per this file's own extensive history, including today's
  four earlier runs), used a single Explore agent, given the full dead-end list
  plus every "do not re-add" taste ruling, to scan both named `draw*` functions
  and the inline `draw()` detail blocks (the file's repeated historical blind
  spot) for a fresh gap. It found the barn's sliding door itself (inline
  `draw()` barn block, ~line 3183-3189): the panel gets an X-brace stroke,
  hinge dots, and a latch highlight, but the door fill (`P(ddx,HOR-9,8,9,
  '#6e2a2a')`) was still flat — the one door left in the whole file without
  the `shA(mixA(...))` top-highlight/bottom-shadow two-tone pair every other
  door (farmhouse, VFW, dealership man-door) already carries. Confirmed via
  grep that the entire barn block (lines 3166-3232) had only one `shA(mixA`
  hit (the loft cobweb stroke) — genuinely missed by every prior door pass
  since it's inline, not a named function. Added the identical two lines,
  mirroring the farmhouse door idiom exactly (`shA(mixA(C('#6e2a2a'),SNOW,
  0.22),0.5)` top row / `shA(mixA(...,NIGHT,0.28),0.55)` bottom row), inserted
  right after the door fill and before the existing X-brace stroke so the
  brace still draws on top unaffected — same "augment then move on" layering
  slot every other two-tone pass in this file uses. Used `ddx` (the door's
  own sliding x-position) for both new rows so the highlight/shadow track the
  door correctly whether closed, ajar, or fully open. Zero new state, no
  timing, no Math.random — pure static structural detail, nothing to reset in
  synth/jumpSeason. Runner-up (briefly considered, not picked): the barn's
  main wall plane and ridge cap are also flat, but they're larger multi-
  detail surfaces already carrying plank lines + eave shadow, less directly
  comparable to a sibling gap than the isolated door panel. Smoke OK, both
  builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-08 #4 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing/sun-catch/wind-sway/eye-pixels/ground-wear/fireflies all exhaustively
  closed (per this file's own extensive history, including today's three
  earlier runs), used a single Explore agent, given the full dead-end list, to
  find a genuinely fresh gap outside all of those families. It found a night-
  lighting asymmetry: `drawCar` (~line 2280) and `drawTruckSm` (~line 2451)
  both paint a `dl<0.3` headlight/taillight glow, but `drawSemi` (~line 2306,
  the parts-delivery semi and equipment-hauler sprite) and `drawPickup` (~line
  2715, the for-sale-lot truck) — the file's two largest, most prominent road
  vehicles, both of which already flip their windshield glass to night-amber
  at `dl<0.4` and already carry the daylight cab-glint — never paint any
  lights at all after dark. Added the identical treatment to both, reusing
  two already-established idioms together rather than inventing a new one:
  `drawTruckSm`'s headlight dot+forward-halo cone
  (`rgba(255,230,150,0.7)`/`rgba(255,230,150,0.08)`) plus `drawCar`'s red
  taillight dot (`rgba(255,90,80,0.7)`), positioned at each vehicle's own
  front-cab edge (headlight) and rear-most body edge (taillight, `drawSemi`
  at the trailer's far corner x=-27.6, `drawPickup` at the bed's rear edge
  x=-7.6), same y-height for both lights within each function so they read as
  bumper-level running lights. `drawSemi`'s block placed after the exhaust
  stack (so the taillight paints over the flatbed/van rear edge as a corner
  marker light, not underneath it); `drawPickup`'s placed right after its
  glint block, both immediately before `g.restore()`. Zero new state, no
  timing convention (gated on the existing `dl` global, not a new
  window+expiry+spread event), no Math.random — nothing to reset in
  synth/jumpSeason. Smoke OK, both builds green (mac .saver + Windows .scr,
  dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced (byte-identical,
  verified). NOT committed (evolve doesn't commit/push itself — uncommitted
  work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-08 #3 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing/sun-catch/wind-sway/eye-pixels/ground-wear all exhaustively closed (per
  this file's own extensive history, including today's earlier two runs), used a
  single Explore agent, given the full dead-end list, to find a genuinely fresh
  gap: the county-fair Ferris wheel (`drawTown`, ~line 2692-2711, live Aug 20-27
  each year via the `fair` date gate) runs at all hours of its live window but
  had zero night treatment — every other night-active structure in the scene
  (town-building windows a few lines above via `dl<0.4&&frac>=0.99`, the crane's
  night beacon just below via `dl<0.25`, the VFW/farmhouse window glows) gets a
  lit state after dark, but the wheel's 5 gondola dots rendered in the same flat
  colors at 2am as at 2pm. Added a small warm glow circle behind each gondola dot,
  gated `dl<0.4` (matching the town-window gate two lines above it, not a new
  eased global — that idiom is reserved for fixing pre-existing pop bugs on
  gates that already existed pre-fix, and the VFW window glow at line 2500 sets
  the "plain instant dl threshold, no easing" precedent for a brand-new light
  state). Reused the file's existing warm-window color family
  (`rgba(255,214,128,...)`, already used twice earlier in the same function) at
  glow alpha 0.35, radius 1.4 — small and subtle to match "keep it calm". Also
  refactored the loop to compute `gx2/gy2` once instead of calling
  `Math.cos(a3)`/`Math.sin(a3)` twice per iteration (was duplicated across the
  stroke-line loop above and the fill loop; left the stroke loop as-is since it's
  a separate loop, only cleaned up the fill loop it touched). Zero new global
  state, no timing convention needed (gated on existing `dl`, not a new
  window+expiry+spread event), no Math.random — nothing to reset in
  synth/jumpSeason. Smoke OK, both builds green (mac .saver + Windows .scr,
  dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced (byte-identical, verified).
  NOT committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-08 #2 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing all exhaustively closed (per this file's own extensive history,
  including this morning's hay baler run), used a single Explore agent, given
  the full dead-end list, to find a genuinely fresh category outside all of
  them. It found Earl's "one more tree each spring" grove of saplings
  (`draw()`, ~line 3362-3375): the canopy circles are drawn dead static, never
  offset, while every mature tree in `TREES` — drawn via `tree(x,big)` (line
  1844) just a few lines below the grove block — already "breathes in the
  wind" via a `swx=Math.sin(rt*0.7+x*0.31)*Math.min(1.1,wind*0.028)` sine
  offset applied to its canopy circles (line 1860). The grove already shares
  `tree()`'s sun-catch highlight idiom (shipped 2026-08-07 #3) but never
  picked up the wind-sway that same sibling function has always had — a fresh
  animation gap, not shading/shadow/glint/exhaust/easing/glint-sweep/plaque/
  critter-life. Added the identical `swx` formula (`swx4`) scaled by the
  sapling's own canopy radius fraction (`cr/6`, since saplings are much
  smaller than a mature tree's canopy) and applied it as an x-offset to both
  the base canopy circle and the sun-catch highlight circle, keeping them in
  sync with each other exactly as `tree()`'s own base/highlight pair move
  together. Confirmed `rt`/`wind` are both valid globals in scope at this
  point in `draw()` (same globals `tree()` itself reads, not locals). Zero new
  state, no timing, no Math.random — pure per-frame trig offset of existing
  globals, nothing to reset in synth/jumpSeason. Smoke OK, both builds green
  (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward across
  many runs; a review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-08 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-easing
  all exhaustively closed (per this file's extensive history), used a single
  Explore agent, given the full dead-end list, to find a fresh gap. It found
  `drawBaler` (~line 2251): the towed hay baler's round chamber (two flat solid-
  color arcs, `#3e7d3b` outer / `#2f5f2e` inner, no shading at all) was the one
  implement body in the green-implement cluster (drawSprayer, drawPlanter,
  drawCart all sit in the same code block) that never got the `shA(mixA(...))`
  treatment its siblings already carry. Baler is a real recurring sprite, not
  background dressing — drawn full-scale as a live towed implement during
  `tm==='bale'` passes and again as trailer cargo via `drawLoad`. Added a small
  offset highlight circle (`shA(mixA(C('#3e7d3b'),SNOW,0.22),0.5)`, radius 1.1,
  centered upper-left of the outer chamber center) between the outer-arc fill
  and the darker inner-ring fill, same "small light-catch dot on a round green
  body" idiom the grove-sapling canopy sun-catch established — the darker inner
  ring still draws on top after it, so only a thin crescent of the highlight
  survives at the chamber's outer rim, reading as a rim glint rather than a flat
  dot. Zero new state, no timing, no Math.random — pure static structural
  detail, nothing to reset in synth/jumpSeason. Smoke OK, both builds green
  (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html
  re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward across
  many runs; a review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-07 #4 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing all exhaustively closed (three earlier runs today), used a single
  focused Explore agent (given the full dead-end list) to find a fresh gap.
  It found `drawTruck` (the crop-fire water truck, ~line 2579-2582): the
  hose-bed box gets the full two-tone treatment (`shA(mixA(...))` roof
  highlight + undercarriage shade) two lines apart, but the very next shape in
  the same function — the cab box (`P(4,-11,6,7,'#b23430')`) — was left flat,
  the one unshaded rect in an otherwise fully two-toned draw call. Added the
  matching roof-highlight row (`shA(mixA(C('#b23430'),SNOW,0.22),0.5)` on the
  cab's top row), same idiom as the hose-bed line right above it, inserted
  before the window fill so the glass still draws on top unaffected. Zero new
  state, no timing, no Math.random — pure static structural detail, nothing to
  reset in synth/jumpSeason. Smoke OK, both builds green (mac .saver + Windows
  .scr, dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced (byte-identical,
  verified). NOT committed (evolve doesn't commit/push itself — uncommitted
  work continues to span 07-22 onward across many runs; a review/commit/push/
  redeploy session remains genuinely overdue).
- 2026-08-07 #3 — With shadow/glint/two-tone/exhaust-puff/wx-easing/light-bloom-
  easing all exhaustively closed (per this file's own extensive history,
  including today's earlier two runs finishing the exhaust-puff thread), used a
  single Explore agent with an explicit list of dead-end categories to hunt for
  a genuinely fresh gap. It found Earl's farmhouse grove — the "one more tree
  each spring" sapling block (`drawTown`, ~line 3358-3368) — draws each young
  tree's canopy as a single flat `circle(sx4+0.5,HOR-hh-1.5,...,shA(spal('tree'),
  0.68))`, a bespoke simplified reimplementation of the shared `tree()` function
  (line 1844) used for every other tree in the file (including the town/verge
  canopies). `tree()` always adds a small sun-catch highlight blob
  (`circle(x-2*s+swx,HOR-15*s,2.4*s,shA(mixA(spal('tree'),C('#f4f0d8'),0.22),
  0.68))`) that the grove's saplings never got — the one foliage sprite in the
  file left flat while its sibling has carried this exact treatment for a
  while, and it's exercisable right now (August, `seasonIdx!==3` gate is true
  in summer). Added a second circle after the base canopy fill, same idiom,
  scaled to each sapling's own age-based radius (`cr`) with the highlight
  offset toward the upper-left at 30% of that radius and sized at 40% of it —
  proportionally smaller than `tree()`'s own highlight since these saplings are
  much smaller than a full tree. Zero new state, no timing, no Math.random —
  pure static structural detail (the canopy radius already varies with `age`,
  an existing global), nothing to reset in synth/jumpSeason. Smoke OK, both
  builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-07 #2 — Picked up the explicit next-candidate note left by the prior run
  today: the equipment-haulers loop (`for(const h of haulers)`, ~line 1329) draws
  the same `drawSemi` asset as the parts-delivery semi (which just got a diesel
  exhaust puff) but never puffs, despite having 3 moving phases across its two
  spawn kinds (non-drop: bay-loading ph0 then departure ph1; drop-off: approach
  ph0, dock ph1, departure ph2). Derived the "rolling" condition per kind: for
  non-drop haulers only `h.ph===1` is actual x-movement (ph0 is stationary
  loading at the bay); for drop-off haulers x moves in every phase except the
  stationary dock (`h.ph===1`), so rolling is `h.ph!==1` — the two kinds are
  mirror images of each other on which phase index is the stationary one.
  Traced `h.dir` through both spawn sites and confirmed it equals the true
  direction of x-travel for the entire lifecycle in both kinds (drop-off's
  ph0 uses `Math.sign(dock-h.x)` for its target-seek, but the spawn x/dock
  combination always makes that sign match `h.dir`, so a straight `h.dir` read
  is safe throughout). Added one line after the per-hauler movement branch,
  before the closing loop brace: `Math.random()<0.25` per-frame puff gated on
  the derived rolling condition and `vdt>0`, computing the stack's world
  position from `drawSemi`'s own transform exactly as the parts semi did
  (`h.x-h.dir*1.7, 110`) but with `vx:-h.dir*4` instead of the parts semi's
  fixed `vx:4`, since haulers travel both directions (parts semi only ever
  moves left, so its trailing-smoke `vx` was a constant; haulers need the
  smoke to trail opposite whichever way `h.dir` actually points). Same
  diesel-grey `'#8b8f99'`/`vy:-5`/`life:0.8`/`max:0.8` as the train/parts-semi
  precedent. Zero new state (reuses the `smoke` array), nothing to reset in
  synth/jumpSeason, `Math.random()` only inside this update()-time spawn call
  (not draw code), matching established precedent. This closes the
  equipment-haulers exhaust gap the prior run flagged — every diesel mover in
  the file (tractor, combine, crop duster, freight train, parts semi, haulers)
  now puffs while rolling. Smoke OK, both builds green (mac .saver + Windows
  .scr, dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced (byte-identical,
  verified). NOT committed (evolve doesn't commit/push itself — uncommitted
  work continues to span 07-22 onward across many runs; a review/commit/push/
  redeploy session remains genuinely overdue).
- 2026-08-07 — With the shadow/glint/two-tone/bloom-easing/wx-easing families all
  exhaustively re-verified closed (per this file's extensive recent history), used
  a single Explore agent to hunt for a genuinely new category of gap rather than
  re-treading one of those five. It found the dealership's parts-delivery semi
  (`drawSemi`, ~line 2338) fully models an exhaust stack (a P() pipe plus a
  highlight cap) but never puffs from it — unlike every other diesel mover in the
  file (tractor, combine, crop duster, freight train) which all already spawn into
  the shared `smoke` array while running. Computed the stack's exact world position
  from `drawSemi`'s own transform (`translate(x,y);scale(dir*s,s)`, stack at local
  `-1.7,-16`, and the parts semi is always drawn `y:126,s:1,dir:-1`) → world
  `(partsT.x+1.7, 110)`. Added one line right after the partsT movement block
  (~line 1311 in `update()`): a `Math.random()<0.25` per-frame puff gated on
  `partsT.ph!==1` (no puff while docked/idle, only while rolling) and `vdt>0`,
  pushing the exact train idiom verbatim — same diesel-grey `'#8b8f99'`, same
  `vy:-5`/`life:0.8`/`max:0.8` — with `vx:4` trailing the smoke behind the truck's
  constant leftward travel (mirrors the train's own `dir*-4` sign convention).
  Deliberately scoped to just the single parts semi, not the equipment-haulers
  loop (same `drawSemi` asset, 3 separate moving branches) — kept this one clean
  insertion rather than a sprawling multi-branch edit; the haulers' exhaust is a
  natural, well-scoped next candidate, noted below. Zero new state (reuses the
  existing `smoke` array), no timing convention involved (this is a per-frame VFX
  spawn like the train's, not a daily-roll event), nothing to reset in
  synth/jumpSeason. `Math.random()` used only inside this spawn call, matching the
  train/tractor/combine's own established precedent for smoke-particle spawns —
  not in draw code. Smoke OK, both builds green (mac .saver + Windows .scr, dotnet
  clean aside from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session remains genuinely
  overdue).
- 2026-08-06 #5 — With the idea backlog's polish bucket confirmed genuinely empty
  (closed by run #4's silo dome glint earlier today) and manual re-treads of
  shadow/glint/two-tone/wx-easing/light-bloom-easing/ground-wear exhausted, used
  a single Explore agent to scan every `draw*` function plus inline detail
  blocks for a fresh gap. It found `drawPlanter` (~line 2237, the planter
  implement towed behind the tractor during `plant` tasks): its four seed-
  hopper boxes (`P(-15+i2*2.7,-9,2,3,'#3e7d3b')`) were pure flat fills, while
  its immediate siblings in the same implement cluster — `drawSprayer` just
  above and `drawCart` just below, both using the identical `#3e7d3b` green —
  already carry the full `shA(mixA(...))` roof-highlight/undercarriage-shade
  sandwich. `drawPlanter` was the one implement in that cluster nobody had
  touched. Added a highlight-only top row per hopper box (each box is only 3px
  tall, same "too thin for a full sandwich" precedent `drawMower`'s deck and
  `drawBike`'s frame already use) — `shA(mixA(C('#3e7d3b'),SNOW,0.22),0.5)`
  looped across all four boxes' top rows, inserted between the hopper fill
  loop and the wheel-arc draws, same "augment then move on" layering slot
  every other two-tone pass in this file uses. Zero new state, no timing, no
  Math.random — pure static structural detail, nothing to reset in
  synth/jumpSeason. Considered the agent's runner-up (October pumpkins, also
  flat) but they're Oct-only and the planter is exercisable in the current
  summer state (planting season). Smoke OK, both builds green (mac .saver +
  Windows .scr, dotnet clean aside from the pre-existing WindowsBase
  version-conflict warnings), kit+zip refreshed, index.html re-synced
  (byte-identical, verified). NOT committed (evolve doesn't commit/push
  itself — uncommitted work continues to span 07-22 onward across many runs;
  a review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-06 #4 — Closed the one explicitly-flagged open item left in the idea backlog
  (flagged 2026-08-05 #4 and again 2026-08-06 #3): the silo dome cap (~line 3222,
  `circle(SILOX+4.5,HOR-26,4.5,S('#c3c9d4'))`) was a flat single-tone dome, noted
  as needing a moving-light sun-glint pass rather than a static two-tone edit — the
  "physical peak of a continuous cycle" idiom the windmill blade flash (`dl>0.5`,
  peaks when `k=-Math.sin(a)>0.82`) and sprinkler droplet (peaks at `ph` near 0.45)
  already use. Built the silo's version from the same family: a 1px sun-glint pixel
  that sweeps horizontally across the dome face as `tod` crosses `noonTod` (the
  file's own solar-noon global), `sgx=clamp((tod-noonTod)*10,-3,3)` staying inside
  the dome's ~9px width, with brightness peaking at solar noon and fading toward the
  daylight edges (`sgp=clamp(1-Math.abs(tod-noonTod)/(dayFrac*0.5),0,1)`, using the
  day's own `dayFrac` so it scales correctly across seasons instead of a fixed
  window). Gated on `dl>0.5`, same threshold the windmill flash uses. Reused the
  windmill flash's own glint color (`rgba(255,252,224,...)`) rather than inventing a
  new one. Placed directly after the dome circle draw and before the existing
  snow-cap overlay, so snow still paints on top unaffected — same "augment then move
  on" layering slot every other pass in this block uses. Zero new persistent state
  (pure function of existing globals `tod`/`dl`/`noonTod`/`dayFrac`, recomputed every
  frame), nothing to reset in synth/jumpSeason, no Math.random. Removed the backlog's
  own "still open" note now that it's shipped — the idea list's polish bucket is
  genuinely empty again. Smoke OK, both builds green (mac .saver + Windows .scr,
  dotnet clean aside from the pre-existing WindowsBase version-conflict warnings),
  kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains genuinely
  overdue).
- 2026-08-06 #3 — With shadow/glint/two-tone/wx-easing/light-bloom-easing/ground-wear/
  silo-ladder/cow-eye/interior-hay-bale/for-sale-sign/picnic-basket/farmhouse-door all
  closed (the latter shipped earlier today), instead of spawning another Explore
  sweep, picked up an explicit unclaimed runner-up this file's own history had
  already flagged twice and never shipped: the roadside mailbox body (`drawMailbox`,
  line 1969) already has an undercarriage-style shade line on its bottom row
  (`fillRect(x-2,HOR-7,5,1)`, darker `#3a4552`) but no matching top highlight — the
  textbook half-done asymmetry this file's two-tone idiom keeps catching elsewhere
  (drawCar's roof/rocker, drawTruckSm's cab). Added the missing highlight row —
  `shA(mixA(C('#54677a'),SNOW,0.22),0.5)` fillRect across the box's top row
  (`x-2,HOR-9,5,1`) — inserted between the box body fill and the existing shade
  line so the sandwich reads fill→highlight→shade, same layering slot every other
  two-tone pass in this file uses. Verified no clash: the post (drawn just before,
  narrower and lower) is occluded under the box as it already was; the snow-cap
  overlay sits one row above (HOR-10) and the flag sits well above that (HOR-12) —
  neither touches the new row. Zero new state, no timing, no Math.random — pure
  static structural detail, nothing to reset in synth/jumpSeason. Deliberately
  skipped the VFW's own "small unmarked plaque" (2px tall, intentionally
  mysterious per its own comment — adding shading risks reading as a change to an
  easter egg, not a polish) and the silo dome cap sun-catch (a moving-light effect,
  more scope than a one-line highlight) — both still open, noted below. Smoke OK,
  both builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward across
  many runs; a review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-06 #2 — With shadow/glint/two-tone/wx-easing/light-bloom-easing/ground-wear/
  silo-ladder/cow-eye/interior-hay-bale/for-sale-sign/picnic-basket all closed (the
  latter shipped earlier today), used a single Explore agent to scan for a fresh
  gap. It found the farmhouse door itself (`draw()` farmhouse block, line 3260,
  `P(hx+10,HOR-7,4,7,'#7a6046')`) was still a flat fill — the one holdout in its
  own block, since the steps, bench, chimney flue, and window frames right around
  it (lines 3262-3282) all already carry the `shA(mixA(...))` two-tone idiom, and
  other buildings' doors (dealership man door, VFW door) explicitly cite the
  farmhouse in their own code comments as the pattern's origin, even though the
  farmhouse door never actually got the treatment itself. Added the identical
  top-highlight/bottom-shadow pair (`shA(mixA(C('#7a6046'),SNOW,0.22),0.5)` on the
  door's top row, `shA(mixA(...,NIGHT,0.28),0.55)` on its bottom row), inserted
  after the door fill and before the existing knob-highlight pixel (mid-door, well
  clear of both new rows) — same "augment then move on" layering slot every other
  pass in this block uses. Zero new state, no timing, no Math.random — pure static
  structural detail, nothing to reset in synth/jumpSeason. No runner-up worth
  logging — the agent checked hillCows/deer/rabbit for a missing eye pixel but
  those sprites are explicitly below the legibility floor for it per an existing
  comment, so that omission is intentional. Smoke OK, both builds green (mac
  .saver + Windows .scr, dotnet clean aside from the pre-existing WindowsBase
  version-conflict warnings), kit+zip refreshed, index.html re-synced
  (byte-identical, verified). NOT committed (evolve doesn't commit/push itself —
  uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-06 — With shadow/glint/two-tone/wx-easing/light-bloom-easing/ground-wear/
  silo-ladder/cow-eye/interior-hay-bale/for-sale-sign all exhaustively closed (per
  this file's own extensive recent history), used a single Explore agent to scan
  the inline `draw()` body — the file's historical blind spot — for a fresh gap
  outside all of those families. It found the picnic-blanket scene (`yardPlay`
  block, ~line 3507-3511): the blanket right there already gets a lighter-check
  overlay pixel row, but the picnic basket beside it (`P(YARDX+4,121,3,2,
  '#a5763c')`) was a single flat fill with zero shading — the clearest local
  asymmetry in the block, confirmed via grep that `#a5763c` appears nowhere else
  in the file. Added one line — `shA(mixA(C('#a5763c'),SNOW,0.22),0.5)` fillRect
  across the basket's top row (`YARDX+4,121,3,1`) — a lid-catching-the-light
  highlight, same idiom the farmhouse steps/bench/chimney/window-frames use,
  highlight-only since the basket is only 2px tall (same "too thin for a full
  sandwich" precedent the bike frame used). Placed directly after the basket
  fill, same "augment then move on" layering slot. yardPlay isn't season-gated
  (year-round family activity), so this is exercisable in the current summer
  state, not an out-of-season addition. Zero new state, no timing, no
  Math.random — pure static structural detail, nothing to reset in
  synth/jumpSeason. Considered the Explore agent's runner-up (the October
  pumpkins, `octo` block, also fully flat) but it's Oct-only and the basket fit
  the current season better. Smoke OK, both builds green (mac .saver + Windows
  .scr, dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced (byte-identical, verified).
  NOT committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue).
- 2026-08-05 #5 — With shadow/glint/two-tone/wx-easing/light-bloom-easing/ground-wear/
  silo-ladder/cow-eye/interior-hay-bale all exhaustively closed (four runs already
  today per this file's own history), used a single Explore agent to scan for a
  fresh gap. It found `drawSign` (~line 2379) — the small roadside "for sale"
  yard sign drawn 3× (farmhouse, VFW, dealership lot) as part of the sagaSigns
  story beat: the 4×3 panel fill (`fillRect(x,y-6,4,3)`) was a single flat
  off-white with no highlight/shade edge at all, unlike every other panel
  surface in the file (roofs, doors, window frames) which already carry the
  `shA(mixA(...))` two-tone idiom — confirmed via grep that `drawSign` had zero
  `shA(` hits nearby. Added the identical top-highlight/bottom-shadow pair
  (`shA(mixA(C('#e8e6de'),SNOW,0.22),0.5)` on row y-6, `shA(mixA(...,NIGHT,0.28),
  0.55)` on row y-4), inserted directly after the panel fill and before the grey
  stripe/red "sold" pixels so those still draw on top unaffected. Zero new
  state, no timing, no Math.random — pure static structural detail, nothing to
  reset in synth/jumpSeason. Considered the Explore agent's runner-up
  (`drawMailbox` body lacking a lid highlight) but the sign was the stronger
  pick — visible 3× across the map vs. the mailbox's single instance. Smoke OK,
  both builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session remains genuinely
  overdue, now 5 runs deep today alone).
- 2026-08-05 #4 — With shadow/glint/two-tone/wx-easing/light-bloom-easing/ground-wear/
  silo-ladder/cow-eye all exhaustively closed (this file's own recent history, three
  runs already today), used a single Explore agent to scan for a fresh gap outside
  all of those families. It found the interior hay bale inside the barn's sliding
  door (inline `draw()` barn block, ~line 3158): `P(bx+12,HOR-3,4,3,'#7a5a34',0.4)`
  — a single flat-color rect, the one bale in the file with no two-tone treatment,
  unlike the round bales stacked outside the barn 40 lines later
  (`circle(...,S('#c2a24e',0.5))` outer straw / `circle(...,S('#8a6a3c',0.5))` darker
  inner core). Not the vehicle/door/window `shA(mixA(...))` panel-shading family —
  a distinct "bale" idiom the interior bale alone had missed. Added one line —
  `P(bx+13,HOR-1,2,1,'#5a4324',0.4)` — a small darker twine-binding strip across the
  bale's bottom edge, same `P()` helper and shading factor (0.4) as the existing
  bale line so it reads as one consistent draw, echoing the outdoor bales' two-tone
  language without literally reusing their circle-based idiom (this bale is a
  rectangle, not a round bale). Zero new state, no timing, no Math.random — pure
  static structural detail, nothing to reset in synth/jumpSeason. Considered the
  Explore agent's runner-up (silo dome cap sun-catch) but it risked overlapping the
  already-closed general highlight/glint sweeps, so the bale was the cleaner, more
  clearly-fresh pick. Smoke OK, both builds green (mac .saver + Windows .scr, dotnet
  clean aside from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session remains genuinely
  overdue, now 4 runs deep today alone).
- 2026-08-05 #3 — With shadow/glint/two-tone/wx-easing/light-bloom-easing/ground-wear/
  silo-ladder all exhaustively closed (this file's own recent history, including
  today's earlier two runs), used a single Explore agent to scan for a fresh gap
  outside all of those threads. It found `drawCow` (~line 2133): every cow on the
  hillside gets a fully detailed body — scissoring legs, angled neck, tail flick,
  Hereford face-blaze — but the head itself was a flat color rect with no facial
  detail at all. No animal in the file has an eye pixel except the kaiju (which
  uses its own gold color, not applicable here) — a genuine asymmetry against the
  deer's throat patch and rabbit's tail, which each get some head-region mark.
  Added a single dark eye pixel (`S('#1a1712',0.6)`, a plain near-black distinct
  from both the Angus and Hereford leg colors so it doesn't read as breed-specific)
  to both the graze and alert head branches, sized off the herd's own existing `s`
  scale and positioned near the front-top of each head rect (`x+D*6.5*s,y-1.6*s`
  graze; `x+D*6.3*s,y-8.6*s` alert) so it flips sides correctly with `D` (facing
  direction) and layers after the Hereford blaze fill in each branch. Two lines
  total, reuses the function's own shading idiom (`S(color,0.6)`, same factor as
  the legs/body), zero new state, no timing, no Math.random — nothing to reset in
  synth/jumpSeason. Considered the runner-up (duck/goose eye) but ducks are small
  and fast-moving on screen, unlike the cow which is large, static, and lingers
  at grazing distance — weaker payoff. Smoke OK, both builds green (mac .saver +
  Windows .scr, dotnet clean aside from the pre-existing WindowsBase
  version-conflict warnings), kit+zip refreshed, index.html re-synced
  (byte-identical, verified). NOT committed (evolve doesn't commit/push itself —
  uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session remains genuinely overdue).
- 2026-08-05 #2 — With the shadow/glint/two-tone/wx-easing/light-bloom-easing/ground-wear
  families all exhaustively re-verified closed (this file's own recent history, plus
  earlier today's gravel-rut run), used a single Explore agent to scan for a fresh gap
  outside all of those rather than re-treading one. It found the silo's "ladder"
  (`drawTown`'s ground-props block, ~line 3212): a single flat 1px vertical stroke at
  0.45 alpha, explicitly commented `// ladder rail`, with nothing rung-like about it —
  it read as a seam or drainpipe, not a ladder. Not shadow/glint/two-tone work (a bare
  stroke, not a shaded panel), so a genuinely fresh angle. Added 5-6 short horizontal
  tick marks crossing the rail at fixed 3.5px intervals (`for(let ry=HOR-20;ry<HOR-1;
  ry+=3.5)g.fillRect(SILOX+5.7,ry,3,0.8)`), same color/alpha (`S('#5a6070')`,0.45) and
  globalAlpha-wrap idiom as the existing tread-mark loop two lines above it, placed
  directly after the rail fillRect and before the dome. Purely static geometry keyed
  off existing `HOR`/`SILOX` constants — zero new state, no timing, no Math.random,
  nothing to reset in synth/jumpSeason (same class as the gravel ruts). Considered the
  Explore agent's two runner-ups (silo dome flat fill, VFW plaque flat fill) but the
  ladder was the strongest fit — round dome risked bleeding into the two-tone family's
  scope, plaque had the lowest visual payoff. Smoke OK, both builds green (mac .saver +
  Windows .scr, dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced (byte-identical, verified). NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-05 — With the shadow audit, glass-glint sweep, two-tone body-shading, wx-easing,
  and light-source bloom-easing threads all re-verified exhaustively closed (grepped
  `shadow(`, the glint tell, `shA(mixA`, and every eased-glow global — genuinely
  nothing left), used an Explore agent to scan for a fresh gap outside all five
  families rather than re-treading one. It surfaced the VFW post's gravel apron
  (`drawVFW`, ~line 2463): a single flat `#9a938a` fill with zero wear detail,
  unlike its sibling — the dealership's paved lot — which already has "tire ruts
  worn into the lot" (`drawDealership`, ~line 1983, snow-gated streaks at low
  alpha). The VFW apron is actually busier (up to 6 vehicles queue/park/depart
  daily per `vfwDaily()`) but never got the equivalent treatment. Added two short
  darker gravel-rut streaks (`#716b65`, same `0.22*(1-snowCover)` alpha idiom,
  same `S(...,0.75)` shading) positioned under the parking-slot span
  (`VFWX-12..VFWX+43`), placed directly after the apron fill and before the brick
  — same "augment then move on" layering slot every other detail pass in this
  file uses. Zero new state, no timing, no Math.random — pure static structural
  detail, nothing to reset in synth/jumpSeason (same class as the tire ruts it
  mirrors). Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean
  aside from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced (byte-identical, verified). NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-04 #4 — Picked up the explicit next-candidate note run #3 left earlier today:
  the porch light (`tod>0.755&&tod<0.9&&dl<0.5`, ~line 3294) had the same instant-snap
  bloom flaw dealerLit/winLit/w2Lit were fixed for. That note worried it "gates a lamp
  shape not a light-circle bloom" and might need different handling — on inspection it
  doesn't: the block is a lamp pixel (`fillRect(hx+12,HOR-8,1,1)`) PLUS a glow circle
  (`circle(hx+12,HOR-6,4,'rgba(255,202,106,0.10)')`), the identical two-part shape
  winLit/w2Lit already handle (lamp/window color instant, circle bloom eased) — the
  precedent applies directly, no new technique needed. Added a fourth eased global,
  `pLit` (0..1, identical idiom/rate — `vdt*0.2`, ~5s time constant), computed in
  `update()` right after the `w2Lit` line from a target expression that mirrors the
  porch-light gate exactly (commented "kept in sync", same duplication precedent as
  the other three). Draw-site now gates the glow circle on `pLit>0.02` and scales it
  via `g.globalAlpha=pLit` (save/restore) instead of the boolean; the lamp pixel itself
  stays on the instant boolean, unchanged — matches the winLit/w2Lit precedent exactly
  ("only the hero bloom gets the ease"). Reset `pLit` to the correct instant target
  (not just 0) in both `synthSeason()` and `jumpSeason()`, identical target expression,
  right after the existing `w2Lit` reset on the same line (used `replace_all` since
  both reset lines are byte-identical). This closes the light-source-bloom-easing
  thread for real this time — sign, bedroom-1, bedroom-2, and porch are all eased now;
  grepped for any other bare `circle(...,'rgba(255,` glow gated by an instant boolean
  and found none left. Zero Math.random in draw code. Smoke OK, both builds green
  (mac .saver + Windows .scr, dotnet clean aside from the pre-existing WindowsBase
  version-conflict warnings), kit+zip refreshed, index.html re-synced (byte-identical,
  verified). NOT committed (evolve doesn't commit/push itself — uncommitted work
  continues to span 07-22 onward across many runs; a review/commit/push/redeploy
  session remains genuinely overdue).
- 2026-08-04 #3 — Picked up the explicit next-candidate note left by run #2 earlier
  today rather than opening a new thread: bedroom-2's window glow (`l2On`, line
  3274, `circle(hx+19,HOR-8,5,'rgba(255,202,106,0.13)')`) had the identical
  instant-snap pop that bedroom-1's glow (`winLit`) was just fixed for. Added a
  third eased global, `w2Lit` (0..1, identical idiom/rate to `dealerLit`/`winLit`
  — `vdt*0.2`, ~5s time constant), computed in `update()` right after the
  `winLit` line from a target expression that mirrors `l2On`'s own formula
  exactly (commented "kept in sync", same duplication precedent `winLit`/
  `dealerLit` already establish — `dayRng('l2')()`/`dayRng('l3')()` confirmed
  idempotent per call). Draw-site now gates the glow circle on `w2Lit>0.02` and
  scales it via `g.globalAlpha=w2Lit` (save/restore) instead of the boolean —
  same technique as the other two. Left the window's own lit-glass color flip
  instant, unchanged (matches precedent — only the bloom gets eased). Reset
  `w2Lit` to the correct instant target (not just 0) in both `synthSeason()`
  and `jumpSeason()`, identical target expression, right after the existing
  `winLit` reset on the same line. This closes the "light-source bloom easing"
  thread that run #2 opened — all three snapping blooms in the file (sign,
  bedroom-1, bedroom-2) are now eased; the porch light (`tod>0.755&&tod<0.9&&
  dl<0.5`) is the one remaining candidate in this family if it's reopened, but
  it gates a lamp glow shape rather than a light-circle bloom so it may need a
  slightly different treatment — left as a note, not attempted this run to
  keep the change singular. Zero Math.random in draw code. Smoke OK, both
  builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced (byte-identical, verified). NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session remains
  genuinely overdue).
- 2026-08-04 #2 — With the shadow/two-tone/glint/wx-easing/Aliveness families all
  exhaustively re-verified closed (grepped the two-tone tell `shA(mixA` and the
  shadow/glint tells across every `draw*` function — genuinely nothing left flat),
  picked a different bucket than another sprite-detail pass: the standing
  "smoother transition somewhere that currently pops" polish item, last closed for
  the KEATING dealership sign's bloom (2026-07-27 — instant `dl<0.45` threshold
  flip on a "reads as a light source" glow got an eased fade). Checked whether any
  other bloom in the file has the same flaw and found one: the farmhouse's
  bedroom-1 window glow (`l1On||l3am`, ~line 3273, `circle(hx+5,HOR-8,5,
  'rgba(255,202,106,0.13)')`) still snapped instantly the moment its `tod`/`dl`
  gate crossed — the exact same "threshold-on-continuous-input pop" bug class the
  sign fix targeted, just never revisited for the house. Added a second eased
  global, `winLit` (0..1, identical idiom/rate to `dealerLit` — `vdt*0.2`, ~5s
  time constant), computed in `update()` right after the `dealerLit` line from a
  target expression that mirrors `l1On||l3am` exactly (commented "kept in sync"
  since it's a deliberate duplicate, same class as `dealerLit`'s own duplicate of
  `drawDealership`'s `lit` gate). Draw-site now gates the glow circle on
  `winLit>0.02` and scales it via `g.globalAlpha=winLit` (save/restore around the
  `circle()` call) instead of the boolean — exact same technique as the sign's
  `g.globalCompositeOperation='lighter';g.globalAlpha=dealerLit` block. Left the
  window's own lit-glass color flip and the moth-attraction gate on the instant
  boolean, unchanged — matches the sign fix's own explicit precedent ("window/
  panel colors... left on the instant lit flip... only the hero bloom... got the
  ease"). Deliberately did NOT also ease the second bedroom window (`l2On`'s
  glow) — a second gap, not touched, left for a future run, same "one gap per
  pass" precedent used throughout this file's history (e.g. hens shipped before
  deer, farmhouse door before VFW door). Reset `winLit` to the exact correct
  instant target (not just 0) in both `synthSeason()` and `jumpSeason()`, using
  the identical target expression — verified `dl`/`tod`/`dayRng` are all valid
  and already correct at that point in both functions (right after their `sync()`
  call), same reasoning `dealerLit`'s own `dl<0.45?1:0` reset already relies on.
  Zero Math.random in draw code, `dayRng(tag)` confirmed idempotent per call (a
  fresh deterministic PRNG re-seeded from `tag+day` every invocation, not a
  continuing stream) so duplicating the `dayRng('l1')()`/`dayRng('wc')()` calls
  across update()/synthSeason/jumpSeason/draw() is safe — all four read the same
  value for the same day. Smoke OK, both builds green (mac .saver + Windows .scr,
  dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced (byte-identical, verified).
  NOT committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy session
  remains genuinely overdue). NOTE for a future run: the sibling bedroom-2
  window's glow (`l2On`, next line down) has the identical pop and would be the
  natural next pick if this "light-source bloom easing" thread is reopened — a
  second `w2Lit` global, same idiom, target mirrors `l2On`'s own formula. The
  porch light (`tod>0.755&&tod<0.9&&dl<0.5`, a few lines further down) has the
  same flaw too, a third candidate in the same family.
- 2026-08-04 — With the vehicle/door/chimney two-tone thread exhaustively re-treaded
  across ~15 prior runs, picked a genuinely different location instead of another
  vehicle or door pass: the VFW post's two windows (`drawVFW`, ~line 2471) had no
  frame at all — just a flat lit/unlit glass rect straight on the brick — unlike the
  farmhouse windows, which got a frame two-tone treatment back on 2026-08-02. Added
  a matching frame (`P(x+1,HOR-5,5,5,'#7e4434')` per window, reusing the VFW's own
  existing corner-post trim color so no new palette entry) with the identical
  top-highlight/bottom-shadow pair (`shA(mixA(C('#7e4434'),SNOW,0.22),0.5)` /
  `shA(mixA(...,NIGHT,0.28),0.55)`), placed directly before the existing window-glass
  fill so the glass still draws on top and only the 1px border shows. Checked
  clearances against the quoins (columns x and x+19, frame stops at x+6/starts at
  x+14 so neither touches), the door (x+8..+12, well clear of both frames), and the
  party-glow window circles (drawn later, unaffected). Zero new state, no timing, no
  Math.random — pure static structural detail, nothing to reset in synth/jumpSeason
  (same class as the farmhouse window frames it mirrors). Smoke OK, both builds
  green (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase version-conflict warnings), kit+zip refreshed, index.html re-synced.
  NOT committed (evolve doesn't commit/push itself — a review/commit/push/redeploy
  session remains overdue).
- 2026-08-03 #5 — Continued the two-tone body-shading thread once more (run #3 and #4
  today already closed the man door and VFW door), but picked a fresh spot rather than
  a third door pass: read the farmhouse detail block in full rather than trusting the
  "closed" claims, and found the chimney flue (`P(hx+19,HOR-21,3,6,'#8a4a42')`, line
  3249) was still a single flat fill — no highlight/shadow edge — sitting directly
  beside the window frames, steps, and bench in the exact same block, all three of
  which already carry the `shA(mixA(...))` two-tone idiom. Added the identical
  top-highlight/bottom-shadow pair (`shA(mixA(C('#8a4a42'),SNOW,0.22),0.5)` on the
  flue's top row, `shA(mixA(C('#8a4a42'),NIGHT,0.28),0.55)` on its bottom row),
  placed between the flue body fill and the existing darker chimney-cap line —
  same "augment then move on" layering slot every other pass in this block uses.
  The cap line itself (a separate, already-darker rain-cap detail) is untouched.
  Zero new state, no timing, no Math.random — pure static structural detail,
  nothing to reset in synth/jumpSeason (same class as the steps/bench/window
  frames right next to it). Smoke OK, both builds green (mac .saver + Windows
  .scr, dotnet clean aside from the pre-existing WindowsBase version-conflict
  warnings), kit+zip refreshed, index.html re-synced. NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22 onward
  across many runs; a review/commit/push/redeploy session is genuinely overdue).
- 2026-08-03 #4 — With the shadow audit, glass-glint sweep, two-tone body-shading
  thread, wx-easing audit, and the "Aliveness" bucket all repeatedly re-verified
  closed, and run #3 earlier today having just swept the dealership building
  for anything the two-tone idiom missed, applied the same logic to the other
  building with its own detail-dense static block: the VFW post. Grepped
  `shA(mixA` (the two-tone idiom's own tell) against the whole file and found
  the VFW's red door (`P(x+8,HOR-6,4,6,'#b03030')`, ~line 2473) was still a
  single flat fill with only a door-handle highlight dot — unlike the man
  door/window frames/porch steps, which all already carry the idiom. Added
  the identical top-highlight/bottom-shadow two-tone lines
  (`shA(mixA(C('#b03030'),SNOW,0.22),0.5)` top edge,
  `shA(mixA(C('#b03030'),NIGHT,0.28),0.55)` bottom edge), placed directly
  after the door fill, before the existing handle-highlight pixel — same
  "augment then move on" layering slot every other pass uses. Verified no
  clash with the party-glow doorway wash (draws later, lower, only during an
  active party) or the handle highlight (unaffected, sits mid-door). Zero new
  state, no timing, no Math.random — pure static structural detail, nothing
  to reset in synth/jumpSeason (same class as the man door/window frames).
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet clean aside
  from the pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced. NOT committed (evolve doesn't commit/push
  itself — uncommitted work continues to span 07-22 onward across many runs;
  a review/commit/push/redeploy session is genuinely overdue).
- 2026-08-03 #3 — With the shadow audit, glass-glint sweep, two-tone body-shading
  thread, wx-easing audit, and the "Aliveness" bucket all repeatedly re-verified
  closed (per this file's own extensive recent history), looked at the
  dealership building itself — already the most detail-dense static structure
  in the file (lot pennants, showroom glint, roll-up door shading, sign bloom)
  — for anything that had slipped through, rather than re-treading a vehicle
  sweep. Found one: the service bay's man door (`P(DX+56,HOR-8,4,8,'#5a6068')`,
  ~line 2052) was a single flat fill with only a handle dot, unlike the
  roll-up door right next to it (which already has jamb/interior shading) and
  unlike the farmhouse window frames (line 3247-3251), which use the exact
  same idiom on a static architectural rect. Added the identical top-highlight/
  bottom-shadow two-tone treatment (`shA(mixA(C('#5a6068'),SNOW,0.22),0.5)` top
  edge, `shA(mixA(C('#5a6068'),NIGHT,0.28),0.55)` bottom edge), placed directly
  after the door fill, before the handle — same "augment then move on"
  layering slot every other detail pass uses. Zero new state, no timing, no
  Math.random — pure static structural detail, nothing to reset in
  synth/jumpSeason (same class as the window frames/porch steps). Smoke OK,
  both builds green (mac .saver + Windows .scr, dotnet clean aside from the
  pre-existing WindowsBase version-conflict warnings), kit+zip refreshed,
  index.html re-synced. NOT committed (evolve doesn't commit/push itself —
  uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session is genuinely overdue).
- 2026-08-03 #2 — Re-verified the glass-glint sweep's own repeated "closed" claim
  by actually grepping the tell (`rgba(255,255,255,0.14)`, the glint's unique
  fillStyle) instead of trusting the memory list — found exactly 8 hits
  (tractor/combine/sprayer/car/semi/truckSm/pickup/bus), one short of every
  enclosed-cab sprite that's gotten a shadow+two-tone pass. `drawTruck` (the
  crop-fire water truck, ~line 2537) has a cab window fillRect (`5,-10,3,3`)
  that got shadow (07-25) and body two-tone (07-31 #2) but was simply never in
  the glint sweep's sprite list — a genuine miss, not a re-tread. Added the
  identical proportional diagonal-quad treatment (22%-40% width top band,
  0%-16% bottom, full window height, `rgba(255,255,255,0.14)`, gated `dl>0.5`,
  matching drawTruckSm's gate since drawTruck has no `lit` night-shift param),
  geometry scaled from this window's own 3×3 rect. Placed directly after the
  window fillRect, before the ladder-rail P() — same "augment then move on"
  layering slot every other glint pass uses. Zero new state, pure per-frame
  draw addition, nothing to reset in synth/jumpSeason, no Math.random. This
  really does look like the last cab-window sprite now — every enclosed-glass
  vehicle in the file (tractor, combine, sprayer, car, semi, truckSm, pickup,
  bus, and now the fire truck) has the glint. Smoke OK, both builds green
  (mac .saver + Windows .scr, dotnet clean aside from the pre-existing
  WindowsBase warnings), kit+zip refreshed, index.html re-synced. NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues to
  span 07-22 onward across many runs; a review/commit/push/redeploy session is
  genuinely overdue).
- 2026-08-03 — With the shadow audit, glass-glint sweep, two-tone body-shading
  thread, and wx-easing audit all exhaustively re-verified closed many times
  over (per this file's own extensive recent history), and the "Aliveness/
  character" backlog bucket now genuinely empty (combine rider, cat rain-
  shelter/pounce, Hal/Manny waves, Manny's coffee steam, mailbox flag — all
  confirmed shipped by grep, not just trusted from memory), audited the
  remaining un-two-toned movers for a real gap rather than a re-tread.
  `drawBike` (Wade's practice bike + VFW patron bikes) had a flat single-fill
  frame box (`fillRect(x-2,y-4,4,2)`) — the one mover in the two-tone-body
  family (car/truckSm/pickup/semi/mower already covered) never touched.
  Considered a totally fresh angle first (a cast ground-shadow for the
  high-soaring hawk) but it's drawn in the "behind-the-horizon" sky layer,
  well before the ground/hills pass — correctly projecting a moving shadow
  onto the field from there would need real depth-ordering surgery, too much
  risk for an unattended run to verify by eye; left as a note for an
  interactive session, not attempted. Added one line —
  `shA(mixA(C(col),SNOW,0.22),0.5)` fillRect across the frame's top row —
  directly after the frame fill, before the rear-wheel-fender fill, using
  drawMower's exact "too thin (2px) for a full sandwich, highlight-only"
  precedent (the frame is even thinner than the mower's 2.6px deck). Zero new
  state, pure per-frame draw addition, nothing to reset in synth/jumpSeason,
  no Math.random. Exercised in smoke.js via both bikeRide (Wade's practice
  laps) and the VFW lot roster (bike-kind patrons, ~40% of nightly arrivals).
  Smoke OK, both builds green (mac .saver + Windows .scr, dotnet build clean
  aside from pre-existing WindowsBase version-conflict warnings), kit+zip
  refreshed, index.html re-synced. NOT committed (evolve doesn't commit/push
  itself — uncommitted work continues to span 07-22 onward across many runs;
  a review/commit/push/redeploy session is genuinely overdue).
- 2026-08-02 #2 — Re-verified the shadow audit's own repeated "closed" claim once
  more (the recurring lesson: inline draw code, not named `draw*` functions, is
  the actual blind spot — windmill/silo/hillcows/bales/tumbleweeds were all
  found this exact way). The snowman (`if(snowman&&snowman.b>0.05)`, drawn
  inline in `draw()`, ~line 3336) had no `shadow()` call, unlike the deer and
  rabbit drawn right next to it in the same block. Added
  `shadow(sx2,sy2,2.6*s2)` as the first statement, before the base-ball
  circle — same x/y as the ball itself, radius keyed to `s2` (the snowman's own
  0→1 build-up fraction) so the shadow grows with him exactly as the ball does,
  matching the deer/rabbit convention of anchoring to the fixed ground line.
  Zero new state, pure per-frame draw addition, nothing to reset in
  synth/jumpSeason (snowman's own state already resets there). Out of season to
  eyeball live (August), but exercised via `smoke.js`'s full simulated year,
  which walks through winter. Smoke OK, both builds green, kit+zip refreshed,
  index.html re-synced. NOT committed (evolve doesn't commit/push itself —
  uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session is genuinely overdue).
- 2026-08-02 — With the shadow audit, glass-glint sweep, two-tone body-shading
  thread, and wx-easing audit all repeatedly re-verified closed (per this
  file's own extensive recent history), audited the farmhouse block — the
  single most detail-dense spot in the file (steps, bench, AC unit, laundry
  line, flowerbed, moths all already two-toned) — for anything that had
  slipped through. Found one: the two window frames (`P(hx+2,HOR-11,6,6,...)`
  / `P(hx+16,...)`, line 3242) were a single flat fill with no light/shadow
  edge, unlike every other detail in that exact block. Added the same
  top-highlight/bottom-shadow two-tone idiom the steps and bench right above
  it already use (`shA(mixA(C('#b9b2a0'),SNOW,0.22),0.5)` top edge /
  `shA(mixA(...,NIGHT,0.28),0.55)` bottom edge), placed directly after the
  frame fill, before the chimney/lamp draws — the window-glass fillRect still
  draws on top later, so only the 1px border strips outside the glass show.
  Zero new state, no timing, no Math.random — pure static structural detail,
  nothing to reset in synth/jumpSeason (same class as the steps/bench/AC).
  Smoke OK, both builds green, kit+zip refreshed, index.html re-synced. NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy
  session is genuinely overdue).
- 2026-08-01 #2 — With the shadow audit, glass-glint sweep, two-tone body-shading
  thread, and wx-easing audit all repeatedly re-verified closed (and this
  morning's farmhouse-porch-steps run already having picked one fresh angle
  today), looked for another genuinely untouched detail rather than a re-tread.
  Found one in the barn block (~line 3143): the hay-loft door opening
  (`P(bx+13,HOR-17,4,3,'#3a1d1d')`, a small dark 4×3 square) had nothing in it
  but flat black — a working barn's loft door realistically collects a cobweb
  in a corner nobody sweeps. Added a tiny static corner cobweb: a few thin
  strands radiating from the top-left corner plus one short connecting thread,
  stroked in `shA(mixA(C('#3a1d1d'),SNOW,0.4),0.35)` at low alpha (0.4) — same
  color-mix idiom the vehicle two-tone sweep uses, applied to a stroke instead
  of a fill. Placed directly after the loft-door fill, before the loft-header
  highlight line — same "augment then move on" layering slot every other
  small-detail addition uses. Deliberately NOT animated and NOT randomized —
  a cobweb doesn't move, so this is pure static geometry, zero new state,
  nothing to reset in synth/jumpSeason, no Math.random anywhere. This is a
  genuinely fresh angle (old-barn authenticity), not another pass over
  shadows/glints/two-tone-shading/wx-easing. Smoke OK, both builds green,
  kit+zip refreshed, index.html re-synced. NOT committed (evolve doesn't
  commit/push itself — uncommitted work continues to span 07-22 onward across
  many runs; a review/commit/push/redeploy session is genuinely overdue).
- 2026-08-01 — With the shadow audit, glass-glint sweep, two-tone body-shading
  thread, and wx-easing audit all repeatedly closed across ~25 prior runs,
  looked outside all four families for a genuinely untouched spot rather than
  a re-tread. Found one: the farmhouse door (`hx+10,HOR-7,4,7`) sits flush at
  ground level with nothing underfoot — every other farmhouse-yard detail
  (flowerbed, laundry line, window AC, moths) got a pass, but the actual
  threshold where Earl/Dot's porch-sit spot (`tx:HX+7`/`HX+11`, y:124) and
  every yard visitor's path converges had no steps, just bare wall-to-lawn.
  Added two small stone steps below the door — an upper door-width step and a
  lower step flared wider — using the exact two-tone highlight/shade idiom
  from the vehicle sweep (`shA(mixA(stC,SNOW,0.22),0.5)` top /
  `shA(mixA(stC,NIGHT,0.25),0.5)` bottom) so it matches the file's established
  light/shadow language without literally re-opening that vehicle-only thread.
  Placed right after the door/knob draw, before the chimney — same "augment
  then move on" layering slot every other farmhouse-decor addition uses, so
  Dot/Earl/dog/anyone crossing the yard still draws on top and occludes it
  correctly. Zero new state, no timing, no Math.random — pure static
  structural detail, nothing to reset in synth/jumpSeason (same class as the
  flowerbed/AC unit). Smoke OK, both builds green, kit+zip refreshed,
  index.html re-synced. NOT committed (evolve doesn't commit/push itself —
  uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session is genuinely overdue at this point).
- 2026-07-31 #4 — Closed the last asymmetry in the two-tone body-shading thread:
  `drawTruckSm` (shared sprite for VFW patron trucks + the weekday mail truck)
  had an undercarriage-shade line on its body box (`fillRect(-6,-4,13,1)`,
  mixed toward NIGHT) but no matching roof-highlight on its cab box
  (`P(1,-10,5,3,col)`) — the exact same asymmetry `drawCar` had before
  yesterday's #3 fix, and `drawPickup` (the closest sibling sprite, same
  body+cab two-box shape) already carries both lines. Added one line —
  `shA(mixA(C(col),SNOW,0.22),0.6)` fillRect across the cab box's top edge
  (`1,-10,5,1`) — directly after the existing undercarriage-shade line, same
  "augment then move on" layering slot and identical color-mix idiom
  `drawPickup`'s cab treatment uses (`SNOW`/`NIGHT` are pre-existing global
  color consts). This really does look like the last body/cab two-box
  vehicle sprite missing either half of the pair now (tractor/combine/
  sprayer/car/truckSm/pickup/semi/boxcar/cart/fire-truck all checked).
  Zero new state, pure per-frame draw addition, nothing to reset in
  synth/jumpSeason, no Math.random. Smoke OK, both builds green, kit+zip
  refreshed, index.html re-synced. NOT committed (evolve doesn't commit/push
  itself — uncommitted work continues to span 07-22 onward across many runs;
  a review/commit/push/redeploy session is genuinely overdue).
- 2026-07-31 #3 — Continued the two-tone body-shading thread once more, but this
  time closing an asymmetry rather than a totally-flat sprite: `drawCar` (the
  family car / employee car / dealership visitor car — likely the single
  most-often-on-screen vehicle) already had an undercarriage-dark rocker
  stripe (`fillRect(-5.5,-2.2,11,0.6)`, flat `#22242c`) but NO roof highlight
  anywhere, unlike `drawTruckSm`/`drawPickup`/`drawSemi`/the recent boxcar/
  cart/fire-truck fixes, which all pair a highlight with a shade line.
  Rather than sandwich the 3px-tall main body box (too thin — 2 shading px
  out of 3 would read heavy), followed `drawPickup`'s actual convention more
  precisely: it applies the roof-highlight to the CAB/greenhouse box, not the
  lower body box. Added one line — `shA(mixA(C(col),SNOW,0.22),0.5)` fillRect
  across the top edge of the cabin box (`-2.5,-7,5.6,1`) — directly after the
  cabin `P()` call, before the window fill (same "augment then move on"
  layering slot every other pass uses; the window and glint still draw on
  top of it, so it only shows as a thin roofline sliver above the glass,
  exactly like the pickup's cab treatment). Left the body's existing dark
  rocker stripe untouched (already functions as the undercarriage shade,
  just a flat tone rather than the mixA(col,NIGHT) idiom — changing it would
  be a second, unrelated edit). Zero new state, pure per-frame draw addition,
  nothing to reset in synth/jumpSeason, no Math.random. Smoke OK, both builds
  green, kit+zip refreshed, index.html re-synced. NOT committed (evolve
  doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session is
  genuinely overdue).
- 2026-07-31 #2 — Continued the two-tone body-shading thread one more stop:
  the crop-fire water truck (`drawTruck`, ~line 2527) was flagged as a
  shadow-sweep fix back on 2026-07-25 but never got the roof-highlight/
  undercarriage-shade pass the boxcars/grain-cart got yesterday — its main
  body box (`P(-10,-9,14,5,'#b23430')`, a 14×5 flat red panel) is the same
  shape class as those. Added the identical two idiom lines verbatim (1px
  line mixed 22% toward SNOW at factor 0.5 along the top edge, 1px line
  mixed 30% toward NIGHT at factor 0.72 along the bottom edge), placed
  directly after the body `P()` call and before the raised cab-box `P()`
  call — same "augment then move on" layering slot every prior two-tone/
  glint pass uses. Deliberately left the smaller cab box, ladder, and
  wheel-well trim untouched (same "only the one flat box-shaped panel"
  scope the cart fix used, not a full-vehicle redesign). Zero new state,
  pure per-frame draw addition, nothing to reset in synth/jumpSeason, no
  Math.random. Smoke OK, both builds green, kit+zip refreshed, index.html
  re-synced. NOT committed (evolve doesn't commit/push itself — uncommitted
  work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session is genuinely overdue).
- 2026-07-31 — Continued the two-tone body-shading thread the freight-train boxcars
  opened yesterday (2026-07-30 #5): audited the remaining flat-fill implement
  sprites (`drawCart`/`drawPlanter`/`drawBaler`) and found the grain cart's
  body (`drawCart`, `P(-7,-9,12,6,'#3e7d3b')`) was the one with real proportions
  for it — a 12×6 flat green box, the same shape class as a train boxcar
  (planter's bed and baler's chute are thin 1px strips, too shallow for a
  visible two-tone split, left alone). Added the identical roof-highlight/
  undercarriage-shade pair verbatim from the boxcar fix: a 1px line mixed 22%
  toward SNOW (factor 0.5) along the top edge, a 1px line mixed 30% toward
  NIGHT (factor 0.72) along the bottom edge, sandwiching the existing flat
  fill. Placed directly after the body `P()` call, before the hitch-bar
  `P(5,-6,3,1,...)` — same "augment then move on" layering every other
  two-tone/glint pass uses; the grain-fill ellipse still draws last and sits
  on top of the roof line exactly as before. Zero new state, pure per-frame
  draw addition, nothing to reset in synth/jumpSeason, no Math.random. Smoke
  OK, both builds green, kit+zip refreshed, index.html re-synced. NOT
  committed (evolve doesn't commit/push itself — uncommitted work continues
  to span 07-22 onward across many runs; a review/commit/push/redeploy
  session is genuinely overdue).
- 2026-07-30 #5 — With shadows, glass-glints, and wx-easing all repeatedly closed
  (four runs today alone across those three families), picked a genuinely
  different bucket: two-tone body shading, the treatment every hero vehicle
  (tractor/combine/sprayer/car/truckSm/pickup/semi) got in the original July
  "quality pass" — a roof-highlight line + an undercarriage-shade line on an
  otherwise flat fill. The freight train's boxcars (`if(train)` block, main
  `draw()`, ~line 3043) were never touched by that pass — the train was added
  in a later wave — and were still a single flat `fillRect` per car, one of
  the last plainly-flat sprites left in the file. Added the identical idiom
  `drawTruckSm`/`drawSemi` already use: a 1px roof line mixed 22% toward SNOW
  (lighter, low night-darkening factor 0.5) and a 1px undercarriage line mixed
  30% toward NIGHT (a fixed darker band, factor 0.72) sandwiching the existing
  flat fill, per boxcar, using each car's own already-varied color (`i%4`) so
  all four palette entries get consistent treatment. Zero new state, pure
  per-frame draw addition, nothing to reset in synth/jumpSeason, no
  Math.random. Smoke OK, both builds green, kit+zip refreshed, index.html
  re-synced. NOT committed (evolve doesn't commit/push itself — uncommitted
  work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session is genuinely overdue).
- 2026-07-30 #4 — Re-checked the shadow audit's own repeated "closed" claim once more
  rather than trusting it (the recurring lesson — inline draw code, not named `draw*`
  functions, is the actual blind spot: windmill/hillcows/bales/tumbleweeds were all
  found this exact way). Grepped for `shadow(` calls near every standalone ground
  structure drawn inline in the barn block (~3100-3165) and found the silo had none —
  it sits right next to the windmill, which got its own shadow fix for the identical
  reason two runs ago, but the silo was never checked. Added
  `shadow(SILOX+4.5,HOR+0.6,4)` as the first statement of the silo block, before the
  cylinder body fillRect — same placement/y-offset convention as the windmill/mailbox
  fixes, radius scaled from the silo's own 9px width (same ~0.46 width-to-radius
  ratio the mailbox uses). The barn/farmhouse/dealership/VFW themselves remain
  correctly shadow-free by design (large buildings whose own footprint reads fine) —
  this is a genuinely separate case, a small standalone structure like the windmill/
  mailbox/sign, not a re-litigation of that design call. Zero new state, pure
  per-frame draw addition, nothing to reset in synth/jumpSeason, no Math.random.
  Smoke OK, both builds green, kit+zip refreshed, index.html re-synced. NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span 07-22
  onward across many runs; a review/commit/push/redeploy session is genuinely
  overdue).
- 2026-07-30 #3 — With the shadow audit, glass-glint sweep, and wx-easing audit all
  re-verified genuinely closed this run (grepped every `draw*` function for a
  `shadow(` call — zero gaps; the only 0-hit functions are the three large
  buildings, left alone by design per memory), picked a fresh angle outside all
  three families: the windmill's spinning blade fan (`draw()`, ~line 3173) was a
  flat single-tone stroked silhouette with no light-catch of any kind, unlike the
  sprinkler droplets which already got a sun-catch glint pass. Added a small
  sun-flash on whichever blade is swinging through the top of its turn: per blade
  `k=-Math.sin(a)` (1 at the exact top, continuous, already-driven by the
  existing `mAng` angular state — no new global, same "physical readout of
  continuous per-frame motion" class as the sprinkler-peak glint, not the static
  diagonal-quad glass-glint idiom used on vehicle cabs), gated `dl>0.5`
  (daylight only) and `k>0.82` with an alpha taper over that last 18% of the
  swing so it reads as a brief flash rather than a lit pixel riding the blade.
  Placed right after the blade-fan stroke, before the hub cap fill — same
  "augment then move on" layering every other glint uses. Zero new state,
  nothing to reset in synth/jumpSeason, no Math.random in draw code. Smoke OK,
  both builds green, kit+zip refreshed, index.html re-synced. NOT committed
  (evolve doesn't commit/push itself — uncommitted work continues to span
  07-22 onward across many runs; a review/commit/push/redeploy session is
  genuinely overdue).
- 2026-07-30 #2 — With every "living work" beat now shipped (Manny welding, Dot's
  laundry chore, Hal's pennants) and the shadow/glass-glint/wx-easing audits all
  repeatedly declared closed, re-checked the glass-glint sweep's own claim rather
  than trusting it (the recurring lesson from the shadow-audit misses): every prior
  sweep note names an explicit sprite list (tractor/combine/sprayer/car/truckSm/
  pickup/semi) — the weekday school bus (`if(bus)` draw block, ~line 3468) has its
  own window strip (`fillRect(x-8,y-7,15,2)` + mullions) but was never in that list,
  so it slipped through every pass. Added the identical daylight diagonal-quad glint
  (`rgba(255,255,255,0.14)`, gated `dl>0.5`), geometry scaled proportionally to this
  window's own wider 15×2 band (22%-40% width top, 0%-16% bottom, full window
  height) rather than reusing another sprite's literal coordinates. Placed after the
  window mullions, before the black skirt stripe — same "last thing before the next
  element" layering slot every other glint uses. Zero new state, pure per-frame draw
  addition, nothing to reset in synth/jumpSeason, no Math.random. This really does
  look like the last enclosed-glass sprite in the file now — every vehicle with a
  window pane (tractor, combine, sprayer, car, truckSm, pickup, semi, and now the
  bus) has the glint; VFW/farmhouse/dealership building windows are lit-glow glass,
  a different established idiom, left alone by design. Smoke OK, both builds green,
  kit+zip refreshed, index.html re-synced. NOT committed (evolve doesn't commit/push
  itself — uncommitted work continues to span 07-22 onward across many runs; a
  review/commit/push/redeploy session is genuinely overdue).
- 2026-07-30 — Picked up the last un-blocked "living work" beat from the 2026-07-11
  brainstorm list (Manny welding shipped, Earl's leaning-fence-post idea RETIRED,
  Wade/Manny tool-handoff needs YRS>=3): Dot's laundry line, shipped 2026-07-23,
  was purely decorative — all 3 garments always up together all day, with no
  connection to any of her folkPlan chores (the line even sits west of the house
  at HX-11..HX-2, outside every range she was ever scheduled to visit). Gave her
  an actual laundry chore: a new early-morning folkPlan branch (`seasonIdx<2 &&
  tod<0.30`, right before her existing "morning garden" window, itself nudged
  from 0.3 to 0.27 only in spring/summer so the other seasons are byte-identical
  in behavior) sends her to `HX-6` — right at the line — with `chore:'laundry'`
  and a small wicker-basket detail at her feet (same idiom as Earl's scattered
  feed / her own watering-can arc, in the existing chore-details draw block).
  The line itself now hangs the 3 garments progressively instead of all at once:
  `hung=tod<0.27?0:tod<0.28?1:tod<0.29?2:3`, a pure function of `tod` — no new
  persistent global, self-resetting every day for free (same class as the window
  AC / flowerbed), nothing to add to synth/jumpSeason. Checked for folk/position
  conflicts at that spot and hour: farmer's own "vis:false before 0.27" threshold
  matches hers, but his chore is 'hay' at the barn (east); kid2's dandelion beat
  is gated afternoon-only (k2At window ~0.66-0.71); nothing else visits HX-9..HX-6
  that early. Smoke OK, both builds green, kit+zip refreshed, index.html re-synced.
  NOT committed (evolve doesn't commit/push itself — uncommitted work continues to
  span 07-22 onward across many runs; a review/commit/push/redeploy session is
  genuinely overdue).
- 2026-07-29 #4 — Re-audited the hero-glass-glint sweep once more since the memory's
  own "closed" claims have been wrong 3-4 times before for shadows (inline draw code
  is the recurring blind spot) — this time checked the CLAIM itself rather than
  trusting it: an old note asserted "semi has its own [glint] already," but grepping
  `drawSemi` (~line 2299) showed its cab window (`fillRect(3,-12,3,3)`) was a flat
  fill with no glint code anywhere in the function — the claim was simply wrong,
  never verified against the source. Added the identical proportional diagonal-quad
  treatment used by every other enclosed-cab sprite (tractor/combine/sprayer/car/
  truckSm/pickup): 22%-40% width band at the top, 0%-16% at the bottom, full window
  height, `rgba(255,255,255,0.14)`, gated `dl>0.5` (matches the car/truckSm/pickup
  gate — the semi has no `lit` night-shift param on this rect, same reasoning as
  those). Geometry scaled from this window's own 3×3 rect (3.66/4.2/3.48/3 x, -12/-9
  y). Placed directly after the window fillRect, before the sun-visor strip — same
  layering slot every other glint uses. Zero new state, pure per-frame draw addition,
  nothing to reset in synth/jumpSeason, no Math.random. This really does close the
  hero-glass-glint sweep now — every enclosed-cab sprite in the file, including the
  one the memory previously (incorrectly) assumed was already done. Smoke OK, both
  builds green, kit+zip refreshed, index.html re-synced. NOT committed (evolve
  doesn't commit/push itself — uncommitted work now spans 07-22 through today across
  many runs; a review/commit/push/redeploy session is genuinely overdue).
- 2026-07-29 #3 — With the shadow audit, glass-glint sweep, and wx-easing audit all
  closed several times over, and the seasonal ambient/living-work backlog items
  either shipped or blocked (Earl's "leaning fence post" idea directly
  contradicts the retired straight-post ruling; Wade/Manny tool-handoff needs
  YRS>=3, years away), picked a genuinely fresh angle instead: the Keating
  dealership building had no lot-front festivity at all, unlike the VFW (party
  glow) or the farmhouse (laundry line, flowerbed) — a used-equipment lot in
  real life almost always strings pennant bunting along the roofline. Added a
  string of small triangular pennants along the dealership roof edge (`drawDealership`,
  right after the building's roof/side panels are drawn, before the ground-
  contact shadow line), alternating Keating green/yellow (colors already used
  elsewhere in the file — Hal's cap/shirt, JD tractor accents — so this isn't a
  new palette), each swaying independently on the exact same wind idiom the VFW
  flag and farmhouse laundry line already use (`sin(rt*(1.3+wind*0.08)+phase)*(0.3+fw4*0.7)`,
  phase offset per pennant index so they ripple rather than sway in lockstep).
  Two small anchor poles + a taut string tie it to the building visually, same
  convention as the laundry line's two posts. Zero new persistent globals — pure
  per-frame draw addition keyed off existing `rt`/`wind` (reused `clamp`, already
  global), nothing to reset in synth/jumpSeason, no Math.random in draw code.
  Placed above the roofline (HOR-17 and up) so it can never overlap the showroom
  glass, roll-up door, or the lot inventory (which draws in a separate later
  block, at ground level). Smoke OK, both builds green, kit+zip refreshed,
  index.html re-synced. NOT committed (evolve doesn't commit/push itself —
  uncommitted work now spans 07-22 through today across many runs; a
  review/commit/push/redeploy session is genuinely overdue).
- 2026-07-29 #2 — With the shadow audit, glass-glint sweep, and wx-easing audit all
  exhaustively closed (per this file's own recent entries), looked for a genuinely
  fresh angle rather than a fourth pass over any of those threads. Picked the summer
  garden sprinkler (`applebee-acres.html` ~3364) — its water droplets were a flat
  single blue tone with no variation, and it's the one seasonal ambient effect that
  had never gotten a "reads as real" detail pass. Added a tiny sun-catch glint: when
  full morning sun is out (`dl>0.55`) and a droplet is near the peak of its parabolic
  arc (`ph` between 0.35 and 0.55, the existing per-droplet arc-position variable),
  it gets one extra 1px pixel above itself tinted warm or cool (alternating by `k`
  parity) at low alpha, tapering off away from the exact peak — reads as the mist
  briefly catching a prismatic sparkle in full sun, a miniature callback to the
  file's existing big-arc rainbow-after-rain effect without touching that mechanic
  at all. Purely a function of existing per-frame state (`dl`, the droplet's own
  `ph`/`dxx`/`dyy`, index `k`) — zero new globals, zero Math.random in draw code,
  nothing to reset in synth/jumpSeason (same "draw-time-only" class as the flowerbed/
  laundry line already in this block). Smoke OK, both builds green, kit+zip
  refreshed, index.html re-synced.
- 2026-07-29 — Re-audited every enclosed-cab sprite against the hero-glass-glint
  convention (the sweep repeatedly declared "closed" after tractor → combine →
  sprayer → car → truckSm) and found one genuine miss: `drawPickup` (the
  dealership sale/trade-in pickup, `drawPickup(sale.x,...)` at the used-vehicle-
  sale event) has its own cab window fillRect (`fillRect(4,-10,3,2.5)`,
  same day/night color logic as drawCar/drawTruckSm) but was never in the
  five-sprite list the sweep's own notes named — it's a distinct sprite, not an
  alias of drawCar, so a memory search for "which sprites got the glint" walked
  right past it. Added the identical proportional diagonal-quad treatment
  (22%-40% width band at the top, 0%-16% at the bottom, full window height,
  `rgba(255,255,255,0.14)`, gated `dl>0.5` — matching the car/truckSm gate since
  drawPickup has no `lit` night-shift param), geometry scaled from this
  window's own 3×2.5 rect (4.66/5.2/4.48/4 x, -10/-7.5 y). Placed directly
  after the window fillRect, before `g.restore()` — same layering slot every
  other glint uses. Zero new state, pure per-frame draw addition, nothing to
  reset in synth/jumpSeason, no Math.random. This really does look like the
  last cab-window sprite in the file now (planter/baler/cart/mower/bike have
  no glass by design). Smoke OK, both builds green, kit+zip refreshed.
- 2026-07-28 #5 — With the shadow audit, glass-glint sweep, and wx-easing audit all
  repeatedly declared closed, looked for a genuinely fresh gap rather than re-treading
  any of the three threads. Found one: the farmhouse's two windows (the `l1On`/`l2On`
  lamp pair, ~line 3181) had never gotten a single physical household detail beyond
  the lit-glass glow + the recent moths — no evidence of how this specific rural
  Kansas family actually lives through a hot summer. Added a window AC unit to the
  right window (`l2On`'s frame, hx+16..22): a small gray box filling the lower half
  of the glass, two darker vent-slat pixels, and a tiny damp drip stain on the sill
  below it — gated `seasonIdx===1` (summer only, same numbering the moths block
  already uses) so it appears for the season and is gone the rest of the year, the
  same "seasonal fixture" idiom as the laundry line (`seasonIdx<2`). Drawn after the
  window's own lit-glass fill/glow so it sits in front of the glass (occludes the
  lower portion when lit), same layering order as everything else in this block.
  Zero new global state — purely a function of `seasonIdx`/`hx`/`HOR`, same class as
  the flowerbed/laundry-line right below it — nothing to reset in synth/jumpSeason.
  No Math.random in draw code. Smoke OK, both builds green, kit+zip refreshed. Not
  committed (evolve doesn't commit/push itself — uncommitted work now spans 07-22
  through today across many runs, a review/commit/push/redeploy session is
  genuinely overdue at this point).
- 2026-07-28 #4 — The standalone-static-prop shadow audit (mailbox/sign, declared
  closed 2026-07-26 #4: "every static ground prop and every mover in the file has
  a shadow") missed one: the windmill tower had no `shadow()` call at all — same
  blind spot class as the hillcows/bales misses (a fixed-ground structure drawn
  inline in the town block, not through a named `draw*` function, so a
  function-grep audit walks right past it). Added `shadow(WMX,HOR+0.6,3.5)` as
  the first statement of the windmill block, before the tower/blade strokes —
  radius sized a bit over half the ~8px leg-base spread (WMX-4..WMX+4), same
  convention/placement as `drawMailbox`/`drawSign`. Zero new state, pure
  per-frame draw addition — nothing to reset in synth/jumpSeason. This really
  does look like the last standalone ground structure with no shadow (barn/
  house/dealership/VFW are large buildings whose own footprint reads fine
  without one — checked, left alone by design). Smoke OK, both builds green,
  kit+zip refreshed. Not committed (evolve doesn't commit/push itself —
  uncommitted work now spans 07-22 through today across many runs, a
  review/commit/push/redeploy session is overdue).
- 2026-07-28 #3 — With the glass-glint sweep and shadow audit both repeatedly
  declared closed (memory's explicit note: stop re-treading either), and both
  summer backlog seasonal items (fireflies, garden sprinkler) already shipped,
  looked for a genuinely fresh gap instead. Found one: the farmhouse's lit
  evening windows (`l1On`/`l2On` in the farmhouse draw block, ~line 3181) had
  no life around them at all — just a static glow circle. Added moths
  fluttering near the lit window (`l1On`) on warm nights: 3 small dots
  orbiting the same glow center (`hx+5,HOR-8`) already used for the window's
  own light-circle, gated `(l1On||l3am) && (seasonIdx===1||(seasonIdx===2&&
  seasonT<0.35))` — reused the fireflies' exact warm-season gate so the two
  nocturnal-insect beats agree on when "warm enough for bugs" is. Motion is
  PURE `rt`-driven (no stored state at all, unlike `flies`/`flutters` which
  keep a persistent wandering array) — each moth's position is a closed-form
  sum of two sine/cosine terms at different frequencies keyed by loop index
  `m`, giving an erratic flutter around the fixed point rather than a smooth
  orbit; alpha also pulses via `sin(mp*4+m)` so they don't read as static
  dots. Zero new globals, zero Math.random in draw code, nothing to reset in
  synth/jumpSeason (same as flies/flutters, which also hold no season-keyed
  state — draw-time gating is enough). Placed right after the existing
  `circle(hx+5,...)` window-glow line, before the porch-light block — same
  farmhouse-decor layering as the flowerbed/laundry-line already there, so
  folk drawn afterward still occlude correctly. Smoke OK, both builds green,
  kit+zip refreshed. Not committed (evolve doesn't commit/push itself —
  uncommitted work now spans 07-22 through today across many runs, a
  review/commit/push/redeploy session is overdue).
- 2026-07-28 #2 — Finished the hero-glass-glint sweep's explicitly-flagged next
  candidate from the prior run's own note: `drawTruckSm` (the shared sprite for
  VFW patron trucks AND the weekday mail truck) still had a flat single-color
  window fill. Added the identical proportional diagonal-quad treatment used by
  drawTractorAt/drawCombine/drawSprayer/drawCar — same 22%-40% width band at the
  top, 0%-16% at the bottom, full window height, `rgba(255,255,255,0.14)`, gated
  `dl>0.5` (daylight only — drawTruckSm has no `lit` night-shift param, matching
  the car/combine/sprayer gate rather than the tractor's fuller one). Window rect
  here is 3×2 (smallest of the four), so the quad coords scale down proportionally
  (2.66/3.2/2.48/2 x, -9/-7 y) rather than reusing another sprite's literal
  numbers. Placed directly after the window fillRect, before the existing
  headlight/window-glow `dl<0.3` block — same layering slot the other four use.
  Zero new state, pure per-frame draw addition, nothing to reset in
  synth/jumpSeason, no Math.random. This closes the hero-glass-glint sweep for
  real this time — every enclosed-cab/cabin sprite in the file (tractor, combine,
  sprayer, car, small truck) now has the same glint; only bare-bed/open vehicles
  (semi cab has its own already, pickup/planter/baler/cart have no glass) are
  left untouched by design. Smoke OK, both builds green, kit+zip refreshed.
  Not committed (evolve doesn't commit/push itself — uncommitted work now spans
  07-22 through today, worth a review/commit/push/redeploy session soon).
- 2026-07-28 — With the hero-glass-glint sweep (tractor/combine/sprayer/showroom)
  and the shadow audit both previously declared closed, looked for a genuinely
  fresh gap rather than re-treading either thread. Found one: `drawCar` (the
  shared sprite for the family car, employee cars, and dealership visitor
  cars — probably the single most-often-on-screen "glass" sprite in the whole
  file) still had a flat single-color window fill, no glint at all. Added the
  identical proportional diagonal-quad treatment used by
  drawCombine/drawSprayer — same 22%-40% width band at the top, 0%-16% at the
  bottom, full window height, `rgba(255,255,255,0.14)`, gated `dl>0.5`
  (daylight only, matching the simpler combine/sprayer gate since drawCar has
  no night-shift `lit` param to factor in like the tractor). Placed directly
  after the window fillRect, before the door-seam accent fillRect below it —
  same layering slot the other three use. Zero new state, pure per-frame draw
  addition, nothing to reset in synth/jumpSeason, no Math.random. NOTE for a
  future run: `drawTruckSm` (VFW patron trucks + the mail truck) has the same
  flat-window gap and would be the natural next pick if another "glass" pass
  is wanted — left alone this run to keep the change singular. Smoke OK, both
  builds green, kit+zip refreshed. Not committed (evolve doesn't commit/push
  itself — uncommitted work now spans 07-22 through today, worth a
  review/commit/push/redeploy session soon).
- 2026-07-27 #5 — Continued the hero-glass-glint thread from run #4 (tractor →
  combine → this run: sprayer). Audited every machine draw function for the
  flat-color cab-window pattern and found one more: the See&Spray sprayer's
  cab window (`drawSprayer`, `fillRect(3,-14,3,2.5)`) was still a single flat
  fill with no daylight glint, unlike the tractor and (as of a few hours ago)
  combine. Added the identical diagonal-quad treatment
  (`rgba(255,255,255,0.14)`), gated `dl>0.5` (daylight only, matching the
  combine's simpler gate since the sprayer has no `lit`/night-shift param to
  factor in like the tractor does). Quad geometry scaled proportionally from
  the combine's streak (same 22%-40% width band at the top, 0%-16% at the
  bottom, full window height) so the glint reads consistently across all
  three cab sizes rather than being hand-eyeballed per machine. Placed
  directly after the window fillRect, before the accent-panel P() call below
  it — same layering slot the tractor/combine use. Zero new state, pure
  per-frame draw addition, nothing to reset in synth/jumpSeason, no
  Math.random. This closes the hero-glass-glint sweep — tractor, combine, and
  sprayer (the three enclosed-cab field machines) all now match; planter/
  baler/cart have no cab to glint. Smoke OK, both builds green, kit+zip
  refreshed. Not committed (evolve doesn't commit/push itself — uncommitted
  work now spans 07-22 through 07-27, worth a review/commit/push/redeploy
  session soon).
- 2026-07-27 #4 — Picked up the memory's standing advice to try a fresh angle
  now that the shadow-audit and wx-transition-pop buckets are both repeatedly
  declared closed. Season is summer and both seasonal backlog items
  (fireflies, garden sprinkler) are already shipped, so looked instead at the
  hi-bit "hero glass glint" convention (dealership showroom window, tractor
  cab) shipped in the July graphics-quality wave — the combine's cab window
  was the one vehicle glass left with no glint, drawn as a single flat color
  rect while its sibling machine (the tractor) got the diagonal daylight
  streak. Added the identical treatment: a small white diagonal quad
  (`rgba(255,255,255,0.14)`) inside the window rect, gated `dl>0.5` (daylight
  only, matching the tractor's gate minus its extra `lit` night-shift check,
  which the combine's window fill doesn't have). Placed between the window
  fillRect and the seated-rider block so the rider silhouette still draws on
  top of the glint, same layering order `drawTractorAt` uses. Zero new state,
  pure per-frame draw addition, nothing to reset in synth/jumpSeason, no
  Math.random. Smoke OK, both builds green, kit+zip refreshed. Not committed
  (evolve doesn't commit/push itself — uncommitted work now spans 07-22
  through 07-27, worth a review/commit/push/redeploy session soon).
- 2026-07-27 #3 — Closed one more real gap in the shadow-audit thread (twice
  declared closed, twice reopened — this file's own recent entries): the
  distant hill cattle (`hillCows`, the persistent year-round trio grazing the
  far ridge, drawn inline in the town block, NOT via `drawCow`) had no
  `shadow()` call. They were missed by every prior function-name-based audit
  for the same reason the bales were two runs ago — they're drawn inline, not
  through a named `draw*` helper, so a grep for "shadow() near function start"
  walks right past them. Added `shadow(c.x,gy,2.2)` sized to roughly half the
  4.6px barrel width (same ratio `drawCow` uses: 5.5/10). The one wrinkle:
  these cows are the rare mid-day UFO-abduction target (`cowUfo`), and their
  draw `y` is lifted during the beam (`y=gy-lift`) — same "shadow anchors to
  the fixed ground line, not the bob/lift-adjusted position" rule the dawn
  deer and field-bale fixes established, so a cow being beamed up correctly
  leaves her shadow behind on the hillside instead of it floating up with
  her. Refactored the shared `Math.max(c.y,hillEdgeY(c.x)+1.5)` ground-line
  calc into one `gy` const so the shadow and the lifted draw position read
  off the same source of truth. Zero new state, pure per-frame draw addition,
  nothing to reset in synth/jumpSeason. Smoke OK, both builds green, kit+zip
  refreshed. Not committed (evolve doesn't commit/push itself).
- 2026-07-27 #2 — Closed a real gap the prior shadow-sweep runs missed: the round
  hay bales had NO `shadow()` call anywhere. The earlier sweeps (fire truck →
  planter/baler/cart → mailbox → hens → deer → tumbleweeds → sign) all audited
  `draw*` *functions*, but the bales are drawn as bare `circle()` calls inline in
  `draw()` in two spots — the 2 stacked bales west of the barn (plus the one Earl
  rolls to the door during his 'hay' chore) and the bales scattered across
  harvested field rows — so neither inline site was ever visited by a
  function-name-based audit. Added `shadow(x,y,r)` before each: farmstead bales
  use their own x/y/radius verbatim (same convention as `drawCow`'s
  `shadow(x,y,5.5*s)` — shadow matches the body circle exactly); the field bales
  use the row's fixed ground line `y` rather than the lifted `byy` (which
  subtracts the rare night-UFO-abduction `b.ry` hop) — same "anchor to the fixed
  ground line, not the bob-adjusted draw position" rule the dawn deer shadow
  established, so a bale being lifted by the 'u' easter egg correctly leaves its
  shadow behind on the ground instead of the shadow floating up with it. Zero new
  state, pure per-frame draw addition, nothing to reset in synth/jumpSeason.
  Smoke OK, both builds green, kit+zip refreshed. Not committed (evolve doesn't
  commit/push itself).
- 2026-07-26 #4 — `drawSign` (the roadside "for sale"/dealership lot signs, `sagaSigns`
  event) gets a ground shadow, closing the last gap in the standalone-static-prop shadow
  audit (backlog's "a touch more shading/detail on an existing sprite that reads flat" —
  the mailbox got this exact fix two runs ago; the sign was the only other standalone
  ground prop with no `shadow()` call — every mover already has one, 33 call sites
  audited). Added `shadow(x+1.5,y+0.6,2)` as the first statement, before the post/box
  fillRects — same convention/placement as `drawMailbox`, sized to the sign's own ~4px
  box footprint centered under the post. Zero new state, pure per-frame draw addition —
  nothing to reset in synth/jumpSeason. With this, every static ground prop and every
  mover in the file has a shadow — the multi-run shadow-audit thread (mailbox → hens →
  deer → fire truck → planter/baler/cart → this) is now fully closed, same as the
  wx-easing audit closed 2026-07-26 #3. Smoke OK, both builds green, kit+zip refreshed.
  Not committed (evolve doesn't commit/push itself).
- 2026-07-27 — The KEATING sign's additive bloom snapped instantly from off to full
  brightness the instant `dl` crossed the `lit` (dl<0.45) threshold — the standing
  "smoother transition somewhere that currently pops" item, previously closed for
  wx-poll pops (wind/cloudCov) but this one is a pure threshold-on-continuous-input
  pop, same bug class. New eased global `dealerLit` (0..1, same idiom as barnO/bayO)
  tracks the lit threshold with a ~5s time constant (`vdt*0.2`), computed in update()
  right after mAng; drawDealership's sign-glow block now scales `globalAlpha` by
  `dealerLit` instead of gating on the boolean. Window/panel colors and the showroom
  glass left on the instant `lit` flip (unchanged, matches the town's existing
  per-building precedent) — only the hero bloom (the one explicitly called "reads as
  a light source" in the doctrine) got the ease, keeping the change small. Reset to
  the correct snapped value (`dl<0.45?1:0`) in both synthSeason/jumpSeason so a
  season jump doesn't trigger a spurious fade. Smoke OK, both builds green, kit+zip
  refreshed. Not committed (evolve doesn't commit/push itself).
- 2026-07-26 #3 — Tumbleweeds (backlog's standing "a touch more shading/detail on an
  existing sprite that reads flat" item) were the last genuinely flat sprite left in the
  file: a single-tone stroked circle + two crossing lines, no shadow, no shading variation
  at all — everything else audited (hens/deer/mailbox/fire-truck/planter/baler/cart) already
  had shadows and most hero sprites got the hi-bit light/detail pass. FIX (draw loop, the
  `for(const t of tumbles)` block): added `shadow(t.x,t.y+t.r*0.6,t.r*0.85)` (same ground-
  contact idiom as every other mover) plus a second darker stroke arc
  (`S('#6e5c38',0.5)`, from `t.a+1.15` spanning `Math.PI*0.85`) drawn over the "far side" of
  the ball before the existing cross-lines — the arc's start angle is keyed to `t.a` (the
  tumbleweed's own spin/roll angle, already time-driven, no new state) so the shaded side
  rotates as it rolls instead of sitting static, selling it as a rolling sphere instead of a
  flat ring. Reset `strokeStyle` back to the original tone before the cross-lines so those
  are unaffected. Zero new globals, zero new Math.random in draw code (reused `t.a`/`t.r`
  which are already position/time state), nothing to reset in synth/jumpSeason. Checked the
  wx-polling audit the prior run flagged as unfinished — traced weather/cloudCov/wind/fogT
  and confirmed ALL of them already ease toward a live target (fogT via
  `fogT+=(wFog-fogT)*vdt*0.08`, snowCover via incremental melt/accumulate rates); the
  wx.tempF boolean gates (coldOut/scorching/melt-rate/snow) are threshold flips, not
  continuous drivers, so they're correctly instant — **that audit is now fully closed, no
  more pops to chase.** Smoke OK, both builds green, kit+zip refreshed. Not committed
  (evolve doesn't commit/push itself).
- 2026-07-26 #2 — Same bug, different variable: `wind` had the identical live-poll-pop flaw
  the prior run just fixed for `cloudCov` (backlog's standing "smoother transition somewhere
  that currently pops" item — the 15-min-poll audit clearly wasn't done, just cloud). `wind`
  was recomputed FRESH every frame straight from `wx.windMph` (`wind=live&&wx?clamp(3+
  wx.windMph*0.8+Math.sin(rt*0.11)*2+weather*8,0,45):...`) with no easing at all — every 15-min
  `fetchWx()` poll could snap it from e.g. 5mph to 20mph in one frame, and `wind` drives the
  VFW/laundry flag droop-vs-whip (`fw2=wind/40`), crop-row wave amplitude, the windmill's
  `mAng` angular speed, the tumbleweed spawn gate (wind>16), and the monarch butterfly
  equilibrium lean — all of which would visibly lurch in lockstep the instant a poll landed.
  FIX: renamed the instant formula to `windT` (target) and eased the `wind` global toward it
  with the exact same idiom/rate as the sibling fix (`wind+=(windT-wind)*Math.min(1,vdt*
  (live?0.15:0.7))`), right below cloudCov's easing line in the update loop. Since `wind` now
  retains state across frames (like weather/cloudCov, unlike before), added the matching reset
  pair per convention: `wind=6` in both `synthSeason()` and `jumpSeason()` alongside the
  weather/cloudCov resets on the same lines. Zero new persistent globals — `wind` was already
  one, just upgraded from instantaneous to eased. Smoke OK, both builds green, kit+zip
  refreshed. Not committed (evolve doesn't commit/push itself). NOTE for next run: worth
  double-checking whether any OTHER instant `wx.*` reads still bypass easing before picking a
  fresh polish item — `wx.tempF` reads (coldOut, scorching, melt, snow) are all boolean/threshold
  gates rather than continuous draw drivers, so a step there reads as a weather-event boundary,
  not a visual pop; wind and cloud were the only two continuous-motion drivers.
- 2026-07-26 — Smoothed a real "pop": live cloud cover (`wx.cloud`) was read directly into
  the sky-drawing `cloudCov` const every frame, but `wx` only refreshes every 15 minutes
  (`fetchWx`/`setInterval`), so the moment a poll landed, cloud opacity (`g.globalAlpha=
  0.15+0.75*cloudCov`) could jump instantly from e.g. 10% to 90% in one frame — a visible
  sky flicker on a 24/7 display, exactly the backlog's standing "smoother transition
  somewhere that currently pops" item (flagged by the prior run as the next polish pick
  once the shadow-audit thread closed out). FIX: promoted `cloudCov` to a persistent global
  (`let ...,cloudCov=0.3`) and eased it toward the live/sim target in the same tick as
  `weather` (`cloudCov+=(cloudT-cloudCov)*Math.min(1,vdt*(live?0.15:0.7))`, mirroring
  weather's own live/sim easing idiom exactly, right below its line in the update loop);
  `draw()` now just reads the eased global instead of recomputing it from `wx.cloud`
  directly. Reset in both `synthSeason()` (`cloudCov=Math.min(cloudCov,0.3)`, alongside
  weather's own clamp) and `jumpSeason()` (`cloudCov=0.3`, alongside weather's own reset) —
  same convention, same two call sites. Zero new draw-path randomness, one new persistent
  scalar. Smoke OK, both builds green, kit+zip refreshed. Not committed (evolve doesn't
  commit/push itself).
- 2026-07-25 #5 — The dawn deer (`if(deer)` draw block) gets a ground shadow, closing the
  last gap from the 2026-07-25 hen-shadow audit (polish backlog item: "a touch more shading/
  detail on an existing sprite that reads flat" — this was the explicitly-noted follow-up
  left for a future run). Added `shadow(dx3+0.5,HOR+3,2.4)` right after the block's existing
  `walkb`/`dx3`/`dy3`/`graze` consts, before the fillStyle draws — same placement convention
  as the rabbit block just below it. Sized 2.4 (between the rabbit's 1.6 and the pheasant's
  2.2) since the deer's body silhouette (6px wide) is noticeably bigger than either. Anchored
  to `HOR+3` (the fixed ground line) rather than `dy3` (which already has the gait-bob
  `walkb` subtracted out) — the shadow should sit still on the ground while the body bobs
  above it, exactly how the rabbit's shadow uses its fixed `ry` rather than its hop-adjusted
  draw position. Zero new state, pure per-frame draw addition — nothing to reset in
  synth/jumpSeason. Every ground animal in the file now has a shadow. Smoke OK, both builds
  green, kit+zip refreshed. Not committed (evolve doesn't commit/push itself).
- 2026-07-25 #4 — Barnyard/field hens (`drawHens`) get a ground shadow (polish backlog item:
  "a touch more shading/detail on an existing sprite that reads flat"). Audited every
  ground-animal draw function (cow/rabbit/pheasant/cat all already have `shadow()`, confirmed
  by grep) and found exactly two gaps: hens and the seasonal deer. Picked hens — they're the
  daily-visible barnyard flock (drawn year-round via two call sites, barnyard cluster + field
  stroll, both routed through the one `drawHens(filt)` helper) vs. the deer's rare dawn-only
  appearance, so this closes the more commonly-seen gap. Added `shadow(c.x+0.5,c.y,1.6)` as
  the first draw statement per chick, sized like the rabbit's (`w:1.6`, comparable ~5px
  sprite footprint) rather than the larger cow/pheasant radii. One line, both call sites
  covered automatically since they share the helper. Zero new state, pure per-frame draw
  addition — nothing to reset in synth/jumpSeason. Left the deer's missing shadow for a
  future run (noted below) rather than doing two changes in one pass. Smoke OK, both builds
  green, kit+zip refreshed. Not committed (evolve doesn't commit/push itself).
- 2026-07-25 #3 — The roadside mailbox (`drawMailbox`) gets a ground shadow (polish backlog
  item: "a touch more shading/detail on an existing sprite that reads flat"). It was the one
  standalone static object left with no `shadow()` call — every mover in the file already has
  one (27 call sites audited), and the mailbox is a singular hero object (the mail-carrier beat
  shipped 2026-07-24) rather than a repeated background element like fence posts, so it reads
  as the natural next candidate. Added `shadow(x,HOR+0.6,2.3)` as the first statement, sized to
  the box's own footprint (5px wide) at its ground contact point — same placement convention as
  every other draw* function. Zero new state, pure per-frame draw addition — nothing to reset
  in synth/jumpSeason. Also removed the stale "Laundry on a line" backlog line above — verified
  already shipped 2026-07-23 (`applebee-acres.html:3177`), just never trimmed from this list.
  Smoke OK, both builds green, kit+zip refreshed. Not committed (evolve doesn't commit/push
  itself).
- 2026-07-25 #2 — Fixed the long-open "white bubbles near Earl" mystery (flagged by Austin
  2026-07-11, unsolved — see project memory's OPEN ITEM note). Root cause: kid2's dandelion
  moment (`k2At`, folkPlan id `kid2`) sat her at `tx:HX-6` — only 2px inside Earl's own
  afternoon putter range (`pa:HX-8,pb:HX+22`, active tod 0.57-0.76, by far his biggest daily
  block and the only stretch that reads as "walking the edge of the yard"). The seed-puff
  particles (`#eef2f2`, wind-driven, `vx` always positive) drift further east from that spawn
  point over their ~2s life, landing squarely in Earl's territory just as the small, crouched
  kid2 sprite becomes easy to miss beside him — puffs with no obviously-connected source,
  right where Austin was looking. FIX: moved the whole beat (enter + sit spot) from
  `enter:HX+16,tx:HX-6` to `enter:HX+20,tx:HX+27` — clears Earl's putter by 5px (his pb caps
  hard at HX+22, so no idle-wander can carry him that far east), clear of the dog's nap spot
  (HX+14, 13px), and still inside the lawn apron (HX-14..HX+30, 3px shy of the edge). Checked
  for new conflicts at the new spot: Dot's flowerbed-tending stands at HX+23 but only until
  tod 0.66 (k2At's earliest start), and the family-car board spot (HX+30, `famAct==='town'`)
  is mutually exclusive with the dandelion branch in kid2's own folkPlan (famCar is checked
  first) — so she's never in both places at once. Pure position change, zero new state,
  nothing to reset in synth/jumpSeason. Smoke OK, both builds green, kit+zip refreshed. Not
  committed (evolve doesn't commit/push itself). Also removed the draw-order backlog item
  above (crater/duster/fireworks-behind-hills) — verified already shipped 2026-07-13, just
  never trimmed from this list.
- 2026-07-25 — The crop-fire water truck (`drawTruck`, ROWY[3]+6 in the fire sequence) gets
  a ground shadow (polish backlog item: "a touch more shading/detail on an existing sprite
  that reads flat"). It was the one remaining vehicle draw function with no `shadow()` call —
  every other mover (car/semi/pickup/truckSm/mower/bike/walker/combine/sprayer/planter/
  baler/cart) already has one. Added `shadow(x+dir*2*s,y,12*s)` as the first statement,
  before the transform — same convention as `drawSemi`/`drawPickup` (offset by `dir*s` to
  center under the body, since the ladder rig extends the silhouette rearward of x=0 rather
  than symmetric front/back). Zero new state, pure per-frame draw addition — nothing to
  reset in synth/jumpSeason. Smoke OK, both builds green, kit+zip refreshed. Not committed
  (evolve doesn't commit/push itself).
- 2026-07-24 #3 — Finished the roadside mailbox (backlog item: mail carrier passes weekday
  late mornings, flag goes up when she reaches the box). Found the state/timing/reset half
  of this already sitting uncommitted in the working tree from an interrupted prior run —
  `mailTruck`/`mailAt`/`mbFlag` + `drawMailbox` were all correct (windowed+expired+spread,
  reset in onNewDay/synth/jumpSeason, weekday-only via the same `live?liveDate.getDay()`
  idiom `SUN` uses) — but the truck itself was NEVER DRAWN: it drove its full route
  (x:-16 → MBX-2 → W+20) as invisible state, so only the flag flip was visible and nothing
  actually "passed." Added the one missing line: `drawTruckSm(mailTruck.x,124,'#d9d2bc',1)`
  next to the school-bus draw call (same road line, y=124 — the established depth for
  through-traffic vs. the mailbox/fence which sit back at HOR). Reused the existing small-truck
  sprite as-is (already used for VFW patron trucks) rather than drawing a new one, and picked a
  plain cream color with no postal branding (same no-real-logos stance as the generic kaiju/
  helicopters). Smoke OK, both builds green, kit+zip refreshed. Not committed (evolve doesn't
  commit/push itself). NOTE for next run: check the working tree for other unfinished code
  before starting fresh — this run's diff already carried ~7 unlogged runs' worth of shipped
  work back to 2026-07-22 (evolve doesn't commit, so it accumulates until an interactive
  session commits it).
- 2026-07-24 #2 — Towed field implements (planter, baler, grain cart) get ground shadows
  (polish backlog item: "a touch more shading/detail on an existing sprite that reads
  flat"). `drawPlanter`/`drawBaler`/`drawCart` were the only movers in the file with no
  `shadow()` call — the tractor's own shadow (radius `9*s`) doesn't reach their trailing
  wheels (planter/baler geometry extends to local x=-14..-16, well outside that radius;
  the cart is towed a further `15*tract.s` behind the tractor entirely, so it had zero
  shadow coverage). Added one `shadow(x,y,w*s)` line to each function, first statement
  before the transform — the exact placement/ordering convention every other draw*At
  function already uses (`drawTractorAt`, `drawCombine`, `drawSprayer`, etc.). Zero new
  state, zero timing logic, pure per-frame draw addition — nothing to reset in
  synth/jumpSeason. Smoke OK, both builds green, kit+zip refreshed. Not committed (evolve
  doesn't commit/push itself).
- 2026-07-24 — Combine cab gets a seated operator (backlog item: "combine cab operator by
  day — the enclosed cab currently reads empty"). `drawCombine` gained a `rider` param drawn
  with the exact same seat/head/cap-silhouette treatment already shipped for `drawTractorAt`
  (same colors, same relative offsets from the cab window, just re-anchored to the combine's
  narrower window rect) — Earl (red cap) rides the combine by day, riderless during the
  autumn night-harvest autonomy shift (`dl<0.35`, the same convention the tractor already
  uses for its own day/night rider swap). Zero new globals/state — the rider flag is computed
  inline at the existing call site from `dl`, no synth/jumpSeason reset needed. Verified with a
  fillRect-tap harness (same technique as the walk/cow-motion checks): forced a full sim-year
  harvest cycle and confirmed the rider's seat-rect fingerprint fires ~340 times during
  daylight combine passes. Smoke OK, both builds green, kit+zip refreshed. Not committed
  (evolve doesn't commit/push itself).
- 2026-07-23 — Steam curls off Manny's coffee at his bay-step lunch on cold days (backlog item,
  fourth run today). Reuses the existing `coldOut` gate (promoted from a local `const` to a
  frame-updated global so drawFolk's render pass can read it too) and the exact chimney-smoke
  particle idiom (irregular puffs pushed onto the shared `smoke` array, spawn-side
  `Math.random()` only). Gated on his actual lunch-sit window (`tod 0.5-0.53 && folk[2].sit`,
  the same window folkPlan already uses for "Manny's lunch on the bay step") so the mug only
  appears while he's genuinely sitting there. New `drawMannyCoffee` draws a tiny mug beside him
  (drawn right after the folk pass so it sits on top of his sprite, same slot as drawWeld); two
  new fields (`coffeeT` timer, `coldOut` promoted global) — neither needs synth/jumpSeason reset,
  same self-clearing-countdown class as `chimT`/`puffT`. Smoke OK, both builds green, kit+zip
  refreshed. Not committed (evolve doesn't commit/push itself).
- 2026-07-23 — Gus the barn cat gets a rain-shelter beat instead of teleport-vanishing (backlog
  item, third run today). He used to blink out the instant `weather>=0.4`; now mirrors the dog's
  proven rain-shelter idiom exactly (`wasVis` latch + `weather<0.62` grace window before hiding) —
  caught out on the fence rail, he trots to the barn (`BARNX+40`, same shelter spot as the dog) at
  a hurried 9px/s instead of his idle 5px/s patrol speed, and only actually vanishes if the rain
  gets heavy (weather>=0.62) or he was already offstage. Sit/pounce logic now gated `weather<0.4`
  so he doesn't try to sit and roll for a mouse mid-downpour. One new field (`cat.wasVis`, live
  transient, same class as `dog.wasVis` — not reset in synth/jumpSeason). Smoke OK, both builds
  green, kit+zip refreshed. Not committed (evolve doesn't commit/push itself).
- 2026-07-23 — Laundry line strung beside the farmhouse, drying in the spring/summer breeze
  (backlog item). Zero new persistent globals — a pure draw-time function of `wind`/`rt`/`seasonIdx`
  (same class as the flowerbed right above it in the code, no synth/jumpSeason reset needed). Two
  posts + a line at `hx-11..hx-2` (clear of the parked tractor's footprint, which ends at hx-12) with
  3 garments (sheet, Earl's shirt, Dot's) hung between; each sways on the flagpole's wind idiom
  (`sin(rt*(1.3+wind*0.08)+phase)*(0.35+fw3*0.85)`, fw3=clamp(wind/40,0,1)) with the hem swinging
  more than the pinned shoulders, so it calms on still days and whips in a blow like the VFW flag.
  Gated `seasonIdx<2` (spring/summer only), drawn in the farmhouse block before `drawFolk` so anyone
  walking the yard correctly occludes it. Smoke OK, both builds green, kit+zip refreshed. Not
  committed (evolve doesn't commit/push itself).
- 2026-07-23 — Gus the barn cat gets a rare mouse-pounce off the fence rail (backlog item). Reused
  his existing sit/dusk-fence state (`cat.sit`/`cat.dir`) — while perched, a low per-frame chance
  (`Math.random()<0.004`, same idiom as Boone's tail-wag roll) starts a 1.6s `cat.pounce` timer.
  Draw-side is a pure function of that timer: a sine arc (`leap`) carries the WHOLE existing sprite
  (body/ears/tail/night eye-glint, unchanged) down toward the grass and back up, with a small
  forward lean in his facing direction and a shadow that fades in only near the ground — no new
  sprite art, no new persistent global beyond one `pounce:0` field on the existing `cat` object.
  Not reset in synth/jumpSeason — same class as dogGreet/halMannyT/porchT (self-clearing countdown).
  Smoke OK (many simulated dusks exercise the roll + timer with no errors), both builds green,
  kit+zip refreshed. Not committed (evolve doesn't commit/push itself).
- 2026-07-22 — Garden sprinkler ticks across the front lawn some summer mornings (~6-8am, ~1 in 3
  clear days, seasonIdx===1 only). Zero new persistent globals — reused the existing `dayRng(tag)`
  per-day deterministic hash (same idiom as the porch lights/owl), so no synth/jumpSeason reset was
  needed. Visual reuses the established water-arc droplet technique from Dot's watering-can chore
  (same math shape, different fan pattern) rather than inventing a new device. Placed at HX+7 on the
  lawn — clear of the door (HX+10..14), the flowerbed (HX+22..27), and both 'water' chore targets
  (HX+22/24), so it never overlaps a folk target. Drawn with the lawn-texture block, before the
  planted signs/movers, so people walk in front of it like any other ground decoration. Smoke OK,
  both builds green, kit+zip refreshed. Not committed (evolve doesn't commit/push itself).
- 2026-07-10 — Backlog created; loop scaffolding installed. First autonomous run pending activation.
