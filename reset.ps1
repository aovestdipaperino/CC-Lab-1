#!/usr/bin/env pwsh
# Restore the repo to its starting state. PowerShell equivalent of reset.sh.
$ErrorActionPreference = 'Continue'
Set-Location -Path $PSScriptRoot

git checkout -- . 2>$null
git clean -fdx --exclude=.git 2>$null
Remove-Item tasks.json -ErrorAction SilentlyContinue

Write-Host "Reset to starting state."
