#!/usr/bin/env pwsh
# Progress gate. Builds, then runs each lab STEP in order (selected by the xUnit
# "step" trait), stopping at the first step that fails. The baseline suite must
# already be green (`dotnet test`); the steps are the features you add.
#
# Unlike labs 2 and 3, this lab has no Stop hook — you drive the loop yourself.
# Run this whenever you want to see how far you've gotten.
$ErrorActionPreference = 'Continue'
Set-Location -Path $PSScriptRoot

Write-Host "== build (Release) =="
dotnet build -c Release
if ($LASTEXITCODE -ne 0) {
    Write-Host "CHECK FAILED (build)"
    exit 1
}

$titles = @(
    'list --json output',
    'list --tag and --priority filters',
    'stats command',
    'list --sort priority',
    'due dates: add --due, list --due-before'
)

for ($step = 1; $step -le 5; $step++) {
    Write-Host ("== STEP {0}/5: {1} ==" -f $step, $titles[$step - 1])
    dotnet test -c Release --no-build --filter "step=$step"
    if ($LASTEXITCODE -ne 0) {
        Write-Host ("CHECK FAILED (step {0})" -f $step)
        exit 1
    }
    Write-Host ("  STEP {0} PASSED" -f $step)
}

Write-Host "CHECK PASSED"
exit 0
