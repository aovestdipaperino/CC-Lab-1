#!/usr/bin/env bash
# Restore the repo to its starting state.
set -uo pipefail
cd "$(dirname "$0")"
git checkout -- . 2>/dev/null || true
git clean -fdx --exclude=.git 2>/dev/null || true
rm -f tasks.json
echo "Reset to starting state."
