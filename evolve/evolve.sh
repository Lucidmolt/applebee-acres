#!/bin/bash
# Applebee Acres — autonomous evolve run. Invoked by launchd every 5h.
# Makes ONE vetted, smoke-gated change to the pixel farm, then stops.
set -u
DIR="/Users/lucid/Creative"
EV="$DIR/evolve"
LOG="$EV/evolve.log"
export HOME="/Users/lucid"
# launchd gives a bare env — spell out everything the agent + build scripts need
export PATH="$HOME/.local/bin:$HOME/.nvm/versions/node/v22.22.1/bin:$HOME/.dotnet:/usr/bin:/bin:/usr/sbin:/sbin:/opt/homebrew/bin"
# make sure we are NOT seen as a nested Claude Code session
unset CLAUDECODE CLAUDE_CODE_SESSION_ID CLAUDE_CODE_ENTRYPOINT CLAUDE_CODE_CHILD_SESSION
# optional: drop ANTHROPIC_API_KEY / ANTHROPIC_BASE_URL here to pick an auth path (see README-EVOLVE.md)
[ -f "$EV/auth.env" ] && . "$EV/auth.env"

cd "$DIR" || exit 1
[ -f "$LOG" ] && tail -n 2000 "$LOG" > "$LOG.tmp" 2>/dev/null && mv "$LOG.tmp" "$LOG"
{
  echo ""
  echo "===== evolve run $(date '+%Y-%m-%d %H:%M:%S') ====="
} >> "$LOG"

# safety net: snapshot the source before the agent touches it
cp -f "$DIR/applebee-acres.html" "$EV/applebee-acres.html.bak" 2>/dev/null

# one budget-capped, permissionless headless run (Sonnet 5 = capable + affordable for a 5x/day loop)
claude --print "$(cat "$EV/evolve-prompt.md")" \
  --model claude-sonnet-5 \
  --dangerously-skip-permissions \
  --max-budget-usd 2.50 \
  >> "$LOG" 2>&1
RC=$?
echo "----- claude exited rc=$RC -----" >> "$LOG"

# belt & suspenders: the source must still pass smoke; if not, roll back and DON'T leave it broken
if ! ( cd "$DIR/tests" && node smoke.js >/dev/null 2>&1 ); then
  echo "!! smoke RED after run — restoring the pre-run backup, nothing shipped" >> "$LOG"
  cp -f "$EV/applebee-acres.html.bak" "$DIR/applebee-acres.html"
else
  echo "ok: source passes smoke" >> "$LOG"
fi
echo "===== end $(date '+%H:%M:%S') =====" >> "$LOG"
