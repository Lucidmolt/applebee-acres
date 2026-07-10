# Applebee Acres — self-evolving loop

A launchd job that wakes every 5 hours and has Claude Code make ONE small, smoke-gated improvement to
the farm, rebuild the savers + USB kit, and log it. Everything is installed and verified EXCEPT the
credential — an unattended job can't borrow this app's gateway login, so it needs its own.

## Files
- `BACKLOG.md`      — operating rules + idea backlog + changelog (the agent reads/updates this each run)
- `evolve-prompt.md`— the exact instructions each run follows
- `evolve.sh`       — the runner (clean env, source backup, budget cap, smoke-restore safety net, logging)
- `auth.env`        — (you create) where the credential goes; sourced by evolve.sh if present
- `evolve.log`      — per-run transcript (auto-trimmed to last 2000 lines)
- `~/Library/LaunchAgents/com.applebeeacres.evolve.plist` — the 5-hour schedule (StartInterval 18000s)

## Activate — one credential, then load (≈2 minutes)
Pick ONE auth path:

**A. Use your Claude subscription (recommended — no per-call billing beyond your plan):**
```
claude setup-token        # one-time, opens a browser; stores a long-lived token
```

**B. Use a pay-as-you-go API key:** create `~/Creative/evolve/auth.env` containing:
```
export ANTHROPIC_API_KEY="sk-ant-..."
# if your key must route through your gateway, also:  export ANTHROPIC_BASE_URL="https://your-gateway/..."
```

Then start the schedule:
```
launchctl load -w ~/Library/LaunchAgents/com.applebeeacres.evolve.plist
```

Optional immediate test (runs one real change now; watch it work):
```
bash ~/Creative/evolve/evolve.sh; tail -40 ~/Creative/evolve/evolve.log
```

## Control it
- Pause:   `launchctl unload ~/Library/LaunchAgents/com.applebeeacres.evolve.plist`
- Resume:  `launchctl load -w ~/Library/LaunchAgents/com.applebeeacres.evolve.plist`
- Watch:   `tail -f ~/Creative/evolve/evolve.log`   ·   review changes in `BACKLOG.md`'s Changelog

## Safety rails (already built in)
- Smoke test gates every ship; if the source ends up red, the runner restores a pre-run backup — a bad
  change can't stick.
- Budget-capped (~$2.50/run) and runs on Sonnet 5 to keep a 5×/day loop affordable.
- Scoped to `~/Creative` + the memory dir. It does NOT redeploy the public artifact and NEVER touches the
  ~30 store machines (those stay on your manual install — so the fleet only changes when YOU choose to).
- To sync the public artifact or push a good run to the store fleet, use an interactive Claude session.

## Note
`--dangerously-skip-permissions` lets the job edit files + run builds unattended. That's broad machine
access on a 5-hour timer; it's scoped by the prompt to this project, but it's your call to run it.
