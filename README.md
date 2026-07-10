# Applebee Acres 🚜

A self-contained pixel-art farm diorama — a living screensaver for the **Keating Tractor** dealership in Liberal, Kansas. It runs on the real clock, the meteorological calendar, and live Liberal weather (Open-Meteo), so the farm follows the actual time of day, seasons, and sky. Leave it a week and the town has grown, the trees are taller, and the story has moved on.

**▶ Play it live:** https://lucidmolt.github.io/applebee-acres/

## What's here
- **`index.html`** / `applebee-acres.html` — the whole thing in one self-contained file (HTML canvas + vanilla JS, no build step, no dependencies).
- **`screensaver/`** — native macOS screensaver (`.saver`, WKWebView / Swift).
- **`windows-screensaver/`** — Windows `.scr` (WinForms + WebView2, built with .NET 8) plus the USB install kit and a Group Policy deploy guide.
- **`tests/`** — headless smoke test (full year + every event) and a 12-year soak/heap test.

## Controls
Click the field to plant sunflowers. `1`–`4` jump seasons · `-`/`=` speed · `L` back to live · `7`/`9` duck hunt · `h` hide the HUD.

## Weather
Over https (like the live link) it fetches real conditions for Liberal, KS; offline or where the fetch is blocked, it quietly simulates its own weather.

---
Built with [Claude Code](https://claude.com/claude-code).
