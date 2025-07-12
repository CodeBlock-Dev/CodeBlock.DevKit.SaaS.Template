@echo off
cd /d "%~dp0..\.."

echo Running: nuke Lint
nuke Lint

pause 