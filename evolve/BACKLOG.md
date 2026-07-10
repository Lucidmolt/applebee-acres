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
  `~/Creative` + the memory dir. No destructive commands.
- **Log every run:** append a dated line to the Changelog below, and add a short note to the memory.

## Idea backlog (pick the best fit for the current season/state; add your own)
Seasonal ambient (match the current season):
- Autumn: a V-formation of migrating geese crossing high, honking south (silhouettes, position-hashed).
- Winter: kids build a snowman together over a few minutes; a wreath on the farmhouse/dealership door in Dec.
- Spring: barn swallows darting around the barn eaves at dusk; a robin on the fence.
- Summer: fireflies blinking in the yard after dark; a garden sprinkler ticking on the lawn some mornings.

Aliveness / character (keep rare and subtle):
- Combine gets a cab operator by day (drawCombine rider — the enclosed cab currently reads empty).
- Gus the cat: a rain-shelter beat (trots off instead of vanishing), and a rare mouse-pounce moment.
- Hal & Manny exchange an occasional nod/wave across the lot (reuse the hold/wave mechanic).
- Steam curling off Manny's coffee at his bay-step lunch (cold mornings).
- A rural mailbox by the road whose flag goes up when a mail truck passes on weekdays.
- Laundry on a line behind the farmhouse, swaying with the wind (spring/summer).

Polish / realism:
- Draw-order rarities (only if you can verify no regression): impact crater under the field machines,
  crop duster / fireworks behind the hills. (Low priority — rare events.)
- A touch more shading/detail on an existing sprite that reads flat.
- Smoother transition somewhere that currently pops.

## Changelog
(most recent first — the evolve agent appends here)
- 2026-07-10 — Backlog created; loop scaffolding installed. First autonomous run pending activation.
