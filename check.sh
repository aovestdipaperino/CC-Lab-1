#!/usr/bin/env bash
# Progress gate. Builds, then runs each lab STEP in order (selected by the xUnit
# "step" trait), stopping at the first step that fails. The baseline suite must
# already be green (`dotnet test`); the steps are the features you add.
#
# Unlike labs 2 and 3, this lab has no Stop hook — you drive the loop yourself.
# Run this whenever you want to see how far you've gotten.
set -uo pipefail
cd "$(dirname "$0")"

echo "== build (Release) =="
dotnet build -c Release
if [ $? -ne 0 ]; then
  echo "CHECK FAILED (build)"
  exit 1
fi

titles=(
  ""
  "list --json output"
  "list --tag and --priority filters"
  "stats command"
  "list --sort priority"
  "due dates: add --due, list --due-before"
)

for step in 1 2 3 4 5; do
  echo "== STEP $step/5: ${titles[$step]} =="
  dotnet test -c Release --no-build --filter "step=$step"
  if [ $? -ne 0 ]; then
    echo "CHECK FAILED (step $step)"
    exit 1
  fi
  echo "  STEP $step PASSED"
done

echo "CHECK PASSED"
exit 0
