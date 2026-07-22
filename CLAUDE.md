# Applebee Acres — working notes for Claude

A self-contained pixel-art farm screensaver for the Keating Tractor dealership
(Liberal, KS). Single HTML canvas + vanilla JS, no deps, no web build step.

**📖 Full reference: [`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md)** — read it before
non-trivial work. The essentials:

## Must-knows (these bite)
- **Two-file byte-sync:** `applebee-acres.html` (source) and `index.html` (GitHub Pages)
  **must be byte-identical.** Edit the former, then `cp applebee-acres.html index.html`,
  and commit both together.
- **The script is one sealed IIFE** — internals (`kaiju`, `tod`, `startCowUfo`…) are not
  on `window`. Can't be poked from outside; add a temp hook *inside* the IIFE to inspect.
- **Push identity:** remote is **Lucidmolt/**applebee-acres but git defaults to
  **CorruptFun** → 403. Push as Lucidmolt, then restore CorruptFun (vault sync needs it):
  ```sh
  gh auth switch --user Lucidmolt
  git -c credential.helper= -c credential.helper='!gh auth git-credential' push origin main
  gh auth switch --user CorruptFun
  ```
- **Public display stays generic** — the kaiju is a "prairie lizard", never branded Godzilla.
  Keep it calm, deterministic, self-contained (no live AI / data feeds beyond weather).

## Commands
```sh
cd tests && node smoke.js          # gate — must print OK
screensaver/build.sh               # macOS .saver → ~/Library/Screen Savers
windows-screensaver/build.sh       # Windows .scr (dotnet 8)
```
Release checklist + Windows USB/GPO staging: see `docs/ARCHITECTURE.md` §5.

## Events (the FAQ)
Two trigger paths: **daily-roll** (`onNewDay()`, once/day in a time window) vs
**exit-roll** (`acresExit()`, every screensaver dismissal). Rare daily rolls are
effectively never seen live; exit rolls feel frequent. Full catalog + demo keys
(`G` kaiju, `U` abduction, `q` asteroid, …) in `docs/ARCHITECTURE.md` §3–4.

## Automation
- `evolve/` — launchd `com.applebeeacres.evolve`, one smoke-gated change every 5h; does
  not commit/push itself.
